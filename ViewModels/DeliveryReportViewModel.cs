using goods.Commands;
using goods.Dao;
using goods.Models.Dto;
using goods.Stores;
using Microsoft.WindowsAPICodePack.Dialogs;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace goods.ViewModels
{
    public class DeliveryReportViewModel : BaseViewModel
    {
        public RelayCommand SearchCommand { get; }
        public RelayCommand ExcelExportCommand { get; }
        public ICommand NavigationBackCommand { get; }

        private DeliveryDao deliveryDao;
        private ObservableCollection<DeliveryOilDto> deliveriesOilDto;

        private string stockName;
        private DateTime deliveryDate;

        public DeliveryReportViewModel(NavigationStore navigationStore)
        {
            SearchCommand = new RelayCommand(Search);
            ExcelExportCommand = new RelayCommand(CreateExcelFile);
            NavigationBackCommand = new NavigateCommand<ReportHomeViewModel>(navigationStore, (n) => new ReportHomeViewModel(n));
            deliveryDao = new DeliveryDao();
            StockName = "Олио";
            DeliveryDate = DateTime.Now;
            DeliveriesOilDto = new ObservableCollection<DeliveryOilDto>(deliveryDao.GetDeliveryOilDtos(StockName, DeliveryDate, 0));
        }

        public ObservableCollection<DeliveryOilDto> DeliveriesOilDto
        {
            get { return deliveriesOilDto; }
            set { deliveriesOilDto = value; OnPropertyChanged(nameof(DeliveriesOilDto)); }
        }

        public string StockName
        {
            get { return stockName; }
            set { stockName = value; OnPropertyChanged(nameof(StockName)); }
        }

        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; OnPropertyChanged(nameof(DeliveryDate)); }
        }

        private void Search()
        {
            DeliveriesOilDto = new ObservableCollection<DeliveryOilDto>(deliveryDao.GetDeliveryOilDtos(StockName, DeliveryDate, 0));
        }

        private void CreateExcelFile()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string pathFile = Path.Combine(dialog.FileName, "DeliveryReport.xlsx");
                if (File.Exists(pathFile))
                {
                    if (MessageBox.Show("Искате ли да презапишите файлът?", "Предупреждение!", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        File.Delete(pathFile);
                    }
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(pathFile))
                {
                    ExcelWorksheet ew = excelPackage.Workbook.Worksheets.Add("DeliveryReport");

                    ew.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ew.Row(1).Style.Font.Size = 12;
                    ew.Row(1).Style.Font.Bold = true;
                    ew.Row(1).Style.Font.Color.SetColor(Color.White);
                    ew.Cells["A1:E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ew.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(48, 84, 150));
                    ew.Columns.Width = 22;

                    ew.Cells["A1"].Value = "Номер на доставчик";
                    ew.Cells["B1"].Value = "Име на стока";
                    ew.Cells["C1"].Value = "Цена с ДДС";
                    ew.Cells["D1"].Value = "Количество";
                    ew.Cells["E1"].Value = "Дата на доставка";

                    if (deliveriesOilDto.Count > 0)
                    {
                        ew.Cells[$"C2:C{deliveriesOilDto.Count + 1}"].Style.Numberformat.Format = "0.00";
                    }

                    for (int i = 2; i <= deliveriesOilDto.Count + 1; i++)
                    {
                        DeliveryOilDto delivery = deliveriesOilDto[i - 2];
                        ew.Cells[$"A{i}"].Value = delivery.ProviderId;
                        ew.Cells[$"B{i}"].Value = delivery.StockName;
                        ew.Cells[$"C{i}"].Value = delivery.PriceVAT;
                        ew.Cells[$"D{i}"].Value = delivery.QuantityValue;
                        ew.Cells[$"E{i}"].Value = $"{delivery.DeliveryDate:d}";
                    }

                    excelPackage.Save();
                }
            }
        }
    }
}

using goods.Commands;
using goods.Dao;
using goods.Models;
using goods.Models.Dto;
using goods.Stores;
using Microsoft.WindowsAPICodePack.Dialogs;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace goods.ViewModels
{
    public class StockReportViewModel : BaseViewModel
    {
        private const string ALL_STOCK_MEASURE = "-- Всички --";

        public RelayCommand SearchCommand { get; }
        public RelayCommand ExcelExportCommand { get; }
        public ICommand NavigationBackCommand { get; }

        private StockDao stockDao;
        private List<StockSummaryDto> stockSummaryDtos;

        private string stockName;
        private List<string> measures;
        private string selectedMeasure;

        public StockReportViewModel(NavigationStore navigationStore)
        {
            SearchCommand = new RelayCommand(Search);
            ExcelExportCommand = new RelayCommand(CreateExcelFile);
            NavigationBackCommand = new NavigateCommand<ReportHomeViewModel>(navigationStore, (n) => new ReportHomeViewModel(n));
            stockDao = new StockDao();
            measures = Enum.GetNames(typeof(StockMeasure)).ToList();
            measures.Insert(0, ALL_STOCK_MEASURE);
            SelectedMeasure = measures[0];
            StockSummaryDtos = stockDao.GetStockSummary(StockName, null);
        }

        public List<StockSummaryDto> StockSummaryDtos
        {
            get { return stockSummaryDtos; }
            set { stockSummaryDtos = value; OnPropertyChanged(nameof(StockSummaryDtos)); }
        }

        public string StockName
        {
            get { return stockName; }
            set { stockName = value; OnPropertyChanged(nameof(StockName)); }
        }

        public List<string> Measures
        {
            get { return measures; }
        }

        public string SelectedMeasure
        {
            get { return selectedMeasure; }
            set { selectedMeasure = value; OnPropertyChanged(nameof(SelectedMeasure)); }
        }

        private void Search()
        {
            StockSummaryDtos = stockDao.GetStockSummary(StockName, 
                SelectedMeasure.Equals(ALL_STOCK_MEASURE) ? null : SelectedMeasure);
        }

        private void CreateExcelFile()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string pathFile = Path.Combine(dialog.FileName, "StockReport.xlsx");
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
                    ExcelWorksheet ew = excelPackage.Workbook.Worksheets.Add("StockReport");

                    ew.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ew.Row(1).Style.Font.Size = 12;
                    ew.Row(1).Style.Font.Bold = true;
                    ew.Row(1).Style.Font.Color.SetColor(Color.White);
                    ew.Cells["A1:E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ew.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(48, 84, 150));
                    ew.Columns.Width = 22;
                    ew.Column(5).Width = 30;

                    ew.Cells["A1"].Value = "Име на стока";
                    ew.Cells["B1"].Value = "Цена";
                    ew.Cells["C1"].Value = "Мярка количество";
                    ew.Cells["D1"].Value = "Брой доставки";
                    ew.Cells["E1"].Value = "Общо доставено количество";

                    if (StockSummaryDtos.Count > 0)
                    {
                        ew.Cells[$"B2:B{StockSummaryDtos.Count + 1}"].Style.Numberformat.Format = "0.00";
                    }

                    for (int i = 2; i <= StockSummaryDtos.Count + 1; i++)
                    {
                        StockSummaryDto stock = StockSummaryDtos[i - 2];
                        ew.Cells[$"A{i}"].Value = stock.Name;
                        ew.Cells[$"B{i}"].Value = stock.Price;
                        ew.Cells[$"C{i}"].Value = stock.Measure;
                        ew.Cells[$"D{i}"].Value = stock.DeliveryCount;
                        ew.Cells[$"E{i}"].Value = stock.TotalQuantity;
                    }

                    excelPackage.Save();
                }
            }
        }
    }
}

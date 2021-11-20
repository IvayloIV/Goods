using System;

namespace goods.Models
{
    public class Provider : BaseModel
    {
        private string id;
        private string name;
        private string address;
        private string phone;
        private string contactPerson;

        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(nameof(Address)); }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(nameof(Phone)); }
        }

        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; OnPropertyChanged(nameof(ContactPerson)); }
        }
    }
}

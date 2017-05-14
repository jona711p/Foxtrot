using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace Foxtrot.Classes
{
    public class User : INotifyPropertyChanged
    {
        public int? ID { get; set; }
        public int Permission { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? WorkPhone { get; set; }
        public string WorkEmail { get; set; }
        public int? WorkFax { get; set; }
        public DataTable UserTable { get; set; }

        private static ObservableCollection<KeyValuePair<int, string>> adminActorObservableCollection = new ObservableCollection<KeyValuePair<int, string>>();

        public ObservableCollection<KeyValuePair<int, string>> AdminActorObservableCollection
        {
            get { return adminActorObservableCollection; }
            set { adminActorObservableCollection = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Administrator : User
    {
        public int? User_ID { get; set; }
        public Administrator()
        {
            Permission = 1;
        }
    }

    public class Actor : User
    {
        public int? User_ID { get; set; }
        public string CompanyName { get; set; }
        public Actor()
        {
            Permission = 2;
        }
    }
}

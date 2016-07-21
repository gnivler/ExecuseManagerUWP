using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using Windows.Storage;

namespace ExcuseManagerUWP
{
    [DataContract]
    class Excuse : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Results { get; set; }

        [DataMember]
        public DateTimeOffset LastUsedDate { get; set; }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

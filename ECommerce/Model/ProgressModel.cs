using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model
{
    public class ProgressModel : INotifyPropertyChanged
    {
        private bool _isBusy { get; set; }
        private int _min { get; set; }
        private int _max { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }


        public int Max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
                OnPropertyChanged("Max");
            }
        }

        public int Min
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
                OnPropertyChanged("Min");
            }
        }
    }
}

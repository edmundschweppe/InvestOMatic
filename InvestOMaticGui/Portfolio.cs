using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InvestOMaticGui.Model
{
    public class Portfolio : INotifyPropertyChanged
    {
        private ObservableCollection<Position> _positions = null;
        public ObservableCollection<Position> Positions
        {
            get
            {
                return _positions;
            }
            set
            {
                if (_positions != value)
                {
                    _positions = value;
                    RaisePropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

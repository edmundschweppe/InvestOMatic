using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestOMaticGui.Model
{
    public class Portfolio : ObservableObject
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
    }
}

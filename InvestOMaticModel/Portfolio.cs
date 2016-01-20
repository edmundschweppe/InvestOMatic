using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestOMaticModel
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
                    //_positions.CollectionChanged += _positions_CollectionChanged;
                    RaisePropertyChanged();
                }
            }
        }

        //void _positions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    RaisePropertyChanged("TotalValue");
        //}

        public double TotalValue
        {
            get
            {
                return Positions.Sum(p => p.Amount);
            }
        }
        public void Recalculate(double newValue)
        {
            double amountPerPortfolio = newValue / Positions.Count();
            foreach(var position in Positions)
            {
                position.Amount = amountPerPortfolio;
            }
            RaisePropertyChanged("TotalValue");
        }
    }
}

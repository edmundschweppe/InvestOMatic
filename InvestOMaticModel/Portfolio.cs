using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestOMaticModel
{
    public class Portfolio : ObservableObject
    {
        private ObservableCollection<Position> _positions = null;

        public Portfolio()
        {
            Positions = new ObservableCollection<Position>();
        }

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
                    if (_positions != null)
                    {
                        foreach (Position p in _positions)
                        {
                            p.PropertyChanged -= PositionPropertyChangedHandler;
                        }
                        _positions.CollectionChanged -= _positions_CollectionChanged;
                    }
                    _positions = value;
                    if (_positions != null)
                    {
                        _positions.CollectionChanged += _positions_CollectionChanged;
                        foreach (Position p in _positions)
                        {
                            p.PropertyChanged += PositionPropertyChangedHandler;
                        }
                    }
                    // We *could* be clever, check to see if TotalValues really changed,
                    // and only report Positions changing if TotalValues stayed the same,
                    // but it's simpler to just assume TotalValue will change as well
                    RaisePropertyChanged(String.Empty);
                }
            }
        }

        private void _positions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    HandlePositionAddEvent(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Move:
                    // don't care
                    break;
                case NotifyCollectionChangedAction.Remove:
                    HandlePositionRemoveEvent(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    HandlePositionAddEvent(e.NewItems);
                    HandlePositionRemoveEvent(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    HandlePositionAddEvent(e.NewItems);
                    HandlePositionRemoveEvent(e.OldItems);
                    break;
                default:
                    throw new Exception(String.Format("unexpected action type {0}", e.Action));
            }
            // We *could* be clever, check to see if TotalValues really changed,
            // and only report Positions changing if TotalValues stayed the same,
            // but it's simpler to just assume TotalValue will change as well
            RaisePropertyChanged(String.Empty);
        }

        private void HandlePositionAddEvent(IList newItems)
        {
            foreach(object newItem in newItems)
            {
                Position p = (Position)newItem;
                p.PropertyChanged += PositionPropertyChangedHandler;
            }
        }

        private void HandlePositionRemoveEvent(IList oldItems)
        {
            foreach (object oldItem in oldItems)
            {
                Position p = (Position)oldItem;
                p.PropertyChanged -= PositionPropertyChangedHandler;
            }
        }

        private void PositionPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Amount")
            {
                RaisePropertyChanged("TotalValue");
            }
        }

        public double TotalValue
        {
            get
            {
                if (Positions == null)
                {
                    return 0.0;
                }
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

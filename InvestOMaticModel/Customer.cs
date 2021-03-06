﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InvestOMaticModel
{
    public class Customer : ObservableObject
    {
        #region Member variables
        private string _firstName = String.Empty;
        private string _lastName = String.Empty;
        private Portfolio _original = null;
        private Portfolio _recommended = null;
        private ICommand _rebalance = null;
        #endregion Member variables

        #region Constructors
        public Customer()
        {
            FirstName = "Joe";
            LastName = "Schmoe";
            OriginalPortfolio = new Portfolio
            {
                Positions = new ObservableCollection<Position>
                {
                    new Position 
                    {
                        SecurityIdentifier = "CASH",
                        Amount = 100000.00
                    }
                }
            };
            RecommendedPortfolio = new Portfolio
            {
                Positions = new ObservableCollection<Position>
                {
                    new Position 
                    {
                        SecurityIdentifier = "247109BR", 
                        Issuer = "DELMARVA POWER AND LIGHT",
                        Coupon="4.00",
                        Maturity="06/01/2042",
                        Amount=20000.00
                    },
                    new Position 
                    {
                        SecurityIdentifier = "369550AT", 
                        Issuer = "GENERAL DYNAMICS CORP",
                        Coupon="3.60",
                        Maturity="11/15/2042",
                        Amount=20000.00
                    },
                    new Position 
                    {
                        SecurityIdentifier = "61746BCY", 
                        Issuer = "MORGAN STANLEY DEAN WITTER",
                        Coupon="5.75",
                        Maturity="11/15/2038",
                        Amount=20000.00
                    },
                    new Position 
                    {
                        SecurityIdentifier = "927804FG", 
                        Issuer = "VIRGINIA ELECTRIC POWER",
                        Coupon="8.875",
                        Maturity="11/15/2038",
                        Amount=20000.00
                    },
                    new Position 
                    {
                        SecurityIdentifier = "931142CM", 
                        Issuer = "WAL-MART STORES",
                        Coupon="6.20",
                        Maturity="04/30/2038",
                        Amount=20000.00
                    }
                }
            };
        }
        #endregion Constructors

        #region Public properties
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("FullName");
                }
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("FullName");
                }
            }
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public Portfolio OriginalPortfolio
        {
            get
            {
                return _original;
            }
            set
            {
                if (_original != value)
                {
                    _original = value;
                    RaisePropertyChanged();
                }

            }
        }
        public Portfolio RecommendedPortfolio 
        {
            get
            {
                return _recommended;
            }
            set
            {
                if (_recommended != value)
                {
                    _recommended = value;
                    RaisePropertyChanged();
                }

            }
        }
        #endregion Public properties

        #region Commands
        public ICommand RebalanceCommand
        {
            get
            {
                if (_rebalance == null)
                {
                    _rebalance = new RelayCommand(DoRebalance, CanRebalance);
                }
                return _rebalance;
            }
        }

        public bool CanRebalance()
        {
            return (OriginalPortfolio.TotalValue > 0.0);
        }
        public void DoRebalance()
        {
            double newAmount = OriginalPortfolio.TotalValue;
            RecommendedPortfolio.Recalculate(newAmount);
        }
        #endregion Commands
    }
}

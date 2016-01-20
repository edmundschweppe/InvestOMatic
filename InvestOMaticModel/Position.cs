using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestOMaticModel
{
    public class Position : ObservableObject
    {
        private string _securityIdentifier;
        private string _issuer;
        private string _coupon;
        private string _maturity;
        private string _amount;

        public string SecurityIdentifier 
        {
            get
            {
                return _securityIdentifier;
            }
            set
            {
                if (_securityIdentifier != value)
                {
                    _securityIdentifier = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string Issuer
        {
            get
            {
                return _issuer;
            }
            set
            {
                if (_issuer != value)
                {
                    _issuer = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string Coupon
        {
            get
            {
                return _coupon;
            }
            set
            {
                if (_coupon != value)
                {
                    _coupon = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string Maturity
        {
            get
            {
                return _maturity;
            }
            set
            {
                if (_maturity != value)
                {
                    _maturity = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}

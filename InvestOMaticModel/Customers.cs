using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestOMaticModel
{
    public class Customers : ObservableCollection<Customer>
    {
        public Customers()
            :base()
        {
            Customer fred = new Customer
            {
                FirstName = "Fred",
                LastName = "Flintstone",
            };
            fred.OriginalPortfolio.Positions.Add(new Position
            {
                SecurityIdentifier = "CASH",
                Amount = 100000
            });
            Customer barney = new Customer
            {
                FirstName = "Barney",
                LastName = "Rubble",
            };
            barney.OriginalPortfolio.Positions.Add(new Position
            {
                SecurityIdentifier = "CASH",
                Amount = 50000
            });
            Customer wilma = new Customer
            {
                FirstName = "Wilma",
                LastName = "Flintstone"
            };
            wilma.OriginalPortfolio.Positions.Add(new Position
            {
                SecurityIdentifier = "CASH",
                Amount = 250000
            });
            this.Add(fred);
            this.Add(wilma);
            this.Add(barney);

        }
    }
}

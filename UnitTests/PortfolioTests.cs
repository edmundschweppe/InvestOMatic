using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvestOMaticModel;
using System.ComponentModel;

namespace UnitTests
{
    [TestClass]
    public class PortfolioTests
    {

        #region Tests

        [TestMethod]
        public void TotalValueZeroForEmptyPortfolio()
        {
            double expectedAmount = 0.0;
            Portfolio target = new Portfolio();

            Assert.AreEqual(expectedAmount, target.TotalValue);
        }

        [TestMethod]
        public void OnePosition_TotalValue()
        {
            double expectedAmount = 12345.67;
            Position pos = new Position { SecurityIdentifier = "foo", Amount = expectedAmount };
            Portfolio target = new Portfolio();

            target.Positions.Add(pos);

            Assert.AreEqual(expectedAmount, target.TotalValue);
        }

        [TestMethod]
        public void TwoPositions_TotalValue()
        {
            Position posOne = new Position { SecurityIdentifier = "foo", Amount = 12345.67 };
            Position posTwo = new Position { SecurityIdentifier = "bar", Amount = 23456.78 };
            double expectedAmount = posOne.Amount + posTwo.Amount;
            Portfolio target = new Portfolio();

            target.Positions.Add(posOne);
            target.Positions.Add(posTwo);

            Assert.AreEqual(expectedAmount, target.TotalValue);
        }

        [TestMethod]
        public void ChangingPositionAmountChangesTotalValue()
        {
            Position posOne = new Position { SecurityIdentifier = "foo", Amount = 12345.67 };
            Position posTwo = new Position { SecurityIdentifier = "bar", Amount = 23456.78 };
            Portfolio target = new Portfolio();
            target.Positions.Add(posOne);
            target.Positions.Add(posTwo);

            posTwo.Amount = 34567.89;

            double expectedAmount = posOne.Amount + posTwo.Amount;
            Assert.AreEqual(expectedAmount, target.TotalValue);
        }

        [TestMethod]
        public void AddingPositionFiresPropertyChangedEvent()
        {
            Position posOne = new Position { SecurityIdentifier = "foo", Amount = 12345.67 };
            Position posTwo = new Position { SecurityIdentifier = "bar", Amount = 23456.78 };
            Portfolio target = new Portfolio();
            target.Positions.Add(posOne);

            List<string> propertyNames = new List<string>();
            target.PropertyChanged += (s, e) =>
            {
                propertyNames.Add(e.PropertyName);
            };

            target.Positions.Add(posTwo);

            Assert.AreEqual(1, propertyNames.Count(), "checking propertyNames Count");
            Assert.IsTrue(propertyNames.Contains(String.Empty), "checking that propertyNames contains empty string");
        }

        [TestMethod]
        public void RemovingPositionFiresPropertyChangedEvent()
        {
            Position posOne = new Position { SecurityIdentifier = "foo", Amount = 12345.67 };
            Position posTwo = new Position { SecurityIdentifier = "bar", Amount = 23456.78 };
            Portfolio target = new Portfolio();
            target.Positions.Add(posOne);
            target.Positions.Add(posTwo);

            List<string> propertyNames = new List<string>();
            target.PropertyChanged += (s, e) =>
            {
                propertyNames.Add(e.PropertyName);
            };

            target.Positions.Remove(posTwo);

            Assert.AreEqual(1, propertyNames.Count(), "checking propertyNames Count");
            Assert.IsTrue(propertyNames.Contains(String.Empty), "checking that propertyNames contains empty string");
        }

        [TestMethod]
        public void ReplacingPositionsCollectionFiresPropertyChangedEvent()
        {
            Position posOne = new Position { SecurityIdentifier = "foo", Amount = 12345.67 };
            Position posTwo = new Position { SecurityIdentifier = "bar", Amount = 23456.78 };
            Position posThree = new Position { SecurityIdentifier = "bletch", Amount = 34567.89 };
            ObservableCollection<Position> origPositions = new ObservableCollection<Position>{
                posOne,
                posTwo
            };
            ObservableCollection<Position> newPositions = new ObservableCollection<Position> { posThree };
            Portfolio target = new Portfolio { Positions = origPositions };

            List<string> propertyNames = new List<string>();
            target.PropertyChanged += (s, e) =>
            {
                propertyNames.Add(e.PropertyName);
            };

            target.Positions = newPositions;

            Assert.AreEqual(1, propertyNames.Count(), "checking propertyNames Count");
            Assert.IsTrue(propertyNames.Contains(String.Empty), "checking that propertyNames contains String.Empty");
        }

        [TestMethod]
        public void ChangingPositionAmountFiresTotalValuePropertyChangedEvent()
        {
            Position posOne = new Position { SecurityIdentifier = "foo", Amount = 12345.67 };
            Position posTwo = new Position { SecurityIdentifier = "bar", Amount = 23456.78 };
            Portfolio target = new Portfolio();
            target.Positions.Add(posOne);
            target.Positions.Add(posTwo);

            List<string> propertyNames = new List<string>();
            target.PropertyChanged += (s, e) =>
            {
                propertyNames.Add(e.PropertyName);
            };

            posTwo.Amount = 34567.89;

            Assert.AreEqual(1, propertyNames.Count(), "checking propertyNames Count");
            Assert.AreEqual("TotalValue", propertyNames[0], "checking propertyNames[0]");
        }

        [TestMethod]
        public void ChangingRemovedPositionDoesNotFirePropertyChangedEvent()
        {
            Position posOne = new Position { SecurityIdentifier = "foo", Amount = 12345.67 };
            Position posTwo = new Position { SecurityIdentifier = "bar", Amount = 23456.78 };
            Portfolio target = new Portfolio();
            target.Positions.Add(posOne);
            target.Positions.Add(posTwo);

            target.Positions.Remove(posTwo);

            List<string> propertyNames = new List<string>();
            target.PropertyChanged += (s, e) =>
            {
                propertyNames.Add(e.PropertyName);
            };

            posTwo.Amount = 34567.89;

            Assert.AreEqual(0, propertyNames.Count(), "checking propertyNames Count");
        }

        [TestMethod]
        public void ChangingPositionAmountInNewCollectionFiresPropertyChangedEvent()
        {
            Position posOne = new Position { SecurityIdentifier = "foo", Amount = 12345.67 };
            Position posTwo = new Position { SecurityIdentifier = "bar", Amount = 23456.78 };
            Position posThree = new Position { SecurityIdentifier = "bletch", Amount = 34567.89 };
            ObservableCollection<Position> origPositions = new ObservableCollection<Position>{
                posOne,
                posTwo
            };
            ObservableCollection<Position> newPositions = new ObservableCollection<Position> { posThree };
            Portfolio target = new Portfolio { Positions = origPositions };
            target.Positions = newPositions;

            List<string> propertyNames = new List<string>();
            target.PropertyChanged += (s, e) =>
            {
                propertyNames.Add(e.PropertyName);
            };

            posThree.Amount = 45678.90;

            Assert.AreEqual(1, propertyNames.Count(), "checking propertyNames Count");
            Assert.IsTrue(propertyNames.Contains("TotalValue"), "checking that propertyNames contains TotalValue");
        }

        [TestMethod]
        public void ChangingPositionAmountInOldCollectionDoesNotFirePropertyChangedEvent()
        {
            Position posOne = new Position { SecurityIdentifier = "foo", Amount = 12345.67 };
            Position posTwo = new Position { SecurityIdentifier = "bar", Amount = 23456.78 };
            Position posThree = new Position { SecurityIdentifier = "bletch", Amount = 34567.89 };
            ObservableCollection<Position> origPositions = new ObservableCollection<Position>{
                posOne,
                posTwo
            };
            ObservableCollection<Position> newPositions = new ObservableCollection<Position> { posThree };
            Portfolio target = new Portfolio { Positions = origPositions };
            target.Positions = newPositions;

            List<string> propertyNames = new List<string>();
            target.PropertyChanged += (s, e) =>
            {
                propertyNames.Add(e.PropertyName);
            };

            posOne.Amount = 45678.90;

            Assert.AreEqual(0, propertyNames.Count(), "checking propertyNames Count");
        }
        #endregion Tests
    }
}

﻿using System;
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
    public class InitialModelTests
    {
        [TestMethod]
        public void CannotRebalanceWithZeroOriginalPositionValue()
        {
            Portfolio orig = new Portfolio{
                Positions = new ObservableCollection<Position>{
                    new Position {
                        SecurityIdentifier = "gumballs",
                        Amount = 0.0
                    }
                }
            };
            Portfolio recommended = new Portfolio
            {
                Positions = new ObservableCollection<Position>{
                    new Position {
                        SecurityIdentifier = "cherries",
                        Amount = 0.0
                    }
                }
            };
            InitialModel target = new InitialModel
            {
                OriginalPortfolio = orig,
                RecommendedPortfolio = recommended
            };

            Assert.IsFalse(target.CanRebalance(), "checking CanRebalance");
        }

        [TestMethod]
        public void CanRebalanceWithPositiveOriginalPositionValue()
        {
            double newValue = 123456.78;
            Portfolio orig = new Portfolio
            {
                Positions = new ObservableCollection<Position>{
                    new Position {
                        SecurityIdentifier = "gumballs",
                        Amount = 0.0
                    }
                }
            };
            Portfolio recommended = new Portfolio
            {
                Positions = new ObservableCollection<Position>{
                    new Position {
                        SecurityIdentifier = "cherries",
                        Amount = 0.0
                    }
                }
            };
            InitialModel target = new InitialModel
            {
                OriginalPortfolio = orig,
                RecommendedPortfolio = recommended
            };

            orig.Positions[0].Amount = newValue;

            Assert.IsTrue(target.CanRebalance(), "checking CanRebalance");
        }

        [TestMethod]
        public void RebalancingWithOneRecommendedPosition()
        {
            double newValue = 123456.78;
            Portfolio orig = new Portfolio
            {
                Positions = new ObservableCollection<Position>{
                    new Position {
                        SecurityIdentifier = "gumballs",
                        Amount = 0.0
                    }
                }
            };
            Portfolio recommended = new Portfolio
            {
                Positions = new ObservableCollection<Position>{
                    new Position {
                        SecurityIdentifier = "cherries",
                        Amount = 0.0
                    }
                }
            };
            InitialModel target = new InitialModel
            {
                OriginalPortfolio = orig,
                RecommendedPortfolio = recommended
            };

            orig.Positions[0].Amount = newValue;
            Assert.IsTrue(target.CanRebalance(), "checking CanRebalance");

            target.DoRebalance();

            Assert.AreEqual(newValue, target.RecommendedPortfolio.TotalValue, "checking recommended total value");
            Assert.AreEqual(newValue, target.RecommendedPortfolio.Positions[0].Amount, "checking recommended position 0 amount");
        }

    }
}

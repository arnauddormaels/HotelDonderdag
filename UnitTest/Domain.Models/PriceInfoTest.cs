using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnitTest.Domain.Models
{
    /// <summary>
    /// Summary description for PriceInfoTest
    /// </summary>
    [TestClass]
    public class PriceInfoTest
    {
        public PriceInfoTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [Test]
        public void CalculatePrice_AdultAgeAndDiscount_ReturnsCorrectPrice()
        {
            // Arrange
            int adultPrice = 100;
            int childPrice = 50;
            int discount = 10;
            int adultAge = 18;

            PriceInfo priceInfo = new PriceInfo(adultPrice, childPrice, discount, adultAge);

            // Act
            int adultGuestAge = 25;
            int childGuestAge = 15;

            int adultGuestPrice = priceInfo.CalculatePrice(adultGuestAge);
            int childGuestPrice = priceInfo.CalculatePrice(childGuestAge);

            // Assert
            Assert.AreEqual(adultPrice - (adultPrice * discount / 100), adultGuestPrice);
            Assert.AreEqual(childPrice - (childPrice * discount / 100), childGuestPrice);
        }

        [Test]
        public void CalculatePrice_ChildAgeAndDiscount_ReturnsCorrectPrice()
        {
            // Arrange
            int adultPrice = 100;
            int childPrice = 50;
            int discount = 10;
            int adultAge = 18;
            PriceInfo priceInfo = new PriceInfo(adultPrice, childPrice, discount, adultAge);

            // Act
            int childGuestAge = 10;

            int childGuestPrice = priceInfo.CalculatePrice(childGuestAge);

            // Assert
            Assert.AreEqual(childPrice - (childPrice * discount / 100), childGuestPrice);
        }

        [Test]
        public void CalculatePrice_EqualAdultAgeAndDiscount_ReturnsCorrectPrice()
        {
            // Arrange
            int adultPrice = 100;
            int childPrice = 50;
            int discount = 10;
            int adultAge = 18;
            PriceInfo priceInfo = new PriceInfo(adultPrice, childPrice, discount, adultAge);

            // Act
            int guestAge = 18;

            int guestPrice = priceInfo.CalculatePrice(guestAge);

            // Assert
            Assert.AreEqual(adultPrice - (adultPrice * discount / 100), guestPrice);
        }
    }
}

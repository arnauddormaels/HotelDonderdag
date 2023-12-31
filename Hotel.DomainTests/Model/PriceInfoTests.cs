﻿using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hotel.Domain.Model.Tests
{
    public class PriceInfoTests
    {
        [Fact]
        public void Constructor_WithParameters_SetsPropertiesCorrectly()
        {
            // Arrange
            int id = 1;
            int adultPrice = 100;
            int childPrice = 50;
            int discount = 10;
            int adultAge = 18;

            // Act
            PriceInfo priceInfo = new PriceInfo(id, adultPrice, childPrice, discount, adultAge);

            // Assert
            Assert.Equal(id, priceInfo.Id);
            Assert.Equal(adultPrice, priceInfo.AdultPrice);
            Assert.Equal(childPrice, priceInfo.ChildPrice);
            Assert.Equal(discount, priceInfo.Discount);
            Assert.Equal(adultAge, priceInfo.AdultAge);
        }

        [Fact]
        public void CalculatePrice_ReturnsCorrectPriceForAdult()
        {
            int adultPrice = 100;
            int childPrice = 50;
            int discount = 10;
            int adultAge = 18;

            PriceInfo priceInfo = new PriceInfo(adultPrice, childPrice, discount, adultAge);

            int adultGuestAge = 25;
            int adultGuestPrice = priceInfo.CalculatePrice(adultGuestAge);

            Assert.Equal(adultPrice - (adultPrice * discount / 100), adultGuestPrice);
        }

        [Fact]
        public void CalculatePrice_ReturnsCorrectPriceForChild()
        {
            int adultPrice = 100;
            int childPrice = 50;
            int discount = 10;
            int adultAge = 18;

            PriceInfo priceInfo = new PriceInfo(adultPrice, childPrice, discount, adultAge);

            int childGuestAge = 10;
            int childGuestPrice = priceInfo.CalculatePrice(childGuestAge);

            Assert.Equal(childPrice - (childPrice * discount / 100), childGuestPrice);
        }

        [Fact]
        public void CalculatePrice_ReturnsCorrectPriceForEqualAdultAge()
        {
            int adultPrice = 100;
            int childPrice = 50;
            int discount = 10;
            int adultAge = 18;

            PriceInfo priceInfo = new PriceInfo(adultPrice, childPrice, discount, adultAge);

            int guestAge = 18;
            int guestPrice = priceInfo.CalculatePrice(guestAge);

            Assert.Equal(adultPrice - (adultPrice * discount / 100), guestPrice);
        }

        [Fact]
        public void CalculatePrice_NegativeAdultPrice_ThrowPriceInfoException()
        {
            int adultPrice = -100;
            int childPrice = 50;
            int discount = 10;
            int adultAge = 18;

            Assert.Throws<PriceInfoException>(() => new PriceInfo(adultPrice,childPrice,discount,adultAge));

        }
        public void CalculatePrice_NegativeChildPrice_ThrowPriceInfoException()
        {
            int adultPrice = 100;
            int childPrice = -50;
            int discount = 10;
            int adultAge = 18;

            Assert.Throws<PriceInfoException>(() => new PriceInfo(adultPrice, childPrice, discount, adultAge));

        }
        public void CalculatePrice_NegativeDiscount_ThrowPriceInfoException()
        {
            int adultPrice = 100;
            int childPrice = 50;
            int discount = -10;
            int adultAge = 18;

            Assert.Throws<PriceInfoException>(() => new PriceInfo(adultPrice, childPrice, discount, adultAge));

        }
        public void CalculatePrice_NegativeAdultAge_ThrowPriceInfoException()
        {
            int adultPrice = 100;
            int childPrice = 50;
            int discount = 10;
            int adultAge = -18;

            Assert.Throws<PriceInfoException>(() => new PriceInfo(adultPrice, childPrice, discount, adultAge));

        }
    }
}
using Hotel.Domain.Exceptions;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hotel.Tests.Model
{
    public class AddressTests
    {
        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateAddressObject()
        {
            // Arrange --Klaarzetten van de data die gebruikt wordt
            string city = "Leuven";
            string street = "Stationstraat";
            string postalCode = "9000";
            string houseNumber = "42";

            // Act -- Uitvoeren van de nodige Constructors/methodes
            Address address = new Address(city, street, postalCode, houseNumber);

            // Assert  -- Controleren of dat de objecten de juiste waardes bevatten
            Assert.NotNull(address);
            Assert.Equal(city, address.City);
            Assert.Equal(street, address.Street);
            Assert.Equal(postalCode, address.PostalCode);
            Assert.Equal(houseNumber, address.HouseNumber);
        }

        [Theory]
        [InlineData("Leuven", "Stationstraat", "9000", "42")]
        [InlineData("Gent", "SintPiertersStraat", "9200", "13")]
        public void ToAddressLine_ShouldReturnCorrectFormat(string city, string street, string postalCode, string houseNumber)
        {
            Address address = new Address(city, street, postalCode, houseNumber);

            string addressLine = address.ToAddressLine();

            string expectedFormat = $"{city}|{postalCode}|{street}|{houseNumber}";
            Assert.Equal(expectedFormat, addressLine);
        }

        [Fact]
        public void Constructor_WithInvalidCity_ShouldThrowCustomerException()
        {
            //Arrange
            string city = "";
            //Act & Assert 
            Assert.Throws<CustomerException>(() => new Address(city, "Stationstraat", "9000", "42"));
        }

        [Fact]
        public void Constructor_WithInvalidPostalCode_ShouldThrowCustomerException()
        {
            string postalCode = "";

            Assert.Throws<CustomerException>(() => new Address("Leuven", "Stationstraat", postalCode, "42"));
        }

        [Fact]
        public void Constructor_WithInvalidStreet_ShouldThrowCustomerException()
        {
            string street = "";

            Assert.Throws<CustomerException>(() => new Address("Leuven", street, "9000", "42"));
        }

        [Fact]
        public void Constructor_WithInvalidHouseNumber_ShouldThrowCustomerException()
        {
            string houseNumber = "";

            Assert.Throws<CustomerException>(() => new Address("Leuven", "Stationstraat", "9000", houseNumber));
        }

        [Theory]
        [InlineData("Leuven [9000] - Stationstraat - 42", "Leuven", "9000", "Stationstraat", "42")]
        [InlineData("Brugge [9200] - Stationstraat - 13", "Brugge", "9200", "Stationstraat", "13")]
        public void ToAddressLine_WithValidFormat_ShouldReturnCorrectAddressLine(string input, string expectedCity, string expectedPostalCode, string expectedStreet, string expectedHouseNumber)
        {
            string addressLine = Address.ToAddressLine(input);

            Assert.Equal($"{expectedCity}|{expectedPostalCode}|{expectedStreet}|{expectedHouseNumber}", addressLine);
        }

        [Fact]
        public void ToAddressLine_WithInvalidFormat_ShouldThrowCustomerException()
        {

            string invalidAddress = "InvalidAddress";

            Assert.Throws<CustomerException>(() => Address.ToAddressLine(invalidAddress));
        }
    }
}

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
            // Arrange
            string city = "City";
            string street = "Street";
            string postalCode = "12345";
            string houseNumber = "42";

            // Act
            Address address = new Address(city, street, postalCode, houseNumber);

            // Assert
            Assert.NotNull(address);
            Assert.Equal(city, address.City);
            Assert.Equal(street, address.Street);
            Assert.Equal(postalCode, address.PostalCode);
            Assert.Equal(houseNumber, address.HouseNumber);
        }

        [Theory]
        [InlineData("City", "Street", "12345", "42")]
        [InlineData("AnotherCity", "AnotherStreet", "54321", "13")]
        public void ToAddressLine_ShouldReturnCorrectFormat(string city, string street, string postalCode, string houseNumber)
        {
            // Arrange
            Address address = new Address(city, street, postalCode, houseNumber);

            // Act
            string addressLine = address.ToAddressLine();

            // Assert
            string expectedFormat = $"{city}|{postalCode}|{street}|{houseNumber}";
            Assert.Equal(expectedFormat, addressLine);
        }

        [Fact]
        public void Constructor_WithInvalidCity_ShouldThrowCustomerException()
        {
            // Arrange
            string city = "";

            // Act & Assert
            Assert.Throws<CustomerException>(() => new Address(city, "Street", "12345", "42"));
        }

        [Fact]
        public void Constructor_WithInvalidPostalCode_ShouldThrowCustomerException()
        {
            // Arrange
            string postalCode = "";

            // Act & Assert
            Assert.Throws<CustomerException>(() => new Address("City", "Street", postalCode, "42"));
        }

        [Fact]
        public void Constructor_WithInvalidStreet_ShouldThrowCustomerException()
        {
            // Arrange
            string street = "";

            // Act & Assert
            Assert.Throws<CustomerException>(() => new Address("City", street, "12345", "42"));
        }

        [Fact]
        public void Constructor_WithInvalidHouseNumber_ShouldThrowCustomerException()
        {
            // Arrange
            string houseNumber = "";

            // Act & Assert
            Assert.Throws<CustomerException>(() => new Address("City", "Street", "12345", houseNumber));
        }

        [Theory]
        [InlineData("City [12345] - Street - 42", "City", "12345", "Street", "42")]
        [InlineData("AnotherCity [54321] - AnotherStreet - 13", "AnotherCity", "54321", "AnotherStreet", "13")]
        public void ToAddressLine_WithValidFormat_ShouldReturnCorrectAddressLine(string input, string expectedCity, string expectedPostalCode, string expectedStreet, string expectedHouseNumber)
        {
            // Act
            string addressLine = Address.ToAddressLine(input);

            // Assert
            Assert.Equal($"{expectedCity}|{expectedPostalCode}|{expectedStreet}|{expectedHouseNumber}", addressLine);
        }

        [Fact]
        public void ToAddressLine_WithInvalidFormat_ShouldThrowCustomerException()
        {
            // Arrange
            string invalidAddress = "InvalidAddress";

            // Act & Assert
            Assert.Throws<CustomerException>(() => Address.ToAddressLine(invalidAddress));
        }
    }
}

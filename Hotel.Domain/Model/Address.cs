using Hotel.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hotel.Domain.Model
{
    public class Address
    {
        private const char splitChar = '|';
        public Address(string city, string street, string postalCode, string houseNumber)
        {
            City = city;
            Street = street;
            PostalCode = postalCode;
            HouseNumber = houseNumber;
        }

        public Address(string addressLine)
        {
            string[] parts = addressLine.Split(splitChar);
            City= parts[0];
            Street= parts[2];
            PostalCode= parts[1];
            HouseNumber= parts[3];
        }

        private string _city;
        public string City { get { return _city; } set { if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("Mun is empty"); _city = value; } }
        private string _postalCode;
        public string PostalCode { get { return _postalCode; } set { if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("Zip is empty"); _postalCode = value; } }
        private string _houseNumber;
        public string HouseNumber { get { return _houseNumber; } set { if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("HN is empty"); _houseNumber = value; } }
        private string _street;
        public string Street { get { return _street; } set { if (string.IsNullOrWhiteSpace(value)) throw new CustomerException("Street is empty"); _street = value; } }

        public override string ToString()
        {
            return $"{City} [{PostalCode}] - {Street} - {HouseNumber}";
        }
        public string ToAddressLine()
        {
            return $"{City}{splitChar}{PostalCode}{splitChar}{Street}{splitChar}{HouseNumber}";

        }
        public static string[] ToAddressArray(string addressLine)
        {
            string[] address = ToAddressLine(addressLine).Split("|");
            return address;
        } 
        public static string ToAddressLine(string addressLine)
        {
            // Onze regex formule
            string pattern = @"^(.*?)\s\[(.*?)\]\s-\s(.*?)\s-\s(.*?)$";

            // Controleren of de input match het de regex
            Match match = Regex.Match(addressLine, pattern);

            if (match.Success)
            {
                string city = match.Groups[1].Value;
                string postalCode = match.Groups[2].Value;
                string street = match.Groups[3].Value;
                string houseNumber = match.Groups[4].Value;

                return $"{city}{splitChar}{postalCode}{splitChar}{street}{splitChar}{houseNumber}";
            }
            else
            {
                throw new CustomerException("Invalid address format");
            }
        }

    }
}

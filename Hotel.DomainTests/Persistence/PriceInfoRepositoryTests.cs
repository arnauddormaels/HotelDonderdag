﻿using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hotel.Tests.Persistence
{
    public class PriceInfoRepositoryTests
    {
        private readonly string testConnectionString;
        private readonly PriceInfoRepository priceInfoRepository;

        public PriceInfoRepositoryTests()
        {
            // Gebruik een testdatabase of in-memory database (bijvoorbeeld SQLite) voor unit tests
            testConnectionString = "Data Source=LAPTOP-UMGHNHQ1\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True";
            priceInfoRepository = new PriceInfoRepository(testConnectionString);
        }

        [Fact]
        public void AddPriceInfo_ShouldAddPriceInfoToDatabase()
        {
            // Arrange
            var priceInfo = new PriceInfo(150, 75, 10, 18);

            // Act
            int id = priceInfoRepository.AddPriceInfo(priceInfo);

            // Assert
            Assert.True(id > 0); // Id should be a positive integer

            // Cleanup
            priceInfoRepository.DeletePriceInfo(id);
        }

        [Fact]
        public void GetPriceInfos_ShouldRetrieveListOfPriceInfos()
        {
            // Arrange
            var priceInfo1 = new PriceInfo(150, 75, 10, 18);
            var priceInfo2 = new PriceInfo(120, 60, 15, 20);

            // Act
            int id1 = priceInfoRepository.AddPriceInfo(priceInfo1);
            int id2 = priceInfoRepository.AddPriceInfo(priceInfo2);

            // Assert
            List<PriceInfo> priceInfos = priceInfoRepository.getPriceInfos();
            Assert.NotNull(priceInfos);
            Assert.Contains(priceInfo1, priceInfos);
            Assert.Contains(priceInfo2, priceInfos);

            // Cleanup
            priceInfoRepository.DeletePriceInfo(id1);
            priceInfoRepository.DeletePriceInfo(id2);
        }

        [Fact]
        public void DeletePriceInfo_ShouldRemovePriceInfoFromDatabase()
        {
            // Arrange
            var priceInfo = new PriceInfo(150, 75, 10, 18);
            int id = priceInfoRepository.AddPriceInfo(priceInfo);

            // Act
            priceInfoRepository.DeletePriceInfo(id);

            // Assert
            // Attempt to retrieve the deleted price info; it should be null
            List<PriceInfo> priceInfos = priceInfoRepository.getPriceInfos();
            Assert.DoesNotContain(priceInfo, priceInfos);
        }
    }
}



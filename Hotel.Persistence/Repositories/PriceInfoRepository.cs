using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class PriceInfoRepository : IPriceInfoRepository
    {
        private string connectionString;

        public PriceInfoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<PriceInfo> getPriceInfos()
        {
            //TODO
            return new List<PriceInfo>();
        }   
    }
}

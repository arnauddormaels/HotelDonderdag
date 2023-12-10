using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class PriceInfoManager
    {
        private IPriceInfoRepository _priceInfoRepo;

        public PriceInfoManager(IPriceInfoRepository repo)
        {
            _priceInfoRepo = repo;
        }

        public int AddPriceInfo(PriceInfo priceInfo)
        {
            try
            {
                return _priceInfoRepo.AddPriceInfo(priceInfo);
            }
            catch (Exception ex)
            {
                throw new PriceInfoManagerException("AddPriceInfo", ex);
            }
        }

        public void DeletePriceInfo(int id)
        {
            try
            {
                _priceInfoRepo.DeletePriceInfo(id);
            }
            catch (Exception ex)
            {

                throw new PriceInfoManagerException("DeletePriceInfo", ex);
            }
        }

        public List<PriceInfo> getPriceInfos()
        {
            return _priceInfoRepo.getPriceInfos();
        }

    }
}

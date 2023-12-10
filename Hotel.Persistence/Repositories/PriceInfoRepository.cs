using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            try
            {
                List<PriceInfo> priceInfos = new List<PriceInfo>();

                string sql = "select id, adultPrice, childPrice, discount, adultAge from PriceInfo;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);

                            priceInfos.Add(new PriceInfo(id, Convert.ToInt32(reader["adultPrice"]), Convert.ToInt32(reader["childPrice"]), Convert.ToInt32(reader["discount"]), Convert.ToInt32(reader["adultAge"])));

                        }
                    }
                }
                return priceInfos;
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("GetPriceInfos", ex);
            }
        }
        public int AddPriceInfo(PriceInfo priceInfo)
        {
            try
            {
                string sql = "INSERT INTO PriceInfo(adultPrice,childPrice,discount,adultAge) output INSERTED.ID VALUES(@adultPrice,@childPrice,@discount,@adultAge)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction sqlTransaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = sqlTransaction;
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@adultPrice", (int)priceInfo.AdultPrice);
                        cmd.Parameters.AddWithValue("@childPrice", (int)priceInfo.ChildPrice);
                        cmd.Parameters.AddWithValue("@discount", (int)priceInfo.Discount);
                        cmd.Parameters.AddWithValue("@adultAge", (int)priceInfo.AdultAge);
                        int id = (int)cmd.ExecuteScalar();
                        priceInfo.Id = id;

                        sqlTransaction.Commit();
                    }
                    catch (Exception ex) { sqlTransaction.Rollback(); throw; }
                }
            }
            catch (Exception ex) { throw new PriceInfoRepositoryException("addPriceInfo", ex); }
            return priceInfo.Id;
        }

        public void DeletePriceInfo(int id)
        {
            try
            {
                string updateSql = "DELETE FROM PriceInfo WHERE id = @id";



                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new PriceInfoRepositoryException("DeletePriceInfo", ex);
            }
        }
    }
}

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
    public class OrganisorRepository : IOrganisorRepository
    {
        private string connectionString;

        public OrganisorRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IReadOnlyList<Organisor> GetOrganisors(string filter)
        {
            try
            {
                Dictionary<int, Organisor> organisors = new Dictionary<int, Organisor>();
                string sql = "select o.id as organisorId, o.name,o.email,o.phone,o.address\r\n\r\nfrom organisor o\r\n\r\nwhere o.status=1";
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql += " and (o.id like @filter or o.name like @filter or o.email like @filter)";
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    if (!string.IsNullOrWhiteSpace(filter)) cmd.Parameters.AddWithValue("@filter", $"%{filter}%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["organisorId"]);

                            if (!organisors.ContainsKey(id))
                            {
                                Organisor organisor = new Organisor(id, (string)reader["name"], new ContactInfo((string)reader["email"], (string)reader["phone"], new Address((string)reader["address"])));
                                organisors.Add(id, organisor);
                            }
                            //if (!reader.IsDBNull(reader.GetOrdinal("activityId")))
                            //{
                            //    PriceInfo priceInfo = new PriceInfo(Convert.ToInt32(reader["priceInfoId"]), Convert.ToInt32(reader["adultPrice"]), Convert.ToInt32(reader["childPrice"]), Convert.ToInt32(reader["discount"]), Convert.ToInt32(reader["adultAge"]));

                            //    Description description = new Description((string)reader["activityName"], Convert.ToInt32(reader["duration"]),(string)reader["location"], (string)reader["description"]);

                            //    Event e = new Event(Convert.ToInt32(reader["activityId"]),(string)reader["activityName"], (DateTime)reader["fixture"],Convert.ToInt32(reader["nrOfPlaces"]),priceInfo, description);
                            //    organisors[id].AddEvent(e);
                            //}
                        }
                    }
                }
                return organisors.Values.ToList();
            }
            catch (Exception ex)
            {
                throw new OrganisorRepositoryException("getorganisor", ex);
            }
        }

        public Organisor AddOrganisor(Organisor organisor)
        {
            try
            {
                string sql = "INSERT INTO Organisor(name,email,phone,address,status) output INSERTED.ID VALUES(@name,@email,@phone,@address,@status)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction sqlTransaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = sqlTransaction;
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@name", organisor.Name);
                        cmd.Parameters.AddWithValue("@email", organisor.Contact.Email);
                        cmd.Parameters.AddWithValue("@phone", organisor.Contact.Phone);
                        cmd.Parameters.AddWithValue("@address", organisor.Contact.Address.ToAddressLine());
                        cmd.Parameters.AddWithValue("@status", 1);
                        organisor.Id = (int)cmd.ExecuteScalar();
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex) { sqlTransaction.Rollback(); throw; }
                }
            }
            catch (Exception ex) { throw new CustomerRepositoryException("addOrganisor", ex); }
            return organisor;
        }

        public void DeleteOrganisor(int organisorId)
        {
            try
            {
                string updateSql1 = "UPDATE Organisor SET status = 0 WHERE id = @organisorId;";
                string updateSql2 = "UPDATE Activity SET status = 0 WHERE organisorId= @organisorId;";



                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.Transaction = conn.BeginTransaction();
                        cmd.CommandText = updateSql1;
                        cmd.CommandText += updateSql2;
                        cmd.Parameters.AddWithValue("@organisorId", organisorId);
                        cmd.ExecuteNonQuery();

                        cmd.Transaction.Commit();
                    }catch(Exception ex)
                    {
                        cmd.Transaction.Rollback();
                        throw new CustomerRepositoryException("DeleteOrganisor rolback", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new CustomerRepositoryException("DeleteOrganisor", ex);
            }
        }

        public Organisor GetOrganisorById(int id)
        {
            throw new NotImplementedException();
        }


        public void UpdateOrganisor(Organisor organisor)
        {

            try
            {
                string updateSql = "UPDATE Organisor SET name = @name, email = @email, phone = @phone, address = @address WHERE id = @id";



                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", organisor.Id);
                    cmd.Parameters.AddWithValue("@name", organisor.Name);
                    cmd.Parameters.AddWithValue("@email", organisor.Contact.Email);
                    cmd.Parameters.AddWithValue("@phone", organisor.Contact.Phone);
                    cmd.Parameters.AddWithValue("@address", organisor.Contact.Address.ToAddressLine());
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("UpdateOrganisor", ex);
            }
        }

    }
}

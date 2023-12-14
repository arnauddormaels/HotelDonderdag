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
    public class RegistrationRepository : IRegistrationRepository
    {
        private string connectionString;

        public RegistrationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int AddRegistration(Registration registration)
        {
            try
            {
                //TODO
                string sql = "INSERT INTO Registration(activityId) output INSERTED.ID VALUES(@activityId)";
                string sql2 = "INSERT INTO RegistrationDetails(registrationId, name,birthday,customerId) output VALUES(@registrationId, @name,@birthday,@customerId))";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction sqlTransaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = sqlTransaction;
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@registrationId", registration.Events);
                        cmd.Parameters.AddWithValue("@activityId", registration.Members);
                        cmd.Parameters.AddWithValue("@priceInfo", registration.PriceInfos);
                        int id = (int)cmd.ExecuteScalar();
                        registration.Id = id;

                        sqlTransaction.Commit();
                    }
                    catch (Exception ex) { sqlTransaction.Rollback(); throw; }
                }
            }
            catch (Exception ex) { throw new RegistrationRepositoryException("addRegistration", ex); }
            return registration.Id;
        }


        public IReadOnlyList<Registration> GetRegistrations()
        {
            try
            {
                List<Registration> registrations = new List<Registration>();

                string sql = "select id, event, member from Registration;";

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

                            registrations.Add(new Registration(id, reader["event"], reader["member"]));

                        }
                    }
                }
                return registrations;
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException("GetRegistrations", ex);
            }
        }

        public void DeleteRegistration(int id)
        {
            try
            {
                string updateSql = "DELETE FROM  Registration WHERE id = @id";

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
                throw new RegistrationRepositoryException("DeleteRegistration", ex);
            }
        }
    }
}

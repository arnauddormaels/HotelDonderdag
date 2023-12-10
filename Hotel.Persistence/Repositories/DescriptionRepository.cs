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
    public class DescriptionRepository : IDescriptionRepository
    {
        private string connectionString;

        public DescriptionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        IReadOnlyList<Description> IDescriptionRepository.GetDescriptions()
        {
            try
            {
                List<Description> descriptions = new List<Description>();

                string sql = "select id, activityName, duration, location, description from Description;";

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
                            
                             descriptions.Add(new Description(id, (string)reader["activityName"], (string)reader["location"], Convert.ToInt32(reader["duration"]), (string)reader["description"]));

                        }
                    }
                }
                return descriptions;
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("GetDescriptions", ex);
            }
        }
        public int AddDescription(Description description)
        {
            try
            {
                string sql = "INSERT INTO Description(activityName,duration,location,description) output INSERTED.ID VALUES(@activityName,@duration,@location,@description)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction sqlTransaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = sqlTransaction;
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@activityName", description.Name);
                        cmd.Parameters.AddWithValue("@duration", (int)description.Duration);
                        cmd.Parameters.AddWithValue("@location", description.Location);
                        cmd.Parameters.AddWithValue("@description", description.DescriptionText);
                        int id = (int)cmd.ExecuteScalar();
                        description.Id = id;
                       
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex) { sqlTransaction.Rollback(); throw; }
                }
            }
            catch (Exception ex) { throw new DescriptionRepositoryException("addDescription", ex); }
            return description.Id;
        }

        public void DeleteDescription(int id)
        {
            try
            {
                string updateSql = "DELETE FROM  Description WHERE id = @id";



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
                throw new DescriptionRepositoryException("DeleteDescription", ex);
            }
        }
    }
}

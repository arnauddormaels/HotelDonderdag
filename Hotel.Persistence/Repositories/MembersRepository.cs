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
    public class MembersRepository : IMembersRepository
    {
        private string connectionString = "@Data Source=LAPTOP-UMGHNHQ1\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True";
        public MembersRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
    /*
        public IReadOnlyList<Customer> GetMembers(string customerId)
        {
            try
            {
                List<Customer> customers = new List<Customer>();
                string sql = "select name, birthday from member where status = 1 and customerid = @customerid";
               
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    if (!string.IsNullOrWhiteSpace(customerId)) cmd.Parameters.AddWithValue("@customerid", $"%{customerId}%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);

                            
                            if (!reader.IsDBNull(reader.GetOrdinal("membername")))
                            {
                                Customer member = new Customer((string)reader["membername"], DateOnly.FromDateTime((DateTime)reader["birthday"]));
                                customers[id].AddMember(member);
                            }
                        }
                    }
                }
                return customers.Values.ToList();
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("getcustomer", ex);
            }
        }
        */
    }
}

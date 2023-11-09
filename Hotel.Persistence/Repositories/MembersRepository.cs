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
        private string connectionString; //= "@Data Source=LAPTOP-UMGHNHQ1\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True";
        public MembersRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Member> GetMembers(int customerId)
        {

            try
            {
                Customer customer = null;
                List<Member> members = new List<Member>();

                string sql = "select t1.id,t1.name customername,t1.email,t1.phone,t1.address,t2.name membername,t2.birthday\r\nfrom customer t1 \r\nleft join (select * from member where status=1) t2 \r\non t1.id=t2.customerId \r\nwhere t1.status=1 and customerId = @customerId;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@customerId", $"{customerId}");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ID"]);
                            if (customer == null)
                            {

                                customer = new Customer(id, (string)reader["customername"], new ContactInfo((string)reader["email"], (string)reader["phone"], new Address((string)reader["address"])));
                            }


                            if (!reader.IsDBNull(reader.GetOrdinal("membername")))
                            {
                                Member member = new Member((string)reader["membername"], DateOnly.FromDateTime((DateTime)reader["birthday"]));
                                members.Add(member);
                            }
                        }
                    }
                }
                return members;
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("GetMembers", ex);
            }
        }
        public void AddMember(int customerId, Member member)
        {
            string sql = "insert into member (name, birthday, customerid, status) values (@name, @birthday, @customerid, @status)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@name", member.Name);
                    cmd.Parameters.AddWithValue("@birthday", member.Birthday.ToDateTime(TimeOnly.MinValue));
                    cmd.Parameters.AddWithValue("@customerid", customerId);
                    cmd.Parameters.AddWithValue("@status", true);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("addmember?", ex);
            }
        }

        public void UpdateMember(int customerId, Member member)
        {
            try
            {
                //TODO Vraagje Voor Tom, waarom ken hij hier id wel en in de databank gebruiken we zelfs niet eens een id.
                string updateSql = "UPDATE Member SET name = @name, birthday = @birthday, customerId = @customerId, status = @status WHERE id = @id";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@name", member.Name);
                    cmd.Parameters.AddWithValue("@birthday", member.Birthday.ToDateTime(TimeOnly.MinValue));
                    cmd.Parameters.AddWithValue("@customerid", customerId);
                    cmd.Parameters.AddWithValue("@status", true);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("Update member", ex);
            }
        }

        public void DeleteMember(int customerId, Member member)
        {
            try
            {
                string updateSql = "UPDATE Member SET status = 0 WHERE name = @name AND birthday = @birthday";



                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", member.Name);
                    cmd.Parameters.AddWithValue("@birthday", member.Birthday.ToDateTime(TimeOnly.MinValue));
                    cmd.Parameters.AddWithValue("@status", false);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("Error bij de repository delete Member ", ex);
            }
        }
    }
}

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
        private string connectionString;
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

                string sql = "select c.id,c.name customername,c.email,c.phone,c.address,m.name membername,m.birthday\r\nfrom customer c \r\nleft join (select * from member where status=1) m \r\non c.id=m.customerId \r\nwhere c.status=1 and customerId = @customerId;";

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
                throw new MemberRepositoryException("GetMembers", ex);
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
                throw new MemberRepositoryException("addmember?", ex);
            }
        }

        public void UpdateMember(int customerId, Member oldMember, Member newMember)
        {
            try
            {
                //TODO Vraagje Voor Tom, waarom ken hij hier id wel en in de databank gebruiken we zelfs niet eens een id.
                string updateSql = "UPDATE Member SET name = @newName, birthday = @newBirthday, customerId = @customerId, status = @status WHERE customerId = @customerId AND name = @oldName AND birthday = @oldBirthday";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@newName", newMember.Name);
                    cmd.Parameters.AddWithValue("@newBirthday", newMember.Birthday.ToDateTime(TimeOnly.MinValue));

                    cmd.Parameters.AddWithValue("@oldName", oldMember.Name);
                    cmd.Parameters.AddWithValue("@oldBirthday", oldMember.Birthday.ToDateTime(TimeOnly.MinValue));

                    cmd.Parameters.AddWithValue("@customerid", customerId);
                    cmd.Parameters.AddWithValue("@status", true);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new MemberRepositoryException("Update member", ex);
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
                throw new MemberRepositoryException("Error bij de repository delete Member ", ex);
            }
        }
    }
}

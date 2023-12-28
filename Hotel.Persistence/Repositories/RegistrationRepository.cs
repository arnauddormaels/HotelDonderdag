using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private string connectionString;
        private EventRepository eventRepository;
        private MembersRepository membersRepository;

        public RegistrationRepository(string connectionString, EventRepository eventRepository, MembersRepository membersRepository)
        {
            this.eventRepository = eventRepository;
            this.connectionString = connectionString;
            this.membersRepository = membersRepository;
        }


        public void AddRegistration(int customerId, int eventId, List<int> memberIds)
        {
            try
            {
                int RegistrationId;
                
                string sql1 = "INSERT INTO Registration(activityId,customerId) output INSERTED.ID VALUES(@activityId,@customerId)";
                string sql2 = "INSERT INTO RegistrationDetails(registrationId, memberId) VALUES(@registrationId, @memberId)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction sqlTransaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = sqlTransaction;
                        cmd.CommandText = sql1;
                        cmd.Parameters.AddWithValue("@activityId", eventId);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        int id = (int)cmd.ExecuteScalar();
                        RegistrationId = id;

                        cmd.Parameters.AddWithValue("@registrationId", RegistrationId);
                        cmd.Parameters.Add("@memberid", System.Data.SqlDbType.Int);
                        foreach (int memberId in memberIds)
                        {
                            cmd.CommandText = sql2;
                            cmd.Parameters["@memberId"].Value = memberId;
                            cmd.ExecuteNonQuery();
                        }

                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        sqlTransaction.Rollback();
                        throw new RegistrationRepositoryException("Rollback in addRegistration", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException("addRegistration", ex);
            }
        }


        public IReadOnlyList<Registration> GetRegistrations(int customerId)
        {
            try
            {
                List<Registration> registrations = new List<Registration>();
                Registration registration = null;
                string sql = "SELECT rd.id, registrationId, memberId, r.activityId\r\nFROM RegistrationDetails rd\r\njoin Registration r on r.id = rd.registrationId\r\nWHERE customerId = @customerId;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int registrationDetailsId = (int)reader["id"];
                            int registrationId = (int)reader["registrationId"];

                            if (registration == null || registration.Id != registrationId)
                            {
                                int activityId = (int)reader["activityId"];
                                Event activity = eventRepository.GetEvent(activityId);
                                registration = new Registration(registrationId, activity);
                                registrations.Add(registration);
                            }

                            Member member = membersRepository.GetMemberWithoutFilterOnMemberStatus((int)reader["memberId"]);          //Maakt member aan
                            registration.Members.Add(registrationDetailsId, member);                                          //Voeg member Toe

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

        public void DeleteRegistration(int registrationId)
        {
            try
            {
                string sql1 = "DELETE FROM RegistrationDetails WHERE registrationId = @registrationId;";
                string sql2 = "DELETE FROM Registration WHERE id = @registrationId;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.Transaction = conn.BeginTransaction();
                        cmd.CommandText = sql1;
                        cmd.CommandText+= sql2;

                        cmd.Parameters.AddWithValue("@registrationId", registrationId);
                        cmd.ExecuteNonQuery();

                        cmd.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        cmd.Transaction.Rollback();
                        throw new RegistrationRepositoryException("Rollback in DeleteRegistration", ex);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new RegistrationRepositoryException("DeleteRegistration", ex);
            }
        }



    }
}

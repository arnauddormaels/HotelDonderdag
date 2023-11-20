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
    public class EventRepository : IEventRepository
    {
        private string connectionString;

        public EventRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Event> GetEventsByOrganisorId(int id)
        {
            try
            {

                string sql = "select a.id, activityName, fixture, nrOfPlaces, duration, location, description,\r\np.id as priceInfoId, adultPrice, childPrice, adultAge, discount\r\nfrom Activity a\r\nleft join PriceInfo p on a.PriceInfoId = p.id\r\nwhere organisorId = @id\r\n";

                List<Event> events = new List<Event>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            PriceInfo priceInfo = new PriceInfo(Convert.ToInt32(reader["priceInfoId"]), Convert.ToInt32(reader["adultPrice"]), Convert.ToInt32(reader["childPrice"]), Convert.ToInt32(reader["discount"]), Convert.ToInt32(reader["adultAge"]));

                            Description description = new Description((string)reader["activityName"], Convert.ToInt32(reader["duration"]), (string)reader["location"], (string)reader["description"]);

                            Event e = new Event(Convert.ToInt32(reader["Id"]), (string)reader["activityName"], (DateTime)reader["fixture"], Convert.ToInt32(reader["nrOfPlaces"]), priceInfo, description);
                            events.Add(e);

                        }


                        return events;
                    }
                }
            }

            catch (Exception ex)
            {

                throw new EventRepositoryException("GetEventsByOrganisorId", ex);
            }
        }
    }
}




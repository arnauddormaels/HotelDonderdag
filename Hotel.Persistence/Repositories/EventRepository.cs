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
    public class EventRepository : IEventRepository
    {
        private string connectionString;

        public EventRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Event> GetEventsByOrganisorId(int customerId)
        {
            try
            {

                string sql = "select a.id, fixture, nrOfPlaces, \r\ndescriptionId, activityName, duration, location, description,\r\np.id as priceInfoId,\r\nadultPrice, childPrice, adultAge, discount, a.status \r\nfrom Activity a \r\nleft join PriceInfo p on a.PriceInfoId = p.id\r\nleft join Description d on d.id = a.descriptionId\r\nwhere organisorId = @id;";

                List<Event> events = new List<Event>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", customerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            PriceInfo priceInfo = new PriceInfo(Convert.ToInt32(reader["priceInfoId"]), Convert.ToInt32(reader["adultPrice"]), Convert.ToInt32(reader["childPrice"]), Convert.ToInt32(reader["discount"]), Convert.ToInt32(reader["adultAge"]));

                            Description description = new Description(Convert.ToInt32(reader["descriptionId"]), (string)reader["activityName"], (string)reader["location"], Convert.ToInt32(reader["duration"]), (string)reader["description"]);

                            Event e = new Event(Convert.ToInt32(reader["Id"]), (DateTime)reader["fixture"], Convert.ToInt32(reader["nrOfPlaces"]), priceInfo, description, (Boolean)reader["status"]);
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
        public IReadOnlyList<Event> GetEvents() //Nog niet getest
        {
            try
            {

                string sql = "select a.id, fixture, nrOfPlaces, \r\ndescriptionId, activityName, duration, location, description,\r\np.id as priceInfoId,\r\nadultPrice, childPrice, adultAge, discount \r\nfrom Activity a \r\nleft join PriceInfo p on a.PriceInfoId = p.id\r\nleft join Description d on d.id = a.descriptionId where a.status = 1;"
                    ;

                List<Event> events = new List<Event>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            PriceInfo priceInfo = new PriceInfo(Convert.ToInt32(reader["priceInfoId"]), Convert.ToInt32(reader["adultPrice"]), Convert.ToInt32(reader["childPrice"]), Convert.ToInt32(reader["discount"]), Convert.ToInt32(reader["adultAge"]));

                            Description description = new Description(Convert.ToInt32(reader["descriptionId"]), (string)reader["activityName"], (string)reader["location"], Convert.ToInt32(reader["duration"]), (string)reader["description"]);

                            Event e = new Event(Convert.ToInt32(reader["Id"]), (DateTime)reader["fixture"], Convert.ToInt32(reader["nrOfPlaces"]), priceInfo, description);
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
        public Event GetEvent(int eventId)
        {
            try
            {

                string sql = "select a.id, fixture, nrOfPlaces, \r\ndescriptionId, activityName, duration, location, description,\r\np.id as priceInfoId,\r\nadultPrice, childPrice, adultAge, discount \r\nfrom Activity a \r\nleft join PriceInfo p on a.PriceInfoId = p.id\r\nleft join Description d on d.id = a.descriptionId\r\nwhere a.id = @id";

                Event @event = null;

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", eventId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            PriceInfo priceInfo = new PriceInfo(Convert.ToInt32(reader["priceInfoId"]), Convert.ToInt32(reader["adultPrice"]), Convert.ToInt32(reader["childPrice"]), Convert.ToInt32(reader["discount"]), Convert.ToInt32(reader["adultAge"]));

                            Description description = new Description(Convert.ToInt32(reader["descriptionId"]), (string)reader["activityName"], (string)reader["location"], Convert.ToInt32(reader["duration"]), (string)reader["description"]);

                            @event = new Event(Convert.ToInt32(reader["Id"]), (DateTime)reader["fixture"], Convert.ToInt32(reader["nrOfPlaces"]), priceInfo, description);

                        }


                        return @event;
                    }
                }
            }

            catch (Exception ex)
            {

                throw new EventRepositoryException("GetEventsByOrganisorId", ex);
            }
        }

        public int AddEvent(int organisorId, Event e)
        {

            string sql = "insert into Activity (fixture, nrOfPlaces, organisorId, priceInfoId, descriptionId, status) output INSERTED.ID values (@fixture, @nrOfPlaces, @organisorId, @priceInfoId, @descriptionId, @status)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@fixture", e.Fixture);
                    cmd.Parameters.AddWithValue("@nrOfPlaces", e.NrOfPlaces);
                    cmd.Parameters.AddWithValue("@organisorId", organisorId);
                    cmd.Parameters.AddWithValue("@status", 1);
                    cmd.Parameters.AddWithValue("@priceInfoId", e.PriceInfo.Id);
                    cmd.Parameters.AddWithValue("@descriptionId", e.Description.Id);

                    int eventId = (int)cmd.ExecuteScalar();
                    e.Id = eventId;
                }
            }
            catch (Exception ex)
            {
                throw new EventRepositoryException("addEvent", ex);
            }
            return e.Id;
        }

        public bool UpdateStatusEvent(Event @event)
        {
            string sql = "update Activity set status = @status where id = @id";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@status", @event.Status);
                    cmd.Parameters.AddWithValue("@id", @event.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new EventRepositoryException("UpdateStatusEvent", ex);
            }
            return @event.Status;
        }
    }
}




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
                string sql = "select o.id as organisorId, o.name,o.email,o.phone,o.address,e.id as activityId, e.activityName, e.description,\r\ne.fixture, e.nrOfPlaces, e.location, e.duration,\r\np.id as priceInfoId, p.adultPrice, p.childPrice, p.discount, p.adultAge\r\nfrom organisor o \r\nleft join (select * from Activity where status=1) e on o.id= organisorId \r\nleft join (select * from PriceInfo) p on e.PriceInfoId = p.id\r\nwhere o.status=1";
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
                            if (!reader.IsDBNull(reader.GetOrdinal("activityId")))
                            {
                                PriceInfo priceInfo = new PriceInfo(Convert.ToInt32(reader["priceInfoId"]), Convert.ToInt32(reader["adultPrice"]), Convert.ToInt32(reader["childPrice"]), Convert.ToInt32(reader["discount"]), Convert.ToInt32(reader["adultAge"]));

                                Description description = new Description((string)reader["activityName"], Convert.ToInt32(reader["duration"]),(string)reader["location"], (string)reader["description"]);

                                Event e = new Event(Convert.ToInt32(reader["activityId"]),(string)reader["activityName"], (DateTime)reader["fixture"],Convert.ToInt32(reader["nrOfPlaces"]),priceInfo, description);
                                organisors[id].AddEvent(e);
                            }
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
            throw new NotImplementedException();
        }

        public void DeleteOrganisor(int id)
        {
            throw new NotImplementedException();
        }

        public Organisor GetOrganisorById(int id)
        {
            throw new NotImplementedException();
        }


        public void UpdateOrganisor(Organisor organisor)
        {
            throw new NotImplementedException();
        }

    }
}

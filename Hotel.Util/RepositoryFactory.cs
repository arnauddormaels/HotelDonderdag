using Hotel.Domain.Interfaces;
using Hotel.Persistence.Repositories;
using System.Configuration;

namespace Hotel.Util
{
    public static class RepositoryFactory
    {
        public static ICustomerRepository CustomerRepository { get { return new CustomerRepository(ConfigurationManager.ConnectionStrings["HotelDonderdag"].ConnectionString); } }
        public static IMembersRepository MembersRepository { get { return new MembersRepository(ConfigurationManager.ConnectionStrings["HotelDonderdag"].ConnectionString); } }
        public static IOrganisorRepository OrganisorRepository { get { return new OrganisorRepository(ConfigurationManager.ConnectionStrings["HotelDonderdag"].ConnectionString); } }
        public static IEventRepository EventRepository { get { return new EventRepository(ConfigurationManager.ConnectionStrings["HotelDonderdag"].ConnectionString); } }
    }
}
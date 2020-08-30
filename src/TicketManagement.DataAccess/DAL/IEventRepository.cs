using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    public interface IEventRepository : IRepository<PublicEvent>
    {
        // start store prosedure
        void CreateEvent(PublicEvent e, double priseBySeat);

        void DeleteEvent(PublicEvent e);

        // should be in logic part
        // thats need to know to create event

        // IEnumerable<Venue>
        // GetAllAvailabeleVenues
        // (DateTime startEvent, DateTime endEvent)
        // IEnumerable<Venue> GetAllVenues()

        // IEnumerable<Area> GetAllLayoutByVenue(int id)

        // void SetPriseBySeat(double prise);  ?

        // void UpdateEvent();    IUnitOfWork ?
    }
}

using TicketManagement.DataAccess.Model;

namespace TicketManagement.DataAccess.DAL
{
    public interface ITMEventRepository : IRepository<TMEvent>
    {
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

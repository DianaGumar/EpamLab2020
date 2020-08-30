namespace TicketManagement.DataAccess.DAL
{
    // ensure all repositories will use same context
    public interface IUnitOfWork
    {
        IEventRepository Events { get; }

        // IVenueRepository Venues { get; } ?
        // save mehod nonexist, becase only for orm
    }
}

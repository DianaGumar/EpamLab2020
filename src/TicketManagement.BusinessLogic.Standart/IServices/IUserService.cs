using TicketManagement.BusinessLogic.Entities;

namespace TicketManagement.BusinessLogic.Standart.IServices
{
    public interface IUserService
    {
        void CreateTMUser(TMUser user);

        TMUser GetTMUserById(object id);

        int UpdateLastName(string userId, string lastName);

        decimal GetBalance(string userId);

        bool IsBaslanceEnough(string userId, decimal price);

        int TopUpBalance(string userId, decimal summ);

        int MakePurchase(string userId, decimal summ);
    }
}

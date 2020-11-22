using TicketManagement.DataAccess.DAL;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.BusinessLogic
{
    public interface IUserService
    {
        void CreateTMUser(TMUser user);

        TMUser GetTMUserById(object id);

        int UpdateLastName(string userId, string lastName);

        decimal GetBalance(string userId);

        int TopUpBalance(string userId, decimal summ);

        int BuyTicket(string userId, int tmeventSeatId);

        void GetPurchaseHistory(string userId);
    }

    public class UserService : IUserService
    {
        private readonly ITMUserRepository _tmuserRepository;
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;

        public UserService(ITMUserRepository tmuserRepository,
            IPurchaseHistoryRepository purchaseHistoryRepository)
        {
            _tmuserRepository = tmuserRepository;
            _purchaseHistoryRepository = purchaseHistoryRepository;
        }

        public int BuyTicket(string userId, int tmeventSeatId)
        {
            throw new System.NotImplementedException();
        }

        public decimal GetBalance(string userId)
        {
            TMUser user = _tmuserRepository.GetById(userId);

            return user.Balance;
        }

        public int TopUpBalance(string userId, decimal summ)
        {
            TMUser user = GetTMUserById(userId);

            user.Balance += summ;

            _tmuserRepository.Update(user);

            return 1;
        }

        public void GetPurchaseHistory(string userId)
        {
            throw new System.NotImplementedException();
        }

        public void CreateTMUser(TMUser user)
        {
            _tmuserRepository.Create(user);
        }

        public TMUser GetTMUserById(object id)
        {
            return _tmuserRepository.GetById(id);
        }

        public int UpdateLastName(string userId, string lastName)
        {
            TMUser user = _tmuserRepository.GetById(userId);
            user.UserLastName = lastName;
            _tmuserRepository.Update(user);

            return 1;
        }
    }
}

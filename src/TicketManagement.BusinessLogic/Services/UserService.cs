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

        bool IsBaslanceEnough(string userId, decimal price);

        int TopUpBalance(string userId, decimal summ);

        int MakePurchase(string userId, decimal summ);
    }

    public class UserService : IUserService
    {
        private readonly ITMUserRepository _tmuserRepository;

        public UserService(ITMUserRepository tmuserRepository)
        {
            _tmuserRepository = tmuserRepository;
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

        public bool IsBaslanceEnough(string userId, decimal price)
        {
            TMUser user = _tmuserRepository.GetById(userId);

            return user.Balance >= price;
        }

        public int MakePurchase(string userId, decimal summ)
        {
            TMUser user = GetTMUserById(userId);

            user.Balance -= summ;

            _tmuserRepository.Update(user);

            return 1;
        }
    }
}

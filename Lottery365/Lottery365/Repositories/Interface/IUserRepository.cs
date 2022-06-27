using Lottery365.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery365.Repositories.Interface
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();
        public User GetUserById(int userId);
        public Task<int> AddUser(User user);
        public Task<int> EditUser(User user);
        public Task DeleteUser(User user);
        public User GetUser(string EmailId, string password);
        public User GetUserByEmailId(string EmailId);
        public List<UserLotteryDetail> GetUserTicketDetails(int userId);
        public List<WheelInfo> GetWheelInfo();
        public IEnumerable<WheelDrawNumberDetail> GetWheelDrawNumbers(long wheelNumber);
        IEnumerable<int> GetTicketDrawNumbers(long ticketNumber);
        public Task SaveUserLotteryDetails(UserLotteryDetail userLotteryDetail);
        public Task SaveUserDrawNumbers(UserDrawNumberDetail userDrawNumber);
        public Task<int> SaveChangesAsync();
        public Task SaveWheelInfo(WheelInfo wheelInfo);
        public Task SaveWheelDrawNumberDetails(WheelDrawNumberDetail wheelNumberDetails);
        public long GetLastInsertedWheelNumber();
        public List<UserLotteryDetail> GetUserTicketsByWheelNumber(long wheelNumber);
        public IEnumerable<int> GetAdminDrawNumbersForWheel(long wheelNumber);
        public List<User> GetUsersByTicketNumber(List<long> ticketNumbers);
        public WheelInfo GetWheelInfoByWheelNumber(long wheelNumber);
        public Task CreateWheelInfo(WheelInfo wheelInfo);
        public WheelInfo GetActiveWheel();
        public bool CheckIfThereIsActiveLotteryForUser(int userId, long wheelNumber);
        public void EditUserLotteryDetails(UserLotteryDetail userLotteryDetail);
        public UserLotteryDetail GetUserLotteryDetail(int userId, long wheelNumber);
        public List<int> GetParticipatedUserIdsForWheelNumber(long wheelNumber);
        
    }
}

using Lottery365.Models;
using Lottery365.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery365.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private Lottery365Context _context;

        public UserRepository(Lottery365Context context)
        {
            _context = context;
        }

        public async Task<int> AddUser(User user)
        {
            var result = await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task DeleteUser(User user)
        {
            var result = _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<int> EditUser(User user)
        {
             _context.Update(user);
            var returnValue = await _context.SaveChangesAsync();
            return returnValue;
        }

        public void EditUserLotteryDetails(UserLotteryDetail userLotteryDetail)
        {
            _context.Update(userLotteryDetail);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Include(x => x.Role).ToList();
        }

        public User GetUser(string EmailId, string password)
        {
            return _context.Users.Include(x => x.Role).FirstOrDefault(x => x.EmailId == EmailId && x.Password == password);
        }

        public User GetUserByEmailId(string EmailId)
        {
            return _context.Users.FirstOrDefault(x => x.EmailId == EmailId);
        }
        public List<UserLotteryDetail> GetUserTicketDetails(int userId)
        {
            var details = _context.UserLotteryDetails.Where(x => x.UserId == userId);
            return details.ToList();
        }

        public List<UserLotteryDetail> GetUserTicketsByWheelNumber(long wheelNumber)
        {
            var userLotteryDetails = _context.UserLotteryDetails.Where(x => x.WheelNumber == wheelNumber);
            return userLotteryDetails.ToList();
        }

        public IEnumerable<int> GetTicketDrawNumbers(long ticketNumber)
        {
            return _context.UserDrawNumberDetails.Where(x => x.TicketNumber == ticketNumber).Select(c => c.Number);
        }

        public List<WheelInfo> GetWheelInfo()
        {
            var wheelInfo = _context.WheelInfos.ToList();

            return wheelInfo;
        }

        public long GetLastInsertedWheelNumber()
        {
            var result = _context.WheelInfos.OrderBy(x => x.Id).LastOrDefault();
            if (result == null)
                return 0;
            else
                return result.WheelNumber;
        }

        public IEnumerable<WheelDrawNumberDetail> GetWheelDrawNumbers(long wheelNumber)
        {
            return _context.WheelDrawNumberDetails.Where(x => x.WheelNumber == wheelNumber).AsEnumerable();
        }

        public async Task SaveUserLotteryDetails(UserLotteryDetail userLotteryDetail)
        {
            await _context.AddAsync(userLotteryDetail);

        }

        public async Task SaveUserDrawNumbers(UserDrawNumberDetail userDrawNumber)
        {
            await _context.AddAsync(userDrawNumber);

        }

        public Task SaveWheelInfo(WheelInfo wheelInfo)
        {
            _context.Update(wheelInfo);
            return Task.CompletedTask;
        }

        public async Task CreateWheelInfo(WheelInfo wheelInfo)
        {
            await _context.AddAsync(wheelInfo);
        }

        public async Task SaveWheelDrawNumberDetails(WheelDrawNumberDetail wheelNumberDetails)
        {
            await _context.AddAsync(wheelNumberDetails);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<int> GetAdminDrawNumbersForWheel(long wheelNumber)
        {
            return _context.WheelDrawNumberDetails.Where(x => x.WheelNumber == wheelNumber).Select(x => x.Number);
        }

        public List<User> GetUsersByTicketNumber(List<long> ticketNumbers)
        {
            var users = _context.UserLotteryDetails.Where(x => ticketNumbers.Contains(x.TicketNumber)).Include(x => x.User).Select(c => c.User);
            return users.ToList();
        }

        public WheelInfo GetWheelInfoByWheelNumber(long wheelNumber)
        {
            return _context.WheelInfos.FirstOrDefault(x => x.WheelNumber == wheelNumber);
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                var result = await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WheelInfo GetActiveWheel()
        {
            return _context.WheelInfos.FirstOrDefault(x => x.Active);
        }

        public bool CheckIfThereIsActiveLotteryForUser(int userId, long wheelNumber)
        {
            return _context.UserLotteryDetails.Any(x => x.UserId == userId && x.WheelNumber == wheelNumber);
        }

        public UserLotteryDetail GetUserLotteryDetail(int userId, long wheelNumber)
        {
            return _context.UserLotteryDetails.FirstOrDefault(x => x.UserId == userId && x.WheelNumber == wheelNumber);
        }

        public List<int> GetParticipatedUserIdsForWheelNumber(long wheelNumber)
        {
            return _context.UserLotteryDetails.Where(x => x.WheelNumber == wheelNumber).Select(x => x.UserId).ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}

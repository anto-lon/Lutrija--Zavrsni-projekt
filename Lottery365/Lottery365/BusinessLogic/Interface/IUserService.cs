using Lottery365.DTO;
using Lottery365.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery365.BusinessLogic.Interface
{
    public interface IUserService
    {
        public Task<int> AddUser(User user);

        public UserDto GetUser(string EmailId, string password);

        public List<UserLotteryDetailsDTO> GetUserTicketDetails(int userId);

        public List<WheelAndWinnerDetailsDTO> GetWheelAndWinnerDetails();

        public Task<int> AddUserLotteryDetails(UserLotteryDetailsDTO userlotteryDetails);

        public Task<int> SaveWheelDetails(WheelAndWinnerDetailsDTO wheelInfo);

        public Task<int> CreateWheelInfo(WheelInfoDTO wheelInfo);

        public List<UserDto> GetAllUserDetails();

        public long GetLastInsertedWheelNumber();

        public long GetWheelNumberForInsert();

        public long? GetActiveWheelNumber();

        public bool CheckIfThereIsActiveLotteryForUser(int userId, long wheelNumber);

        public User GetUserByEmailId(string EmailId);

        public User GetUserById(int userId);

        public Task<int> UpdateUser(User user);
    }
}

using AutoMapper;
using Lottery365.BusinessLogic.Interface;
using Lottery365.DTO;
using Lottery365.Models;
using Lottery365.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery365.BusinessLogic.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<int> AddUser(User user)
        {
            user.RoleId =2; //User 
            return await _userRepo.AddUser(user);
        }

        public UserDto GetUser(string emailId, string password)
        {
            var user = _userRepo.GetUser(emailId, password);
            var userDetails = _mapper.Map<UserDto>(user);
            return userDetails;
        }

        public List<UserLotteryDetailsDTO> GetUserTicketDetails(int userId)
        {
            var result = _userRepo.GetUserTicketDetails(userId);
            var userTicketDetails = new List<UserLotteryDetailsDTO>();
            foreach (var ticket in result)
            {
                var userLotteryDetails = _mapper.Map<UserLotteryDetailsDTO>(ticket);

                userLotteryDetails.DrawNumbers = string.Join(' ', _userRepo.GetTicketDrawNumbers(ticket.TicketNumber));
                userTicketDetails.Add(userLotteryDetails);
            }
            return userTicketDetails;
        }

        private Dictionary<long, IEnumerable<int>> GetUserTicketAndDrawNumbersForWheel(long wheelNumber)
        {
            var userLotteryDetailsForWheel = _userRepo.GetUserTicketsByWheelNumber(wheelNumber);
            var ticketnumbersfortheWheel = userLotteryDetailsForWheel.Select(x => x.TicketNumber);
            Dictionary<long, IEnumerable<int>> userLotteryNumbers = new Dictionary<long, IEnumerable<int>>();
            foreach (var ticket in ticketnumbersfortheWheel)
            {
                var userDrawNumbers = _userRepo.GetTicketDrawNumbers(ticket).OrderBy(x => x);
                userLotteryNumbers.Add(ticket, userDrawNumbers);
            }
            return userLotteryNumbers;
        }

        private string GetWinnersForWheel(long wheelNumber)
        {
            var adminDrawNumbers = _userRepo.GetAdminDrawNumbersForWheel(wheelNumber).OrderBy(x => x);
            var userticketsAndDrawNumbers = GetUserTicketAndDrawNumbersForWheel(wheelNumber);
            List<long> winningTickets = new List<long>();
            foreach (var userTicket in userticketsAndDrawNumbers.Values)
            {
                if (adminDrawNumbers.Count() != 0 && adminDrawNumbers.All(userTicket.Contains))
                {
                    var ticketNumber = userticketsAndDrawNumbers.FirstOrDefault(x => userTicket.All(x.Value.Contains)).Key;
                    winningTickets.Add(ticketNumber);
                }
            }
            var winningUsers = _userRepo.GetUsersByTicketNumber(winningTickets);
            return string.Join(',', winningUsers.Select(x => x.EmailId));
        }

        public List<WheelAndWinnerDetailsDTO> GetWheelAndWinnerDetails()
        {
            var wheelInfo = _userRepo.GetWheelInfo();
            var result = new List<WheelAndWinnerDetailsDTO>();
            foreach (var info in wheelInfo)
            {
                var drawNumbers = _userRepo.GetWheelDrawNumbers(info.WheelNumber).Select(x => x.Number);
                var wheelDetails = _mapper.Map<WheelAndWinnerDetailsDTO>(info);
                wheelDetails.DrawNumbers = string.Join(' ', drawNumbers);
                wheelDetails.Winners = GetWinnersForWheel(info.WheelNumber);
                result.Add(wheelDetails);
            }
            return result;
        }

        public long? GetActiveWheelNumber()
        {
            return _userRepo.GetActiveWheel()?.WheelNumber;
        }

        public async Task<int> AddUserLotteryDetails(UserLotteryDetailsDTO userlotteryDetails)
        {
            var lotteryDetails = _mapper.Map<UserLotteryDetail>(userlotteryDetails);
            var drawNumbers = userlotteryDetails.DrawNumbers.Split(' ');
            await _userRepo.SaveUserLotteryDetails(lotteryDetails);
            foreach (var number in drawNumbers)
            {
                UserDrawNumberDetail drawNumber = new UserDrawNumberDetail();
                drawNumber.Number = Convert.ToInt32(number);
                drawNumber.TicketNumber = lotteryDetails.TicketNumber;
                await _userRepo.SaveUserDrawNumbers(drawNumber);
            }
            var result = await _userRepo.SaveChangesAsync();
            return result;
        }

        public async Task<int> SaveWheelDetails(WheelAndWinnerDetailsDTO wheelInfo)
        {
            //  var wheelDetails = _mapper.Map<WheelInfo>(wheelInfo);
            var wheelDetails = _userRepo.GetWheelInfoByWheelNumber(wheelInfo.WheelNumber);
            wheelDetails.Active = false;
            var drawNumbers = wheelInfo.DrawNumbers.Split(' ');
            await _userRepo.SaveWheelInfo(wheelDetails);
            foreach (var drawNumber in drawNumbers)
            {
                WheelDrawNumberDetail detail = new WheelDrawNumberDetail();
                detail.Number = Convert.ToInt32(drawNumber);
                detail.WheelNumber = wheelInfo.WheelNumber;
                await _userRepo.SaveWheelDrawNumberDetails(detail);
            }
            #region save user status details
            var winners = GetWinnersForWheel(wheelInfo.WheelNumber);
            List<int> winningUserIds = new List<int>();
            foreach (var winner in winners.Split(','))
            {
                if (!string.IsNullOrEmpty(winner))
                {
                    var user = _userRepo.GetUserByEmailId(winner);
                    winningUserIds.Add(user.Id);
                    var lotteryDetail = _userRepo.GetUserLotteryDetail(user.Id, wheelInfo.WheelNumber);
                    if (lotteryDetail != null)
                    {
                        lotteryDetail.Status = "Winner";
                        _userRepo.EditUserLotteryDetails(lotteryDetail);
                    }
                }
            }
            var allParticipatedUsers = _userRepo.GetParticipatedUserIdsForWheelNumber(wheelInfo.WheelNumber);
            foreach (var userId in allParticipatedUsers)
            {
                if (!winningUserIds.Contains(userId))
                {
                    var lotteryDetail = _userRepo.GetUserLotteryDetail(userId, wheelInfo.WheelNumber);
                    if (lotteryDetail != null)
                    {
                        lotteryDetail.Status = "Lost";
                        _userRepo.EditUserLotteryDetails(lotteryDetail);
                    }
                   
                }
            }
            #endregion
            var result = await _userRepo.SaveChangesAsync();
            return result;
        }

        public List<UserDto> GetAllUserDetails()
        {
            var allUsers = _userRepo.GetAllUsers();
            return _mapper.Map<List<User>, List<UserDto>>(allUsers);
        }

        public long GetLastInsertedWheelNumber()
        {
            return _userRepo.GetLastInsertedWheelNumber();
        }

        public long GetWheelNumberForInsert()
        {
            var wheelNumber = GetLastInsertedWheelNumber();
            return wheelNumber + 1;
        }

        public async Task<int> CreateWheelInfo(WheelInfoDTO wheelInfodto)
        {
            var wheelInfo = _mapper.Map<WheelInfo>(wheelInfodto);
            await _userRepo.CreateWheelInfo(wheelInfo);
            return await _userRepo.SaveChangesAsync();
        }

        public bool CheckIfThereIsActiveLotteryForUser(int userId, long wheelNumber)
        {
            return _userRepo.CheckIfThereIsActiveLotteryForUser(userId, wheelNumber);
        }

        public User GetUserByEmailId(string EmailId)
        {
            return _userRepo.GetUserByEmailId(EmailId);
        }

        public User GetUserById(int userId)
        {
            return _userRepo.GetUserById(userId);
        }

        public async Task<int> UpdateUser(User user)
        {
            var result = await _userRepo.EditUser(user);
            return result;
        }
    }
}

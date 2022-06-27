using AutoMapper;
using Lottery365.BusinessLogic.Interface;
using Lottery365.DTO;
using Lottery365.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery365.Controllers
{
    [ApiController]

    public class AccountsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AccountsController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AccountsController(ILogger<AccountsController> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Api/Accounts/UserLogin")]
        public UserDto UserLogin(LoginDto login)
        {
            return _userService.GetUser(login.EmailId, login.Password);
        }

        [HttpPost]
        [Route("Api/Accounts/CreateUser")]
        public async Task<ResponseDto> CreateUser(UserDto userDetails)
        {
            var userExists = _userService.GetUserByEmailId(userDetails.EmailId);
            if (userExists != null)
            {
                return new ResponseDto
                {
                    Status = "Failed",
                    ErrorMessage = "User Already Exists"
                };
            }
            var user = _mapper.Map<User>(userDetails);
            var result = await _userService.AddUser(user);
            if (result > 0)
            {
                return new ResponseDto
                {
                    Status = "Success",
                    ErrorMessage = string.Empty
                };
            }
            return new ResponseDto
            {
                Status = "Failed",
                ErrorMessage = "Failed to create user"
            };
        }

        [HttpPut]
        [Route("Api/Accounts/UpdateUser")]
        public async Task<ResponseDto> UpdateUser(UserDto userDetails)
        {
            var user = _userService.GetUserById(Convert.ToInt32(userDetails.UserId));
            user.RoleId = userDetails.Role == "Admin" ?1 : 2;
            var result = await _userService.UpdateUser(user);
            if (result > 0)
            {
                return new ResponseDto
                {
                    Status = "Success",
                    ErrorMessage = string.Empty
                };
            }
            return new ResponseDto
            {
                Status = "Failed",
                ErrorMessage = "Failed to update user"
            };

        }

        [HttpGet]
        [Route("Api/Accounts/UserTicketDetails")]
        public List<UserLotteryDetailsDTO> UserTicketDetails(int userId)
        {
            return _userService.GetUserTicketDetails(userId);
        }

        [HttpGet]
        [Route("Api/Accounts/AllUsers")]
        public List<UserDto> GetUserDetails()
        {
            return _userService.GetAllUserDetails();
        }

        [HttpPost]
        [Route("Api/Accounts/AddUserLotteryDetails")]
        public async Task<ResponseDto> AddUserLotteryDetails(UserLotteryDetailsDTO userlotteryDetails)
        {
            var result = await _userService.AddUserLotteryDetails(userlotteryDetails);
            if (result > 0)
            {
                return new ResponseDto
                {
                    Status = "Success",
                    ErrorMessage = string.Empty
                };
            }

            return new ResponseDto
            {
                Status = "Failed",
                ErrorMessage = "Failed to create user"
            };
        }

        [HttpPost]
        [Route("Api/Accounts/AddWheelInfo")]
        public async Task<ResponseDto> AddWheelInfo(WheelAndWinnerDetailsDTO wheelInfo)
        {
            var result = await _userService.SaveWheelDetails(wheelInfo);
            if (result > 0)
            {
                return new ResponseDto
                {
                    Status = "Success",
                    ErrorMessage = string.Empty
                };
            }

            return new ResponseDto
            {
                Status = "Failed",
                ErrorMessage = "Failed to add admin draw number details"
            };
        }

        [HttpPost]
        [Route("Api/Accounts/CreateWheelInfo")]
        public async Task<ResponseDto> CreateWheelInfo(WheelInfoDTO wheelInfo)
        {
            var result = await _userService.CreateWheelInfo(wheelInfo);
            if (result > 0)
            {
                return new ResponseDto
                {
                    Status = "Success",
                    ErrorMessage = string.Empty
                };
            }

            return new ResponseDto
            {
                Status = "Failed",
                ErrorMessage = "Failed to add admin draw number details"
            };
        }

        [HttpGet]
        [Route("Api/Accounts/WheelDetails")]
        public List<WheelAndWinnerDetailsDTO> WheelDetails()
        {
            return _userService.GetWheelAndWinnerDetails();
        }

        [HttpGet]
        [Route("Api/Accounts/GetWheelNumberForInsert")]
        public long GetWheelNumberForInsert()
        {
            return _userService.GetWheelNumberForInsert();
        }

        [HttpGet]
        [Route("Api/Accounts/ActiveWheel")]
        public long? getActiveWheel()
        {
            var activeWheel = _userService.GetActiveWheelNumber();
            return activeWheel;
        }

        [HttpGet]
        [Route("Api/Accounts/AdminDrawNumbers")]
        public IEnumerable<int> AdminDrawNumbers()
        {
            return DrawNumbers(6, 49);
        }

        [HttpGet]
        [Route("Api/Accounts/CheckIfUserHasLotteryNumber")]
        public bool CheckIfUserHasLotteryNumber(int userId, long wheelNumber)
        {
            return _userService.CheckIfThereIsActiveLotteryForUser(userId, wheelNumber);
        }

        private IEnumerable<int> DrawNumbers(int count, int MaxNumbers)
        {
            var r = new Random();
            var possibles = Enumerable.Range(1, MaxNumbers).ToList();
            for (int i = 0; i < count; i++)
            {
                var index = r.Next(i, MaxNumbers);
                yield return possibles[index];
                possibles[index] = possibles[i];
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RakSport.Application.Interfaces;
using RakSport.Application.Shared;
using RakSport.Domain.Interfaces.UnitOfWorkInterface;

namespace RakSport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public UsersController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }


        [HttpGet]
        [Route("Get-Current-User")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _userService.GetCurrentUserAsync(User);
            return Ok(new ResponseHandler("Account information: ", result));
        }


        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {

            return Ok(new ResponseHandler("You Logout successfully."));
        }
    }
}

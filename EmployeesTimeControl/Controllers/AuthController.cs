using EmployeesTimeControl.Models;
using EmployeesTimeControl.Repositories;
using EmployeesTimeControl.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesTimeControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService=new JwtService();
        private readonly ILoginInfoRepository _repository;

        public AuthController(ILoginInfoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public JsonResult Login(LoginInfo loginInfo)
        {
            string response = _repository.Login(loginInfo);
            return new JsonResult(response);
        }

            

    }
}

using EmployeesTimeControl.Models;
using EmployeesTimeControl.Services;
using Microsoft.AspNetCore.Mvc;


namespace EmployeesTimeControl.Repositories
{
    public class LoginInfoRepository : ILoginInfoRepository
    {
        private readonly JwtService _jwtService = new JwtService();
        private readonly IConfiguration _configuration;

        public LoginInfoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Login([FromBody] LoginInfo loginInfo)
        {
            if (loginInfo.UserName == "Admin" && loginInfo.Password == "Admin123")
            {
                var token = _jwtService.GenerateToken(loginInfo.UserName);
                return token;
            }
            else
            {
                return "Wrong username or password";
            }
        }

    }
}

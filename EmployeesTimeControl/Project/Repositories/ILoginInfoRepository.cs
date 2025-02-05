using EmployeesTimeControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesTimeControl.Repositories
{
    public interface ILoginInfoRepository
    {
        string Login([FromBody] LoginInfo loginInfo);
    }
}

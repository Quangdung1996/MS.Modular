using Microsoft.AspNetCore.Mvc;
using MS.Modular.AccountManagement.Application.Accounts;
using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.Controllers;
using System.Threading.Tasks;

namespace MS.Modular.Modules.AccountManagement
{
    [Route("v{version:apiVersion}/api/[controller]")]
    [ApiController]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AccountManagementController : BaseController

    {
        private readonly IAccountManagementModule _accountManagementModule;

        public AccountManagementController(IAccountManagementModule accountManagementModule)
        {
            _accountManagementModule = accountManagementModule;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateAccount createAccount)
        {
            var result = await _accountManagementModule.ExecuteCommandAsync(new CreateAccountCommand(createAccount));
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAccountAsync([FromBody] AccountSignIn accountSignIn)
        {
            var result = await _accountManagementModule.ExecuteCommandAsync(new LoginAccountCommand(accountSignIn));
            return Ok(result);
        }
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
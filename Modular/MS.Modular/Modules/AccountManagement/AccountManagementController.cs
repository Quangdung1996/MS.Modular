using Microsoft.AspNetCore.Mvc;
using MS.Modular.AccountManagement.Application.Accounts;
using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Accounts;
using System.Threading.Tasks;

namespace MS.Modular.Modules.AccountManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagementController : ControllerBase
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
}
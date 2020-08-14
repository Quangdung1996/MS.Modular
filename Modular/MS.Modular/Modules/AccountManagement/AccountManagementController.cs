using Microsoft.AspNetCore.Mvc;
using MS.Modular.AccountManagement.Application.Accounts;
using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Domain.AccountManagements;
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
        public async Task<IActionResult> RegisterUserAsync([FromBody] AccountDataTransformation accountDataTransformation)
        {
            var result = await _accountManagementModule.ExecuteCommandAsync(new CreateAccountCommand(accountDataTransformation));
            return Ok(result);
        }
    }
}
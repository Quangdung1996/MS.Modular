using MediatR;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.ViewModels;
using MS.Modular.BuildingBlocks.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Application.Accounts
{
    internal class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, ReturnResponse<RegisterAccountViewModel>>
    {
        private readonly IAccountManagementService _accountManagementService;
        internal LoginAccountCommandHandler(IAccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }
        public async Task<ReturnResponse<RegisterAccountViewModel>> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            return await _accountManagementService.LoginAsync(request.AccountLogin);
        }
    }
}
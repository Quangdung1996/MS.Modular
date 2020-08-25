using MediatR;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Application.Accounts
{
    internal class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, ReturnResponse<UpdateAccount>>
    {
        private readonly IAccountManagementService _accountManagementService;
        internal UpdateAccountCommandHandler(IAccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }
        public async Task<ReturnResponse<UpdateAccount>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            return default;
        }
    }
}
﻿using MediatR;
using MS.Modular.AccountManagement.Domain;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.ViewModels;
using MS.Modular.BuildingBlocks.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Application.Accounts
{
    internal class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ReturnResponse<RegisterAccountViewModel>>
    {
        private readonly IAccountManagementService _accountManagementService;

        internal CreateAccountCommandHandler(IAccountManagementService accountManagementService)
        {
            _accountManagementService = accountManagementService;
        }

        public  async Task<ReturnResponse<RegisterAccountViewModel>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            return await _accountManagementService.RegisterAsync(request.CreateAccount);
        }
    }
}
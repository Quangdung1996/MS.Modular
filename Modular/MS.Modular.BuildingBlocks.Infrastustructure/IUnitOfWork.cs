using System;
using System.Threading;
using System.Threading.Tasks;

namespace MS.Modular.BuildingBlocks.Infrastustructure
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null);
    }
}
using MS.Modular.AccountManagement.Application.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MS.Modular.AccountManagement.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase<>).Assembly;
        public static readonly Assembly ApplicationUpdate = typeof(InternalCommandUpdate<>).Assembly;
    }
}

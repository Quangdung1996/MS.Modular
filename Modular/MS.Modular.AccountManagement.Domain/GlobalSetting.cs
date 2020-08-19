using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Modular.AccountManagement.Domain
{
    public static class GlobalSetting
    {
        public static readonly JsonSerializerSettings JsonSetting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
        };
    }
}

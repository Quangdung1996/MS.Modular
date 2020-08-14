using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Modular.BuildingBlocks.Domain
{
    public class ReturnResponse<T>
    {
        public T Data { get; set; }
        public bool Successful { get; set; }
        public string Error { get; set; }
        public int StatusCode { get; set; }
    }

}

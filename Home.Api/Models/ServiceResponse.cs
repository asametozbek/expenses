using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Api.Models
{
    public class ServiceResponse
    {
        public int Result { get; set; }
        public bool IsSuccesful  { get; set; }
        public object  Data  { get; set; }
        public string Message { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.DTO
{
    public class DefaultResponce
    {
        public DefaultResponce(bool suc = true, string mess = null)
        {
            Success = suc;
            Message = mess;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.WebApi.Models
{
    public class Token
    {
        public int Id { get; set; }
        public Guid GuidToken { get; set; }
        public string ValueToken { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

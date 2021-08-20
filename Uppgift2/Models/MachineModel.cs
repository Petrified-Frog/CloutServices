using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift2.Models
{
    public class MachineModel
    {
        public Guid id { get; set; }
        public string description { get; set; }
        public bool connected { get; set; }
        public IEnumerable<string> data { get; set; }
    }
}

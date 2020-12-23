using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTServices.Models
{
    public class NIF
    {

        public class Root
        {
            public string result { get; set; }
            public bool nif_validation { get; set; }
            public bool is_nif { get; set; }
        }
    }
}

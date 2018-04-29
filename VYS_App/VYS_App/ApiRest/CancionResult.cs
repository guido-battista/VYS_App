using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VYS_App.ApiRest
{
    class CancionResult
    {
        public string _id { get; set; }
        public string idEvento { get; set; }
        public string titulo { get; set; }
        public int votos { get; set; }
        public string estado { get; set; }
        public int __v { get; set; }
    }
}

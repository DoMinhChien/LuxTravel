using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class JsonPagedDataResult
    {
        public int records { get; set; }
        public dynamic rows { get; set; }
        public int Total { get; set; }
    }
}

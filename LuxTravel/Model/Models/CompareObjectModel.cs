using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
  public  class CompareObjectModel
    {
        public int Id { get; set; }
        public int historyId { get; set; }
        public dynamic OldValue { get; set; }
        public dynamic NewValue { get; set; }
        public dynamic Field { get; set; }
    }
}

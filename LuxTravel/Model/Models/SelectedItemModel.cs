using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class SelectedItemModel
    {
        public dynamic Value { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Quantity { get; set; }
    }
}

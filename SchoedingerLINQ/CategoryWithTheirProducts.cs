using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoedingerLINQ
{
    public class CategoryWithTheirProducts
    {
        public string CategoryName { get; set; }
        public IEnumerable<Product> ProductInfos { get; set; }
    }
}

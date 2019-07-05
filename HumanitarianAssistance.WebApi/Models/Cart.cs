using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Models
{
    public class Cart
    {
    public IEnumerable<Product> ProductList { get; set; }
    public string Status { get; set; }
    }
}

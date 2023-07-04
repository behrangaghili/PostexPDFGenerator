using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postex.Receipt.Domain.Models
{
    public class CreateReceiptModel
    {
        public string TemplateName { get; set; }
        public Dictionary<string, string> Values { get; set; }
    }
}

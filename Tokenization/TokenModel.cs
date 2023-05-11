using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenization
{
    public class TokenModel
    {
        public int ID { get; set; }
        public string Context { get; set; }
        public string Mark { get; set; }
    }
    public class PunctuationModel
    {
        public string Marks { get; set; }
        public int index { get; set; } = -999;
    }
}

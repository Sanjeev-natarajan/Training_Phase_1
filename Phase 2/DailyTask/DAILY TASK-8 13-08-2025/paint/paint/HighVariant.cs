using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint
{
    public class HighVariant : IBrush
    {
        public string Proc { get; set; }
        public int Size { get; set; }
        public string Variant { get; set; }

        public HighVariant(string proc, int size, string variant)
        {
            Proc = proc;
            Size = size;
            Variant = variant;
        }

        public bool IsHighVariant()
        {
            return true;
        }
    }
}

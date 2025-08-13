using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint
{
    public class Brush : IBrush
    {
        public string Proc {  get; set; }
        public int Size { get; set; }
        public string Variant { get; set; }

        public Brush (string proc, int size, string variant)
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

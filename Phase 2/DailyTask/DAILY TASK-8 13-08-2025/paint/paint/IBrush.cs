using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint
{
    public interface IBrush
    {
        public string Proc { get; set; }
        public int Size { get; set; }
        public string Variant { get; set; }

        public bool IsHighVariant();
    }
}

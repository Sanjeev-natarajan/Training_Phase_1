using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint
{
    public class Painter
    {
        public string Name { get; set; }
        public IBrush brush { get; set; } 

        public Painter (string _name, IBrush _brush)
        {
            Name = _name;
            brush = _brush;
        }

        public void PaintJob()
        {
            Console.WriteLine($"Uses his brush {brush.Proc} made of {brush.Variant}  and its size is {brush.Size}" );
        }
    }
}

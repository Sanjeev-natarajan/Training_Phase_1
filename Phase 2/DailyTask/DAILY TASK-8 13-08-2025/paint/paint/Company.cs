using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint
{
    public  static class Company
    {
        public static IBrush Fetchbrush(bool v)
        {
            if (v)
            {
                return new Brush("Wall Brush", 8, "Asian Paints");
            }
            else
            {
                return new HighVariant("Sweep brush", 8, "Panasonic");
            }
        }
    }
}

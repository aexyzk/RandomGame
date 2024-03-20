using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace YACS2DGE.YACS2DGE
{
    public class Color
    {
        public int r {get; set;}
        public int g {get; set;}
        public int b {get; set;}

        public Color()
        {
            r = White().r;
            g = White().g;
            b = White().b;
        }

        public Color(int r, int g, int b){
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public static Color White()
        {
            return new Color(255,255,255);
        }

        public static Color Black()
        {
            return new Color(0,0,0);
        }

        public static Color Red()
        {
            return new Color(255,0,0);
        }

        public static Color Green()
        {
            return new Color(0,255,0);
        }

        public static Color Blue()
        {
            return new Color(0,0,255);
        }
    }
}
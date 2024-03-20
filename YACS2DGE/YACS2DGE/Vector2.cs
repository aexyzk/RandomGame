using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace YACS2DGE.YACS2DGE
{
    public class Vector2
    {
        public float x {get; set;}
        public float y {get; set;}

        public Vector2()
        {
            x = Zero().x;
            y = Zero().y;
        }

        public Vector2(float x, float y){
            this.x = x;
            this.y = y;
        }

        // Returns a Zeroed vector2
        public static Vector2 Zero()
        {
            return new Vector2(0,0);
        }
    }
}
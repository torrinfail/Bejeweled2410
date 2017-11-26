using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Bejeweled
{
    class Gem
    {
        static Color[] colors = new[] { Color.Red, Color.Blue, Color.Orange, Color.Green };
        public static Gem selectedGem;
        public Color Color { get; }
        public Rectangle Rect { get; }
        //public Vector2 Position { get; }
        public Gem(int color,Rectangle rect)
        {
            this.Color = colors[color];
            this.Rect = rect;
        }

        static void SwapGems(ref Gem gem1,ref Gem gem2)
        {
            Gem tempGem = gem1;
            gem1 = gem2;
            gem2 = tempGem;
        }
        
    }
}

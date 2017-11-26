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
        static Color[] colors = { Color.Red, Color.Blue, Color.Orange, Color.Green };
        public static Gem selectedGem;
		public Color Color { get; private set;}
        public Rectangle Rect { get; }
        //public Vector2 Position { get; }
        public Gem(int color,Rectangle rect)
        {
            this.Color = colors[color];
            this.Rect = rect;
        }

        public static void SwapGems(Gem gem1,Gem gem2)
        {
			//Gem tempGem = gem1;
			var tempColor = gem1.Color;
			gem1.Color = gem2.Color;
			gem2.Color = tempColor;
        }
        
    }
}

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
        static Random random = new Random();
        public static Gem selectedGem;
		public int Color { get; private set;}
        public Rectangle Rect { get; }
        public Gem(Rectangle rect)
        {
            Color = random.Next(7);
            Rect = rect;
        }

        public void SetNewColor()
        {
            Color = random.Next(7);
        }
        public Gem(int color,Rectangle rect)
        {
            this.Color = color;
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

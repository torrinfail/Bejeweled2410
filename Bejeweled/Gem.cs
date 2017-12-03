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
        int _color;
		public int Color {
            get
            {
                return _color;
            }
            set
            {
                if (value > -1 && value < 8)
                {
                    _color = value;
                }
                else
                    throw new InvalidOperationException("Needs to be less than 8 or greater than -1");
            }
        }
        public Rectangle Rect { get; }
        public Gem(Rectangle rect)
        {
            Color = random.Next(7);
            Rect = rect;
        }

        public int SetNewColor(int excludedColor)
        {
            var newColor = random.Next(7);
            var oldColor = Color;
            
            if(newColor.Equals(oldColor) || newColor.Equals(excludedColor))
            {
                return SetNewColor(excludedColor);
            }
            Color = newColor;
            return newColor;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Bejeweled
{
    /// <summary>
    /// Author: Aidan Hubert
    /// represents the an individual gem on the board
    /// </summary>
    class Gem
    {
        static Random random = new Random();
        /// <summary>
        /// the gem currently selected by the player
        /// </summary>
        public static Gem selectedGem;
        int _color;
        /// <summary>
        /// the color of the gem. this determines the appearance, and what gems this gem will mathc with.
        /// </summary>
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
        bool _recentlyMatched;

        /// <summary>
        /// returns true if this gem was recently matched with another gem
        /// </summary>
        public bool RecentlyMatched
        {
            get { return _recentlyMatched; }
            set
            {
                if(value)
                {
                    Timer.NewTimer(1d, () => RecentlyMatched = false);
                }
                _recentlyMatched = value;
            }
        }
        //public double TimeSinceMatch { get; set; }
        /// <summary>
        /// stores the location and position of this gem on the screen
        /// </summary>
        public Rectangle Rect { get; }
        /// <summary>
        /// contructor of a gem takes a rectangle argument to place it on the screen where desired
        /// </summary>
        public Gem(Rectangle rect)
        {
            Color = random.Next(7);
            Rect = rect;
        }
        /// <summary>
        /// selects a random new color for the gem
        /// </summary>
        /// <param name="excludedColor">
        /// excludes a specific color to for preventing unwanted matches
        /// </param>
        /// <returns></returns>
        public int SetNewColor(int excludedColor)
        {
            var newColor = random.Next(7);
            var oldColor = Color;
            
            if(newColor.Equals(oldColor) || newColor.Equals(excludedColor))
            {
                return SetNewColor(excludedColor);
            }
            Color = newColor;
            RecentlyMatched = true;
            return newColor;
        }
        /// <summary>
        /// swaps the color of two gems
        /// </summary>
        public static void SwapGems(Gem gem1,Gem gem2)
        {
			//Gem tempGem = gem1;
			var tempColor = gem1.Color;
			gem1.Color = gem2.Color;
			gem2.Color = tempColor;
        }
        
    }
}

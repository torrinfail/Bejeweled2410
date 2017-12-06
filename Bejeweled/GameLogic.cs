using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bejeweled
{
    /// <summary>
    /// Author: Aidan Hubert
    /// Author: Chloe Bleak
    /// this class handles the main logic for finding matches and keeping score for the game
    /// </summary>
    class GameLogic
    {
        static Random random = new Random();
        static GameLogic _instance;
        /// <summary>
        /// Returns the singleton instance of this class as there can only be one
        /// </summary>
        public static GameLogic Instance
        {
            get
            {
                return _instance;
            }
        }
        /// <summary>
        /// The players score
        /// </summary>
        public int Score { get; private set; }
        Gem[,] _gems = new Gem[8, 8];
        /// <summary>
        /// represents all the gems on the board
        /// </summary>
        public Gem[,] Gems
        {
            get
            {
                return _gems;
            }
        }
        /// <summary>
        /// the size of elements on the screen
        /// </summary>
        public int Size { get; set; }

        static GameLogic()
        {
            _instance = new GameLogic();
        }

        GameLogic() { } //prevents instantiation

        /// <summary>
        /// Initializes all fields
        /// </summary>
        public void Initialze()
        {
            for (int i = 0; i < Gems.GetLength(0); i++)
                for (int j = 0; j < Gems.GetLength(1); j++)
                {
                    _gems[i, j] = new Gem(new Rectangle(j * Size, i * Size, Size, Size));
                }
            CheckWin(_gems);
            Score = 0;
        }
        /// <summary>
        /// Checks all gems for matches recursively until all matches have been found
        /// </summary>
        public int CheckWin(Gem[,] gem)
        {
            int matchesFound = 0;
            var dropGems = new List<Gem>();
            int score3 = 3;
            int score4 = 4;
            for (int i = 7; i >= 0; i--) //rows
            {
                for (int j = 7; j >= 0; j--) //columns
                {

                    int gemAt = gem[i, j].Color;

                    if (i - 2 >= 0) //make sure that we won't get an array out of bounds exception going up / vertical
                    {
                        if (gem[i - 1, j].Color == gemAt && gem[i - 2, j].Color == gemAt)
                        {
                            if (i - 3 >= 0 && gem[i - 3, j].Color == gemAt)
                            {
                                Score += score4; //or add 5 for a bonus point for four in a row
                                _gems[i - 3, j].SetNewColor(8);
                                _gems[i-3, j].RecentlyMatched = true;
                            }
                            //if these are all the same player then updates score 3 points (one for each gem)
                            _gems[i, j].SetNewColor(_gems[i - 1, j].SetNewColor(_gems[i - 2, j].SetNewColor(8)));
                            _gems[i, j].RecentlyMatched = true;
                            _gems[i-1, j].RecentlyMatched = true;
                            _gems[i-2, j].RecentlyMatched = true;
                            matchesFound++;
                          
                            Score += score3;
                        }
                    }
                    if (j - 2 >= 0) //going horizontal
                    {
                        if (gem[i, j - 1].Color == gemAt && gem[i, j - 2].Color == gemAt)
                        {
                            if (j - 3 >= 0 && gem[i, j - 3].Color == gemAt)
                            {
                                _gems[i, j - 3].SetNewColor(8);
                                _gems[i, j-3].RecentlyMatched = true;
                                Score += score4;
                            }
                            //if these are all the same player then win

                           
                            _gems[i, j].SetNewColor(_gems[i, j - 1].SetNewColor(_gems[i, j - 2].SetNewColor(8)));
                            _gems[i, j].RecentlyMatched = true;
                            _gems[i, j-2].RecentlyMatched = true;
                            _gems[i, j-1].RecentlyMatched = true;
                            matchesFound++;
                            
                            Score += score3;
                        }
                    }
                }
            }
            if (matchesFound > 0)
                return CheckWin(gem);
            Console.WriteLine($"Score: {Score}");

            return matchesFound;
        }
    }
}


using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bejeweled
{
    class MatchCheck
    {
        public int score = 0;

        

        public void CheckWin(Gem[,] gem)
        {
            int score3 = 3;
            int score4 = 4;
            for (int i = 7; i >= 0; i--) //rows
            {
                for (int j = 7; j >= 0; j--) //columns
                {

                    int gemAt = gem[i, j].Color;

                    if (i - 3 >= 0) //make sure that we won't get an array out of bounds exception going up / vertical
                    {
                        if (gem[i - 1, j].Color == gemAt && gem[i - 2, j].Color == gemAt)
                        {
                            if (gem[i - 3, j].Color == gemAt)
                            {
                                score += score4; //or add 5 for a bonus point for four in a row
                            }
                            //if these are all the same player then updates score 3 points (one for each gem)
                            score += score3;
                        }

                        if (j - 3 >= 0) //going horizontal
                        {
                            if (gem[i, j - 1].Color == gemAt && gem[i, j - 2].Color == gemAt)
                            {
                                if (gem[i, j - 3].Color == gemAt)
                                {
                                    score += score4;
                                }
                                //if these are all the same player then win
                                score += score3;
                            }
                        }
                    }
                }
            }
        }
    }
}

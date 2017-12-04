using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bejeweled
{
    class GameLogic
    {
        static Random random = new Random();
        static GameLogic _instance;
        public static GameLogic Instance
        {
            get
            {
                return _instance;
            }
        }
        public int Score { get; private set; }
        Gem[,] _gems = new Gem[8, 8];
        public Gem[,] Gems
        {
            get
            {
                return _gems;
            }
        }

        public int Size { get; set; }

        static GameLogic()
        {
            _instance = new GameLogic();
        }

        GameLogic()
        {

        }

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
                            }
                            //if these are all the same player then updates score 3 points (one for each gem)
                            _gems[i, j].SetNewColor(_gems[i - 1, j].SetNewColor(_gems[i - 2, j].SetNewColor(8)));
                            matchesFound++;
                            //dropGems.Add(_gems[i, j]);
                            //dropGems.Add(_gems[i - 1, j]);
                            //dropGems.Add(_gems[i - 2, j]);
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
                                Score += score4;
                            }
                            //if these are all the same player then win

                           
                            _gems[i, j].SetNewColor(_gems[i, j - 1].SetNewColor(_gems[i, j - 2].SetNewColor(8)));
                            matchesFound++;
                            //dropGems.Add(_gems[i, j]);
                            //dropGems.Add(_gems[i, j - 1]);
                            //dropGems.Add(_gems[i, j - 2]);
                            Score += score3;
                        }
                    }
                }
            }
            if (matchesFound > 0)
                return CheckWin(gem);
            Console.WriteLine($"Score: {Score}");

            FileIO fileio = new FileIO();
            fileio.WriteScoreToFile(Score);

            return matchesFound;
        }

        void DropOneDown(IList<Gem> dropGems)
        {
            int current = 0;
            for (int i = 7; i >= 0; i--) //rows
            {
                for (int j = 7; j >= 0; j--) //columns
                {
                    if (Gems[i, j].Equals(dropGems[current]))
                    {
                        try
                        {
                            Gems[i, j].Color = Gems[i - 1, j].Color;
                            Gems[i - 1, j].SetNewColor(2);

                        }
                        catch (IndexOutOfRangeException)
                        {
                            Gems[i, j].SetNewColor(2);

                            i++;
                        }
                    }
                }
            }
        }
    }
}


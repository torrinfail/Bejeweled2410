using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bejeweled
{
    class FileIO
    {
        /// <summary>
        /// Method to write the current score to a text file. 
        /// </summary>
        /// <param name="currentScore">Score of the game from MatchCheck class</param>
        public void WriteScoreToFile(int currentScore)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("ScoreFile.txt"))
                {
                        writer.WriteLine($"{currentScore}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("A problem occured:");
                Console.WriteLine(ex.Message);
            }
        }
        
    }
}

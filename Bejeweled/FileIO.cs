using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bejeweled
{
    /// <summary>
    /// Author: Aidan Hubert
    /// Author: Chloe Bleak
    /// this class handles all file reading and writing for the game.
    /// </summary>
    static class FileIO
    {
        public static int HighScore { get; set; }
        /// <summary>
        /// Method to write the current score to a text file. 
        /// </summary>
        /// <param name="currentScore">Score of the game from MatchCheck class</param>
        public static void WriteScoreToFile(int currentScore)
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
        /// <summary>
        /// gets all the previous scores from the save file and returns them.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> GetScores()
        {
            IList < string >readScores = ReadScores();
            if (readScores.Count == 0)
            {
                return null;
            }
            var results = 
                from s in readScores
                orderby int.Parse(s) ascending
                select int.Parse(s);
            HighScore = results.Last();
            return results;
        }

        static IList<string> ReadScores()
        {
            List<string> read = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader("ScoreFile.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        read.Add( reader.ReadLine());
                    }
                    reader.Close();
                    reader.Dispose();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("A problem occured:");
                Console.WriteLine(ex.Message);
            }
            return read;
        }
        
    }
}

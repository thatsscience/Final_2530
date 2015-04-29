using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_2530
{
    class ScoreKeeper
    {
        StreamReader input;
        StreamWriter output;
        List<long> scores;

        public ScoreKeeper(string filepath)
        {
            input = new StreamReader(filepath);
            output = new StreamWriter(filepath);
            scores = new List<long>();

            ReadScores();

        }

        public void ReadScores()
        {
            string line;
            while ((line = input.ReadLine()) != null)
            {
               // string[] tokens = line.Split(',');
                //scores.Add(long.Parse(line));
                Console.WriteLine(line);
            }
            scores.Sort();
        }

        public void AddScore(long score)
        {
            scores.Add(score);
            scores.Sort();
            output.WriteLine(score);
        }

        public long[] GetScores()
        {
            long[] top3 = new long[3];
            for (int i = 0; i < 3; i++)
            {
                top3[i] = scores[i];
            }
            return top3;
        }
    }
}

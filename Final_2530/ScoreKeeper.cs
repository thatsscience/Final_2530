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
        List<int> scores;
        string filepath;

        public ScoreKeeper(string file)
        {
            scores = new List<int>();
            filepath = file;
            ReadScores();
        }

        public void ReadScores()
        {
            using (input = new StreamReader(filepath))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    string[] tokens = line.Split(',');
                    scores.Add(int.Parse(tokens[0]));
                }
            }
            scores.Sort((a, b) => -1 * a.CompareTo(b));
        }

        public void AddScore(int score)
        {
            scores.Add(score);
            scores.Sort((a, b) => -1 * a.CompareTo(b));
            using (output = new StreamWriter(filepath))
            {
                foreach (int l in scores)
                {
                    output.WriteLine(l);
                }
            }
        }

        public int[] GetScores()
        {
            int[] top3 = new int[3];
            int length = (scores.Count < 3) ? scores.Count : 3;
            for (int i = 0; i < length; i++)
            {
                top3[i] = scores[i];
            }
            return top3;
        }
    }
}

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
            //string line;
            //long score;
            //string[] namesAndScores = new String[3];
            //while ((line = input.ReadLine()) !=null)
            //{
            //    string[] tokens = line.Split(',');
            //    score = long.Parse(tokens[1]);
            //    scores.Add(tokens[0],score);
            //}

            //int count = 0
            //foreach (KeyValuePair<string,long> item in scores.OrderBy(key=> key.Value))
            //{ 
            //    if (count == 3) return namesAndScores;
            //    namesAndScores[count] = string.Format("Name: {0} Score: {1}",item.Key, item.Value);
            //}

            //return namesAndScores;

            string line;
            while ((line = input.ReadLine()) !=null)
            {
                string[] tokens = line.Split(',');
                scores.Add(long.Parse(tokens[0]));
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

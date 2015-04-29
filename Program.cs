using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Final_2530
{
    class Program
    {

        private Timer timer = new Timer(100);
        long elapsedTimeTotal = 0;
        long elapsedTimeTemporary = 0;
        long lastTick = 0;
        Random rand = new Random();

        int score = 0;
        int seconds = -1;
        int secondsChecked = -1;
        int numberOfArms;

        Arm[] arms;


        Program(int numbArms)
        {
            this.numberOfArms = numbArms;
            arms = new Arm[numberOfArms];

            for (int i = 0; i < numberOfArms; i++)
            {
                //arms[i] = new Arm(this, i);
            }

            SetUpTimer();
            Console.Write("\n{0,45}\n\n", "Wack-A-Monster");
            Console.WriteLine("{0,5}", "   How to play: It's time to go to sleep, but there's a monster under your \n   bed!"
                                    + " If you want survive the night, you will have to"
                                    + " slap it's slimy, \n   scaly, arms that reach out from below before they maul you. Just enter the"
                                    + " \n   number corresponding to the reaching arm and press enter. You will \n   only"
                                    + " have a couple of seconds after an arm starts reaching to smack it away!\n   And be warned,"
                                    + " monsters get more tenacious as dawn approaches. See how \n   many points you can earn"
                                    + " before sunup!\n");
            Console.WriteLine("\n{0,45}\n\n", "Press enter to start...");
            Console.ReadLine();
            timer.Start();
            RunGame();
        }

        private void RunGame()
        {
            while (true)
            {
                string line = Console.ReadLine();
                int arm;
                try
                {
                    arm = int.Parse(line) - 1;
                    // if (arm < numberOfArms && arm >= 0) ClickArm(arm);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input");
                }

            }
        }


        private void SetUpTimer()
        {
            timer.Elapsed += timer_Tick;
            lastTick = DateTime.Now.Ticks / 10000;
        }

        void timer_Tick(object sender, ElapsedEventArgs e)
        {
            long currentTime = DateTime.Now.Ticks / 10000;
            long elapsedTime = currentTime - lastTick;
            lastTick = currentTime;
            elapsedTimeTotal += elapsedTime;
            elapsedTimeTemporary += elapsedTime;

            foreach (Arm a in arms) a.Tick(elapsedTime);

            //if (elapsedTimeTemporary > Interval())
            //{
            //    int randomArm = rand.Next(0, numberOfArms);
            //    arms[randomArm].Reach(randomArm);
            //    elapsedTimeTemporary = 0;
            ////}

            if (elapsedTimeTotal / 1000 > seconds)
            {
                seconds++;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Score: {0}", score);
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 0 && secondsChecked != 0)
            {
                secondsChecked = 1;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("8 PM");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 15 && secondsChecked != 15)
            {
                secondsChecked = 15;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("9 PM");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 30 && secondsChecked != 30)
            {
                secondsChecked = 30;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("10 PM");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 45 && secondsChecked != 45)
            {
                secondsChecked = 45;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("11 PM");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 60 && secondsChecked != 60)
            {
                secondsChecked = 60;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Midnight");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 75 && secondsChecked != 75)
            {
                secondsChecked = 75;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("1 AM");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 90 && secondsChecked != 90)
            {
                secondsChecked = 90;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("2 AM");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 105 && secondsChecked != 105)
            {
                secondsChecked = 105;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("3 AM");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 120 && secondsChecked != 120)
            {
                secondsChecked = 120;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("4 AM");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 135 && secondsChecked != 135)
            {
                secondsChecked = 135;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("5 AM");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 150 && secondsChecked != 150)
            {
                secondsChecked = 150;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("6 AM");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 165 && secondsChecked != 165)
            {
                secondsChecked = 165;
                Console.CursorLeft = 20;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Dawn");
                Console.ForegroundColor = ConsoleColor.Blue;
                timer.Stop();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n{0,30}", (score > 285000) ? "The sun is up! You survived the night!" : "You did not survive the night. The monster ate you for breakfast.");

                Console.WriteLine("\n{0,30}{1}", "Final Score: ", score);
                Console.ForegroundColor = ConsoleColor.White;

            }
        }

        //private void ClickArm(int arm)
        //{
        //    arms[arm].Click();
        //}

        private long Interval()
        {
            if (elapsedTimeTotal < 10000) return (rand.Next(4500, 10000) / 5) * numberOfArms;
            if (elapsedTimeTotal < 20000) return (rand.Next(3000, 8000) / 5) * numberOfArms;
            if (elapsedTimeTotal < 30000) return (rand.Next(1000, 6000) / 5) * numberOfArms;
            if (elapsedTimeTotal < 40000) return (rand.Next(0, 4000) / 5) * numberOfArms;
            if (elapsedTimeTotal < 50000) return (rand.Next(0, 3000) / 5) * numberOfArms;
            return rand.Next(0, 2000); ;
        }

        public void AddToScore(int value)
        {
            score += value;
            //Console.CursorLeft = 20;
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine(value);
            //Console.ForegroundColor = ConsoleColor.White;
        }

        public void SubtractFromScore(int value)
        {
            score -= value;
            //Console.CursorLeft = 20;
            //Console.ForegroundColor = ConsoleColor.Magenta;
            //Console.WriteLine(value);
            //Console.ForegroundColor = ConsoleColor.White;
        }


    }
}

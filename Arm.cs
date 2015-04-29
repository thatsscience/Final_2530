using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace Final_2530
{
    class Arm
    {
        bool reaching = false;
        bool retreating = false;
        long elapsedTime = 0;
        int reaches = 0;
        int intervalGap = 0;
        int arm;
        Random rand = new Random();
        MainWindow program;
        Button button;

        public Arm(MainWindow p, int armNumber, Button b)
        {
            arm = armNumber;
            program = p;
            this.button = b;
            intervalGap = Interval();
        }

        public void Tick(long elapsed)
        {
            if (reaching || retreating) elapsedTime += elapsed;
            if (reaching && elapsedTime > 2000)
            {
                int penalty = intervalGap * rand.Next(1, ((reaches > 1) ? reaches : 2) / 2);
                // program.SubtractFromScore(penalty);
                //reaching = false;
                //elapsedTime = 0;
                //retreating = true;
                Click();
            }

            if (retreating && elapsedTime > intervalGap)
            {
                elapsedTime = 0;
                retreating = false;
                intervalGap = Interval();
            }

        }

        private int Interval()
        {
            if (reaches < 5) return rand.Next(5000, 8000);
            if (reaches < 10) return rand.Next(3000, 5000);
            if (reaches < 15) return rand.Next(1000, 3000);
            return rand.Next(0, 1000); ;
        }

        public void Reach()
        {
            if (reaching || retreating) return;
            reaching = true;
            reaches++;
            Application.Current.Dispatcher.Invoke(() =>
            {
                button.Visibility = Visibility.Visible;
                button.IsEnabled = true;
            });

            Console.WriteLine("test");
        }

        public bool Click()
        {

            if (reaching)
            {
                reaching = false;
                retreating = true;
                elapsedTime = 0;
                intervalGap = Interval();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    button.Visibility = Visibility.Hidden;
                    button.IsEnabled = false;
                });
                button.Visibility = Visibility.Hidden;
                return true;
            }
            return false;
        }

    }
}

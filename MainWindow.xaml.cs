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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer_arms = new DispatcherTimer();
        Arm[] arm = new Arm[4];
        Button[] buttons = new Button[4];
        private Timer timer = new Timer(100);
        long elapsedTimeTotal = 0;
        long elapsedTimeTemporary = 0;
        long lastTick = 0;
        Random rand = new Random();

        int score = 0;
        int seconds = -1;
        int secondsChecked = -1;
        public MainWindow()
        {
            Console.WriteLine("this sucks");
            InitializeComponent();
            init();
            timer.Start();
            

        }

        BitmapImage sourceBitmap = new BitmapImage(new Uri("pack://application:,,,/Images/arm_shaded.png"));
        WriteableBitmap destinationBitmap = null;
        const int frameHeight = 80, totalFrames = 17;
        int currentFrame = 0, x = 0;

        



        #region working
        public void init()
        {
            SetBackground();

            timer_arms.Interval = new TimeSpan(0, 0, 0, 0, 180);
            timer_arms.Tick += new EventHandler(enterFrame);
            timer_arms.Start();

            SetUpTimer();
            buttons[0] = arm1;
            buttons[1] = arm2;
            buttons[2] = arm3;
            buttons[3] = arm4;

            for (int i = 0; i < 4; i++)
            {
                arm[i] = new Arm(this, i, buttons[i]);
            }

            arm1.IsEnabled = false;
            arm2.IsEnabled = false;
            arm3.IsEnabled = false;
            arm4.IsEnabled = false;

            arm1.Visibility = Visibility.Hidden;
            arm2.Visibility = Visibility.Hidden;
            arm3.Visibility = Visibility.Hidden;
            arm4.Visibility = Visibility.Hidden;
        }

        private void SetBackground()
        {
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                new BitmapImage(new Uri(@"pack://application:,,,/Images/background.png"));
            this.Background = myBrush;
        }

        private void arm_Click(object sender, RoutedEventArgs e)
        {
            if (sender == arm1)
               
                arm[0].Click();
            if (sender == arm2)
                arm[1].Click();
            if (sender == arm3)
                arm[2].Click();
            if (sender == arm4)
                arm[3].Click();

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
            System.Diagnostics.Debug.Write("reaching");
            foreach (Arm a in arm) a.Tick(elapsedTime);

            if (elapsedTimeTemporary > Interval())
            {
                int randomArm = rand.Next(0, 4);
                Console.WriteLine("reaching " + randomArm);
                
                if (randomArm == 0)                   
                    
                    arm[0].Reach();
                if (randomArm == 1)
                    arm[1].Reach();
                if (randomArm == 2)
                    arm[2].Reach();
                if (randomArm == 3)
                    arm[3].Reach();
                elapsedTimeTemporary = 0;
            }

            if (elapsedTimeTotal / 1000 > seconds)
            {
                seconds++;
            }

            if (seconds == 0 && secondsChecked != 0)
            {
                secondsChecked = 1;
            }

            if (seconds == 15 && secondsChecked != 15)
            {
                secondsChecked = 15;
            }

            if (seconds == 30 && secondsChecked != 30)
            {
                secondsChecked = 30;
            }

            if (seconds == 45 && secondsChecked != 45)
            {
                secondsChecked = 45;
            }

            if (seconds == 60 && secondsChecked != 60)
            {
                secondsChecked = 60;
            }

            if (seconds == 75 && secondsChecked != 75)
            {
                secondsChecked = 75;
            }

            if (seconds == 90 && secondsChecked != 90)
            {
                secondsChecked = 90;
            }

            if (seconds == 105 && secondsChecked != 105)
            {
                secondsChecked = 105;
            }

            if (seconds == 120 && secondsChecked != 120)
            {
                secondsChecked = 120;
            }

            if (seconds == 135 && secondsChecked != 135)
            {
                secondsChecked = 135;
            }

            if (seconds == 150 && secondsChecked != 150)
            {
                secondsChecked = 150;
            }

            if (seconds == 165 && secondsChecked != 165)
            {
                secondsChecked = 165;
                timer.Stop();

            }
        }

        private long Interval()
        {
            if (elapsedTimeTotal < 10000) return (rand.Next(4500, 10000) / 5) * 4;
            if (elapsedTimeTotal < 20000) return (rand.Next(3000, 8000) / 5) * 4;
            if (elapsedTimeTotal < 30000) return (rand.Next(1000, 6000) / 5) * 4;
            if (elapsedTimeTotal < 40000) return (rand.Next(0, 4000) / 5) * 4;
            if (elapsedTimeTotal < 50000) return (rand.Next(0, 3000) / 5) * 4;
            return rand.Next(0, 2000); ;
        }

        public void AddToScore(int value)
        {
            score += value;
        }

        public void SubtractFromScore(int value)
        {
            score -= value;
        }
        #endregion

        #region spriting code
        private void enterFrame(object sender, EventArgs e)
        {
            ArmHandler(Arm);
            ArmHandler(Arm2);
            ArmHandler(Arm3);
            ArmHandler(Arm4);
        }

        private void ArmHandler(Image Arm)
        {
            destinationBitmap = new WriteableBitmap((int)Arm.Width, (int)Arm.Height,
                96, 96, PixelFormats.Pbgra32, null);
            Arm.Source = destinationBitmap;

            // make a copy buffer
            int nRowBytes = sourceBitmap.PixelWidth * sourceBitmap.Format.BitsPerPixel / 8;
            byte[] buffer = new byte[nRowBytes * frameHeight];
            // copy through buffer 
            sourceBitmap.CopyPixels(new Int32Rect(0, currentFrame * frameHeight, sourceBitmap.PixelWidth, frameHeight), buffer, nRowBytes, 0);
            destinationBitmap.WritePixels(new Int32Rect(x, 0, sourceBitmap.PixelWidth, frameHeight), buffer, nRowBytes, 0);
            if (++currentFrame == totalFrames) currentFrame = 0;
            if (x > (int)Arm.Width - sourceBitmap.PixelWidth) x = 0;
        }
        #endregion
    }
}

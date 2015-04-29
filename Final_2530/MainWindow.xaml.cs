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
    public partial class MainWindow : Window
    {
        #region vari

        TimeSpan time;
        DispatcherTimer timer_sprites = new DispatcherTimer();
        DispatcherTimer countDown;        
        private Timer timer = new Timer(100);
        
        Random rand = new Random();
        BitmapImage sourceBitmap = new BitmapImage(new Uri("pack://application:,,,/Images/a1.png"));
        WriteableBitmap destinationBitmap = null;

        Arm[] arms = new Arm[4];
        Button[] btns = new Button[4];

        long elapsedTimeTotal = 0;
        long elapsedTimeTemporary = 0;
        long lastTick = 0;

        const int frameHeight = 80, totalFrames = 17;
        int currentFrame = 0, x = 0;

        int score = 0;
        int seconds = -1;
        int secondsChecked = -1;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            disableGameOverBtn();
            //ScoreKeeper tops = new ScoreKeeper(@"pack://application:,,,/scores.txt");
        }

        #region StartGame & GameOver

        private void StartGame()
        {
            disableWelcomeBtn();
            init();
            timer.Start();            
        }

        private void GameOver()
        {
            finalScore.Text = score.ToString();
            countDown.Stop();
            disableBtns();
            disableClock();
            disableScorebox();
            enableGameOverBtn();
            arms = new Arm[0];
        }
        
        #endregion

        // Initialize
        #region Init, SetBackground
        // this initializes everything
        public void init()
        {
            // Setting things up
            SetBackground();
            Application.Current.Dispatcher.Invoke(() =>clock.Text = "CLOCK: 00:00:00");
            Application.Current.Dispatcher.Invoke(() =>scorebox.Text = "SCORE: 0");

            //timerstuff for handling sprite animation
            timer_sprites.Interval = new TimeSpan(0, 0, 0, 0, 180);
            timer_sprites.Tick += new EventHandler(SpriteHandler);
            timer_sprites.Start();
            SetUpTimer();

            btns[0] = arm1;
            btns[1] = arm2;
            btns[2] = arm3;
            btns[3] = arm4;

            for (int i = 0; i < 4; i++)
            {
                arms[i] = new Arm(this, i, btns[i]);
            }

            CountdownClock();

            // starts things off unclickable until Reach() 
            disableBtns();
        }

        // sets background
        private void SetBackground()
        {
            ImageBrush game_bg = new ImageBrush();
            game_bg.ImageSource =
                new BitmapImage(new Uri(@"pack://application:,,,/Images/bg.png"));
            this.Background = game_bg;
        }

        #endregion

        // Score stuff
        #region AddToScore, SubtractFromScore

        public void AddToScore(int value)
        {
            score += value;
            Application.Current.Dispatcher.Invoke(() => scorebox.Text = "SCORE: " + score.ToString());
        }

        public void SubtractFromScore(int value)
        {
            score -= value;
            Application.Current.Dispatcher.Invoke(() => scorebox.Text = "SCORE: " + score.ToString());

        }

        #endregion

        // Time Management
        #region CountdownClock, SetUpTimer, timer_Tick, Interval

        // Countdown clock managed here
        private void CountdownClock()
        {
            // choose how long you want the application to run
            // Change the value in TimeSpan.FromSeconds to update how long the game lasts
            time = TimeSpan.FromSeconds(165);

            countDown = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                clock.Text = "CLOCK: " + time.ToString("c");
                if (time == TimeSpan.Zero)
                {
                    GameOver();
                }
                // count down by using -1
                time = time.Add(TimeSpan.FromSeconds(-1));
                if (time <= TimeSpan.FromSeconds(10) ) clock.Foreground = new SolidColorBrush(Colors.Red);

            }, Application.Current.Dispatcher);

            countDown.Start();
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

            if (elapsedTimeTemporary > Interval())
            {
                int randomArm = rand.Next(0, 4);

                if (randomArm == 0)
                    arms[0].Reach();
                if (randomArm == 1)
                    arms[1].Reach();
                if (randomArm == 2)
                    arms[2].Reach();
                if (randomArm == 3)
                    arms[3].Reach();

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

        #endregion

        // Used to enable and disable buttons as necessary
        #region enable/disable btns
        
        private void disableBtns()
        {
            arm1.IsEnabled = false;
            arm2.IsEnabled = false;
            arm3.IsEnabled = false;
            arm4.IsEnabled = false;

            arm1.Visibility = Visibility.Hidden;
            arm2.Visibility = Visibility.Hidden;
            arm3.Visibility = Visibility.Hidden;
            arm4.Visibility = Visibility.Hidden;
        }

        private void enableBtns()
        {
            arm1.IsEnabled = true;
            arm2.IsEnabled = true;
            arm3.IsEnabled = true;
            arm4.IsEnabled = true;

            arm1.Visibility = Visibility.Visible;
            arm2.Visibility = Visibility.Visible;
            arm3.Visibility = Visibility.Visible;
            arm4.Visibility = Visibility.Visible;
        }

        private void disableGameOverBtn()
        {
            game_over.IsEnabled = false;
            game_over.Visibility = Visibility.Hidden;
        }

        private void enableGameOverBtn()
        {
            game_over.IsEnabled = true;
            game_over.Visibility = Visibility.Visible;
        }

        private void disableWelcomeBtn()
        {
            menu_screen.IsEnabled = false;
            menu_screen.Visibility = Visibility.Hidden;
        }

        private void enableWelcomeBtn()
        {
            menu_screen.IsEnabled = true;
            menu_screen.Visibility = Visibility.Visible;
        }

        private void disableClock()
        {
            clock.IsEnabled = false;
            clock.Visibility = Visibility.Hidden;
        }

        private void disableScorebox()
        {
            scorebox.IsEnabled = false;
            scorebox.Visibility = Visibility.Hidden;
        }
        #endregion

        // all "OnClick" actions are here
        #region ClickHandlers

        private void arm_Click(object sender, RoutedEventArgs e)
        {
            if (sender == arm1)
                arms[0].Click();
            if (sender == arm2)
                arms[1].Click();
            if (sender == arm3)
                arms[2].Click();
            if (sender == arm4)
                arms[3].Click();
        }

        private void menu_screen_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void game_over_Click(object sender, RoutedEventArgs e)
        {
            //TODO:
            //Restart
        }
        #endregion

        // all Spriting Code
        #region spriting code

        private void SpriteHandler(object sender, EventArgs e)
        {
            // Passing in image from XAML to ArmHandler(Image Arm)
            Sprite(Arm);
            Sprite(Arm2);
            Sprite(Arm3);
            Sprite(Arm4);
        }

        private void Sprite(Image Arm)
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
 
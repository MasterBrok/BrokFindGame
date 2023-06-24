using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Schema;

namespace FindBrok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Score = 0;
        private List<Rectangle> rectangles = new List<Rectangle>();
        private static readonly double speed = 2.5;
        public MainWindow()
        {
            InitializeComponent();
            lblScore.Content = "Score is " + Score;
            for (int i = 0; i < Main.Children.Count; i++)
            {
                if (Main.Children[i].GetType() == typeof(Rectangle))
                {
                    rectangles.Add((Rectangle)Main.Children[i]);
                }
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (isOut())
            {
                if (rectangles[0].Width == 29)
                {
                    elBrok.Width = 5;
                    elBrok.Height = 5;
                }
                if (rectangles[0].Width == 35)
                {
                    elBrok.Width = 2.5;
                    elBrok.Height = 2.5;
                }
                if (rectangles[0].Width == 38)
                {
                    if (MessageBox.Show("Finish Game\n I Love You 💘 \nTelegram : @BrokDeveloper", "Finish", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        Application.Current.Shutdown();
                    }
                }
              
                if (e.Key == Key.W)
                {
                    Canvas.SetTop(elBrok, Canvas.GetTop(elBrok) - speed);

                }
                if (e.Key == Key.A)
                {
                    Canvas.SetLeft(elBrok, Canvas.GetLeft(elBrok) - speed);
                }
                if (e.Key == Key.D)
                {
                    Canvas.SetLeft(elBrok, Canvas.GetLeft(elBrok) + speed);
                }

                if (e.Key == Key.S)
                {
                    Canvas.SetTop(elBrok, Canvas.GetTop(elBrok) + speed);
                }

                IntersectsWith();
                if (Winner())
                {
                    Update();
                    Reset();
                }
            }
            else
            {
                Reset();
                MessageBox.Show("ععععععععععععع.تقلب ؟");
                Update();
                Score -= 3;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


        }
        /// <summary>
        /// Lose
        /// </summary>
        public void IntersectsWith()
        {
            int line1 = (int)(Canvas.GetLeft(elBrok) / 40);

            Rect brok = new Rect(Canvas.GetLeft(elBrok), Canvas.GetTop(elBrok), elBrok.Width, elBrok.Height);
            Rect line = new Rect(Canvas.GetLeft(rectangles[line1]), Canvas.GetTop(rectangles[line1]), rectangles[line1].Width, rectangles[line1].Height);

            if (line.IntersectsWith(brok))
            {
                MessageBox.Show("Oh.Lose");
                Reset();
            }
        }
        /// <summary>
        /// Winner
        /// </summary>
        public bool Winner()
        {
            Rect brok = new Rect(Canvas.GetLeft(elBrok), Canvas.GetTop(elBrok), elBrok.Width, elBrok.Height);
            Rect end = new Rect(Canvas.GetLeft(lblFinsish), Canvas.GetTop(lblFinsish), lblFinsish.Width, lblFinsish.Height);
            if (end.IntersectsWith(brok))
            {
                MessageBox.Show("Winner\nLevel Up");
                lblScore.Content = "Score is " + Score;
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Reset Game
        /// </summary>
        public void Reset()
        {
            Canvas.SetLeft(elBrok, 15);
            Canvas.SetTop(elBrok, 35);
        }

        /// <summary>
        /// is Out Object
        /// </summary>
        /// <returns></returns>
        public bool isOut()
        {
            if (Canvas.GetTop(elBrok) < 0 || Canvas.GetLeft(elBrok) < 0)
            {
                return false;
            }
            else
                return true;
        }

        public void Update()
        {
            foreach (var item in Main.Children)
            {
                if (item is Rectangle rect)
                {
                    rect.Width += 3;
                }
            }
        }

        private void WindowMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}

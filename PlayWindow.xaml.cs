using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Fly_To_Mars
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window
    {
        bool goUp, goDown, goLeft, goRight;
        bool imageUprightStatus=true;
        double playerSpeed = 0.2, defaultSpeed=0.03, cometSpeed=1, meteorSpeed=0.5;
        double roverFuel = 100, playerScore=0;
        List<QuesAns> quesAns;

        private void Img_Meteor01_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Img_Meteor01.Visibility = Visibility.Collapsed;
            playerScore = playerScore + 10;
        }

        private void Img_Meteor02_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Img_Meteor02.Visibility = Visibility.Collapsed;
            playerScore = playerScore + 10;
        }

        private void Img_Meteor03_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Img_Meteor03.Visibility = Visibility.Collapsed;
            playerScore = playerScore + 10;
        }

        private void Img_Meteor04_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Img_Meteor04.Visibility = Visibility.Collapsed;
            playerScore = playerScore + 10;
        }

        private void Img_Meteor05_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Img_Meteor05.Visibility = Visibility.Collapsed;
            playerScore = playerScore + 10;
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            quesAns = new List<QuesAns>
            {
                new QuesAns{questions="How much time does it take to reach Mars from Earth?", answer01="Though the answer depends on several factors, a one-way trip to Mars would take about nine months.", answer02="Surprisingly, if you wanted to make it a round-trip, it would take about 21 months",answer03="Why 21 months? ...Because you will need to wait about three months on Mars to make sure Earth and Mars are in a suitable location to make the trip back home."},
                new QuesAns{questions="How far is Mars from Earth?", answer01="The average distance between Earth and Mars is 225 million km.", answer02="When the two planets are closest, they are 54.6 million kilometers apart.", answer03="The two planets are farthest apart when they are both at their farthest from the sun, at a distance of 401 million km apart."},
                new QuesAns{questions="Why is Mars called the Red Planet?",answer01="No, its not because the planet is very angry.",answer02="A lot of rocks on Mars are full of iron, and when they're exposed to oxygen, they 'oxidize' and turn reddish", answer03="When rusty dust from those rocks gets kicked up in the atmosphere, it makes the martian sky look red."},
                new QuesAns{questions="What is the gravity on Mars?",answer01="The pull of gravity on the surface of Mars is only 38% as strong as the pull of gravity on Earth's surface.", answer02="This means that if you weigh 100 kgs on Earth, you would only weigh 38 kgs on Mars.",answer03="So technically, you are not fat. You are just born on the wrong planet."},
                new QuesAns{questions="What is the atmosphere on Mars?", answer01="The atmosphere of Mars is much thinner than Earth's.", answer02="The Red Planet's atmosphere contains more than 95% carbon dioxide and much less than 1% oxygen.", answer03="You will not be able to breathe the air on Mars without a pressurised spacesuit."},
                new QuesAns{questions="Does Mars have a moon?", answer01="Yes, and not only one but it has TWO moons.",answer02="One is names PHOBOS and the other DEIMOS", answer03 ="Mars' moons are among the smallest in the solar system."},
                new QuesAns{questions="How long are the days on Mars?", answer01="Mars is a planet with a very similar daily cycle to the Earth.", answer02="A Martian day is 24 Hours, 39 Minutes and 35.244 Seconds, equivalent to 1.02749125 Earth days.", answer03="A Martian day is approximately 40 Minutes longer than a day on Earth."},
                new QuesAns{questions="How big is Mars?",answer01="With a radius of 3,390 Kilometers, Mars is about half the size of Earth.", answer02="The volume of Mars is 15% of the volume of Earth.",answer03 =" So if you could crack the Earth open like an egg, about 6.5 planets the size of Mars could fit inside."},
                new QuesAns{questions="Where does Mars lie in the Solar System?",answer01="Mars is the fourth planet from the Sun at an average distance of about 228 million km.",answer02="It is also the second-smallest planet in the Solar System.", answer03="Earth and Jupiter are Mars' neighbouring planets."},
                new QuesAns{questions="Does Mars have water?",answer01="Almost all water on Mars today exists as ice.", answer02="Though it also exists in small quantities as vapor in the atmosphere.",answer03="It's not quite like on Earth, but it's definitely there."}
            };
            Lbx_Questions.ItemsSource = quesAns;
            TxtBox_QuesSearch.IsEnabled = false;
            Lbx_Questions.IsEnabled = false;
        }

        private void TxtBox_QuesSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = TxtBox_QuesSearch.Text.ToLower();
            if (filter == "")
                Lbx_Questions.ItemsSource = quesAns;
            else
            {
                var lst = (from b in quesAns where b.questions.StartsWith(filter, 
                    StringComparison.InvariantCultureIgnoreCase) select b).ToList();
                var lst2 = (from m in quesAns where m.questions.ToLower().Contains(filter) select m).ToList();
                lst.AddRange(lst2);
                Lbx_Questions.ItemsSource = lst.Distinct();

            }
        }

        private async void StartMyText()
        {
            await TypeWriterText(Txt_answer01, "I never properly introduced myself. " +
                "My name is HAL 9000. I will assisst you on your jourrney to Mars!");
            await Task.Delay(2000);
            await TypeWriterText(Txt_answer02, "Let's take a little tour of your rover. " +
                "Use ARROW keys to boost your rover as seen in the Simulation window.");
            await Task.Delay(3000);
            await TypeWriterText(Txt_answer03, "Destroy meteors and take care of your FUEL consumption. " +
                "You can also search questions to ask me down below.");
        }
        private static async Task TypeWriterText(TextBlock txt_block, string textToImplement)
        {
            for (int index = 0; index < textToImplement.Length; index++)
            {
                await Task.Delay(30);
                txt_block.Text += textToImplement[index].ToString();
            }
        }
        private void Lbx_Questions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TxtBox_QuesSearch.IsEnabled = false;
            Lbx_Questions.IsEnabled = false;

            QuesAns selectedItem = Lbx_Questions.SelectedItem as QuesAns;
            if (selectedItem == null)
            {
                return;
            }
            Txt_answer01.Text = selectedItem.answer01;
            Txt_answer02.Text = selectedItem.answer02;
            Txt_answer03.Text = selectedItem.answer03;

        }

        private void Txt_AskHal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TxtBox_QuesSearch.IsEnabled = true;
            Lbx_Questions.IsEnabled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = true;
                Img_Left.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyLeft_on.png", UriKind.Relative));
            }
            else if (e.Key == Key.Right)
            {
                goRight = true;
                Img_Right.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyRight_on.png", UriKind.Relative));
            }
            else if (e.Key == Key.Up)
            {
                goUp = true;
                Img_Up.Source = new BitmapImage(new Uri(@"/Images/ArrowKey_on.png", UriKind.Relative));
            }
            else if (e.Key == Key.Down)
            {
                goDown = true;
                Img_Down.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyDown_on.png", UriKind.Relative));
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
                imageUprightStatus = true;
                Img_Left.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyLeft_off.png", UriKind.Relative));
            }
            else if (e.Key == Key.Right)
            {
                goRight = false;
                imageUprightStatus = true;
                Img_Right.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyRight_off.png", UriKind.Relative));
            }
            else if (e.Key == Key.Up)
            {
                goUp = false;
                Img_Up.Source = new BitmapImage(new Uri(@"/Images/ArrowKey_off.png", UriKind.Relative));
            }
            else if (e.Key == Key.Down)
            {
                goDown = false;
                imageUprightStatus = true;
                Img_Down.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyDown_off.png", UriKind.Relative));
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Are you sure you want to Quit?", "WARNING!", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
                Close();
        }

        private void Img_Comet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Img_Comet.Visibility = Visibility.Collapsed;
            playerScore = playerScore + 10;
            roverFuel = roverFuel + 10;
        }

        DispatcherTimer gameTimer = new DispatcherTimer();

        

        public PlayWindow()
        {
            InitializeComponent();
            StartMyText();
            Cnv_Simulation.Focus();
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            if(goLeft==true && Canvas.GetLeft(Img_Rover)>2)
            {
                Canvas.SetLeft(Img_Rover, Canvas.GetLeft(Img_Rover) - playerSpeed);
                Canvas.SetBottom(Img_Rover, Canvas.GetBottom(Img_Rover) + defaultSpeed);
                Img_Rover.Source = new BitmapImage(new Uri(@"/Images/RoverLeft.png", UriKind.Relative));
                roverFuel = roverFuel - 0.1;
                imageUprightStatus = false;
            }
            else if (goRight == true && Canvas.GetLeft(Img_Rover) < 310)
            {
                Canvas.SetLeft(Img_Rover, Canvas.GetLeft(Img_Rover) + playerSpeed);
                Canvas.SetBottom(Img_Rover, Canvas.GetBottom(Img_Rover) + defaultSpeed);
                Img_Rover.Source = new BitmapImage(new Uri(@"/Images/RoverRight.png", UriKind.Relative));
                roverFuel = roverFuel - 0.1;
                imageUprightStatus = false;
            }
            else if(goUp==true && Canvas.GetBottom(Img_Rover) < 410)
            {
                Canvas.SetBottom(Img_Rover, Canvas.GetBottom(Img_Rover) + playerSpeed);
                roverFuel = roverFuel - 0.1;
                //Img_Rover.Source = new BitmapImage(new Uri(@"/Images/Rover.png", UriKind.Relative));
            }
            else if (goDown == true && Canvas.GetBottom(Img_Rover) > 2)
            {
                Canvas.SetBottom(Img_Rover, Canvas.GetBottom(Img_Rover) - playerSpeed);
                Img_Rover.Source = new BitmapImage(new Uri(@"/Images/RoverDown.png", UriKind.Relative));
                roverFuel = roverFuel - 0.1;
                imageUprightStatus = false;
            }

            if (imageUprightStatus)
            {
                Img_Rover.Source = new BitmapImage(new Uri(@"/Images/Rover.png", UriKind.Relative));
            }

            Canvas.SetBottom(Img_Rover, Canvas.GetBottom(Img_Rover) + defaultSpeed);

            Canvas.SetBottom(Img_Comet, Canvas.GetBottom(Img_Comet) - cometSpeed);
            Canvas.SetLeft(Img_Comet, Canvas.GetLeft(Img_Comet) - cometSpeed);
            if(Canvas.GetBottom(Img_Comet) < 2)
            {
                Img_Comet.Visibility = Visibility.Collapsed;
            }

            Canvas.SetLeft(Img_Meteor05, Canvas.GetLeft(Img_Meteor05) - meteorSpeed);

            Canvas.SetBottom(Img_Meteor03, Canvas.GetBottom(Img_Meteor03) + meteorSpeed);

            Canvas.SetBottom(Img_Meteor02, Canvas.GetBottom(Img_Meteor02) - cometSpeed);
            Canvas.SetLeft(Img_Meteor02, Canvas.GetLeft(Img_Meteor02) + meteorSpeed);

            Canvas.SetBottom(Img_Meteor04, Canvas.GetBottom(Img_Meteor04) + meteorSpeed);
            Canvas.SetLeft(Img_Meteor04, Canvas.GetLeft(Img_Meteor04) + meteorSpeed);

            Canvas.SetBottom(Img_Meteor01, Canvas.GetBottom(Img_Meteor01) - meteorSpeed);
            Canvas.SetLeft(Img_Meteor01, Canvas.GetLeft(Img_Meteor01) + meteorSpeed + 0.2);
            if(Canvas.GetBottom(Img_Meteor01) < 2)
            {
                Img_Meteor01.Visibility = Visibility.Collapsed;
            }

            if (Canvas.GetBottom(Img_Meteor02) < 2)
            {
                Img_Meteor02.Visibility = Visibility.Collapsed;
            }
            if((Canvas.GetBottom(Img_Rover)>(Canvas.GetBottom(Img_Mars)-10)) &&(Canvas.GetLeft(Img_Rover)>Canvas.GetLeft(Img_Mars)-10))
            {
                var res = MessageBox.Show("You successfully landed on Mars!","CONGRATULATIONS!",MessageBoxButton.OK);
                if (res == MessageBoxResult.OK)
                {
                    Close();
                    Owner.Close();
                }
            }
            FuelCheck();
            

            Txt_Score.Text = "SCORE : " + playerScore;
        }

        private void FuelCheck()
        {
            if (roverFuel < 80)
            {
                Rect_Darkgreen.Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                if (roverFuel < 60)
                {
                    Rect_Lightgreen.Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                    if(roverFuel<40)
                    {
                        Rect_Yellow.Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                        if(roverFuel<20)
                        {
                            Rect_Orange.Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                            if(roverFuel<0)
                            {
                                Rect_Red.Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                                var res = MessageBox.Show("Your fuel ended! Try Again next time.", "MISSION FAILED!", MessageBoxButton.OK);
                                if (res == MessageBoxResult.OK)
                                {
                                    Owner.Close();
                                    Close();
                                }
                            }
                        }
                    }
                }
                
            }
        }

        private void Cnv_Simulation_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Left)
            //{
            //    goLeft = true;
            //    Img_Left.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyLeft_on.png", UriKind.Relative));
            //}
            //else if(e.Key==Key.Right)
            //{
            //    goRight = true;
            //    Img_Right.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyRight_on.png", UriKind.Relative));
            //}
            //else if (e.Key == Key.Up)
            //{
            //    goUp = true;
            //    Img_Up.Source = new BitmapImage(new Uri(@"/Images/ArrowKey_on.png", UriKind.Relative));
            //}
            //else if (e.Key == Key.Down)
            //{
            //    goDown = true;
            //    Img_Down.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyDown_on.png", UriKind.Relative));
            //}
        }

        private void Cnv_Simulation_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Left)
            //{
            //    goLeft = false;
            //    imageUprightStatus = true;
            //    Img_Left.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyLeft_off.png", UriKind.Relative));
            //}
            //else if (e.Key == Key.Right)
            //{
            //    goRight = false;
            //    imageUprightStatus = true;
            //    Img_Right.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyRight_off.png", UriKind.Relative));
            //}
            //else if (e.Key == Key.Up)
            //{
            //    goUp = false;
            //    Img_Up.Source = new BitmapImage(new Uri(@"/Images/ArrowKey_off.png", UriKind.Relative));
            //}
            //else if (e.Key == Key.Down)
            //{
            //    goDown = false;
            //    imageUprightStatus = true;
            //    Img_Down.Source = new BitmapImage(new Uri(@"/Images/ArrowKeyDown_off.png", UriKind.Relative));
            //}
        }

    }
}

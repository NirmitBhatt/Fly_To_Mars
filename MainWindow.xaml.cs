using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fly_To_Mars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string playerName, roverName;
        public static MainWindow Instance;
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            StartMyText();
        }

        private async void StartMyText()
        {
            await TypeWriterText(Txt_Hello01, "Hello. This is the MARS RANGERS PROGRAM.");
            await Task.Delay(1000);
            await TypeWriterText(Txt_NameQ02, "What is your name fellow Mars Ranger? \n(Type your name and press the Enter key.)");
            TxtBox_Name.Visibility = Visibility.Visible;
            TxtBox_Name.Focus();
        }

        private static async Task TypeWriterText(TextBlock txt_block ,string textToImplement)
        {
            for(int index=0;index<textToImplement.Length;index++)
            {
                await Task.Delay(30);
                txt_block.Text += textToImplement[index].ToString(); 
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TxtBox_Name.Visibility = Visibility.Collapsed;
            TxtBox_RoverName.Visibility = Visibility.Collapsed;
            Btn_Launch.Visibility=Visibility.Collapsed;
        }

        private void TxtBox_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Return)
            {
                playerName = TxtBox_Name.Text;
                TxtBox_Name.IsEnabled = false;
                StartMyNextText();
            }
        }

        private async void StartMyNextText()
        {
            await TypeWriterText(Txt_Welcome03, "Welcome "+playerName+"! You are one of the brave Rangers that will set foot on Mars!");
            await Task.Delay(700);
            await TypeWriterText(Txt_RoverNeed04, "Every Ranger who plans to explore the uncharted territories on Mars, needs to have a Rover of his own.");
            await Task.Delay(1000);
            await TypeWriterText(Txt_RoverNameQ05, "What do you want to name your Rover, " + playerName + "?");
            TxtBox_RoverName.Visibility = Visibility.Visible;
            TxtBox_RoverName.Focus();
        }

        private void TxtBox_Name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        //This is used for TxtBox_Name validation 
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return _regex.IsMatch(text);
        }

        private void Btn_Launch_Click(object sender, RoutedEventArgs e)
        {
            PlayWindow playWindow = new PlayWindow();
            playWindow.Owner = this;
            Visibility = Visibility.Collapsed;
            playWindow.Show();
        }

        private async void TxtBox_RoverName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                roverName = TxtBox_RoverName.Text;
                TxtBox_RoverName.IsEnabled = false;
                await TypeWriterText(Txt_CoolName06, roverName+" is one of the coolest names I've ever heard. So "+playerName+"...");
                await Task.Delay(1000);
                await TypeWriterText(Txt_Ready07, "Are you ready to take " + roverName + " beyond the blue skies and straight to the Red Planet?");
                Btn_Launch.Visibility = Visibility.Visible;
                Btn_Launch.IsEnabled = true;
                //Txt_06.Text = roverName+" will commence flight in 3...2...1! Ready for Launch Nirmit?";
            }
        }
    }
}

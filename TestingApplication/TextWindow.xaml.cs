using System.Windows;
using System.Windows.Controls;
using TestingApplication.Models;

namespace TestingApplication
{
    /// <summary>
    /// Interaction logic for TextWindow.xaml
    /// </summary>
    public partial class TextWindow : Window
    {
        public UIProperties ViewModel { get; }

        public TextBox txtBox;
        public string bgColor = "";

        //public string EmailAddressEntered
        //{
        //    get { return this.bgColor; }
        //}
        //public UIProperties UIProperties { get; }

        public TextWindow()
        {
            DataContext = this;
            InitializeComponent();
            //Application.Current.Dispatcher.Invoke((Action)delegate {
            string color = "";
            txtBox = tbTxt;
            UIProperties UI = new UIProperties();
            UI.BorderThickness = tbTxt.BorderThickness;
            //});
            bgColor = tbTxt.SelectionBrush.ToString();
            color = tbTxt.BorderThickness.ToString();
            //UIProperties UIproperties = new UIProperties();
            //UIproperties._background = bgColor;
            //UIproperties.foreground = tbTxt.Foreground.ToString();
            //Application.Current.Properties["UIProperties"] = bgColor;
            //MessageBox.Show(bgColor);
            //Console.WriteLine(tbTxt_disabled.Name);
            //var background = tbTxt_disabled.Background;          

        }

        public void tbTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine(tbTxt.SelectionBrush);
        }

        public string GetTextBoxBg()
        {
            return tbTxt.SelectionBrush.ToString();
        }
        public string GetTextBoxValue()
        {
            return tbTxt.Text;
        }


    }
}

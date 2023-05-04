using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private MainPage _form1;
        public SettingsPage(MainPage form1)
        {
            InitializeComponent();
            _form1 = form1;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            _form1.SetIP(IP.Text, ushort.Parse(Ports.Text));
        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
            _form1.SetIP("92.124.142.200", 12345);
            IP.Text = "";
            Ports.Text = "";
        }
    }
}
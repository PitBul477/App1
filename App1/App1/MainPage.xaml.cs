using System;
using System.Net;
using System.Net.Sockets;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        private string _nickname;
        private string _ipAddress = "92.124.142.200";
        private int _port = 12345;
        private bool isEyeCrossed;

        public MainPage()
        {
            InitializeComponent();
            isEyeCrossed = false;
        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
            SendPing(1);
            SendPing(2);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Entry.Text.Length > 0)
                {
                    _nickname = Entry.Text;
                    Button2.IsEnabled = true;
                    Entry.IsEnabled = false;
                    Label.IsVisible = false;
                    Button.Focus();
                }
                else
                {
                    DisplayAlert("Ошибка", "НИК НЕ ВВЕДЁН", "OK");
                }
            }
            catch (Exception ex)
            {
                Label.Text = $"Error: {ex.Message}";
                Label.IsVisible = true;
            }
        }

        private void SendPing(int pingNumber)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(_ipAddress);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(new IPEndPoint(ipAddress, _port));
                string currentTime = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss:fff");
                string message = $"{pingNumber}|{currentTime}|{_nickname}";
                byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
                socket.Send(messageBytes);
                byte[] buffer = new byte[1024];
                int bytes = socket.Receive(buffer);
                string response = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                Label.Text = response;
            }
            catch (Exception ex)
            {
                Label.Text = $"Error: {ex.Message}";
                Label.IsVisible = true;
            }
        }

        private void EyeButton_Clicked(object sender, EventArgs e)
        {
            if (isEyeCrossed)
            {
                EyeButton.Source = "eye.png";
            }
            else
            {
                EyeButton.Source = "eye_crossed.png";
            }
            Label.IsVisible = !Label.IsVisible;
            isEyeCrossed = !isEyeCrossed;
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SettingsPage(this));
        }

        public void SetIP(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
        }
    }
}

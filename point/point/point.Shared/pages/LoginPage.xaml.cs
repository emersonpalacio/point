using point.Components;
using point.helpers;
using point.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace point.pages
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            EmailTextBox.Text = "emersonpalaciootalvaro@gmail.com";
            PasswordPasswordBox.Password = "123456";

        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = await validForm();
            if (!isValid) return;

            Loader loader = new Loader("Patiense please");
            loader.Show();


            Response response = await ApiService.LoginAsync(new LoginRequest
            {
                Email = EmailTextBox.Text,
                Password = PasswordPasswordBox.Password
            });
            loader.Close();

            MessageDialog messafeDialog;

            if (!response.IsSucces) { 
               messafeDialog = new MessageDialog(response.Message," Response failed ");
               await messafeDialog.ShowAsync();
                return;
            }

            User user = (User)response.Result;
            if(user == null)
            {
                messafeDialog = new MessageDialog("Null User ");
                await messafeDialog.ShowAsync();
                return;
            }

            messafeDialog = new MessageDialog($"Bienvenidp : { user.FullName}", "Ok");
            await messafeDialog.ShowAsync();

            //Frame.Navigate(typeof(MainPage), user);
        }

        private async Task<bool> validForm()
        {
            MessageDialog messafeDialog; 
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                messafeDialog = new MessageDialog("Add email ");
                await messafeDialog.ShowAsync();
                return false;
            }

            if (!RegexUtilities.IsValidEmail(EmailTextBox.Text))
            {
                messafeDialog = new MessageDialog("Add  valid email ");
                await messafeDialog.ShowAsync();
                return false;
            }


            if (PasswordPasswordBox.Password.Length < 6)
            {
                messafeDialog = new MessageDialog("Add more than six digit in the password");
                await messafeDialog.ShowAsync();
                return false;
            }

            return true;
                
        }
    }
}

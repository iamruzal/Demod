using System;
using System.Collections.Generic;
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
using System.IO;
using EasyCaptcha.Wpf;

namespace Testik.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authentication.xaml
    /// </summary>
    public partial class Authentication : Window
    {
        public Authentication()
        {
            InitializeComponent();
            MyCaptcha.Visibility = Visibility.Hidden;
            enterCaptcha.Visibility = Visibility.Hidden;
            captchaTB.Visibility = Visibility.Hidden;
          
        }
        /// <summary>
        /// Method for downloading images to database
        /// </summary>
        public void LoadImage()
        {
            var path = @"E:\images";
            using (TestDemoEntities context = new TestDemoEntities())
            {
                var images = Directory.GetFiles(path);
                foreach (var tovari in context.Tovari)
                {
                    try
                    {
                        tovari.Image= File.ReadAllBytes(images.FirstOrDefault(x=>x.Contains(tovari.Name)));
                    }
                    catch
                    {
                        tovari.Image = File.ReadAllBytes(images.FirstOrDefault(x => x.Contains("pic")));
                    }
                }
                context.SaveChanges();
            }
            
        }
        
        /// <summary>
        /// Method for authorization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            using (TestDemoEntities db = new TestDemoEntities())
            {
                try
                {
                    var user = db.User.FirstOrDefault(x => x.login == LogintT.Text && x.password == PasswordT.Text);
                    if (user != null && user.RoleId == 1)
                    {
                        AdminWindow adw = new AdminWindow(user);
                        this.Close();
                        adw.Show();

                    }
                    else if (user != null && user.RoleId == 2)
                    {
                        ManagerWindow mnw = new ManagerWindow(user);
                        this.Close();
                        mnw.Show();
                    }
                    else
                    {
                        MessageBox.Show("Проверьте даныне");
                        MyCaptcha.Visibility = Visibility.Visible;
                        enterCaptcha.Visibility = Visibility.Visible;
                        captchaTB.Visibility = Visibility.Visible;
                        LogintT.Visibility = Visibility.Hidden;
                        PasswordT.Visibility = Visibility.Hidden;
                        AuthButton.Visibility = Visibility.Hidden;
                        MyCaptcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
                    }
                   
                    }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте даныне");
                }

            }
                
        }
        /// <summary>
        /// Method for entering captcha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterCaptcha_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MyCaptcha.CaptchaText == captchaTB.Text)
                {
                    MyCaptcha.Visibility = Visibility.Hidden;
                    enterCaptcha.Visibility = Visibility.Hidden;
                    captchaTB.Visibility = Visibility.Hidden;
                    LogintT.Visibility = Visibility.Visible;
                    PasswordT.Visibility = Visibility.Visible;
                    AuthButton.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Неверно");
                    MyCaptcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
                }
            }


           catch (Exception){
                MessageBox.Show("Неверно");
            }
        }
    }
}

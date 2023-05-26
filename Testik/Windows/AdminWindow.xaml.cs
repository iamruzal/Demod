using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace Testik.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
       
        public AdminWindow(User user)
        {
            InitializeComponent();
            using (TestDemoEntities context = new TestDemoEntities())
            {
                TovariList.ItemsSource = context.Tovari.ToList();
            }
            userName.Content = "Приветик, " + user.login;
            nameCombobox.SelectedIndex = 0;
            
        }

        private void searchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (TestDemoEntities context2 = new TestDemoEntities())
            {
                TovariList.ItemsSource = context2.Tovari.Where(x=>x.Name.Contains(searchTB.Text)).ToList();
            }
        }

        private void nameCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            using (TestDemoEntities context3 = new TestDemoEntities())
            {
                if (nameCombobox.SelectedIndex==0)
                {
                    TovariList.ItemsSource = context3.Tovari.ToList();

                }
                if (nameCombobox.SelectedIndex == 1)
                {
                    TovariList.ItemsSource = context3.Tovari.Where(x=>x.Name.Contains("ruz")).ToList();

                }
                if (nameCombobox.SelectedIndex == 2)
                {
                    TovariList.ItemsSource = context3.Tovari.Where(x => x.Name.Contains("aleg")).ToList();

                }
                if (nameCombobox.SelectedIndex == 3)
                {
                    TovariList.ItemsSource = context3.Tovari.Where(x => x.Name.Contains("agusha")).ToList();

                }
                if (nameCombobox.SelectedIndex == 4)
                {
                    TovariList.ItemsSource = context3.Tovari.Where(x => x.Name.Contains("alexx")).ToList();

                }
                if (nameCombobox.SelectedIndex == 5)
                {
                    TovariList.ItemsSource = context3.Tovari.Where(x => x.Name.Contains("green")).ToList();

                }
                if (nameCombobox.SelectedIndex == 6)
                {
                    TovariList.ItemsSource = context3.Tovari.Where(x => x.Name.Contains("brown")).ToList();

                }

            }
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            Tovari tov = (Tovari)(sender as Button).DataContext;
            using (TestDemoEntities context4 = new TestDemoEntities())
            {
                var currentTovar = context4.Tovari.FirstOrDefault(x=>x.Id == tov.Id);
                context4.Tovari.Remove(currentTovar);
                context4.SaveChanges();
                TovariList.ItemsSource = context4.Tovari.ToList();
            }

        }
    }
}

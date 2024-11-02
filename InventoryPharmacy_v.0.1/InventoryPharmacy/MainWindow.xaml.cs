using InventoryPharmacy.Model;
using InventoryPharmacy.View;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryPharmacy
{
    public partial class MainWindow : Window
    {
        String loginText = "adminSaveLogin";
        String passText = "adminSavePassword";

        public MainWindow()
        {
            InitializeComponent();
            loginButton.Click += ToHomeScreen;
        }

        private void ToHomeScreen(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string pass = passwordTextBox.Text;

            if  (login == loginText && pass == passText)
            {
                HomeScreenWindow homeScreenWindow = new HomeScreenWindow();
                homeScreenWindow.Show();
                homeScreenWindow.roleHome.Text = "Администратор";
                homeScreenWindow.SwichRole("Администратор");
                Close();
            }
            else
            {
                Personal authPer = null;
                using (ApplicationPersonal dbPersonal = new ApplicationPersonal())
                {
                    authPer = dbPersonal.Personals.Where(b => b.Login == login && b.Pass == pass).FirstOrDefault();

                    if (authPer != null)
                    {
                        HomeScreenWindow homeScreenWindow = new HomeScreenWindow();
                        homeScreenWindow.Show();
                        homeScreenWindow.roleHome.Text = authPer.Role;
                        homeScreenWindow.SwichRole(authPer.Role);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль введены неправильно!", "Ошибка авторизации");
                    }
                }
            }

        }
    }
}

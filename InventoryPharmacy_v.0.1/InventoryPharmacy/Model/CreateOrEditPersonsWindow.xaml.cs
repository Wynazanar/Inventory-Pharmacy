using InventoryPharmacy.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace InventoryPharmacy.Model
{
    public partial class CreateOrEditPersonsWindow : Window
    {
        ApplicationPersonal dbPersonal;
        TgMessage tg;
        public string nameText, loginText, passwordText, emailText, phoneText, roleText;

        public CreateOrEditPersonsWindow()
        {
            InitializeComponent();
            dbPersonal = new ApplicationPersonal();
            backButton.Click += ToPersonalWindow;
            savePersonal.Click += AddPersonal;
            saveChangePersonal.Click += ChangeItem;

            tg = new TgMessage();
        }

        public void AddPersonal(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text.Trim();
            string login = loginTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            string email = emailTextBox.Text.Trim();
            string phone = phoneTextBox.Text.Trim();
            string role = roleComboBox.SelectedItem.ToString().Trim();

            if (role.Contains("Админ"))
            {
                role = "Администратор";
            }
            else if (role.Contains("Фармацевт"))
            {
                role = "Фармацевт";
            }
            
            if (phone.Length != 11)
            {
                phoneTextBox.ToolTip = "Это поле введено некорректно!";
                phoneTextBox.Background = Brushes.Red;
            }

            else
            {
                Personal personal = new Personal(name, login, password, email, phone, role);
                dbPersonal.Personals.Add(personal);
                dbPersonal.SaveChanges();

                MessageBox.Show("Сотрудник успешно добавлен!", "Добавление сотрудника");
                PersonalsWindow personalsWindow = new PersonalsWindow();
                personalsWindow.Show();
                personalsWindow.rolePersonal.Text = roleAddPersonals.Text;
                Close();

                tg.SendMessage($"Добавление сотрудника: ```\n\rФИО: {name} \n\rЛогин: {login} \n\rПароль: {password} \n\rЭл. Почта: {email} \n\rТелефон: {phone} \n\rРоль: {role} \n\r ``` \r\n");

            }
        }

        private void ToPersonalWindow(object sender, RoutedEventArgs e)
        {
            PersonalsWindow personalsWindow = new PersonalsWindow();
            personalsWindow.Show();
            personalsWindow.rolePersonal.Text = roleAddPersonals.Text;
            Close();
        }

        public void ChangeItem(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idTextBox.Text);
            string name = nameTextBox.Text;
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;
            string email = emailTextBox.Text;
            string phone = phoneTextBox.Text;
            string role = roleComboBox.SelectedItem.ToString();

            if (role.Contains("Админ"))
            {
                role = "Администратор";
            }
            else if (role.Contains("Фармацевт"))
            {
                role = "Фармацевт";
            }
            else if (role.Contains("Провизор"))
            {
                role = "Провизор";
            }    

            var personal = dbPersonal.Personals.FirstOrDefault(p => p.id == id);
            personal.Name = name;
            personal.Login = login;
            personal.Pass = password;
            personal.Email = email;
            personal.Phone = phone;
            personal.Role = role;
            dbPersonal.SaveChanges();

            MessageBox.Show("Сотрудник успешно обновлен!", "Обновление персонала");
            PersonalsWindow personalsWindow = new PersonalsWindow();
            personalsWindow.Show();
            personalsWindow.rolePersonal.Text = roleAddPersonals.Text;
            Close();

            tg.SendMessage($"Редактирование сотрудника: ```\n\rФИО: {nameText} --> {name} \n\rЛогин: {loginText} --> {login} \n\rПароль: {passwordText} --> {password} \n\rЭл. Почта: {emailText} --> {email} \n\rТелефон: {phoneText} --> {phone} \n\rРоль: {roleText} --> {role} \n\r ``` \r\n");

        }
    }
}

using InventoryPharmacy.View;
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

namespace InventoryPharmacy.Model
{
    public partial class PersonalsWindow : Window
    {
        ApplicationPersonal dbPersonal;
        TgMessage tg;

        public PersonalsWindow()
        {
            InitializeComponent();
            dbPersonal = new ApplicationPersonal();
            LoadDataGrid();
            backButton.Click += ToHomeScreen;
            addPersonalButton.Click += ToCreateOrEditPersonal;
            changePersonalButton.Click += ToEditePersonals;
            deletePersonalButton.Click += RemovePersonals;
            tgBut.Click += TestTgCommands;
            tg = new TgMessage();
        }

        private void ToHomeScreen(object sender, RoutedEventArgs e)
        {
            HomeScreenWindow homeScreenWindow = new HomeScreenWindow();
            homeScreenWindow.Show();
            homeScreenWindow.roleHome.Text = rolePersonal.Text;
            homeScreenWindow.SwichRole(rolePersonal.Text);
            Close();
        }

        private void ToCreateOrEditPersonal(object sender, RoutedEventArgs e)
        {
            CreateOrEditPersonsWindow addOrEditPersonal = new CreateOrEditPersonsWindow();
            addOrEditPersonal.Show();
            addOrEditPersonal.roleAddPersonals.Text = rolePersonal.Text;

            addOrEditPersonal.idPersonPanel.Visibility = Visibility.Collapsed;
            addOrEditPersonal.saveChangePersonal.Visibility = Visibility.Collapsed;

            Close();
        }

        private void ToEditePersonals(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                CreateOrEditPersonsWindow createOrEditPersons = new CreateOrEditPersonsWindow();
                createOrEditPersons.Show();
                createOrEditPersons.roleAddPersonals.Text= rolePersonal.Text;
                createOrEditPersons.savePersonal.Visibility = Visibility.Collapsed;

                if (dataGrid.SelectedItem is Personal selectedPerson)
                {
                    createOrEditPersons.idTextBox.Text = selectedPerson.id.ToString();
                    createOrEditPersons.nameTextBox.Text = selectedPerson.Name;
                    createOrEditPersons.loginTextBox.Text = selectedPerson.Login;
                    createOrEditPersons.passwordTextBox.Text = selectedPerson.Pass;
                    createOrEditPersons.emailTextBox.Text = selectedPerson.Email;
                    createOrEditPersons.phoneTextBox.Text = selectedPerson.Phone;
                    createOrEditPersons.roleComboBox.Text = selectedPerson.Role;

                    createOrEditPersons.nameText = selectedPerson.Name;
                    createOrEditPersons.loginText = selectedPerson.Login;
                    createOrEditPersons.passwordText = selectedPerson.Pass;
                    createOrEditPersons.emailText = selectedPerson.Email;
                    createOrEditPersons.phoneText = selectedPerson.Phone;
                    createOrEditPersons.roleText = selectedPerson.Role;
                }

                Close();
            }
        }

        private void RemovePersonals(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Personal selectedItem)
            {
                dbPersonal.Personals.Remove(selectedItem);
                dbPersonal.SaveChanges();
                LoadDataGrid();
                tg.SendMessage($"Удаление сотрудника: ```\n\rФИО: {selectedItem.Name} \n\rЛогин: {selectedItem.Login} \n\rПароль: {selectedItem.Pass} \n\rЭл. Почта: {selectedItem.Email} \n\rТелефон: {selectedItem.Phone} \n\rРоль: {selectedItem.Role} \n\r ``` \r\n");
            }
        }

        private void LoadDataGrid()
        {
            try
            {
                var items = dbPersonal.Personals.ToList();
                dataGrid.ItemsSource = items;
            }
            catch (Exception) { }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = searchTextBox.Text.ToLower();
            if (search == "")
            {
                LoadDataGrid();
            }
            else
            {
                var filteredItems = dbPersonal.Personals.Where(i => i.Name.ToLower().Contains(search)).ToList();
                dataGrid.ItemsSource = filteredItems;
            }
        }

        private void TestTgCommands(object sender, RoutedEventArgs e)
        {
            List<Personal> personals = dbPersonal.Personals.ToList();
            string message = "";
            foreach (Personal personal in personals)
            {
                message += $"ФИО: {personal.Name} | Логин: {personal.Login} | Пароль: {personal.Pass} | Эл. почта: {personal.Email} | Телефон: {personal.Phone} | Роль: {personal.Role}\r\n";
            }
            tg.SendMessage($"Список сотрудников: ```\r\n{message}\r\n```");

        }
    }
}

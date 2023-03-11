using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls;


namespace inputValidation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() { InitializeComponent(); }

        private string _file = "employee.txt";

        // function of the button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textID = Identifier.Text;
            string textSurname = Surname.Text;
            string textName = Name.Text;
            string textMidlName = MiddleName.Text;
            string textPasport = Passport.Text;
            string textPhone = MobPhone.Text; 
            string textEmail = Email.Text;

            if (string.IsNullOrWhiteSpace(textID) ||
                string.IsNullOrWhiteSpace(textName) ||
                string.IsNullOrWhiteSpace(textSurname) ||
                string.IsNullOrWhiteSpace(textPasport) ||
                string.IsNullOrWhiteSpace(textEmail))
            {
                MessageBox.Show("Введены некоректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (_checkEmail(textEmail) && 
                    _checkPhone(textPhone) &&
                    _checkPasport(textPasport))
                {
                    File.AppendAllText(_file, "ID: " + textID + " ");
                    File.AppendAllText(_file, "Surname: " + textSurname + " ");
                    File.AppendAllText(_file, "Name: " + textName + " ");
                    File.AppendAllText(_file, "Patronymic: " + textMidlName + " ");
                    File.AppendAllText(_file, "Passport: " + textPasport + " ");
                    File.AppendAllText(_file, "Phone: " + textPhone + " ");
                    File.AppendAllText(_file, "Email: " + textEmail + " ");
                    File.AppendAllText(_file, "\n");
                }
                // condition for mail
                //if (_checkEmail(textEmail))
                //{
                //    MobPhone.BorderBrush = Brushes.Gray;
                //    MessageBox.Show("Данные сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                //    File.AppendAllText("employee.txt", textEmail);
                //    Email.Text = null;
                //}
                //else
                //    Email.BorderBrush = Brushes.Red;

                //// condition for phone
                //if (_checkPhone(textPhone))
                //{
                //    MobPhone.BorderBrush = Brushes.Gray;
                //    MessageBox.Show("Данные сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                //    File.AppendAllText("employee.txt", textPhone);
                //    MobPhone.Text = null;
                //}
                //else
                //    MobPhone.BorderBrush = Brushes.Red;

                //// condition for pasport
                //if (_checkPasport(textPasport))
                //{
                //    Passport.BorderBrush = Brushes.Gray;
                //    MessageBox.Show("Данные сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                //    File.AppendAllText("employee.txt", textPhone);
                //    Passport.Text = null;
                //}
                //else
                //    Passport.BorderBrush = Brushes.Red;
            }

            _checkID(textID);
        }

        // functions used
        // mail check 
        private static bool _checkEmail(string email)
        {
            if (!email.Contains("@"))
                return false;

            string[] delimiter = email.Split('@');
            if (delimiter.Length != 2)
                return false;

            string login = delimiter[0];
            string domain = delimiter[1];

            if (login.Length == 0 || domain.Length == 0)
                return false;

            char firstSymbol = login[0];
            if (!Regex.IsMatch(firstSymbol.ToString(), @"[a-z]") || !Regex.IsMatch(firstSymbol.ToString(), @"[A-Z]"))
                return false;

            string[] arrDobain = domain.Split('.');
            if (arrDobain.Length != 2)
                return false;

            return true;
        }

        // phone check
        private static bool _checkPhone(string phone)
        {
            if (Regex.IsMatch(phone.ToString(), @"[a-z]") || Regex.IsMatch(phone.ToString(), @"[A-Z]"))
                return false;

            switch (phone[0]) 
            {
                case '8':
                    if (phone.Length != 11)
                        return false;
                    break;
                case '+':
                    if (phone.Length != 12 && phone[1] != '7')
                        return false; 
                    break;
                default:
                    return false;
            }

            return true; 
        }

        // pasport check
        private static bool _checkPasport(string pasport)
        {
            if (Regex.IsMatch(pasport.ToString(), @"[a-z]") || Regex.IsMatch(pasport.ToString(), @"[A-Z]"))
                return false;

            string[] arrPasport;
            try
            {
                arrPasport = pasport.Split(' ');
            }
            catch
            {
                return false;
            }

            string series = arrPasport[0];
            if (series.Length != 4) 
                return false;

            string number = arrPasport[1];
            if (number.Length != 6)
                return false;

            return true; 
        }

        // identity check
        private static bool _checkID(string id)
        {
            string[] fileContains = new string[] { };
            try
            {
                fileContains = File.ReadAllLines("employee.txt");
            }
            catch
            {
                return true;
            }

            foreach (string line in fileContains)
            {
                //if (line.Split()[])
            }
            return true;
        }
    }
}

using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace inputValidation
{
    public partial class MainWindow : Window
    {
        public MainWindow() { InitializeComponent(); }

        private static string _file = "employee.txt";

        // function of the button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string textID = Identifier.Text;
            string textSurname = Surname.Text;
            string textName = Name.Text;
            string textPatronymic = Patronymic.Text;
            string textPassport = Passport.Text;
            string textPhone = MobPhone.Text;
            string textEmail = Email.Text;

            if (string.IsNullOrWhiteSpace(textID) ||
                string.IsNullOrWhiteSpace(textName) ||
                string.IsNullOrWhiteSpace(textSurname) ||
                string.IsNullOrWhiteSpace(textPatronymic) ||
                string.IsNullOrWhiteSpace(textPassport) ||
                string.IsNullOrWhiteSpace(textPhone) ||
                string.IsNullOrWhiteSpace(textEmail))
            {
                if (string.IsNullOrWhiteSpace(textID))
                    Identifier.BorderBrush = Brushes.Red;
                if (string.IsNullOrWhiteSpace(textName))
                    Name.BorderBrush = Brushes.Red;
                if (string.IsNullOrWhiteSpace(textSurname))
                    Surname.BorderBrush = Brushes.Red;
                if (string.IsNullOrWhiteSpace(textPatronymic))
                    Patronymic.BorderBrush = Brushes.Red;
                if (string.IsNullOrWhiteSpace(textPassport))
                    Passport.BorderBrush = Brushes.Red;
                if (string.IsNullOrWhiteSpace(textPhone))
                    MobPhone.BorderBrush = Brushes.Red;
                if (string.IsNullOrWhiteSpace(textEmail))
                    Email.BorderBrush = Brushes.Red;
            }
            else
            {
                if (_checkID(textID) &&
                    _checkNSP(textName, textSurname, textPatronymic) &&
                    _checkPasport(textPassport) &&
                    _checkPhone(textPhone) &&
                    _checkEmail(textEmail))
                {
                    File.AppendAllText(_file, "ID " + textID + " ");
                    Identifier.BorderBrush = Brushes.Gray;
                    Identifier.Text = null;
                    File.AppendAllText(_file, "Surname " + textSurname + " ");
                    Surname.BorderBrush = Brushes.Gray;
                    Surname.Text = null;
                    File.AppendAllText(_file, "Name " + textName + " ");
                    Name.BorderBrush = Brushes.Gray;
                    Name.Text = null;
                    File.AppendAllText(_file, "Patronymic " + textPatronymic + " ");
                    Patronymic.BorderBrush = Brushes.Gray;
                    Patronymic.Text = null;
                    File.AppendAllText(_file, "Passport " + textPassport + " ");
                    Passport.BorderBrush = Brushes.Gray;
                    Passport.Text = null;
                    File.AppendAllText(_file, "Phone " + textPhone + " ");
                    MobPhone.BorderBrush = Brushes.Gray;
                    MobPhone.Text = null;
                    File.AppendAllText(_file, "Email " + textEmail);
                    Email.BorderBrush = Brushes.Gray;
                    Email.Text = null;
                    File.AppendAllText(_file, "\n");
                    MessageBox.Show("Данные сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (!_checkID(textID))
                        Identifier.BorderBrush = Brushes.Red;
                    if (!_checkPasport(textPassport))
                        Passport.BorderBrush = Brushes.Red;
                    if (!_checkPhone(textPhone))
                        MobPhone.BorderBrush = Brushes.Red;
                    if (!_checkEmail(textEmail))
                        Email.BorderBrush = Brushes.Red;
                    if (!_checkNSP(textName, textSurname, textPatronymic))
                    {
                        Name.BorderBrush = Brushes.Red;
                        Surname.BorderBrush = Brushes.Red;
                        Patronymic.BorderBrush = Brushes.Red;
                    }
                }
            }
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
            if (Regex.IsMatch(firstSymbol.ToString(), @"[A-Z][a-z]"))
                return false;

            string[] arrDobain = domain.Split('.');
            if (arrDobain.Length != 2)
                return false;

            return true;
        }

        // phone check
        private static bool _checkPhone(string phone)
        {
            if (Regex.IsMatch(phone.ToString(), @"[a-z]!@#\/") || Regex.IsMatch(phone.ToString(), @"[A-Z]!@#\/"))
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
            if (!Regex.IsMatch(pasport.ToString(), @"[0-9]"))
                return false;

            string[] arrPasport = pasport.Split(' '); ;
            if (arrPasport.Length != 2)
                return false;

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
            if (!(Regex.IsMatch(id.ToString(), @"[0-9]")))
                return false; 

            string[] fileContains = File.ReadAllLines(_file);
            foreach (string line in fileContains)
            {
                if (line.Split()[0] == "ID")
                {
                    if (line.Split()[1] == id)
                        return false;
                }
            }

            return true;
        }

        // checking the name, surname and patronymic
        private static bool _checkNSP(string name, string surname, string patronymic)
        {
            if ((!Regex.IsMatch(name.ToString(), @"[А-Я]") || !Regex.IsMatch(name.ToString(), @"[а-я]")) ||
                (!Regex.IsMatch(surname.ToString(), @"[А-Я]") || !Regex.IsMatch(surname.ToString(), @"[а-я]")) ||
                (!Regex.IsMatch(patronymic.ToString(), @"[А-Я]") || !Regex.IsMatch(patronymic.ToString(), @"[а-я]")))
                return false;

            if (!(name[0] == name.ToUpper()[0]) ||
                !(surname[0] == surname.ToUpper()[0]) ||
                !(patronymic[0] == patronymic.ToUpper()[0]))
                return false; 

            return true; 
        }
    }
}
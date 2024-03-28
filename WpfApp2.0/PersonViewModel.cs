using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2._0;

namespace WpfApp2._0
{
    internal class PersonViewModel
    {
        private PersonModel _person;
        private MainWindow _window;

        public PersonViewModel(MainWindow mainWindow)
        {
            _person = new PersonModel("", "", "", DateTime.Now);
            _window = mainWindow;
            ProceedCommand = new RelayCommand(Proceed, CanProceed);
        }

        public ICommand ProceedCommand { get; }

        public string Name
        {
            get { return _person.Name; }
            set { _person.Name = value; }
        }

        public string Surname
        {
            get { return _person.Surname; }
            set { _person.Surname = value; }
        }

        public string Mail
        {
            get { return _person.Mail; }
            set { _person.Mail = value; }
        }

        public DateTime Birthdate
        {
            get { return _person.Birthdate; }
            set { _person.Birthdate = value; }
        }

        public bool IsAdult
        {
            get { return _person.IsAdult; }
        }

        public string SunSign
        {
            get { return _person.SunSign; }
        }

        public string ChineseSign
        {
            get { return _person.ChineseSign; }
        }

        public bool IsBirthday
        {
            get { return _person.IsBirthday; }
        }

        private bool CanProceed(object parameter)
        {
            if (string.IsNullOrEmpty(_window.NameInput.Text)) return false;
            if (string.IsNullOrEmpty(_window.SurnameInput.Text)) return false;
            if (string.IsNullOrEmpty(_window.MailInput.Text)) return false;
            if (!_window.BirthdatePicker.SelectedDate.HasValue) return false;
            return true;
        }

        private async void Proceed(object parameter)
        {
            _window.PersonData.IsEnabled = false;
            _window.NameOutput.Text = "";
            _window.SurnameOutput.Text = "";
            _window.MailOutput.Text = "";
            _window.DateOfBirthOutput.Text = "";
            _window.IsAdultOutput.Text = "";
            _window.SunSignOutput.Text = "";
            _window.ChineseSignOutput.Text = "";
            _window.HappyBirthdayGreeting.Content = "";
            try
            {

                string name = _window.NameInput.Text;
                string surname = _window.SurnameInput.Text;
                string mail = _window.MailInput.Text;
                DateTime date = _window.BirthdatePicker.SelectedDate.Value;
                await Task.Run(() => { _person = new PersonModel(name, surname, mail, date); });
                int age = _person.Age();
                if (age < 0 || age >= 135) MessageBox.Show("Your age must be in range from 0 to 135", "Invalid age input");
                else
                {
                    _window.NameOutput.Text = Name;
                    _window.SurnameOutput.Text = Surname;
                    _window.MailOutput.Text = Mail;
                    _window.DateOfBirthOutput.Text = Birthdate.ToShortDateString();
                    _window.IsAdultOutput.Text = IsAdult ? "Yes" : "No";
                    _window.SunSignOutput.Text = SunSign;
                    _window.ChineseSignOutput.Text = ChineseSign;
                    _window.HappyBirthdayGreeting.Content = IsBirthday ? "Happy birthday!" : "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally { _window.PersonData.IsEnabled = true; }
        }
    }
}

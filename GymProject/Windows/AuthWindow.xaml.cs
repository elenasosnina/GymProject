using GymProject.Infrastructure.Consts;
using GymProject.Infrastructure.DataBase;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace GymProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private EmployeeRepository _empRepository;
        private EmployeeViewModel _empViewModel;
        private PositionRepository _posRepository;
        private PositionViewModel _posViewModel;
        public AuthWindow()
        {
            InitializeComponent();
            _empRepository = new EmployeeRepository();
            _empViewModel = new EmployeeViewModel();
            _posRepository = new PositionRepository();
            _posViewModel = new PositionViewModel();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            string login = Login.Text;
            string password = Password.Password;
            _empRepository.Login(login, password);
            if (login == "" && password == "")
            {
                MessageBox.Show("Логин и пароль не могут быть пустыми строками!");
                return;
            }
            if (login == "")
            {
                MessageBox.Show("Логин не может быть пустой строкой!");
                return;
            }
            if (password == "")
            {
                MessageBox.Show("Пароль не может быть пустой строкой!");
                return;
            }


            using (Infrastructure.Context context = new Infrastructure.Context())
            {
                var user = context.Employees.FirstOrDefault(x => x.Login == login && x.Password == password && x.PositionId == 2);
                var user1 = context.Clients.FirstOrDefault(x => x.Login == login && x.Password == password);
                var user2 = context.Employees.FirstOrDefault(x => x.Login == login && x.Password == password && x.PositionId != 2);
                if (user1 != null || user2 != null)
                {
                    CurrentUser.PositionId = "3";
                    CurrentUser.PositionName = "Пользователь";
                    CurrentUser.EmployeeName = $" {login}";
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                else if (user != null)
                {
                    CurrentUser.PositionId = "2";
                    CurrentUser.PositionName = "Администратор";
                    CurrentUser.EmployeeName = $" {login}";
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }
            }
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            //Close();

        }
        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.PositionId = "1";
            CurrentUser.PositionName = "Гость";
            CurrentUser.EmployeeName= "Гость";
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

    }
}

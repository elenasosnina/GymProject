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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GymProject.Infrastructure.Consts;
using GymProject.Pages;
using GymProject.Windows;

namespace GymProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (CurrentUser.PositionName == "Гость" || CurrentUser.PositionName == "Пользователь")
            {
                Height= 260;
            }
        }
       
        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            ClientsPage clientsPage = new ClientsPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = clientsPage.Title;
            mainWindow.MainFrame.Navigate(clientsPage);

        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            ProductsPage productsPage = new ProductsPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = productsPage.Title;
            mainWindow.MainFrame.Navigate(productsPage);

        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            EmployeesPage employeesPage = new EmployeesPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = employeesPage.Title;
            mainWindow.MainFrame.Navigate(employeesPage);
        }

        private void LessonProgramms_Click(object sender, RoutedEventArgs e)
        {
            LessonProgramsPage lessonProgramsPage = new LessonProgramsPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = lessonProgramsPage.Title;
            mainWindow.MainFrame.Navigate(lessonProgramsPage);
        }

        private void Subscriptions_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionsPage subscriptionsPage = new SubscriptionsPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = subscriptionsPage.Title;
            mainWindow.MainFrame.Navigate(subscriptionsPage);
        }

        private void Lessons_Click(object sender, RoutedEventArgs e)
        {
            LessonsPage lessonsPage = new LessonsPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = lessonsPage.Title;
            mainWindow.MainFrame.Navigate(lessonsPage);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.PositionId = null;
            CurrentUser.PositionName = null;
            CurrentUser.EmployeeName = null;
            CurrentUser.EmployeeId = null;
            AuthWindow mainWindow = new AuthWindow();
            mainWindow.Show();
            Close();
           
        }
    }
}

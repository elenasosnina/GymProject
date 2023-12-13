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
using GymProject.Windows;
using GymProject.Infrastructure.Consts;
using System.Data;

namespace GymProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            // Отображение информации о текущем пользователе (роли и имени).
            roleName.Text = ($"Роль:  {CurrentUser.PositionName}");
            userName.Text = ($"Пользователь:  {CurrentUser.EmployeeName}");
            // Скрывание разделов меню в зависимости от роли пользователя.
            if (CurrentUser.PositionName == "Гость" || CurrentUser.PositionName == "Пользователь")
            {
                Employees.Visibility = Visibility.Hidden;
                Employees.IsEnabled = false;
                Products.Visibility = Visibility.Hidden;
                Products.IsEnabled = false;
                Clients.Visibility = Visibility.Hidden;
                Clients.IsEnabled = false;
                row.Height=new GridLength(0); // Сокрытие строки с промежутком в меню.
            }    
            
        }
       
        private void Clients_Click(object sender, RoutedEventArgs e)// Обработчик события нажатия кнопки "Клиенты".
        {    // Создание и отображение страницы клиентов в главном окне.
            ClientsPage clientsPage = new ClientsPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = clientsPage.Title;
            mainWindow.MainFrame.Navigate(clientsPage);

        }
        private void Products_Click(object sender, RoutedEventArgs e)// Обработчик события нажатия кнопки "Товары".
        {    // Создание и отображение страницы товаров в главном окне.
            ProductsPage productsPage = new ProductsPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = productsPage.Title;
            mainWindow.MainFrame.Navigate(productsPage);

        }

        private void Employees_Click(object sender, RoutedEventArgs e)// Обработчик события нажатия кнопки "Сотрудники".
        {    // Создание и отображение страницы сотрудников в главном окне.
            EmployeesPage employeesPage = new EmployeesPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = employeesPage.Title;
            mainWindow.MainFrame.Navigate(employeesPage);
        }

        private void LessonProgramms_Click(object sender, RoutedEventArgs e)// Метод для обработки события нажатия кнопки "Программы занятий".
        {    // Создание и отображение страницы программ занятий в главном окне.
            LessonProgramsPage lessonProgramsPage = new LessonProgramsPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = lessonProgramsPage.Title;
            mainWindow.MainFrame.Navigate(lessonProgramsPage);
        }

        private void Subscriptions_Click(object sender, RoutedEventArgs e)// Метод для обработки события нажатия кнопки "Подписки".
        { // Создание и отображение страницы подписок в главном окне.
            SubscriptionsPage subscriptionsPage = new SubscriptionsPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = subscriptionsPage.Title;
            mainWindow.MainFrame.Navigate(subscriptionsPage);
        }

        private void Lessons_Click(object sender, RoutedEventArgs e)// Метод для обработки события нажатия кнопки "Занятий".
        { // Создание и отображение страницы занятий в главном окне.
            LessonsPage lessonsPage = new LessonsPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = lessonsPage.Title;
            mainWindow.MainFrame.Navigate(lessonsPage);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)// Обработчик события нажатия кнопки "Выход".
        {    // Сброс информации о текущем пользователе 
            CurrentUser.PositionId = null;
            CurrentUser.PositionName = null;
            CurrentUser.EmployeeName = null;
            CurrentUser.EmployeeId = null;
            // Открытие окна авторизации
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            // Закрытие текущего окна.
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }
    }
}

using GymProject.CardWindows;
using GymProject.Infrastructure;
using GymProject.Infrastructure.DataBase;
using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
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
using System.Data.Entity;
using GymProject.Infrastructure.QR;
using GymProject.Windows;
using GymProject.Infrastructure.Report;
using System.IO;
using System.Reflection;

namespace GymProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        private EmployeeRepository _repository;
        public EmployeesPage()
        {
            InitializeComponent();
            _repository = new EmployeeRepository();// Инициализация репозитория сотрудников
            UpdateGrid();// Обновление данных в таблице
        }
        // Метод для обновления данных в таблице.
        private void UpdateGrid()
        {
            EmployeesGrid.ItemsSource = _repository.GetList();// Установка источника данных таблицы из репозитория.

        }
        // Обработчик события нажатия на кнопку "Exit"
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuPage menuPage = new MenuPage();// Создание новой страницы меню.
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this); // Получение основного окна приложения.
            mainWindow.Title = menuPage.Title;// Установка заголовка основного окна и навигация на страницу меню.
            mainWindow.MainFrame.Navigate(menuPage);

        }
        // Метод для получения списка сотрудников из базы данных.
        public List<EmployeeViewModel> GetList()
        {
            using (var context = new Context())
            {// Использование контекста базы данных.
                var items = context.Employees.Include(x => x.Position).ToList();// Получение всех сотрудников из базы данных включая информацию о должности.
                return EmployeeMapper.Map(items);// Маппинг объектов данных в представление
            }
        }
        // Метод для получения сотрудника по его идентификатору
        public EmployeeViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Employees.FirstOrDefault(x => x.Id == id);// Поиск сотрудника по идентификатору в базе данных.
                return EmployeeMapper.Map(item);// Маппинг найденного сотрудника в представление.
            }
        }
        // Обработчик события нажатия на кнопку "Add"
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var employeeCard = new EmployeeCardWindow();// Создание окна для добавления нового сотрудника
            employeeCard.ShowDialog();// Отображение окна в виде диалога.
            UpdateGrid();// Обновление данных в таблице после закрытия окна добавления сотрудника.

        }
        // Обработчик события нажатия на кнопку "Delete".
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, выбран ли сотрудник для удаления.
            if (EmployeesGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }
            // Получение выбранного объекта из таблицы
            var item = EmployeesGrid.SelectedItem as EmployeeViewModel;
            // Проверка, удалось ли получить данные о сотруднике
            if (item == null)
            {
                MessageBox.Show("Не удалось получить данные");
                return;
            }
            // Удаление сотрудника из репозитория и обновление данных в таблице.
            _repository.Delete(item.Id);
            UpdateGrid();
        }
        // Обработчик события нажатия кнопки "Change"
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            // Проверка наличия выбранного объекта в таблице.
            if (EmployeesGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для изменения");
                return;
            }
            // Открытие окна редактирования для выбранного объекта и обновление данных в таблице.
            var clientCard = new EmployeeCardWindow(EmployeesGrid.SelectedItem as EmployeeViewModel);
            clientCard.ShowDialog();
            UpdateGrid();
        }
        // Метод для поиска сотрудников по запросу.
        public List<EmployeeViewModel> Search(string search)
        {// Удаление лишних пробелов и приведение к нижнему регистру.
            search = search.Trim().ToLower();

            using (var context = new Context())
            {// Поиск сотрудников, чьи имена содержат введенный запрос и имеют такую же длину, как запрос.
                var result = context.Employees.Include(x => x.Position).Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return EmployeeMapper.Map(result);
            }

        }
        // Обработчик события нажатия кнопки поиска
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string search = find.Text;
            if (string.IsNullOrEmpty(search))
            {
                UpdateGrid();
            }
            else
            {
                List<EmployeeViewModel> searchResult = _repository.Search(search);// Выполнить поиск по запросу.
                EmployeesGrid.ItemsSource = searchResult;// Отобразить результаты поиска.
            }
        }
        // Обработчик события нажатия кнопки "Generate".
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem != null)
            { // Создание QR-кода для выбранного сотрудника и его отображение в новом окне.
                var qrManager = new QRManager();
                var qrCodeImage = qrManager.Generate(EmployeesGrid.SelectedItem);
                var qrWindow = new QRWindow();
                qrWindow.qrImage.Source = qrCodeImage;
                qrWindow.Show();
            }
            else
            {
                MessageBox.Show("Объект не выбран");
            }
        }
        // Обработчик события нажатия кнопки "Export"
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            { // Генерация отчета на основе данных из таблицы и сохранение в файле Excel.
                var reportManager = new ReportManager();
                var data = reportManager.GenerateReport(EmployeesGrid.ItemsSource as List<EmployeeViewModel>);

                var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Сотрудники_{DateTime.Now.ToShortDateString()}.xlsx");
                using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    stream.Write(data, 0, data.Length);
                }


                MessageBox.Show("Отчет успешно выгружен.", "Выгрузка отчета", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выгрузке отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

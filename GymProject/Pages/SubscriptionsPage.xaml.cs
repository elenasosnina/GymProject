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
using GymProject.Infrastructure.Consts;
using System.Security.Cryptography;
using GymProject.Infrastructure.QR;
using GymProject.Windows;
using GymProject.Infrastructure.Report;
using System.IO;
using System.Reflection;

namespace GymProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для SubscriptionsPage.xaml
    /// </summary>
    public partial class SubscriptionsPage : Page
    {
        private SubscriptionRepository _repository;
        public SubscriptionsPage()
        {
            InitializeComponent();
            if (CurrentUser.PositionName == "Гость" || CurrentUser.PositionName == "Пользователь")//Если user имеет роли Гость или Пользователь, ограничивается доступ к следующим элементам
            {
                add.Visibility = Visibility.Hidden;
                add.IsEnabled = false;
                del.Visibility = Visibility.Hidden;
                del.IsEnabled = false;
                change.Visibility = Visibility.Hidden;
                change.IsEnabled = false;
            }
            _repository = new SubscriptionRepository();// Инициализация репозитория подписок
            UpdateGrid();// Обновление данных в таблице.

        }
        private void Exit_Click(object sender, RoutedEventArgs e) // Обработчик события нажатия на кнопку "Exit".
        {
            MenuPage menuPage = new MenuPage();// Создание новой страницы меню.
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);// Получение основного окна приложения.
            mainWindow.Title = menuPage.Title;// Установка заголовка основного окна и навигация на страницу меню.
            mainWindow.MainFrame.Navigate(menuPage);

        }
        private void UpdateGrid()// Метод для обновления данных в таблице.
        {
            SubscriptionsGrid.ItemsSource = _repository.GetList();// Установка источника данных таблицы из репозитория.

        }
        public List<SubscriptionViewModel> GetList()// Метод для получения списка подписок из базы данных.
        {// Использование контекста базы данных.
            using (var context = new Context())
            {
                var items = context.Subscriptions.Include(x => x.Status).Include(x => x.Client).Include(x => x.Subscription_type).ToList();// Получение всех подписок из базы данных включая информацию о статусе, типе подписки, клиентах
                return SubscriptionMapper.Map(items);// Маппинг объектов данных в представление
            }
        }
        public SubscriptionViewModel GetById(long id)// Метод для получения подписок по его идентификатору
        {
            using (var context = new Context())
            {
                var item = context.Subscriptions.FirstOrDefault(x => x.Id == id);// Поиск подписок по идентификатору в базе данных.
                return SubscriptionMapper.Map(item);// Маппинг найденной подписки в представление.
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)// Обработчик события нажатия на кнопку "Add"
        {
            var subscriptionCard = new SubscriptionCardWindow();// Создание окна для добавления нового подписки
            subscriptionCard.ShowDialog();// Отображение окна в виде диалога.
            UpdateGrid();// Обновление данных в таблице после закрытия окна добавления подписок.

        }
        // Обработчик события нажатия на кнопку "Delete".
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SubscriptionsGrid.SelectedItem == null)// Проверка, выбрана ли подписка для удаления.
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }

            var item = SubscriptionsGrid.SelectedItem as SubscriptionViewModel; // Получение выбранного объекта из таблицы
            if (item == null) // Проверка, удалось ли получить данные о подписке
            {
                MessageBox.Show("Не удалось получить данные");
                return;
            }
            // Удаление подписки из репозитория и обновление данных в таблице.
            _repository.Delete(item.Id);
            UpdateGrid();
        }
        // Обработчик события нажатия кнопки "Change"
        private void Change_Click(object sender, RoutedEventArgs e)
        {// Проверка наличия выбранного объекта в таблице.
            if (SubscriptionsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для изменения");
                return;
            }
            // Открытие окна редактирования для выбранного объекта и обновление данных в таблице.
            var subscriptionCard = new SubscriptionCardWindow(SubscriptionsGrid.SelectedItem as SubscriptionViewModel);
            subscriptionCard.ShowDialog();
            UpdateGrid();
        }
        // Метод для поиска подписок по запросу.
        public List<SubscriptionViewModel> Search(string search)
        {// Удаление лишних пробелов и приведение к нижнему регистру.
            search = search.Trim().ToLower();

            using (var context = new Context())
            {// Поиск подписок, чьи даты начала действия содержат введенный запрос и имеют такую же длину, как запрос.
                var result = context.Subscriptions.Include(x => x.Status).Include(x => x.Subscription_type).Where(x => x.ValidityStartDate.Contains(search) && x.ValidityStartDate.Length == search.Length).ToList();
                return SubscriptionMapper.Map(result);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) // Обработчик события нажатия кнопки поиска
        {
            string search = find.Text;
            if (string.IsNullOrEmpty(search))
            {
                SubscriptionsGrid.ItemsSource = _repository.GetList();// Показать все элементы, если запрос пуст.
            }
            else
            {
                List<SubscriptionViewModel> searchResult = _repository.Search(search);// Выполнить поиск по запросу.
                SubscriptionsGrid.ItemsSource = searchResult;// Отобразить результаты поиска.
            }
        }
        private void GenerateButton_Click(object sender, RoutedEventArgs e)  // Обработчик события нажатия кнопки "Generate".
        {
            if (SubscriptionsGrid.SelectedItem != null)
            {// Создание QR-кода для выбранного подписки и его отображение в новом окне.
                var qrManager = new QRManager();
                var qrCodeImage = qrManager.Generate(SubscriptionsGrid.SelectedItem);
                var qrWindow = new QRWindow();
                qrWindow.qrImage.Source = qrCodeImage;
                qrWindow.Show();
            }
            else
            {
                MessageBox.Show("Объект не выбран");
            }
        }
        private void ExportButton_Click(object sender, RoutedEventArgs e)// Обработчик события нажатия кнопки "Export"
        {
            try
            { // Генерация отчета на основе данных из таблицы и сохранение в файле Excel.
                var reportManager = new ReportManager();
                var data = reportManager.GenerateReport(SubscriptionsGrid.ItemsSource as List<SubscriptionViewModel>);

                var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Абонементы_{DateTime.Now.ToShortDateString()}.xlsx");
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

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
    /// Логика взаимодействия для LessonsPage.xaml
    /// </summary>
    public partial class LessonsPage : Page
    {
        private LessonRepository _repository;
        private SubscriptionRepository repository = new SubscriptionRepository();
        public LessonsPage()
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
            _repository = new LessonRepository();// Инициализация репозитория клиентов
            UpdateGrid();// Обновление данных в таблице.

        }
        // Обработчик события нажатия на кнопку "Exit".
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuPage menuPage = new MenuPage();// Создание новой страницы меню.
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);// Получение основного окна приложения.
            mainWindow.Title = menuPage.Title;// Установка заголовка основного окна и навигация на страницу меню.
            mainWindow.MainFrame.Navigate(menuPage);

        }
        // Метод для обновления данных в таблице.
        private void UpdateGrid()
        {
            LessonsGrid.ItemsSource = _repository.GetList();// Установка источника данных таблицы из репозитория.
        }
        // Метод для получения списка занятий из базы данных.
        public List<LessonViewModel> GetList()
        {// Использование контекста базы данных.
            using (var context = new Context())
            {// Получение всех занятий из базы данных включая информацию о зале, тренажерном зале, подписке, программе занятий
                var items = context.Lessons.Include(x => x.Hall).Include(x => x.Gym).Include(x => x.Lesson_programs).Include(x => x.Subscription).ToList();
                return LessonMapper.Map(items);// Маппинг объектов данных в представление
            }
        }
        // Метод для получения занятия по его идентификатору
        public LessonViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Lessons.FirstOrDefault(x => x.Id == id);// Поиск занятия по идентификатору в базе данных.
                return LessonMapper.Map(item); // Маппинг найденного занятия в представление.
            }
        }
        // Обработчик события нажатия на кнопку "Add"
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var lessonCard = new LessonCardWindow();// Создание окна для добавления нового занятия
            lessonCard.ShowDialog();// Отображение окна в виде диалога.
            UpdateGrid();// Обновление данных в таблице после закрытия окна добавления занятия.

        }
        // Обработчик события нажатия на кнопку "Delete".
        private void Delete_Click(object sender, RoutedEventArgs e)
        {// Проверка, выбрано ли занятие для удаления.
            if (LessonsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }
            // Получение выбранного объекта из таблицы
            var item = LessonsGrid.SelectedItem as LessonViewModel;
            // Проверка, удалось ли получить данные
            if (item == null)
            {
                MessageBox.Show("Не удалось получить данные");
                return;
            }
            // Удаление занятия из репозитория и обновление данных в таблице.
            _repository.Delete(item.Id);
            UpdateGrid();
        }
        // Обработчик события нажатия кнопки "Change"
        private void Change_Click(object sender, RoutedEventArgs e)
        {// Проверка наличия выбранного объекта в таблице.
            if (LessonsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для изменения");
                return;
            }
            // Открытие окна редактирования для выбранного объекта и обновление данных в таблице.
            var lessonCard = new LessonCardWindow(LessonsGrid.SelectedItem as LessonViewModel);
            lessonCard.ShowDialog();
            UpdateGrid();
        }
        // Метод для поиска занятия по запросу.
        public List<LessonViewModel> Search(string search)
        {// Удаление лишних пробелов и приведение к нижнему регистру.
            search = search.Trim();

            using (var context = new Context())
            {// Поиск занятий, чьи дата и время содержат введенный запрос и имеют такую же длину, как запрос.
                var result = context.Lessons.Include(x => x.Hall).Include(x => x.Gym).Include(x => x.Lesson_programs).Include(x => x.Subscription.Subscription_type).Where(x => x.DateAndTime.ToLower().Contains(search) && x.DateAndTime.Length == search.Length).ToList();
                return LessonMapper.Map(result);
            }
        }
        // Обработчик события нажатия кнопки поиска
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string search = find.Text;
            if (string.IsNullOrEmpty(search))
            {
                LessonsGrid.ItemsSource = _repository.GetList();// Показать все элементы, если запрос пуст.
            }
            else
            {
                List<LessonViewModel> searchResult = _repository.Search(search);// Выполнить поиск по запросу.
                LessonsGrid.ItemsSource = searchResult;// Отобразить результаты поиска.
            }
        }
        // Обработчик события нажатия кнопки "Generate".
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (LessonsGrid.SelectedItem != null)
            {// Создание QR-кода для выбранного занятия и его отображение в новом окне.
                var qrManager = new QRManager();
                var qrCodeImage = qrManager.Generate(LessonsGrid.SelectedItem);
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
            {// Генерация отчета на основе данных из таблицы и сохранение в файле Excel.
                var reportManager = new ReportManager();
                var data = reportManager.GenerateReport(LessonsGrid.ItemsSource as List<LessonViewModel>);

                var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Занятия_{DateTime.Now.ToShortDateString()}.xlsx");
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

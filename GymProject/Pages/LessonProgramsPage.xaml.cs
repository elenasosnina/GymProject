using GymProject.CardWindows;
using GymProject.Infrastructure;
using GymProject.Infrastructure.Consts;
using GymProject.Infrastructure.DataBase;
using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.QR;
using GymProject.Infrastructure.Report;
using GymProject.Infrastructure.ViewModels;
using GymProject.Windows;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace GymProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для LessonProgramsPage.xaml
    /// </summary>
    public partial class LessonProgramsPage : Page
    {
        private LessonProgramRepository _repository;
        public LessonProgramsPage()
        {
            InitializeComponent();
            // Проверка роли
            if (CurrentUser.PositionName == "Гость" || CurrentUser.PositionName == "Пользователь")//Если user имеет роли Гость или Пользователь, ограничивается доступ к следующим элементам
            {
                add.Visibility = Visibility.Hidden;
                add.IsEnabled = false;
                del.Visibility = Visibility.Hidden;
                del.IsEnabled = false;
                change.Visibility = Visibility.Hidden;
                change.IsEnabled = false;
            }
            _repository = new LessonProgramRepository();// Инициализация репозитория программ занятий
            UpdateGrid();// Обновление данных в таблице.

        }
        // Обработчик события нажатия на кнопку "Exit".
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuPage menuPage = new MenuPage(); // Создание новой страницы меню.
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);// Получение основного окна приложения.
            mainWindow.Title = menuPage.Title;// Установка заголовка основного окна и навигация на страницу меню.
            mainWindow.MainFrame.Navigate(menuPage);

        }
        private void UpdateGrid()
        {
            LessonProgramsGrid.ItemsSource = _repository.GetList();// Установка источника данных таблицы из репозитория.

        }
        // Метод для получения списка программ занятий из базы данных.
        public List<LessonProgramViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.LessonPrograms.ToList();// Получение всех программ занятий из базы данных.
                return LessonProgramMapper.Map(items);// Маппинг объектов данных в представление
            }
        }
        // Метод для получения программ занятий по его идентификатору
        public LessonProgramViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.LessonPrograms.FirstOrDefault(x => x.Id == id);// Поиск программ занятий по идентификатору в базе данных.
                return LessonProgramMapper.Map(item);// Маппинг найденной программы занятий в представление.
            }
        }
        // Обработчик события нажатия на кнопку "Add"
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var lessonProgramCard = new LessonProgramCardWindow();// Создание окна для добавления нового программы занятий
            lessonProgramCard.ShowDialog();// Отображение окна в виде диалога.
            UpdateGrid();// Обновление данных в таблице после закрытия окна добавления программы занятий.

        }
        // Обработчик события нажатия на кнопку "Delete".
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, выбрана ли программа занятий для удаления.
            if (LessonProgramsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }
            // Получение выбранного объекта из таблицы
            var item = LessonProgramsGrid.SelectedItem as LessonProgramViewModel;
            // Проверка, удалось ли получить данные 
            if (item == null)
            {
                MessageBox.Show("Не удалось получить данные");
                return;
            }
            // Удаление программы занятий из репозитория и обновление данных в таблице.
            _repository.Delete(item.Id);
            UpdateGrid();
        }
        // Обработчик события нажатия кнопки "Change"
        private void Change_Click(object sender, RoutedEventArgs e)
        {// Проверка наличия выбранного объекта в таблице.
            if (LessonProgramsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для изменения");
                return;
            }
            // Открытие окна редактирования для выбранного объекта и обновление данных в таблице.
            var lessonProgram = new LessonProgramCardWindow(LessonProgramsGrid.SelectedItem as LessonProgramViewModel);
            lessonProgram.ShowDialog();
            UpdateGrid();
        }
        // Метод для поиска программ занятий по запросу.
        public List<LessonProgramViewModel> Search(string search)
        {// Удаление лишних пробелов и приведение к нижнему регистру.
            search = search.Trim().ToLower();

            using (var context = new Context())
            {// Поиск программ занятий, чьи имена содержат введенный запрос и имеют такую же длину, как запрос.
                var result = context.LessonPrograms.Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return LessonProgramMapper.Map(result);
            }

        }
        // Обработчик события нажатия кнопки поиска
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string search = find.Text;
            if (string.IsNullOrEmpty(search))
            {
                LessonProgramsGrid.ItemsSource = _repository.GetList();// Показать все элементы, если запрос пуст.
            }
            else
            {
                List< LessonProgramViewModel> searchResult = _repository.Search(search);// Выполнить поиск по запросу.
                LessonProgramsGrid.ItemsSource = searchResult;// Отобразить результаты поиска.
            }
        }
        // Обработчик события нажатия кнопки "Generate".
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (LessonProgramsGrid.SelectedItem != null)
            {// Создание QR-кода для выбранной программы занятий и его отображение в новом окне.
                var qrManager = new QRManager();
                var qrCodeImage = qrManager.Generate(LessonProgramsGrid.SelectedItem);
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
                var data = reportManager.GenerateReport(LessonProgramsGrid.ItemsSource as List<LessonProgramViewModel>);

                var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Программы_Занятий_{DateTime.Now.ToShortDateString()}.xlsx");
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

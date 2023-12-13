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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        private ProductRepository _repository;
        public ProductsPage()
        {
            InitializeComponent();
            _repository = new ProductRepository();// Инициализация репозитория товаров
            UpdateGrid();// Обновление данных в таблице.

        }
        private void Exit_Click(object sender, RoutedEventArgs e)// Обработчик события нажатия на кнопку "Exit".
        {
            MenuPage menuPage = new MenuPage();// Создание новой страницы меню.
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);// Получение основного окна приложения.
            mainWindow.Title = menuPage.Title;// Установка заголовка основного окна и навигация на страницу меню.
            mainWindow.MainFrame.Navigate(menuPage);

        }
        private void UpdateGrid()
        {
            ProductsGrid.ItemsSource = _repository.GetList();// Установка источника данных таблицы из репозитория.

        }
        public List<ProductViewModel> GetList() // Метод для получения списка товаров из базы данных.
        {// Использование контекста базы данных.
            using (var context = new Context())
            {
                var items = context.Products.Include(x => x.Product_category).ToList();// Получение всех товаров из базы данных включая его категорию
                return ProductMapper.Map(items);// Маппинг объектов данных в представление
            }
        }
        public ProductViewModel GetById(long id)// Метод для получения товаров по его идентификатору
        {
            using (var context = new Context())
            {
                var item = context.Products.FirstOrDefault(x => x.Id == id);// Поиск товаров по идентификатору в базе данных.
                return ProductMapper.Map(item);// Маппинг найденного товара в представление.
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)// Обработчик события нажатия на кнопку "Add"
        {
            var productCard = new ProductCardWindow();// Создание окна для добавления нового товара
            productCard.ShowDialog();// Отображение окна в виде диалога.
            UpdateGrid(); // Обновление данных в таблице после закрытия окна добавления товаров.

        }
        // Обработчик события нажатия на кнопку "Delete"
        private void Delete_Click(object sender, RoutedEventArgs e)
        {// Проверка, выбран ли товар для удаления.
            if (ProductsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }
            // Получение выбранного объекта из таблицы
            var item = ProductsGrid.SelectedItem as ProductViewModel;
            if (item == null)// Проверка, удалось ли получить данные о товарах
            {
                MessageBox.Show("Не удалось получить данные");
                return;
            }
            // Удаление товаров из репозитория и обновление данных в таблице.
            _repository.Delete(item.Id);
            UpdateGrid();
        }
        // Обработчик события нажатия кнопки "Change"
        private void Change_Click(object sender, RoutedEventArgs e)
        {// Проверка наличия выбранного объекта в таблице.
            if (ProductsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для изменения");
                return;
            }
            // Открытие окна редактирования для выбранного объекта и обновление данных в таблице.
            var productCard = new ProductCardWindow(ProductsGrid.SelectedItem as ProductViewModel);
            productCard.ShowDialog();
            UpdateGrid();
        }
        public List<ProductViewModel> Search(string search) // Метод для поиска товаров по запросу.
        {// Удаление лишних пробелов и приведение к нижнему регистру.
            search = search.Trim().ToLower();

            using (var context = new Context())
            {// Поиск товаров, чьи имена содержат введенный запрос и имеют такую же длину, как запрос.
                var result = context.Products.Include(x => x.Product_category).Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return ProductMapper.Map(result);
            }

        }
        // Обработчик события нажатия кнопки поиска
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string search = find.Text;
            if (string.IsNullOrEmpty(search))
            {
                ProductsGrid.ItemsSource = _repository.GetList();// Показать все элементы, если запрос пуст.
            }
            else
            {
                List<ProductViewModel> searchResult = _repository.Search(search);// Выполнить поиск по запросу.
                ProductsGrid.ItemsSource = searchResult;// Отобразить результаты поиска.
            }
        }
        // Обработчик события нажатия кнопки "Generate".
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {// Создание QR-кода для выбранного товара и его отображение в новом окне.
                var qrManager = new QRManager();
                var qrCodeImage = qrManager.Generate(ProductsGrid.SelectedItem);
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
                var data = reportManager.GenerateReport(ProductsGrid.ItemsSource as List<ProductViewModel>);

                var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Товары_{DateTime.Now.ToShortDateString()}.xlsx");
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

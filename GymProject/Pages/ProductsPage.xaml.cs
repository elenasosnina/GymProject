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
            _repository = new ProductRepository();
            UpdateGrid();

        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuPage menuPage = new MenuPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = menuPage.Title;
            mainWindow.MainFrame.Navigate(menuPage);

        }
        private void UpdateGrid()
        {
            ProductsGrid.ItemsSource = _repository.GetList();

        }
        public List<ProductViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Products.Include(x => x.Product_category).ToList();
                return ProductMapper.Map(items);
            }
        }
        public ProductViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Products.FirstOrDefault(x => x.Id == id);
                return ProductMapper.Map(item);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var productCard = new ProductCardWindow();
            productCard.ShowDialog();
            UpdateGrid();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }

            var item = ProductsGrid.SelectedItem as ProductViewModel;
            if (item == null)
            {
                MessageBox.Show("Не удалось получить данные");
                return;
            }

            _repository.Delete(item.Id);
            UpdateGrid();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для изменения");
                return;
            }

            var productCard = new ProductCardWindow(ProductsGrid.SelectedItem as ProductViewModel);
            productCard.ShowDialog();
            UpdateGrid();
        }
        public List<ProductViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Products.Include(x => x.Product_category).Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return ProductMapper.Map(result);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string search = find.Text;
            if (string.IsNullOrEmpty(search))
            {
                ProductsGrid.ItemsSource = _repository.GetList();
            }
            else
            {
                List<ProductViewModel> searchResult = _repository.Search(search);
                ProductsGrid.ItemsSource = searchResult;
            }
        }
    }
}

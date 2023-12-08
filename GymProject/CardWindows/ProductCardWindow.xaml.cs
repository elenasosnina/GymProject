using GymProject.Infrastructure.DataBase;
using GymProject.Infrastructure.ViewModels;
using GymProject.Infrastructure;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace GymProject.CardWindows
{
    /// <summary>
    /// Логика взаимодействия для ProductCardWindow.xaml
    /// </summary>
    public partial class ProductCardWindow : Window
    {
        public ProductCardWindow()
        {
            InitializeComponent();
            Productcategory.ItemsSource = repository.GetList();
        }
        private ProductViewModel _selectedItem = null;
        private ProductRepository _repository = new ProductRepository();
        private ProductCategoryRepository repository = new ProductCategoryRepository();
        public ProductCardWindow(ProductViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;
            FillFormFields();
        }
        private void FillFormFields()
        {
            if (_selectedItem != null)
            {
                Name.Text = _selectedItem.Name;
                Cost.Text = _selectedItem.Cost.ToString();
                Quantity.Text = _selectedItem.Quantity.ToString();
                ExpirationDate.Text = _selectedItem.ExpirationDate;
                Productcategory.ItemsSource = repository.GetList();
                var result = new List<ProductCategoryViewModel>();
                foreach (ProductCategoryViewModel product in Productcategory.ItemsSource)
                {
                    if (_selectedItem.ProductCategoryId == product.Id)
                    {
                        Productcategory.SelectedItem = product;
                        break;
                    }
                    else
                    {
                        result.Add(product);
                    }
                 Productcategory.SelectedItem = result[0];
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductCategoryViewModel selected = Productcategory.SelectedItem as ProductCategoryViewModel;
                ProductEntity entity = new ProductEntity();
                entity.Name = Name.Text;
                entity.Cost = long.TryParse(Cost.Text, out long cost) ? cost : 1;
                entity.Quantity = long.TryParse(Quantity.Text, out long quantity) ? quantity : 1;
                entity.ExpirationDate = ExpirationDate.Text;
                entity.ProductCategoryId = selected.Id;


                if (_selectedItem != null)
                {
                    entity.Id = _selectedItem.Id;
                    _repository.Update(entity);
                }
                else
                {
                    _repository.Add(entity);
                }

                MessageBox.Show("Запись успешно сохранена.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}


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
            Productcategory.ItemsSource = repository.GetList();// Заполнение списка категории товара в окне
        }
        private ProductViewModel _selectedItem = null;// Переменная для хранения выбранного товара
        private ProductRepository _repository = new ProductRepository();// Репозиторий для работы с товарами
        private ProductCategoryRepository repository = new ProductCategoryRepository();// Репозиторий для работы с категориями товара
        public ProductCardWindow(ProductViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;
            FillFormFields();// Заполнение полей формы выбранными значениями
        }
        private void FillFormFields()
        {
            if (_selectedItem != null)
            {// Заполнение полей формы значениями выбранного элемента
                Name.Text = _selectedItem.Name;
                Cost.Text = _selectedItem.Cost.ToString();
                Quantity.Text = _selectedItem.Quantity.ToString();
                ExpirationDate.Text = _selectedItem.ExpirationDate;
                Productcategory.ItemsSource = repository.GetList();
                var result = new List<ProductCategoryViewModel>();// Заполнение списка категорий товара в окне
                foreach (ProductCategoryViewModel product in Productcategory.ItemsSource)
                {
                    if (_selectedItem.ProductCategoryId == product.Id)
                    {
                        Productcategory.SelectedItem = product;// Установка выбранного элемента в списке категорий товара
                        break;
                    }
                    else
                    {
                        result.Add(product);
                    }
                 Productcategory.SelectedItem = result[0];// Установка первого элемента списка категорий товара по умолчанию
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductCategoryViewModel selected = Productcategory.SelectedItem as ProductCategoryViewModel;// Получение выбранной категории товара
                ProductEntity entity = new ProductEntity();// Создание объекта с данными товара
                entity.Name = Name.Text;
                entity.Cost = long.Parse(Cost.Text);
                entity.Quantity = long.Parse(Quantity.Text);
                entity.ExpirationDate = ExpirationDate.Text;
                if (selected == null)
                {
                    throw new Exception("Не все поля заполнены");// Выброс исключения, если не все поля заполнены
                }
                else
                {
                    entity.ProductCategoryId = selected.Id;// Запись ID выбранной категории товара
                }

                if (_selectedItem != null)
                {
                    entity.Id = _selectedItem.Id;
                    _repository.Update(entity);// Обновление данных товара
                }
                else
                {
                    _repository.Add(entity);// Добавление нового товара
                }

                MessageBox.Show("Запись успешно сохранена.");// Вывод сообщения об успешном сохранении
                this.Close();// Закрытие окна
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);// Вывод сообщения об ошибке
            }
        }
    }
}


using GymProject.Infrastructure;
using GymProject.Infrastructure.DataBase;
using GymProject.Infrastructure.ViewModels;
using GymProject.Infrastructure.Mappers;
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
using System.Runtime.Remoting.Contexts;
using Microsoft.SqlServer.Server;

namespace GymProject.CardWindows
{
    /// <summary>
    /// Логика взаимодействия для ClientCardWindow.xaml
    /// </summary>
    public partial class ClientCardWindow : Window
    {
        private ClientViewModel _selectedItem = null; // Переменная для хранения выбранного элемента
        private ClientRepository _repository = new ClientRepository();// Репозиторий для работы с клиентами
        private DiscountRepository repository = new DiscountRepository();// Репозиторий для работы с скидками


        public ClientCardWindow()
        {
            InitializeComponent();
            Discountt.ItemsSource = repository.GetList();// Заполнение списка скидок в окне

        }

        public ClientCardWindow(ClientViewModel selectedItem)
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
                SecondName.Text = _selectedItem.SecondName;
                MiddleName.Text = _selectedItem.MiddleName;
                DateOfBirth.Text = _selectedItem.DateOfBirth;
                Login.Text = _selectedItem.Login;
                Password.Text = _selectedItem.Password;
                Discountt.ItemsSource = repository.GetList();
                var result = new List<DiscountViewModel>();// Заполнение списка скидок в окне
                foreach (DiscountViewModel discount in Discountt.ItemsSource)
                {
                    if (_selectedItem.DiscountId == discount.Id)
                    {
                        Discountt.SelectedItem = discount;// Установка выбранного элемента в списке скидок
                        break;
                    }
                    else
                    {
                        result.Add(discount);
                    }
                Discountt.SelectedItem = result[0];// Установка первого элемента списка скидок по умолчанию
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                DiscountViewModel selectedDiscount = Discountt.SelectedItem as DiscountViewModel;// Получение выбранной скидки
                ClientEntity entity = new ClientEntity  // Создание объекта с данными клиента
                {
                    Name = Name.Text,
                    SecondName = SecondName.Text,
                    MiddleName = MiddleName.Text,
                    DateOfBirth = DateOfBirth.Text,
                    Login = Login.Text,
                    Password = Password.Text
                };
                if (selectedDiscount == null)
                {
                    throw new Exception("Не все поля заполнены");// Выброс исключения, если не все поля заполнены
                }
                else
                {
                    entity.DiscountId = selectedDiscount.Id;// Запись ID выбранной скидки
                }
                if (_selectedItem != null)
                {
                    entity.Id = _selectedItem.Id;
                    _repository.Update(entity);// Обновление данных клиента
                }
                else
                {
                    _repository.Add(entity);// Добавление нового клиента
                }

                MessageBox.Show("Запись успешно сохранена."); // Вывод сообщения об успешном сохранении
                this.Close(); // Закрытие окна
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Вывод сообщения об ошибке
            }
        }
    }

}





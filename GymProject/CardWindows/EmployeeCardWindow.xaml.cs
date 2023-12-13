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
using System.Windows.Controls.Primitives;

namespace GymProject.CardWindows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeCardWindow.xaml
    /// </summary>
    public partial class EmployeeCardWindow : Window
    { 
        private  EmployeeViewModel _selectedItem = null;// Переменная для хранения выбранного элемента
        private  EmployeeRepository _repository = new EmployeeRepository();// Репозиторий для работы с сотрудниками
        private  PositionRepository repository = new PositionRepository();// Репозиторий для работы с должностями
        public EmployeeCardWindow()
        {
            InitializeComponent();
            Positiion.ItemsSource = repository.GetList();// Заполнение списка должностей в окне
        }
       
        public EmployeeCardWindow(EmployeeViewModel selectedItem)
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
                SurName.Text = _selectedItem.SurName;
                MiddleName.Text = _selectedItem.MiddleName;
                DateOfBirth.Text = _selectedItem.DateOfBirth;
                Login.Text = _selectedItem.Login;
                Password.Text = _selectedItem.Password;
                Gender.Text = _selectedItem.Gender;
                LengthOfService.Text = _selectedItem.LengthOfService.ToString();
                Positiion.ItemsSource = repository.GetList();
                var result = new List<PositionViewModel>();// Заполнение списка должностей в окне
                foreach (PositionViewModel discount in Positiion.ItemsSource)
                {
                    if (_selectedItem.Position.Title == discount.Title)
                    {
                        Positiion.SelectedItem = discount;// Установка выбранного элемента в списке должностей
                        break;
                    }
                    else
                    {
                        result.Add(discount);
                    }
                    Positiion.SelectedItem = result[0];// Установка первого элемента списка должностей по умолчанию
                }
                
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PositionViewModel selected = Positiion.SelectedItem as PositionViewModel;// Получение выбранной должности
                EmployeeEntity entity = new EmployeeEntity // Создание объекта с данными сотрудника
                {
                    Name = Name.Text,
                    SurName = SurName.Text,
                    MiddleName = MiddleName.Text,
                    DateOfBirth = DateOfBirth.Text,
                    Login = Login.Text,
                    Password = Password.Text,
                    Gender = Gender.Text,
                    LengthOfService = decimal.Parse(LengthOfService.Text),
                    PositionId = selected.Id// Запись ID выбранной должности
                };


                if (_selectedItem != null)
                {
                    entity.Id = _selectedItem.Id;
                    _repository.Update(entity);// Обновление данных сотрудника
                }
                else
                {
                    _repository.Add(entity);// Добавление нового сотрудника
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

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
        private EmployeeViewModel _selectedItem = null;
        private EmployeeRepository _repository = new EmployeeRepository();
        private PositionRepository repository = new PositionRepository();
        public EmployeeCardWindow()
        {
            InitializeComponent();
            Positiion.ItemsSource = repository.GetList();
        }
       
        public EmployeeCardWindow(EmployeeViewModel selectedItem)
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
                SurName.Text = _selectedItem.SurName;
                MiddleName.Text = _selectedItem.MiddleName;
                DateOfBirth.Text = _selectedItem.DateOfBirth;
                Login.Text = _selectedItem.Login;
                Password.Text = _selectedItem.Password;
                Gender.Text = _selectedItem.Gender;
                LengthOfService.Text = _selectedItem.LengthOfService.ToString();
                Positiion.ItemsSource = repository.GetList();
                var result = new List<PositionViewModel>();
                foreach (PositionViewModel discount in Positiion.ItemsSource)
                {
                    if (_selectedItem.Position.Title == discount.Title)
                    {
                        Positiion.SelectedItem = discount;
                        break;
                    }
                    else
                    {
                        result.Add(discount);
                    }
                    Positiion.SelectedItem = result[0];
                }
                
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PositionViewModel selected = Positiion.SelectedItem as PositionViewModel;
                EmployeeEntity entity = new EmployeeEntity();
                entity.Name = Name.Text;
                entity.SurName = SurName.Text;
                entity.MiddleName = MiddleName.Text;
                entity.DateOfBirth = DateOfBirth.Text;
                entity.Login = Login.Text;
                entity.Password = Password.Text;
                entity.Gender = Gender.Text;
                entity.LengthOfService = decimal.TryParse(LengthOfService.Text, out decimal lengthOfService) ? lengthOfService : 1;
                entity.PositionId = selected.Id;


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

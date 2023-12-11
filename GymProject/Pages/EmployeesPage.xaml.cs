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

namespace GymProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        private EmployeeRepository _repository;
        public EmployeesPage()
        {
            InitializeComponent();
            _repository = new EmployeeRepository();
            UpdateGrid();


        }
        private void UpdateGrid()
        {
            EmployeesGrid.ItemsSource = _repository.GetList();

        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuPage menuPage = new MenuPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = menuPage.Title;
            mainWindow.MainFrame.Navigate(menuPage);

        }

        public List<EmployeeViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Employees.Include(x => x.Position).ToList();
                return EmployeeMapper.Map(items);
            }
        }
        public EmployeeViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Employees.FirstOrDefault(x => x.Id == id);
                return EmployeeMapper.Map(item);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var employeeCard = new EmployeeCardWindow();
            employeeCard.ShowDialog();
            UpdateGrid();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }

            var item = EmployeesGrid.SelectedItem as EmployeeViewModel;
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
            if (EmployeesGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для изменения");
                return;
            }

            var clientCard = new EmployeeCardWindow(EmployeesGrid.SelectedItem as EmployeeViewModel);
            clientCard.ShowDialog();
            UpdateGrid();
        }
        public List<EmployeeViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Employees.Include(x => x.Position).Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return EmployeeMapper.Map(result);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string search = find.Text;
            if (string.IsNullOrEmpty(search))
            {
                UpdateGrid();
            }
            else
            {
                List<EmployeeViewModel> searchResult = _repository.Search(search);
                EmployeesGrid.ItemsSource = searchResult;
            }
        }
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem != null)
            {
                var qrManager = new QRManager();
                var qrCodeImage = qrManager.Generate(EmployeesGrid.SelectedItem);
                var qrWindow = new QRWindow();
                qrWindow.qrImage.Source = qrCodeImage;
                qrWindow.Show();
            }
            else
            {
                MessageBox.Show("Объект не выбран");
            }
        }
    }
}

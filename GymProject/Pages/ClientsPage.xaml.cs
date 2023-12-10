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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        private ClientRepository _repository;
        public ClientsPage()
        {
            InitializeComponent();
            _repository = new ClientRepository();
            UpdateGrid();
        }
        private void UpdateGrid()
        {
          ClientsGrid.ItemsSource = _repository.GetList();
        }

        public List<ClientViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Clients.Include(x => x.Discount).ToList();
                return ClientMapper.Map(items);
            }
        }
        public ClientViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Clients.FirstOrDefault(x => x.Id == id);
                return ClientMapper.Map(item);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuPage menuPage = new MenuPage();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Title = menuPage.Title;
            mainWindow.MainFrame.Navigate(menuPage);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var clientCard = new ClientCardWindow();
            clientCard.ShowDialog();
            UpdateGrid();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }

            var item = ClientsGrid.SelectedItem as ClientViewModel;
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
            if (ClientsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для изменения"); 
                return;
            }
                
            var clientCard = new ClientCardWindow(ClientsGrid.SelectedItem as ClientViewModel);
            clientCard.ShowDialog();
            UpdateGrid();
        }
        public List<ClientEntity> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Clients.Include(x => x.Discount).Where(x => x.Name.Contains(search) && x.Name.Length == search.Length).ToList();
                return result;
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string search = find.Text;
            if (string.IsNullOrEmpty(search))
            {
                ClientsGrid.ItemsSource = _repository.GetList(); 
            }
            else
            {
                List<ClientEntity> searchResult = _repository.Search(search);
                ClientsGrid.ItemsSource = searchResult;
            }
        }
    }
}

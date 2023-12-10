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
using GymProject.Infrastructure.Consts;
using System.Security.Cryptography;

namespace GymProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для SubscriptionsPage.xaml
    /// </summary>
    public partial class SubscriptionsPage : Page
    {
        private SubscriptionRepository _repository;
        public SubscriptionsPage()
        {
            InitializeComponent();
            if (CurrentUser.PositionName == "Гость" || CurrentUser.PositionName == "Пользователь")
            {
                add.Visibility = Visibility.Hidden;
                add.IsEnabled = false;
                del.Visibility = Visibility.Hidden;
                del.IsEnabled = false;
                change.Visibility = Visibility.Hidden;
                change.IsEnabled = false;
            }
            _repository = new SubscriptionRepository();
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
            SubscriptionsGrid.ItemsSource = _repository.GetList();

        }
        public List<SubscriptionViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Subscriptions.Include(x => x.Status).Include(x => x.Client).Include(x => x.Subscription_type).ToList();
                return SubscriptionMapper.Map(items);
            }
        }
        public SubscriptionViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Subscriptions.FirstOrDefault(x => x.Id == id);
                return SubscriptionMapper.Map(item);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var subscriptionCard = new SubscriptionCardWindow();
            subscriptionCard.ShowDialog();
            UpdateGrid();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SubscriptionsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }

            var item = SubscriptionsGrid.SelectedItem as SubscriptionViewModel;
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
            if (SubscriptionsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для изменения");
                return;
            }

            var subscriptionCard = new SubscriptionCardWindow(SubscriptionsGrid.SelectedItem as SubscriptionViewModel);
            subscriptionCard.ShowDialog();
            UpdateGrid();
        }
        public List<SubscriptionEntity> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Subscriptions.Include(x => x.Client).Include(x => x.Status).Include(x => x.Subscription_type).Where(x => x.ValidityStartDate.Contains(search) && x.ValidityStartDate.Length == search.Length).ToList();
                return result;
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string search = find.Text;
            if (string.IsNullOrEmpty(search))
            {
                SubscriptionsGrid.ItemsSource = _repository.GetList();
            }
            else
            {
                List<SubscriptionEntity> searchResult = _repository.Search(search);
                SubscriptionsGrid.ItemsSource = searchResult;
            }
        }
    }
}

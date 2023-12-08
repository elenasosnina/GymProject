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
    /// Логика взаимодействия для SubscriptionCardWindow.xaml
    /// </summary>
    public partial class SubscriptionCardWindow : Window
    {
          private StatusRepository status_repository = new StatusRepository();
        public SubscriptionCardWindow()
        {
            InitializeComponent();
            status.ItemsSource = status_repository.GetList();
            client.ItemsSource = client_repository.GetList(); 
            subscription_type.ItemsSource = subscription_type_repository.GetList();
        }
        private SubscriptionViewModel _selectedItem = null;
        private SubscriptionRepository _repository = new SubscriptionRepository();
        private SubscriptionTypeRepository subscription_type_repository = new SubscriptionTypeRepository();
        private ClientRepository client_repository = new ClientRepository();
     
        public SubscriptionCardWindow(SubscriptionViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;
            FillFormFields();
        }
        private void FillFormFields()
        {
            if (_selectedItem != null)
            {
                ValidityStartDate.Text = _selectedItem.ValidityStartDate;
                ValidityExpirationDate.Text = _selectedItem.ValidityExpirationDate;

                client.ItemsSource = client_repository.GetList();
                var sub = new List<ClientViewModel>();
                foreach (ClientViewModel discount in client.ItemsSource)
                {
                    if (_selectedItem.ClientId == discount.Id)
                    {
                        client.SelectedItem = discount;
                        break;
                    }
                    else
                    {
                        sub.Add(discount);
                    }
                    client.SelectedItem = sub[0];
                }
                status.ItemsSource = status_repository.GetList();
                var result = new List<StatusViewModel>();
                foreach (StatusViewModel item in status.ItemsSource)
                {
                    if (_selectedItem.StatusId == item.Id)
                    {
                        status.SelectedItem = item;
                        break;
                    }
                    else
                    {
                        result.Add(item);
                    }
                    status.SelectedItem = result[0];
                }

                subscription_type.ItemsSource = subscription_type_repository.GetList();
                var type = new List<SubscriptionTypeViewModel>();
                foreach (SubscriptionTypeViewModel item in subscription_type.ItemsSource)
                {
                    if (_selectedItem.SubscriptionTypeId == item.Id)
                    {
                        subscription_type.SelectedItem = item;
                        break;
                    }
                    else
                    {
                        type.Add(item);
                    }
                    subscription_type.SelectedItem = type[0];
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientViewModel selectedClient = client.SelectedItem as ClientViewModel;
                SubscriptionTypeViewModel selectedSubscriptionType = subscription_type.SelectedItem as SubscriptionTypeViewModel;
                StatusViewModel selectedStatus = status.SelectedItem as StatusViewModel;
                SubscriptionEntity entity = new SubscriptionEntity();
                entity.ValidityStartDate = ValidityStartDate.Text;
                entity.ClientId = selectedClient.Id;
                entity.ValidityExpirationDate = ValidityExpirationDate.Text;
                entity.SubscriptionTypeId = selectedSubscriptionType.Id;
                entity.StatusId = selectedStatus.Id;
                


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

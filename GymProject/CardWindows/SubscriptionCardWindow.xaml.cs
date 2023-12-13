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
            status.ItemsSource = status_repository.GetList();// Заполнение списка статуса в окне
            client.ItemsSource = client_repository.GetList(); // Заполнение списка клиентов в окне
            subscription_type.ItemsSource = subscription_type_repository.GetList();// Заполнение списка типа подписки в окне
        }
        private SubscriptionViewModel _selectedItem = null;// Переменная для хранения выбранного элемента
        private SubscriptionRepository _repository = new SubscriptionRepository();// Репозиторий для работы с подписками
        private SubscriptionTypeRepository subscription_type_repository = new SubscriptionTypeRepository();// Репозиторий для работы с типами подписок
        private ClientRepository client_repository = new ClientRepository();// Репозиторий для работы с клиентами

        public SubscriptionCardWindow(SubscriptionViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;
            FillFormFields();// Заполнение полей формы выбранными значениями
        }
        private void FillFormFields()
        {
            if (_selectedItem != null)
            {// Заполнение полей формы значениями выбранного элемента
                ValidityStartDate.Text = _selectedItem.ValidityStartDate;
                ValidityExpirationDate.Text = _selectedItem.ValidityExpirationDate;

                client.ItemsSource = client_repository.GetList();
                var sub = new List<ClientViewModel>();// Заполнение списка клиентов в окне
                foreach (ClientViewModel item in client.ItemsSource)
                {
                    if (_selectedItem.ClientId == item.Id)
                    {
                        client.SelectedItem = item;// Установка выбранного элемента в списке клиентов
                        break;
                    }
                    else
                    {
                        sub.Add(item);
                    }
                    client.SelectedItem = sub[0];// Установка первого элемента списка скидок по умолчанию
                }
                status.ItemsSource = status_repository.GetList();
                var result = new List<StatusViewModel>();// Заполнение списка статуса в окне
                foreach (StatusViewModel item in status.ItemsSource)
                {
                    if (_selectedItem.StatusId == item.Id)
                    {
                        status.SelectedItem = item;// Установка выбранного элемента в списке статуса
                        break;
                    }
                    else
                    {
                        result.Add(item);
                    }
                    status.SelectedItem = result[0];// Установка первого элемента списка статуса по умолчанию
                }

                subscription_type.ItemsSource = subscription_type_repository.GetList();
                var type = new List<SubscriptionTypeViewModel>();// Заполнение списка типа подписок в окне
                foreach (SubscriptionTypeViewModel item in subscription_type.ItemsSource)
                {
                    if (_selectedItem.SubscriptionTypeId == item.Id)
                    {
                        subscription_type.SelectedItem = item;// Установка выбранного элемента в списке типа подписок
                        break;
                    }
                    else
                    {
                        type.Add(item);
                    }
                    subscription_type.SelectedItem = type[0];// Установка первого элемента списка типа подписок по умолчанию
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClientViewModel selectedClient = client.SelectedItem as ClientViewModel;// Получение выбранного клиента
                SubscriptionTypeViewModel selectedSubscriptionType = subscription_type.SelectedItem as SubscriptionTypeViewModel;// Получение выбранного типа подписок
                StatusViewModel selectedStatus = status.SelectedItem as StatusViewModel;// Получение выбранного статуса
                SubscriptionEntity entity = new SubscriptionEntity();// Создание объекта с данными подписки
                entity.ValidityStartDate = ValidityStartDate.Text;
                entity.ValidityExpirationDate = ValidityExpirationDate.Text;

                if (selectedSubscriptionType == null || selectedStatus == null || selectedClient == null)
                {
                    throw new Exception("Не все поля заполнены");// Выброс исключения, если не все поля заполнены
                }
                else
                {
                    entity.SubscriptionTypeId = selectedSubscriptionType.Id;// Запись ID выбранного типа подписки
                    entity.StatusId = selectedStatus.Id;  // Запись ID выбранного статуса
                    entity.ClientId = selectedClient.Id;// Запись ID выбранного клиента
                }
                if (_selectedItem != null)
                {
                    entity.Id = _selectedItem.Id;
                    _repository.Update(entity);// Обновление данных подписки
                }
                else
                {
                    _repository.Add(entity);// Добавление новой подписки
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

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
    /// Логика взаимодействия для LessonCardWindow.xaml
    /// </summary>
    public partial class LessonCardWindow : Window
    {
        public LessonCardWindow()
        {
            InitializeComponent();
            gym.ItemsSource = gym_repository.GetList();
            hall.ItemsSource = hall_repository.GetList();
            subscription.ItemsSource = subscription_repository.GetList();
            lesson_program.ItemsSource = program_repository.GetList();

        }
        private LessonViewModel _selectedItem = null;
        private LessonRepository _repository = new LessonRepository();
        private LessonProgramRepository program_repository = new LessonProgramRepository();
        private HallRepository hall_repository = new HallRepository();
        private GymRepository gym_repository = new GymRepository();
        private SubscriptionRepository subscription_repository = new SubscriptionRepository();



        public LessonCardWindow(LessonViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;
            FillFormFields();
        }
        private void FillFormFields()
        {
            if (_selectedItem != null)
            {
                DateAndTime.Text = _selectedItem.DateAndTime;

                gym.ItemsSource = gym_repository.GetList();
                var result = new List<GymViewModel>();
                foreach (GymViewModel item in gym.ItemsSource)
                {
                    if (_selectedItem.GymId == item.Id)
                    {
                        gym.SelectedItem = item;
                        break;
                    }
                    else
                    {
                        result.Add(item);
                    }
                    gym.SelectedItem = result[0];
                }

                hall.ItemsSource = hall_repository.GetList();
                var list = new List<HallViewModel>();
                foreach (HallViewModel item in hall.ItemsSource)
                {
                    if (_selectedItem.HallId == item.Id)
                    {
                        hall.SelectedItem = item;
                        break;
                    }
                    else
                    {
                        list.Add(item);
                    }
                    hall.SelectedItem = list[0];
                }

                subscription.ItemsSource = subscription_repository.GetList();
                var sub = new List<SubscriptionViewModel>();
                foreach (SubscriptionViewModel discount in subscription.ItemsSource)
                {
                    if (_selectedItem.SubscriptionId == discount.Id)
                    {
                        subscription.SelectedItem = discount;
                        break;
                    }
                    else
                    {
                        sub.Add(discount);
                    }
                    subscription.SelectedItem = sub[0];
                }
                lesson_program.ItemsSource = program_repository.GetList();
                var lesson = new List<LessonProgramViewModel>();
                foreach (LessonProgramViewModel item in lesson_program.ItemsSource)
                {
                    if (_selectedItem.ProgramId == item.Id)
                    {
                        lesson_program.SelectedItem = item;
                        break;
                    }
                    else
                    {
                        lesson.Add(item);
                    }
                    lesson_program.SelectedItem = lesson[0];
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GymViewModel selectedGym = gym.SelectedItem as GymViewModel;
                HallViewModel selectedHall = hall.SelectedItem as HallViewModel;
                SubscriptionViewModel selectedSubscription = subscription.SelectedItem as SubscriptionViewModel;
                LessonProgramViewModel selectedLessonProgram = lesson_program.SelectedItem as LessonProgramViewModel;
                LessonEntity entity = new LessonEntity();
                entity.GymId = selectedGym.Id;
                entity.DateAndTime = DateAndTime.Text;
                entity.HallId = selectedHall.Id;
                entity.SubscriptionId = selectedSubscription.Id;
                entity.ProgramId = selectedLessonProgram.Id;


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

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
            gym.ItemsSource = gym_repository.GetList(); // Заполнение списка залов в окне
            hall.ItemsSource = hall_repository.GetList(); // Заполнение списка тренажерных залов в окне
            subscription.ItemsSource = subscription_repository.GetList(); // Заполнение списка подписок в окне
            lesson_program.ItemsSource = program_repository.GetList(); // Заполнение списка программ тренировок в окне

        }
        private LessonViewModel _selectedItem = null; // Переменная для хранения выбранного элемента
        private LessonRepository _repository = new LessonRepository(); // Репозиторий для работы с уроками
        private LessonProgramRepository program_repository = new LessonProgramRepository(); // Репозиторий для работы с программами тренировок
        private HallRepository hall_repository = new HallRepository(); // Репозиторий для работы с залами
        private GymRepository gym_repository = new GymRepository(); // Репозиторий для работы с тренажерными залами
        private SubscriptionRepository subscription_repository = new SubscriptionRepository(); // Репозиторий для работы с подписками



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
                DateAndTime.Text = _selectedItem.DateAndTime; // Установка значения даты и времени занятия

                gym.ItemsSource = gym_repository.GetList(); // Заполнение списка залов в окне
                var result = new List<GymViewModel>();
                foreach (GymViewModel item in gym.ItemsSource)
                {
                    if (_selectedItem.GymId == item.Id)
                    {
                        gym.SelectedItem = item; // Установка выбранного элемента в списке залов
                        break;
                    }
                    else
                    {
                        result.Add(item);
                    }
                    gym.SelectedItem = result[0];
                }

                hall.ItemsSource = hall_repository.GetList(); // Заполнение списка тренажерных залов в окне
                var list = new List<HallViewModel>();
                foreach (HallViewModel item in hall.ItemsSource)
                {
                    if (_selectedItem.HallId == item.Id)
                    {
                        hall.SelectedItem = item; // Установка выбранного элемента в списке тренажерных залов
                        break;
                    }
                    else
                    {
                        list.Add(item);
                    }
                    hall.SelectedItem = list[0];
                }

                subscription.ItemsSource = subscription_repository.GetList(); // Заполнение списка подписок в окне
                var sub = new List<SubscriptionViewModel>();
                foreach (SubscriptionViewModel discount in subscription.ItemsSource)
                {
                    if (_selectedItem.SubscriptionId == discount.Id)
                    {
                        subscription.SelectedItem = discount; // Установка выбранного элемента в списке подписок
                        break;
                    }
                    else
                    {
                        sub.Add(discount);
                    }
                    subscription.SelectedItem = sub[0];
                }
                lesson_program.ItemsSource = program_repository.GetList(); // Заполнение списка программ тренировок в окне
                var lesson = new List<LessonProgramViewModel>();
                foreach (LessonProgramViewModel item in lesson_program.ItemsSource)
                {
                    if (_selectedItem.ProgramId == item.Id)
                    {
                        lesson_program.SelectedItem = item; // Установка выбранного элемента в списке программ тренировок
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
                GymViewModel selectedGym = gym.SelectedItem as GymViewModel;// Получение выбранного зала
                HallViewModel selectedHall = hall.SelectedItem as HallViewModel;// Получение выбранного тренажерного зала
                SubscriptionViewModel selectedSubscription = subscription.SelectedItem as SubscriptionViewModel;// Получение выбранной подписки
                LessonProgramViewModel selectedLessonProgram = lesson_program.SelectedItem as LessonProgramViewModel;// Получение выбранной программы тренировок
                LessonEntity entity = new LessonEntity
                {
                    DateAndTime = DateAndTime.Text// Создание нового объекта LessonEntity и установка значения даты и времени занятия
                };

                if (selectedLessonProgram == null || selectedGym == null || selectedHall == null || selectedSubscription == null)
                {
                    throw new Exception("Не все поля заполнены");// Если не все поля заполнены, выбрасываем исключение
                }
                else
                {
                    entity.GymId = selectedGym.Id;// Установка идентификатора выбранного зала
                    entity.HallId = selectedHall.Id;// Установка идентификатора выбранного тренажерного зала
                    entity.SubscriptionId = selectedSubscription.Id;// Установка идентификатора выбранной подписки
                    entity.ProgramId = selectedLessonProgram.Id;// Установка идентификатора выбранной программы тренировок
                }
                if (_selectedItem != null)
                {
                    entity.Id = _selectedItem.Id;
                    _repository.Update(entity);// Обновление данных занятия
                }
                else
                {
                    _repository.Add(entity);// Добавление нового занятия
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

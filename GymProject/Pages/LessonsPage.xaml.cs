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
    /// Логика взаимодействия для LessonsPage.xaml
    /// </summary>
    public partial class LessonsPage : Page
    {
        private LessonRepository _repository;
        private SubscriptionRepository repository = new SubscriptionRepository();
        public LessonsPage()
        {
            InitializeComponent();
            _repository = new LessonRepository();
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
            LessonsGrid.ItemsSource = _repository.GetList();


        }
        public List<LessonViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Lessons.Include(x => x.Hall).Include(x => x.Gym).Include(x => x.Lesson_programs).Include(x => x.Subscription).Include(x => x.Subscription.Client).ToList();
                return LessonMapper.Map(items);
            }
        }
        public LessonViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Lessons.FirstOrDefault(x => x.Id == id);
                return LessonMapper.Map(item);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var lessonCard = new LessonCardWindow();
            lessonCard.ShowDialog();
            UpdateGrid();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (LessonsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }

            var item = LessonsGrid.SelectedItem as LessonViewModel;
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
            if (LessonsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для изменения");
                return;
            }

            var lessonCard = new LessonCardWindow(LessonsGrid.SelectedItem as LessonViewModel);
            lessonCard.ShowDialog();
            UpdateGrid();
        }
    }
}

using GymProject.CardWindows;
using GymProject.Infrastructure;
using GymProject.Infrastructure.Consts;
using GymProject.Infrastructure.DataBase;
using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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

namespace GymProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для LessonProgramsPage.xaml
    /// </summary>
    public partial class LessonProgramsPage : Page
    {
        private LessonProgramRepository _repository;
        public LessonProgramsPage()
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
            _repository = new LessonProgramRepository();
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
            LessonProgramsGrid.ItemsSource = _repository.GetList();

        }
        public List<LessonProgramViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.LessonPrograms.ToList();
                return LessonProgramMapper.Map(items);
            }
        }
        public LessonProgramViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.LessonPrograms.FirstOrDefault(x => x.Id == id);
                return LessonProgramMapper.Map(item);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var lessonProgramCard = new LessonProgramCardWindow();
            lessonProgramCard.ShowDialog();
            UpdateGrid();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (LessonProgramsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для удаления");
                return;
            }

            var item = LessonProgramsGrid.SelectedItem as LessonProgramViewModel;
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
            if (LessonProgramsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбран объект для изменения");
                return;
            }

            var lessonProgram = new LessonProgramCardWindow(LessonProgramsGrid.SelectedItem as LessonProgramViewModel);
            lessonProgram.ShowDialog();
            UpdateGrid();
        }
        public List<LessonProgramViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.LessonPrograms.Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return LessonProgramMapper.Map(result);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string search = find.Text;
            if (string.IsNullOrEmpty(search))
            {
                LessonProgramsGrid.ItemsSource = _repository.GetList();
            }
            else
            {
                List< LessonProgramViewModel> searchResult = _repository.Search(search);
                LessonProgramsGrid.ItemsSource = searchResult;
            }
        }
    }
}

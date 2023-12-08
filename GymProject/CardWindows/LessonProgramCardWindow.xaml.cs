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
    /// Логика взаимодействия для LessonProgramCardWindow.xaml
    /// </summary>
    public partial class LessonProgramCardWindow : Window
    {
        public LessonProgramCardWindow()
        {
            InitializeComponent();
        }
        private LessonProgramViewModel _selectedItem = null;
        private LessonProgramRepository _repository = new LessonProgramRepository();
        public LessonProgramCardWindow(LessonProgramViewModel selectedItem)
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
                Description.Text = _selectedItem.Description;
                ProgramDuration.Text = _selectedItem.ProgramDuration.ToString();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _repository = new LessonProgramRepository();
                LessonProgramEntity entity = new LessonProgramEntity();
                entity.Name = Name.Text;
                entity.Description = Description.Text;
                entity.ProgramDuration = decimal.TryParse(ProgramDuration.Text, out decimal programDuration) ? programDuration : 1;



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

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
        private LessonProgramViewModel _selectedItem = null;// Переменная для хранения выбранного элемента
        private LessonProgramRepository _repository = new LessonProgramRepository();// Репозиторий для работы с программами занятий
        public LessonProgramCardWindow(LessonProgramViewModel selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;
            FillFormFields();// Заполнение полей формы выбранными значениями
        }
        private void FillFormFields()
        {
            if (_selectedItem != null)
            {// Заполнение полей формы значениями выбранного элемента
                Name.Text = _selectedItem.Name;
                Description.Text = _selectedItem.Description;
                ProgramDuration.Text = _selectedItem.ProgramDuration.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LessonProgramEntity entity = new LessonProgramEntity();// Создание объекта с данными программ занятий
                entity.Name = Name.Text;
                entity.Description = Description.Text;
                entity.ProgramDuration = decimal.Parse(ProgramDuration.Text);



                if (_selectedItem != null)
                {
                    entity.Id = _selectedItem.Id;
                    _repository.Update(entity);// Обновление данных программы занятий
                }
                else
                {
                    _repository.Add(entity);// Добавление новой программы занятий
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

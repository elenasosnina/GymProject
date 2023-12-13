using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.ViewModels
{
    public partial class LessonViewModel// Класс представления данных занятий.
    {
        public long Id { get; set; }
        public string DateAndTime { get; set; }
        public long GymId { get; set; }
        public GymViewModel Gym { get; set; }
        public long ProgramId { get; set; }
        public LessonProgramViewModel Lesson_program { get; set; }
        public long SubscriptionId { get; set; }
        public SubscriptionViewModel Subscription { get; set; }
        public long HallId { get; set; }
        public HallViewModel Hall { get; set; }
    }
}

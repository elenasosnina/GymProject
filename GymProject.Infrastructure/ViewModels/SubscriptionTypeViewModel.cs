using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.ViewModels
{
    public partial class SubscriptionTypeViewModel// Класс представления данных типа абонемента.
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Duration { get; set; }
        public decimal NumberOfClasses { get; set; }
        public string DateAndTimeOfPurchase { get; set; }
        public decimal Cost { get; set; }
    }
}

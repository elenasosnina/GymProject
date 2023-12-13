using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.ViewModels
{
    public partial class SubscriptionViewModel// Класс представления данных абонемента.
    {
        public long Id { get; set; }
        public string ValidityStartDate { get; set; }
        public string ValidityExpirationDate { get; set; }
        public long ClientId { get; set; }
        public ClientViewModel Client { get; set; }
        public long StatusId { get; set; }
        public StatusViewModel Status { get; set; }
        public long SubscriptionTypeId { get; set; }
        public SubscriptionTypeViewModel Subscription_type { get; set; }
    }
}

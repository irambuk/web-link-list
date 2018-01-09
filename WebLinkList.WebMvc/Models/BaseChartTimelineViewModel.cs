using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLinkList.WebMvc.Models
{
    public class BaseChartTimelineViewModel
    {
        public List<UsageDropDownViewModel> UsageDropDownViewModels { get; set; } = new List<UsageDropDownViewModel>();

        public UsageDropDownViewModel SelectedUsageDropDownViewModel { get; set; }

        public List<UsageDataPerUnitViewModel> UsageData { get; set; } = new List<UsageDataPerUnitViewModel>();

        public IList<DateTime> UsageTimelineDates
        {
            get
            {
                var dates = new List<DateTime>();

                if (SelectedUsageDropDownViewModel == null)
                {
                    return dates;
                }

                var startDate = SelectedUsageDropDownViewModel.GetCalculatedStartDate();
                var currentDate = startDate;

                do
                {
                    dates.Add(currentDate);

                    currentDate = currentDate.AddDays(1);

                } while (currentDate <= DateTime.Today.AddDays(1));


                return dates;
            }
        }

        public IList<string> UsageTimelineNames
        {
            get
            {
                return UsageTimelineDates.Select(d => d.ToShortDateString()).ToList();
            }
        }

        public BaseChartTimelineViewModel(UsageDropDownTypes type)
        {
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.LastWeek });
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.LastMonth });
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.Last3Months });
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.Last6Months });
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.LastYear });
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.All });

            SelectedUsageDropDownViewModel = UsageDropDownViewModels.First(vm => vm.Type == type);
        }
    }
}

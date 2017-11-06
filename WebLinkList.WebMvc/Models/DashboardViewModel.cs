using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebLinkList.WebMvc.Models
{
    public class DashboardViewModel
    {
        public List<UsageDropDownViewModel> UsageDropDownViewModels { get; set; } = new List<UsageDropDownViewModel>();

        public UsageDropDownViewModel SelectedUsageDropDownViewModel { get; set; }

        public List<UsageDataPerCategoryViewModel> UsageDataPerCategoryViewModels = new List<UsageDataPerCategoryViewModel>();

        public DashboardViewModel()
        {
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.LastWeek });
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.LastMonth });
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.Last3Months });
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.Last6Months });
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.LastYear });
            UsageDropDownViewModels.Add(new UsageDropDownViewModel { Type = UsageDropDownTypes.All });

            SelectedUsageDropDownViewModel = UsageDropDownViewModels.First(vm => vm.Type == UsageDropDownTypes.LastWeek);
        }

        public void SelectType(UsageDropDownTypes type) {
            var viewModel = UsageDropDownViewModels.FirstOrDefault(vm => vm.Type == type);
            SelectedUsageDropDownViewModel = viewModel ?? throw new NotSupportedException($"Given type {type} is not supported");
        }

        public DateTime GetCalculatedStartDate(UsageDropDownTypes type)
        {
            var now = DateTime.Now;

            switch (type)
            {
                case UsageDropDownTypes.LastWeek:
                    return now.Subtract(new TimeSpan(7, 0, 0, 0));
                case UsageDropDownTypes.LastMonth:
                    return now.Subtract(new TimeSpan(30, 0, 0, 0));
                case UsageDropDownTypes.Last3Months:
                    return now.Subtract(new TimeSpan(90, 0, 0, 0));
                case UsageDropDownTypes.Last6Months:
                    return now.Subtract(new TimeSpan(180, 0, 0, 0));
                case UsageDropDownTypes.LastYear:
                    return now.Subtract(new TimeSpan(365, 0, 0, 0));
                case UsageDropDownTypes.All:
                    return DateTime.MinValue;
                default:
                    throw new NotSupportedException($"Given UsageDropDownTypes {type} is not supported");
            }
        }
    }

    public class UsageDropDownViewModel {
        public UsageDropDownTypes Type { get; set; }
        public string Name { get {
                return Type
                .GetType()
                .GetMember(Type.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description;
            }
        }
    }

    public enum UsageDropDownTypes {
        [Description("Last Week")]
        LastWeek = 0,
        [Description("Last Month")]
        LastMonth = 1,
        [Description("Last 3 Months")]
        Last3Months = 2,
        [Description("Last 6 Months")]
        Last6Months = 3,
        [Description("Last Year")]
        LastYear = 4,
        [Description("All")]
        All = 5
    }

    public class UsageDataPerCategoryViewModel {
        public string CategoryName { get; set; }

        public int NoOfVisits { get; set; }

        public Color SelectedColor { get; set; }

        public string BackgroundColor {
            get
            {
                return $"rgba({SelectedColor.R},{SelectedColor.G},{SelectedColor.B},1)";
            }
        }

        public string BoarderColor
        {
            get
            {
                return $"rgba({SelectedColor.R},{SelectedColor.G},{SelectedColor.B},1)";
            }
        }

        public UsageDataPerCategoryViewModel() {
            SelectedColor = Color.Aqua;
        }

    }
}

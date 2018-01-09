using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebLinkList.WebMvc.Models
{
    public class DashboardViewModel : BaseChartTimelineViewModel
    {
        public DashboardViewModel(UsageDropDownTypes type) : base(type)
        {
        }
    }

    public class UsageDropDownViewModel {
        public UsageDropDownTypes Type { get; set; } = UsageDropDownTypes.LastWeek;
        public string Name { get {
                return Type
                .GetType()
                .GetMember(Type.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description;
            }
        }

        public DateTime GetCalculatedStartDate()
        {
            var now = DateTime.Now;

            switch (Type)
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
                    throw new NotSupportedException($"Given UsageDropDownTypes {Type} is not supported");
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

    public class UsageDataPerUnitViewModel {
        public string UnitName { get; set; }

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

        public UsageDataPerUnitViewModel() {
            SelectedColor = Color.Aqua;
        }

    }
}

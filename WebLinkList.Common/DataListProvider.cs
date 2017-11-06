using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace WebLinkList.Common
{
    public class DataListProvider
    {
        public static List<Color> GetColors()
        {
            var colors = new List<Color>();

            Type colorType = typeof(System.Drawing.Color);
            // We take only static property to avoid properties like Name, IsSystemColor ...
            PropertyInfo[] propInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo propInfo in propInfos)
            {
                Console.WriteLine(propInfo.Name);

                var color = propInfo.GetValue(new Color());
                if (color is Color)
                {
                    colors.Add((Color)color);
                }
            }

            return colors;
        }
    }
}

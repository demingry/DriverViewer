using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GalaSoft.MvvmLight;

namespace DriverViewer.Converters
{
    class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;
            if (path == null) return null;

            var name = MainWindow.GetFileFolderName(path);

            var Icon = "\xe666";

            if (string.IsNullOrEmpty(name))
            {
                Icon = "\xe72a";
            }else if(new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
            {
                Icon = "\xe60c";
            }

            return Icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

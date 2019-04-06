namespace AocWikiTranslationHelper.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    public class NumberOfLinksToNotificationConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values,
                                            Type targetType,
                                            object parameter,
                                            CultureInfo culture)
        {
            if (values.Length != 2)
                throw new InvalidOperationException();
            var val1 = (int)values[0];
            var val2 = (int)values[1];

            if (targetType == typeof(Brush))
            {
                return val1 == val2
                           ? Brushes.Green
                           : Brushes.Red;
            }

            if (targetType == typeof(FontWeight))
            {
                return val1 == val2
                           ? FontWeights.Normal
                           : FontWeights.Bold;
            }

            throw new ArgumentOutOfRangeException(nameof(targetType));
        }

        object[] IMultiValueConverter.ConvertBack(object value,
                                                  Type[] targetTypes,
                                                  object parameter,
                                                  CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
namespace AocWikiTranslationHelper.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class NumberOfLinksToTextConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values,
                                            Type targetType,
                                            object parameter,
                                            CultureInfo culture)
        {
            if (values.Length != 2)
                throw new InvalidOperationException();
            if (!(values[0] is string))
                return null;
            return values[1].ToString();
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
using System.Globalization;

namespace TransactionManager.Application.Extensions;

public static class StringExtensions
{
    public static (double lat, double lng) SplitCoordinatesIntoDouble(this string coordinates)
    {
        var splittedCoordinates = coordinates.Split(',');
        double.TryParse(splittedCoordinates[0].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lat);
        double.TryParse(splittedCoordinates[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lng);
        
        return (lat, lng);
    }
}
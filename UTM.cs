using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGration.AppCode
{
    class UTM
    {
        private const double a = 6378137.0; // WGS84 semi-major axis
        private const double f = 1 / 298.257223563; // WGS84 flattening
        private const double k0 = 0.9996; // UTM scale factor
        private const double E = 0.00669438; // eccentricity squared
        private const double E_P2 = E / (1.0 - E);

        public string[] ConvertToUTM(string longitude, string latitude)
        {
            // Parse input strings to doubles
            if (!double.TryParse(latitude, out double lat) || !double.TryParse(longitude, out double lon))
                throw new ArgumentException("Invalid latitude or longitude format. Must be numeric.");

            // Validate input
            if (lat < -90 || lat > 90)
                throw new ArgumentException("Latitude must be between -90 and 90 degrees");
            if (lon < -180 || lon > 180)
                throw new ArgumentException("Longitude must be between -180 and 180 degrees");

            // Convert to radians
            double latRad = lat * Math.PI / 180.0;
            double longRad = lon * Math.PI / 180.0;

            // Calculate zone number
            int zoneNumber = (int)((lon + 180.0) / 6.0) + 1;
            if (lat >= 56.0 && lat < 64.0 && lon >= 3.0 && lon < 12.0)
                zoneNumber = 32;

            // Norway special zones
            if (lat >= 72.0 && lat < 84.0)
            {
                if (lon >= 0.0 && lon < 9.0) zoneNumber = 31;
                else if (lon >= 9.0 && lon < 21.0) zoneNumber = 33;
                else if (lon >= 21.0 && lon < 33.0) zoneNumber = 35;
                else if (lon >= 33.0 && lon < 42.0) zoneNumber = 37;
            }

            // Zone letter
            char zoneLetter = GetZoneLetter(lat);

            // Central meridian
            double longOrigin = (zoneNumber - 1) * 6 - 180 + 3;
            double longOriginRad = longOrigin * Math.PI / 180.0;

            // Calculate parameters
            double N = a / Math.Sqrt(1 - E * Math.Sin(latRad) * Math.Sin(latRad));
            double T = Math.Tan(latRad) * Math.Tan(latRad);
            double C = E_P2 * Math.Cos(latRad) * Math.Cos(latRad);
            double A = Math.Cos(latRad) * (longRad - longOriginRad);

            // Meridian arc
            double M = a * ((1 - E / 4 - 3 * E * E / 64 - 5 * E * E * E / 256) * latRad
                - (3 * E / 8 + 3 * E * E / 32 + 45 * E * E * E / 1024) * Math.Sin(2 * latRad)
                + (15 * E * E / 256 + 45 * E * E * E / 1024) * Math.Sin(4 * latRad)
                - (35 * E * E * E / 3072) * Math.Sin(6 * latRad));

            // Easting
            double easting = k0 * N * (A + (1 - T + C) * Math.Pow(A, 3) / 6
                + (5 - 18 * T + T * T + 72 * C - 58 * E_P2) * Math.Pow(A, 5) / 120)
                + 500000.0;

            // Northing
            double northing = k0 * (M + N * Math.Tan(latRad) * (A * A / 2
                + (5 - T + 9 * C + 4 * C * C) * Math.Pow(A, 4) / 24
                + (61 - 58 * T + T * T + 600 * C - 330 * E_P2) * Math.Pow(A, 6) / 720));

            if (lat < 0)
                northing += 10000000.0; // Southern hemisphere adjustment

            // Return UTM coordinates as string array
            return new string[]
            {
            easting.ToString(),
            northing.ToString(),
            zoneNumber.ToString(),
            zoneLetter.ToString()
            };
        }

        private static char GetZoneLetter(double latitude)
        {
            string letters = "CDEFGHJKLMNPQRSTUVWXZ";
            int index = (int)((latitude + 80) / 8);
            return index >= 0 && index < letters.Length ? letters[index] : 'C';
        }


    }
}

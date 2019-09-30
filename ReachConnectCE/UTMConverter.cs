/* This UTM converter was developed by Chuck Taylor and is available here:
 * http://home.hiwaay.net/~taylorc/toolbox/geography/geoutm.html
 * It is a Java script tool and was translated into C# to run in this software.
 * Many comments explain the allgorythm in the original Java script tool. */

using System;

namespace ReachConnectCE
{
    static class UTMConverter
    {
        static double[] dblUTMCoordinates = new double[4];
        static double pi = 3.14159265358979;

        // Ellipsoid model constants (actual values here are for WGS84)
        static double sm_a = 6378137.0;
        static double sm_b = 6356752.314;

        static double UTMScaleFactor = 0.9996;

        public static double[] latlontoUTM(double lat, double lon)
        {
            double[] xy = { lon, lat };
            double dblHemisphere;

            // Compute the UTM zone
            double zone = Math.Floor((lon + 180.0) / 6) + 1;

            // Compute UTM coordinates
            zone = LatLonToUTMXY(DegToRad(lat), DegToRad(lon), zone, xy);

            if (lat < 0)
                // South Hemisphere.
                dblHemisphere = 0;
            else
                // North Hemisphere.
                dblHemisphere = 1;

            dblUTMCoordinates[0] = xy[0];
            dblUTMCoordinates[1] = xy[1];
            dblUTMCoordinates[2] = zone;
            dblUTMCoordinates[3] = dblHemisphere;

            return dblUTMCoordinates;
        }

        private static double DegToRad(double deg)
        {
            return (deg / 180.0f * pi);
        }

        private static double ArcLengthOfMeridian(double phi)
        {
            double alpha, beta, gamma, delta, epsilon, n, result;

            /* Precalculate n */
            n = (sm_a - sm_b) / (sm_a + sm_b);

            /* Precalculate alpha */
            alpha = ((sm_a + sm_b) / 2.0)
               * (1.0 + (Math.Pow(n, 2.0) / 4.0) + (Math.Pow(n, 4.0) / 64.0));

            /* Precalculate beta */
            beta = (-3.0 * n / 2.0) + (9.0 * Math.Pow(n, 3.0) / 16.0)
               + (-3.0 * Math.Pow(n, 5.0) / 32.0);

            /* Precalculate gamma */
            gamma = (15.0 * Math.Pow(n, 2.0) / 16.0)
                + (-15.0 * Math.Pow(n, 4.0) / 32.0);

            /* Precalculate delta */
            delta = (-35.0 * Math.Pow(n, 3.0) / 48.0)
                + (105.0 * Math.Pow(n, 5.0) / 256.0);

            /* Precalculate epsilon */
            epsilon = (315.0 * Math.Pow(n, 4.0) / 512.0);

            /* Now calculate the sum of the series and return */
            result = alpha * (phi + (beta * Math.Sin(2.0 * phi))
                + (gamma * Math.Sin(4.0 * phi))
                + (delta * Math.Sin(6.0 * phi))
                + (epsilon * Math.Sin(8.0 * phi)));

            return result;
        }

        private static double UTMCentralMeridian(double zone)
        {
            double cmeridian;

            cmeridian = DegToRad(-183.0 + (zone * 6.0));

            return cmeridian;
        }

        private static void MapLatLonToXY(double phi, double lambda, double lambda0, double[] xy)
        {
            double N, nu2, ep2, t, t2, l;
            double l3coef, l4coef, l5coef, l6coef, l7coef, l8coef;
            double tmp;

            /* Precalculate ep2 */
            ep2 = (Math.Pow(sm_a, 2.0) - Math.Pow(sm_b, 2.0)) / Math.Pow(sm_b, 2.0);

            /* Precalculate nu2 */
            nu2 = ep2 * Math.Pow(Math.Cos(phi), 2.0);

            /* Precalculate N */
            N = Math.Pow(sm_a, 2.0) / (sm_b * Math.Sqrt(1 + nu2));

            /* Precalculate t */
            t = Math.Tan(phi);
            t2 = t * t;
            tmp = (t2 * t2 * t2) - Math.Pow(t, 6.0);

            /* Precalculate l */
            l = lambda - lambda0;

            /* Precalculate coefficients for l**n in the equations below
               so a normal human being can read the expressions for easting
               and northing
               -- l**1 and l**2 have coefficients of 1.0 */
            l3coef = 1.0 - t2 + nu2;

            l4coef = 5.0 - t2 + 9 * nu2 + 4.0 * (nu2 * nu2);

            l5coef = 5.0 - 18.0 * t2 + (t2 * t2) + 14.0 * nu2
                - 58.0 * t2 * nu2;

            l6coef = 61.0 - 58.0 * t2 + (t2 * t2) + 270.0 * nu2
                - 330.0 * t2 * nu2;

            l7coef = 61.0 - 479.0 * t2 + 179.0 * (t2 * t2) - (t2 * t2 * t2);

            l8coef = 1385.0 - 3111.0 * t2 + 543.0 * (t2 * t2) - (t2 * t2 * t2);

            /* Calculate easting (x) */
            xy[0] = N * Math.Cos(phi) * l
                + (N / 6.0 * Math.Pow(Math.Cos(phi), 3.0) * l3coef * Math.Pow(l, 3.0))
                + (N / 120.0 * Math.Pow(Math.Cos(phi), 5.0) * l5coef * Math.Pow(l, 5.0))
                + (N / 5040.0 * Math.Pow(Math.Cos(phi), 7.0) * l7coef * Math.Pow(l, 7.0));

            /* Calculate northing (y) */
            xy[1] = ArcLengthOfMeridian(phi)
                + (t / 2.0 * N * Math.Pow(Math.Cos(phi), 2.0) * Math.Pow(l, 2.0))
                + (t / 24.0 * N * Math.Pow(Math.Cos(phi), 4.0) * l4coef * Math.Pow(l, 4.0))
                + (t / 720.0 * N * Math.Pow(Math.Cos(phi), 6.0) * l6coef * Math.Pow(l, 6.0))
                + (t / 40320.0 * N * Math.Pow(Math.Cos(phi), 8.0) * l8coef * Math.Pow(l, 8.0));
        }

        private static double LatLonToUTMXY(double lat, double lon, double zone, double[] xy)
        {
            MapLatLonToXY(lat, lon, UTMCentralMeridian(zone), xy);

            /* Adjust easting and northing for UTM system. */
            xy[0] = xy[0] * UTMScaleFactor + 500000.0;
            xy[1] = xy[1] * UTMScaleFactor;
            if (xy[1] < 0.0)
                xy[1] = xy[1] + 10000000.0;

            return zone;
        }
    }
}

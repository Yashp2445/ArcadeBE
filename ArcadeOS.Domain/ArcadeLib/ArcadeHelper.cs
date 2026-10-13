using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ArcadeOS.Domain.ArcadeLib
{
    public class ArcadeHelper
    {
        public static string GetUtcNowIso()
        {
            return DateTime.UtcNow.ToString(
                "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                CultureInfo.InvariantCulture
            );
        }
    }
}

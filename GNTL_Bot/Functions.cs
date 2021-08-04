using System;
using System.Collections.Generic;
using System.Text;

namespace GNTL_Bot {
    class Functions {
        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        static readonly double MaxUnixSeconds = (DateTime.MaxValue - UnixEpoch).TotalSeconds;


        public static DateTime ConvertUnixtoDateTime(double unixTime) {
            return unixTime > MaxUnixSeconds
               ? UnixEpoch.AddMilliseconds(unixTime)
               : UnixEpoch.AddSeconds(unixTime);

        }
    }
}

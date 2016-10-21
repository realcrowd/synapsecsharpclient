using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient
{
    ///<summary>
    ///</summary>
    internal static class UnixDateTimeHelper
    {
        private static readonly DateTime epoc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly DateTimeOffset epocOffset = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        static UnixDateTimeHelper()
        {
        }

        /// <summary>
        ///   Convert a long into a DateTime
        /// </summary>
        public static DateTime FromUnixTime(this Int64 self)
        {
            return epoc.AddSeconds(self);
        }

        public static DateTime FromUnixTimeMilliseconds(this Int64 self)
        {
            return epoc.AddMilliseconds(self);
        }

        public static DateTimeOffset OffsetFromUnixTime(this Int64 self)
        {
            return epocOffset.AddSeconds(self);
        }

        public static DateTimeOffset OffsetFromUnixTimeMilliseconds(this Int64 self)
        {
            return epocOffset.AddMilliseconds(self);
        }

        /// <summary>
        ///   Convert a DateTime into a long
        /// </summary>
        public static Int64 ToUnixTime(this DateTime self)
        {
            if (self == DateTime.MinValue)
            {
                return 0;
            }

            var delta = self - epoc;

            return delta.Ticks / TimeSpan.TicksPerSecond;
        }

        public static Int64 ToUnixTimeMilliseconds(this DateTime self)
        {
            if (self == DateTime.MinValue)
            {
                return 0;
            }

            var delta = self - epoc;

            return (long)delta.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public static Int64 ToUnixTime(this DateTimeOffset self)
        {
            if (self == DateTimeOffset.MinValue)
            {
                return 0;
            }

            var date = self.ToUniversalTime();
            var delta = date - epocOffset;

            return delta.Ticks / TimeSpan.TicksPerSecond;
        }

        public static Int64 ToUnixTimeMilliseconds(this DateTimeOffset self)
        {
            if (self == DateTimeOffset.MinValue)
            {
                return 0;
            }

            var date = self.ToUniversalTime();
            var delta = date - epocOffset;

            return (long)delta.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}

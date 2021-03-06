using System;

namespace RaccoonBlog.Web.Common
{
	public class DateTimeOffsetUtil
	{
		public static DateTimeOffset ConvertFromUnixTimestamp(long timestamp)
		{
			var origin = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, DateTimeOffset.Now.Offset);
			return origin.AddSeconds(timestamp);
		}

		public static DateTimeOffset ConvertFromJsTimestamp(long timestamp)
		{
			var origin = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, DateTimeOffset.Now.Offset);
			return origin.AddMilliseconds(timestamp);
		}
	}
}

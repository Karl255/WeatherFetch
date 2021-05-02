using System.Collections.Generic;

namespace WeatherFetch
{
	public static class TextIcons
	{
		public static readonly Dictionary<(int code, bool isDay), string[]> IconList;

		public static readonly string[] Sunny =
		{
			@"   \  /   ",
			@" _ /""""\ _ ",
			@"   \__/   ",
			@"   /  \   ",
			@"          "
		};

		public static readonly string[] ClearNight =
		{
			@"   .   .  ",
			@" .   .    ",
			@"    .   . ",
			@" .       .",
			@".     .   "
		};

		public static readonly string[] PartiallyCloudy =
		{
			@"    \  /  ",
			@"  __/""""\ _",
			@" (  )__/  ",
			@"(___(_)\  ",
			@"          "
		};
		
		public static readonly string[] PartiallyCloudyNight =
		{
			@"   .    . ",
			@".  __  .  ",
			@"  (  )__  ",
			@" (___(__) ",
			@"  .     . "
		};
		
		public static readonly string[] Cloudy =
		{
			@"          ",
			@"   __     ",
			@"  (  )__  ",
			@" (___(__) ",
			@"          "
		};
		
		public static readonly string[] CloudyNight =
		{
			@"          ",
			@"   __     ",
			@"  (  )__  ",
			@" (___(__) ",
			@"          "
		};
		
		public static readonly string[] Overcast =
		{
			@"          ",
			@"   __     ",
			@"  (  )__  ",
			@" (___(__) ",
			@"          "
		};
		
		public static readonly string[] OvercastNight =
		{
			@"          ",
			@"   __     ",
			@"  (  )__  ",
			@" (___(__) ",
			@"          "
		};


		static TextIcons()
		{
			IconList = new()
			{
				{ (1000, true), Sunny },
				{ (1000, false), ClearNight },
				{ (1003, true), PartiallyCloudy },
				{ (1003, false), PartiallyCloudyNight },
				{ (1006, true), Cloudy },
				{ (1006, false), CloudyNight },
				{ (1009, true), Overcast },
				{ (1009, false), OvercastNight },
			};
		}
	}
}

using System;

namespace ClickerClass.Utilities
{
	internal static class MiscHelper
	{
		/// <summary>
		/// Cycles the given enum value incrementally. If a range is specified via <paramref name="start"/> and <paramref name="end"/>, 
		/// cycling is constrained to that range. If no range is specified, the entire enum is cycled.
		/// </summary>
		/// <typeparam name="T">The enum type.</typeparam>
		/// <param name="enumValue">The current enum value to cycle.</param>
		/// <param name="start">Optional starting point for the cycle range.</param>
		/// <param name="end">Optional ending point for the cycle range.</param>
		/// <param name="backwards">If cycling should happen backwards.</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="start"/> or <paramref name="end"/> is not part of the enum.</exception>
		public static void CycleEnum<T>(ref T enumValue, T? start = null, T? end = null, bool backwards = false) where T : struct, Enum
		{
			var enumValues = Enum.GetValues<T>();
			int startIndex = start.HasValue ? Array.IndexOf(enumValues, start.Value) : 0;
			int endIndex = end.HasValue ? Array.IndexOf(enumValues, end.Value) : enumValues.Length - 1;

			if (startIndex == -1 || endIndex == -1)
			{
				throw new ArgumentException("Start or end value is not a valid enum member.");
			}

			int currentIndex = Array.IndexOf(enumValues, enumValue);
			if (currentIndex == -1)
			{
				throw new ArgumentException("enumValue is not a valid enum member.");
			}

			if (!backwards)
			{
				currentIndex = currentIndex >= endIndex ? startIndex : currentIndex + 1;
			}
			else
			{
				currentIndex = currentIndex <= startIndex ? endIndex : currentIndex - 1;
			}
			enumValue = enumValues[currentIndex];
		}
	}
}

namespace FF.DataEntry.Utils
{
    public static class EnumerableExtension
    {
		public static void ForEachWithIndex<T>(this IEnumerable<T> enumerable, Action<T, int> handler)
		{
			int idx = 0;
			foreach (T item in enumerable)
				handler(item, idx++);
		}
	}
}

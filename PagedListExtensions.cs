using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace X.PagedList.Mvc.PagedListExt
{
	[DebuggerStepThrough]
	public static class PagedListExtensions
	{
		public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index)
		{
			if (index < 1)
			{
				throw new ArgumentException("page index must be more than 0");
			}
			return new PagedList<T>(source, index - 1, 10, 0);
		}

		public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index, int pageSize)
		{
			if (index < 1)
			{
				throw new ArgumentException("page index must be more than 0");
			}
			return new PagedList<T>(source, index - 1, pageSize, 0);
		}

		public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index)
		{
			if (index < 1)
			{
				throw new ArgumentException("page index must be more than 0");
			}
			return new PagedList<T>(source.AsQueryable<T>(), index - 1, 10, 0);
		}

		public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index, int pageSize)
		{
			if (index < 1)
			{
				throw new ArgumentException("page index must be more than 0");
			}
			return new PagedList<T>(source.AsQueryable<T>(), index - 1, pageSize, 0);
		}
	}
}
using System;

namespace X.PagedList.Mvc.PagedListExt
{
	public interface IPagedList
	{
		bool IsNextPage
		{
			get;
		}

		bool IsPreviousPage
		{
			get;
		}

		int PageIndex
		{
			get;
		}

		int PageSize
		{
			get;
		}

		int TotalCount
		{
			get;
		}

		int TotalPages
		{
			get;
		}
	}
}
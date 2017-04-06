using System.Collections;
using System.Collections.Generic;

namespace X.PagedList.Mvc.PagedListExt
{
	public interface IPagedList<T> : IPagedList, IEnumerable<T>, IEnumerable
	{

	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace X.PagedList.Mvc.PagedListExt
{
    public class PagedList<T> : IPagedList<T>, IPagedList, IEnumerable<T>, IEnumerable
    {
        private readonly List<T> _items;

        private int _pageSize;

        public bool IsNextPage
        {
            get
            {
                if (this.TotalCount <= 0)
                {
                    throw new ArgumentOutOfRangeException("TotalCount");
                }
                PagedList<T> pagedList = this;
                int pageIndex = pagedList.PageIndex + 1;
                int num = pageIndex;
                pagedList.PageIndex = pageIndex;
                return num * this.PageSize < this.TotalCount;
            }
        }

        public bool IsPreviousPage
        {
            get
            {
                return this.PageIndex > 0;
            }
        }

        public int PageIndex
        {
            get
            {
                return JustDecompileGenerated_get_PageIndex();
            }
            set
            {
                JustDecompileGenerated_set_PageIndex(value);
            }
        }

        private int JustDecompileGenerated_PageIndex_k__BackingField;

        public int JustDecompileGenerated_get_PageIndex()
        {
            return this.JustDecompileGenerated_PageIndex_k__BackingField;
        }

        public void JustDecompileGenerated_set_PageIndex(int value)
        {
            this.JustDecompileGenerated_PageIndex_k__BackingField = value;
        }

        public int PageSize
        {
            get
            {
                return JustDecompileGenerated_get_PageSize();
            }
            set
            {
                JustDecompileGenerated_set_PageSize(value);
            }
        }

        public int JustDecompileGenerated_get_PageSize()
        {
            return this._pageSize;
        }

        public void JustDecompileGenerated_set_PageSize(int value)
        {
            this._pageSize = value;
        }

        public int TotalCount
        {
            get
            {
                return JustDecompileGenerated_get_TotalCount();
            }
            set
            {
                JustDecompileGenerated_set_TotalCount(value);
            }
        }

        private int JustDecompileGenerated_TotalCount_k__BackingField;

        public int JustDecompileGenerated_get_TotalCount()
        {
            return this.JustDecompileGenerated_TotalCount_k__BackingField;
        }

        private void JustDecompileGenerated_set_TotalCount(int value)
        {
            this.JustDecompileGenerated_TotalCount_k__BackingField = value;
        }

        public int TotalPages
        {
            get
            {
                if (this.TotalCount <= 0)
                {
                    return 0;
                }
                return (int)Math.Ceiling((double)this.TotalCount / this.PageSize);
            }
        }

        public PagedList()
        {
            this.PageIndex = 0;
            this._items = new List<T>();
            this.TotalCount = 0;
        }

        public PagedList(IQueryable<T> source, int index, int pageSize, int total = 0) : this()
        {
            this.PageIndex = 0;
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (index <= -1)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize");
            }
            this.TotalCount = (total == 0 ? source.Count<T>() : total);
            this.PageSize = pageSize;
            this.PageIndex = (index > this.TotalPages ? 0 : index);
            if (source.Count<T>() <= pageSize)
            {
                this._items.AddRange(source);
                return;
            }
            this._items.AddRange(source.Skip<T>(this.PageIndex * pageSize).Take<T>(pageSize).AsParallel<T>().ToList<T>());
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
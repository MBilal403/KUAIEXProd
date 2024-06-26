﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Impl
{
    public class PagedResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalSize { get; set; }
        public int FilterRecored { get; set; }
        public List<T> Data { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalSize / PageSize);
            }
        }
    }

}

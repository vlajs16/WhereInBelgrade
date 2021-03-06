﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public class EventParams
    {
		private const int MaxPageSize = 50;
		public int PageNumber { get; set; } = 1;
		private int pageSize = 10;
		public string Criteria { get; set; }

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
		}

	}
}

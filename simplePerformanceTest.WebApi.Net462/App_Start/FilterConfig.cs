﻿using System.Web;
using System.Web.Mvc;

namespace simplePerformanceTest.WebApi.Net462
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

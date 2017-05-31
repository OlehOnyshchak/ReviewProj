using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewProj.WebUI.Models
{
    public class PagingInfo
    {
        public PagingInfo()
        {

        }

        public PagingInfo(PagingInfo pi)
        {
            if (pi != null)
            {
                TotalItems = pi.TotalItems;
                ItemsPerPage = pi.ItemsPerPage;
                CurrentPage = pi.CurrentPage;
            }
        }

        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}
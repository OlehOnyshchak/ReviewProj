using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReviewProj.Domain.Entities;

namespace ReviewProj.WebUI.Models
{
    public class EnterpriseListViewModel
    {
        public IEnumerable<Enterprise> Enterprises { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string SearchString { get; set; }
        public List<string> AvailableCategories { get; set; }
        public List<string> SelectedCategories { get; set; }
    }
}
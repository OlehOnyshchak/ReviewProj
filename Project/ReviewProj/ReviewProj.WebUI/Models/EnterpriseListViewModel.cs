using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ReviewProj.Domain.Entities;

namespace ReviewProj.WebUI.Models
{
    public class EnterpriseListViewModel
    {
        public IEnumerable<Enterprise> Enterprises { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string SearchString { get; set; }
        public string SelectedSortingCategory { get; set; }
        public IList<SelectListItem> RatingCategories { get; set; }
        public IList<SelectListItem> TypeCategories { get; set; }
        public IList<SelectListItem> SortingCategories { get; set; }

        public EnterpriseListViewModel()
        {
            Enterprises = new List<Enterprise>();
            RatingCategories = new List<SelectListItem>();
            TypeCategories = new List<SelectListItem>();
            SortingCategories = new List<SelectListItem>();
        }
    }
}
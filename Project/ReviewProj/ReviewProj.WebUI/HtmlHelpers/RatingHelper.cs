using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewProj.WebUI.HtmlHelpers
{
    public class RatingHelper
    {
        public static string styleForStarIndexWithRating(int starIndex, double rating)
        {
            if(starIndex <= Convert.ToInt32(rating)) 
            {
                return "yellow-star";
            }
            else
            {
                return "transparent-star";
            }
        }
    }
}
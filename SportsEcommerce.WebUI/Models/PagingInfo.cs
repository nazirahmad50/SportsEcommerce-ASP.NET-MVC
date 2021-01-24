using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsEcommerce.WebUI.Models
{
    /// <summary>
    /// Information about the number of pages available
    /// </summary>
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
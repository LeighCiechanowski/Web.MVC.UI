using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciechanowski.Framework.Web.MVC.UI
{
    public class PagedModel<T>
    {
        public int PageNumber { get; set; }
        public IEnumerable<T> Rows { get; set; }
        public int TotalRows { get; set; }
        public int PageSize { get; set; }        
    }
}

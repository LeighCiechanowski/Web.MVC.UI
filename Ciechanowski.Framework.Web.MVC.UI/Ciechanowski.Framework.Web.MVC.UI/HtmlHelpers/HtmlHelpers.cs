using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using HtmlAgilityPack;

namespace Ciechanowski.Framework.Web.MVC.UI.HtmlHelpers
{
    public static class HtmlHelpers
    {
        public static IHtmlString GetExtendedWebGrid(WebGrid webGrid, IHtmlString webGridHtml, int? expandColumnOnMobileDevice = null, int[] columnsToHideOnMobile = null, int[] columnsToHideOnTablet = null)
        {
            HtmlString result;

            string webGridHtmlString = webGridHtml.ToString();

            HtmlDocument htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(webGridHtmlString);
            HtmlNode htmlNodeAnchor = htmlDocument.DocumentNode.SelectSingleNode("//thead").SelectSingleNode("//a[contains(@href,'sort=" + webGrid.SortColumn + "')]");

            if (htmlNodeAnchor != null)
            {
                string imgSortDirection;
                if (webGrid.SortDirection == SortDirection.Ascending)
                    imgSortDirection = "asc";
                else
                    imgSortDirection = "desc";

                htmlNodeAnchor.SetAttributeValue("class", "search_column_" + imgSortDirection);
            }

            if (expandColumnOnMobileDevice != null)
            {
                HtmlNode expandColumn = htmlDocument.DocumentNode.SelectNodes("//th")[expandColumnOnMobileDevice.Value];
                expandColumn.Attributes.Add("data-class", "expand");
            }

            if (columnsToHideOnMobile != null)
            {
                for (int i = 0; i < columnsToHideOnMobile.Length; i++)
                {
                    HtmlNode hiddenColumn = htmlDocument.DocumentNode.SelectNodes("//th")[columnsToHideOnMobile[i]];
                    hiddenColumn.Attributes.Add("data-hide", "phone");
                }
            }
            if (columnsToHideOnTablet != null)
            {
                for (int i = 0; i < columnsToHideOnTablet.Length; i++)
                {
                    HtmlNode hiddenColumn = htmlDocument.DocumentNode.SelectNodes("//th")[columnsToHideOnTablet[i]];
                    var alreadyExistingAttribute = hiddenColumn.Attributes["data-hide"];
                    if (alreadyExistingAttribute == null)
                    {
                        hiddenColumn.Attributes.Add("data-hide", "tablet");
                    }
                    else
                    {
                        alreadyExistingAttribute.Value = alreadyExistingAttribute.Value + ",tablet";
                    }
                }
            }
            webGridHtmlString = htmlDocument.DocumentNode.OuterHtml;
            result = new HtmlString(webGridHtmlString);

            return result;
        }
    }
    
}

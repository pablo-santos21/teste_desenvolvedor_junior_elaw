using HtmlAgilityPack;

namespace Webcrawler.Services
{
    public class TotalPage
    {
        public int GetTotalPages(HtmlDocument document)
        {
            int totalPages = 1;

            var paginationNode = document.DocumentNode.QuerySelector("ul.pagination");
            if (paginationNode != null)
            {
                var pageItems = paginationNode.QuerySelectorAll("li.page-item a.page-link");
                totalPages = pageItems
                    .Select(node => int.TryParse(node.InnerText.Trim(), out int pageNumber) ? pageNumber : 0)
                    .Max();
            }
            return totalPages;
        }
    }
}

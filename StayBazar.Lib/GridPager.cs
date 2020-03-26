using System;
using System.Collections.Generic;

using System.Text;


namespace StayBazar.Lib
{
    public class GridPager
    {
        //set Additional class
        public string ulClass {get;set;}
        public int RowsInAPage {get;set;}
        public int ColumnsInPager { get; set; }
        public string JavascriptFuncName { get; set; }

        public GridPager()
        {
            RowsInAPage = 10;
            ColumnsInPager = 5;
        }
        public string GetPager(int totalRows,int currentPage)
        {
            StringBuilder pager = new StringBuilder();
            if (totalRows < 1) return "";
            int totalPages;
            totalPages = totalRows / RowsInAPage;
            totalPages = totalPages + ((totalRows % RowsInAPage) > 0 ? 1 : 0);
            if (totalPages < 2) return "";
            int min, max; //page no in the current pager
            int i=0, maxPage;
            min = 1;
            max = 1;
            for (i = 1; i < totalPages;i=i+ColumnsInPager )
            {
                maxPage = i + ColumnsInPager;
                if(currentPage >= i &&  currentPage <= maxPage)
                { 
                    min = i;
                    if (totalPages < maxPage)
                        max = totalPages;
                    else
                        max = maxPage;
                    break;
                }
            }
            //ul
            pager.Append("<ul class=\"pagination\"");
            if(ulClass != "")
            {
                pager.Append(" ");
                pager.Append(ulClass);
            }
            pager.Append(">");
            //ul end

            string atag = "<a href=\"#\" onclick=\"" + JavascriptFuncName + "(";

            if(currentPage > 1)
            {
                pager.Append("<li>");
                pager.Append(atag);
                pager.Append(currentPage - 1);
                pager.Append(")\" >&laquo;</a></li>");
            }
            else
            { pager.Append("<li class=\"disabled\"><a href=\"#\">&laquo;</a></li>");}
            for (i = min; i <= max; i ++ )
            {
                if(currentPage == i)
                    pager.Append("<li class=\"active\" >");
                else
                    pager.Append("<li>");
                pager.Append(atag);
                pager.Append(i);
                pager.Append(")\" ");
                pager.Append(">");
                pager.Append(i);
                pager.Append("</a></li>");
            }
            if (currentPage >= totalPages)
                pager.Append("<li class=\"disabled\"><a href=\"#\">&raquo;</a></li>");
            else
            {
                pager.Append("<li>");
                pager.Append(atag);
                pager.Append(currentPage + 1);
                pager.Append(")\" >&raquo;</a></li>");
            }
            pager.Append("</ul>");
            return pager.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helper
{
    public class UserPager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string Active { get; set; }
        public string id { get; set; }
        public string Author { get; set; }
        public string Research { get; set; }
        public int StageId { get; set; }
        public int FieldId { get; set; }
        public string Date { get; set; }
        public bool UnRead { get; set; }







        public UserPager()
        {

        }
        public UserPager(int totalItems, int page,string searchText,string selectedRole,string active, int pageSize = 20, string actionName = "Index",
            string controllerName = "Account", string Id=""
            , string author="", string research="", string date="", int field=0, int stage=0, bool unread=false)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            int currentPage = page;
            int startPage = currentPage - 5;
            int endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            ActionName = actionName;
            ControllerName = controllerName;
            Active = active;
            Name = searchText;
            RoleName = selectedRole;
            id = Id;
            author = Author;
            research = Research;
            date = Date;
            field = FieldId;
            stage = StageId;
            unread = UnRead;

        }
    }
}

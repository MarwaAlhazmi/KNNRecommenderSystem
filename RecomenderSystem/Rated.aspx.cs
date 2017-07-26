using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecomenderSystem
{
    public partial class Rated : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getRated();
            }
        }

        private void getRated()
        {
            using (BooksEntities book = new BooksEntities())
            {
                string username = User.Identity.Name;
                var id = book.Users.Where(a => a.UserName == username).Select(a => a.UserID).First();
                var books = book.Ratings.Where(a => a.UserID == id).Select(a => a.Book).ToList();
                lvRated.DataSource = books;
                lvRated.DataBind();
            }
        }

        protected void DataPager1_PreRender(object sender, EventArgs e)
        {
            getRated();
        }
    }
}
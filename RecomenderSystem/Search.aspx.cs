using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecomenderSystem
{
    public partial class Search : System.Web.UI.Page
    {
        private static List<Book> data;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                data = null;
            }
        }

      

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            using (BooksEntities book = new BooksEntities())
            {
                string key = tbKeyword.Text;
                switch (rbType.SelectedIndex)
                {
                    case 0:
                        data = getRankedBooks();
                        lblError.Visible = false;
                        break;

                    case 1:
                        data = book.Books.Where(a => a.Auther.Contains(key)).ToList();
                        lblError.Visible = false;
                        break;

                    case 2:
                        data = book.Books.Where(a => a.Publisher.Contains(key)).ToList();
                        lblError.Visible = false;
                        break;

                    case 3:
                        int y;
                        if (Int32.TryParse(key, out y))
                        {
                            lblError.Visible = false;
                            data = book.Books.Where(a => a.Year == y).ToList();
                        }
                        else
                        {
                            lblError.Visible = true;
                        }
                        
                     
                        break;
                }
                lvknn.DataSource = data;
                lvknn.DataBind();
                pnlResult.Visible = true;
            }
        }

        protected void DataPager1_PreRender(object sender, EventArgs e)
        {
            
            lvknn.DataSource = data;
            lvknn.DataBind();
            //pnlResult.Visible = true;
        }

        public List<Book> getRankedBooks()
        {
            string[] query = tbKeyword.Text.ToLower().Split(' ');
            int userid;
            using (BooksEntities book = new BooksEntities())
            {
                string username = User.Identity.Name;
                var re = book.Users.Where(a => a.UserName == username).Select(a => a.UserID).First();
                userid = re;

                var result = VSM.vsmSearch(query, userid);
                List<Book> finalresult = new List<Book>();
                foreach (var t in result)
                {
                    var r = book.Books.Where(a => a.ISBN == t.Key).First();
                    finalresult.Add(r);
                }
                
                return finalresult;
            }

        }

    }
}
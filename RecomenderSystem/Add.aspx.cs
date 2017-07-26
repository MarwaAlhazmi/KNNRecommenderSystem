using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace RecomenderSystem
{
    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Roles.IsUserInRole("Admin"))
            { 
                Response.Redirect("Error.aspx");
            }
        }

        protected void RB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RB.SelectedIndex == 0)
            {
                pnlCat.Visible = true;
                pnlBook.Visible = false;
            }
            else
            {
                pnlCat.Visible = false;
                pnlBook.Visible = true;
                using (BooksEntities book = new BooksEntities())
                {
                    var re = from o in book.Categories
                             select o;
                    ddlCat.DataSource = re;
                    ddlCat.DataTextField = "Name";
                    ddlCat.DataValueField = "ID";
                    ddlCat.DataBind();
                }
            }
        }

        protected void btnCatSave_Click(object sender, EventArgs e)
        {
            string name = tbCatName.Text;
            using (BooksEntities book = new BooksEntities())
            {
                Category cat = new Category();
                cat.Name = name;
                book.Categories.AddObject(cat);
                book.SaveChanges();
            }
            string Message = "Category Added Successfuly";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');", Message), true);
            pnlCat.Visible = false;
            tbCatName.Text = "";
            RB.SelectedIndex = -1;
        }

        protected void btnBookSave_Click(object sender, EventArgs e)
        {
            using (BooksEntities book = new BooksEntities())
            {
                Book bo = new Book();
                bo.ISBN = tbisbn.Text;
                bo.Auther = tbAuthor.Text;
                bo.Title = tbTitle.Text;
                bo.Publisher = tbPub.Text;
                bo.Year = Convert.ToInt32(tbYear.Text);
                bo.Description = tbDesc.Text;
                bo.CategoryID = Convert.ToInt32(ddlCat.SelectedValue);

                book.Books.AddObject(bo);
                // Extract keyword and add them
                Dictionary<string, int> key = VSM.removeSW(bo.Title);
                foreach (var item in key)
                {
                    Keyword k = new Keyword();
                    k.ISBN = bo.ISBN;
                    k.Word = item.Key;
                    k.Freq = item.Value;
                    book.Keywords.AddObject(k);
                }
                book.SaveChanges();

                string Message = "Book Added Successfuly";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{0}');", Message), true);
                pnlBook.Visible = false;
                tbAuthor.Text = "";
                tbisbn.Text = "";
                tbTitle.Text = "";
                tbPub.Text = "";
                tbYear.Text = "";
                tbDesc.Text = "";
                RB.SelectedIndex = -1;
                
            }
        }
    }
}
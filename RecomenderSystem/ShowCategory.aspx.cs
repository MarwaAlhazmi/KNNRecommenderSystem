using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecomenderSystem
{
    public partial class ShowCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                 getCategories();
            }
            //using (BooksEntities bo = new BooksEntities())
            //{
            //    var result = bo.KNN(User.Identity.Name);
            //    lvknn.DataSource = result;
            //    lvknn.DataBind();
            //}
        }

        private void getCategories()
        {
            if (Request["ID"] != null)
            {
                int cat = Convert.ToInt32(Request["ID"]);
                using (BooksEntities book = new BooksEntities())
                {
                    var result = from o in book.Books
                                 where o.CategoryID == cat
                                 select o;
                    lvknn.DataSource = result;
                    lvknn.DataBind();
                    lblCat.InnerText = result.First().Category.Name;
                }
            }
        }


        protected void DataPager1_PreRender(object sender, EventArgs e)
        {

            getCategories();


        }

 
      

        
    }
}
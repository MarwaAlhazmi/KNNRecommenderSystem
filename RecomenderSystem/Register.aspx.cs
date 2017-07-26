using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace RecomenderSystem
{
    public partial class Register : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
            
            //if (!Page.IsPostBack)
            //{
            //    //DropDownList cat = (DropDownList)RegisterUserWizardStep.ContentTemplateContainer.FindControl("ddlCat");
            //    //using (BooksEntities book = new BooksEntities())
            //    //{
            //    //    var result = from o in book.Categories
            //    //                 select o;
            //    //    cat.DataSource = result;
            //    //    cat.DataTextField = "Name";
            //    //    cat.DataValueField = "ID";
            //    //    cat.DataBind();
            //    //}
            //}
               
            
       
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        protected void CreateUserButton_Click(object sender, EventArgs e)
        {
            TextBox tbusername = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("UserName");
            TextBox tbName = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("tbName");
            TextBox tblocation = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("tbLocation");
            TextBox tbage = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("tbAge");
            DropDownList cat = (DropDownList)RegisterUserWizardStep.ContentTemplateContainer.FindControl("ddlCat");
            string name = tbName.Text;
            string location = tblocation.Text;
            int age = Convert.ToInt32(tbage.Text);
            string username = tbusername.Text;
            int pcat = Convert.ToInt32(cat.SelectedValue);
            using (BooksEntities book = new BooksEntities())
            {
                User uu = new User();
                uu.Age = age;
                uu.Location = location;
                uu.Name = name;
                uu.UserName = username;
                uu.PrefCat = pcat;
                book.Users.AddObject(uu);
                book.SaveChanges();
            }
        }

        protected void RegisterUser_CreatingUser(object sender, LoginCancelEventArgs e)
        {

        }

    }
}

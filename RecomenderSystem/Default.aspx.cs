using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using System.Web.Security;
namespace RecomenderSystem
{
    public partial class _Default : System.Web.UI.Page
    {
        private static List<Book> data;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsCallback)
            {
               
               
                data = null;
                using (BooksEntities book = new BooksEntities())
                {
                    string user = User.Identity.Name;
                    int id = book.Users.Where(a=>a.UserName == user).Select(a=>a.UserID).First();
                    var rate = book.Ratings.Where(a => a.UserID == id).FirstOrDefault();
                    if (rate == null)
                    {
                        pnlNew.Visible = true;
                        pnlknn.Visible = false;
                        pnlvsm.Visible = false;
                        // get data
                        data = book.NewUserRecommendation(user);
                        lvNew.DataSource = data;
                        lvNew.DataBind();
                    }
                    else 
                    {
                        // if there are ratings get knn and vsm
                        pnlNew.Visible = false;
                        pnlknn.Visible = true;
                        pnlvsm.Visible = true;

                        data = book.KNN(user);
                        lvknn.DataSource = data;
                        lvknn.DataBind();

                        // get vsm

                        var datavsm = VSM.recommendvsm(User.Identity.Name);
                        lvvsm.DataSource = datavsm;
                        lvvsm.DataBind();
                    }
                }


            }
            
           
            /*using (BooksEntities book = new BooksEntities())
            {
                var all = from o in book.Books
                          select o;

                foreach (var o in all)
                {
                    Dictionary<string,int> key;
                    key = VSP.removeSW(o.Title);
                    foreach (var item in key)
                    {
                        Keyword k = new Keyword();
                        k.ISBN = o.ISBN;
                        k.Word = item.Key;
                        k.Freq = item.Value;
                        book.Keywords.AddObject(k);
                    }
                }
                book.SaveChanges();

            }
            //VSP.removeSW("this is the orange book");*/
        }

        protected void DataPager1_PreRender(object sender, EventArgs e)
        {
        }
        /*
         * 
                using (BooksEntities book = new BooksEntities())
                {
                    var all = from o in book.Books
                              select o;

                    foreach (var o in all)
                    {
                        Dictionary<string, int> key;
                        key = VSP.removeSW(o.Title);
                        foreach (var item in key)
                        {
                            Keyword k = new Keyword();
                            k.ISBN = o.ISBN;
                            k.Word = item.Key;
                            k.Freq = item.Value;
                            book.Keywords.AddObject(k);
                        }
                    }
                    book.SaveChanges();

                }
         * */

        //XmlReader reader = XmlReader.Create(@"E:\ASSIGNMNET5.xml");
        //XmlSchemaSet schemaSet = new XmlSchemaSet();
        //XmlSchemaInference schema = new XmlSchemaInference();

        //schemaSet = schema.InferSchema(reader);
        //System.IO.TextWriter tr = File.CreateText("E:\\answer.txt");

        //foreach (XmlSchema s in schemaSet.Schemas())
        //{
        //    s.Write(tr);
        //}
      
    }
}

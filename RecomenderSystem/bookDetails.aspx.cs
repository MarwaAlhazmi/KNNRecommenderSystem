using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecomenderSystem
{
    public partial class bookDetails : System.Web.UI.Page
    {
        private static string isbn;
        private static string algorithm;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["isbn"] != null)
                {
                    isbn = Request["isbn"].ToString();
                    using (BooksEntities book = new BooksEntities())
                    {
                        var bo = (from o in book.Books
                                  where o.ISBN == isbn
                                  select o).First();
                        // avg rating 
                        avgRate();
                        // get user rating
                        string username = User.Identity.Name;
                        var userrate = (from o in book.Ratings
                                        where o.ISBN == isbn && o.User.UserName == username
                                        select o.Rate).FirstOrDefault();
                        // fill info
                        img.ImageUrl = "images/" + bo.ISBN + ".jpg";
                        lblTitle.Text = bo.Title;
                        lblYear.Text = bo.Year.ToString();
                        lblAuthor.Text = bo.Auther;
                        lblPub.Text = bo.Publisher;
                        lblDesc.Text = bo.Description;
                        lblCat.Text = bo.Category.Name;

                        

                        // fill users rate
                        if (userrate != 0)
                        {
                            rbRate.SelectedValue = userrate.ToString();
                        }
                        else
                        {
                            rbRate.SelectedIndex = -1;
                            if (Request["Rec"] != null)
                            {
                                algorithm = Request["Rec"].ToString();
                                pnlRec.Visible = true;
                            }
                            else
                            {
                                pnlRec.Visible = false;
                                algorithm = null;
                            }
                        }
                    }
                }
            }
        }

        private void avgRate()
        {
            using (BooksEntities book = new BooksEntities())
            {
                // get average ratings
                var avr = (double?)(from o in book.Ratings
                                    where o.ISBN == isbn
                                    select o).Average(a => (double?)a.Rate);
                // fill average rate
                if (avr == 0 || avr == null)
                {
                    imgRate.ImageUrl = "images/0.jpg";
                }
                else if (avr > 0 && avr <= 0.5)
                {
                    imgRate.ImageUrl = "images/0-5.jpg";
                }
                else if (avr > 0.5 && avr <= 1)
                {
                    imgRate.ImageUrl = "images/1.jpg";
                }
                else if (avr > 1 && avr <= 1.5)
                {
                    imgRate.ImageUrl = "images/1-5.jpg";
                }
                else if (avr > 1.5 && avr <= 2)
                {
                    imgRate.ImageUrl = "images/2.jpg";
                }
                else if (avr > 2 && avr <= 2.5)
                {
                    imgRate.ImageUrl = "images/2-5.jpg";
                }
                else if (avr > 2.5 && avr <= 3)
                {
                    imgRate.ImageUrl = "images/3.jpg";
                }
                else if (avr > 3 && avr <= 3.5)
                {
                    imgRate.ImageUrl = "images/3-5.jpg";
                }
                else if (avr > 3.5 && avr <= 4)
                {
                    imgRate.ImageUrl = "images/4.jpg";
                }
                else if (avr > 4 && avr <= 4.5)
                {
                    imgRate.ImageUrl = "images/4-5.jpg";
                }
                else if (avr > 4.5 && avr <= 5)
                {
                    imgRate.ImageUrl = "images/5.jpg";
                }
            }
        }

        protected void rbRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rate = Convert.ToInt32(rbRate.SelectedValue);
            string username = User.Identity.Name;

            using (BooksEntities book = new BooksEntities())
            {
                var userid = (from o in book.Users
                              where o.UserName == username
                              select o).First();
                var userRate = (from o in book.Ratings
                            where o.ISBN == isbn && o.UserID == userid.UserID
                            select o).FirstOrDefault();
                if (userRate == null)
                {
                    // insert the rate
                    Rating r = new Rating();
                    r.Rate = rate;
                    r.ISBN = isbn;
                    r.UserID = userid.UserID;
                    r.Date = DateTime.Now.Date;
                    book.Ratings.AddObject(r);
                    book.SaveChanges();
                    lblRate.Visible = true;
                    pnlRec.Visible = false;
                }
                else
                {
                    // update
                    userRate.Rate = rate;
                    userRate.Date = DateTime.Now.Date;
                    book.SaveChanges();
                    lblRate.Visible = true;
                }
            }
            avgRate();
        }

        protected void rbRec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbRec.SelectedIndex == 1)
            {
                //insert the book isbn to the table

                userFeedback(algorithm, false);
            }
            else
            {
                
            }
        }

        private void userFeedback(string algo, bool value)
        {
            using (BooksEntities book = new BooksEntities())
            {
                string username = User.Identity.Name;
                var r = book.Users.Where(a => a.UserName == username).Select(a => a.UserID).First();
                Dislike dis = new Dislike();
                dis.ISBN = isbn;
                dis.UserID = r;
                dis.Value = value;
                dis.Algorithm = algo;
                dis.Date = DateTime.Now.Date;
                book.Dislikes.AddObject(dis);
                book.SaveChanges();

                pnlRec.Visible = false;
            }
            lblFeed.Visible = true;
        }
    }
}
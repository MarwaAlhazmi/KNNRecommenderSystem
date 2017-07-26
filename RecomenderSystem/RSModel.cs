using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace RecomenderSystem
{
    public partial class BooksEntities
    {
        public List<Book> NewUserRecommendation(string username)
        {
            using (BooksEntities book = new BooksEntities())
            {
                // taking books from the same category with highest avearge ratings
                var user = book.Users.Where(a => a.UserName == username).First();
                var books = book.Books.Where(a => a.CategoryID == user.PrefCat && a.Ratings.Count != 0);
                Dictionary<string, double> rate = new Dictionary<string, double>();
                foreach (var row in books)
                {
                    if (!(book.Dislikes.Where(a=>a.UserID == user.UserID).Select(a=>a.ISBN).Contains(row.ISBN)))
                        rate.Add(row.ISBN, row.Ratings.Average(a=>a.Rate));
                }
                var top = rate.OrderByDescending(a=>a.Value).Take(6);
                List<Book> finalR = new List<Book>();
                foreach (var t in top)
                {
                    finalR.Add(books.Where(a => a.ISBN == t.Key).First());
                }
                return finalR;

            }
        }

        public List<Book> KNN(string username)
        {
            using (BooksEntities book = new BooksEntities())
            {
                var userid = (from o in book.Users
                              where o.UserName == username
                              select o.UserID).First();
                // get list of isbn for the user
                var isbn = from o in book.Ratings
                           where o.UserID == userid
                           select o.ISBN;
               
                // get the shared users [list of ids]
                var otherusersids = (from o in book.Ratings
                                 where (isbn.Contains(o.ISBN) && o.UserID!= userid) 
                                 select o.User).Distinct();
                //======================================
                var userratings = from o in book.Ratings
                                  where o.UserID == userid
                                  select o;
                //var otherusers = from o in book.Users
                //                 where otherusersids.Contains(o.UserID)
                //                 select o;
                Dictionary<int,double> result = new Dictionary<int,double>();
                foreach (var row in otherusersids)
                {
                    double sum = 0;
                    foreach (var rate in row.Ratings.Where(a=>isbn.Contains(a.ISBN)))
                    {
                        var userR = (from o in userratings
                                    where o.ISBN == rate.ISBN
                                    select o.Rate).First();
                        sum += (userR - rate.Rate) * (userR - rate.Rate);
                    }
                    var sqr = Math.Sqrt(sum);
                    result.Add(row.UserID, sqr);
                }

                // rank and get the books that are not rated by the user
                var orderedResult = result.OrderBy(a=>a.Value);
                // convert to similarity
                Dictionary<int, double> similist = new Dictionary<int, double>();
                foreach (var row in orderedResult)
                {
                        similist.Add(row.Key, 1 / (row.Value+1));
                }
                
                // the final result dictionary
                Dictionary<string, double> finalresult = new Dictionary<string, double>();
                // get books rated by other users not rated by main user
                foreach (var row in similist)
                {
                    var nonrated = book.Ratings.Where(a => row.Key == a.UserID && !isbn.Contains(a.ISBN) && a.Rate > 3).OrderByDescending(a => a.Rate).Take(10);
                    // gte disliked isbn
                    var disliked = book.Dislikes.Where(a=>a.UserID == userid).Select(a=>a.ISBN);
                    
                    foreach (var rate in nonrated)
                    {
                        if (!disliked.Contains(rate.ISBN))
                        {
                            if (finalresult.Keys.Contains(rate.ISBN))
                            {
                                finalresult[rate.ISBN] += rate.Rate * row.Value;
                            }
                            else
                            {
                                finalresult.Add(rate.ISBN, rate.Rate * row.Value);
                            }
                        }
                    }
                    
                }
                // rearrange final result 
                var finalresult2 = finalresult.OrderByDescending(a => a.Value).Take(5);
              
                // get the books
                //Dictionary<Book, double> ii = new Dictionary<Book, double>();
                List<string> ids = new List<string>();
                foreach (var row in finalresult2)
                {
                    ids.Add(row.Key);
                    //var r = (from o in book.Books
                    //         where o.ISBN == row.Key
                    //         select o).First();
                    //ii.Add(r, row.Value);
                }
                var rr = (from o in book.Books
                         where ids.Contains(o.ISBN)
                         select o).ToList();
                return rr;
                //return ii;
            }
        }
    }
}
using Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication2.Entity;

namespace Sample.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            int id = 5;
            Info info = new Info();
            using (var x = new MyEntities())
            {
                
                var allAccount = x.Accounts.FirstOrDefault(a => a.AccountId == id);
                var allBook = x.Books.ToList();
                var allResrv = x.BookReservations.Where(br => br.AccountId == id).ToList();
                info.Account.Add(allAccount);
                info.Book.AddRange(allBook);
                info.BookReservation.AddRange(allResrv);
                info.BookReservationInfo.AddRange(allResrv.Select(b =>
                    new BookReservationInfo
                    {
                        Id = b.Id,
                        BookName = allBook.FirstOrDefault(book => book.BookId == b.BookId).Name,
                        BorrowersName = x.Accounts.FirstOrDefault(account => account.AccountId == b.AccountId).Name,
                        Qty = (int)b.Qty
                    }));
            }
            return View(info);
        }
        public ActionResult Reserve(int AccountId, int BookId, int Qty)
        {
            using (var db = new MyEntities())
            {
                var selected = db.Accounts.FirstOrDefault(x => x.AccountId == AccountId);
                db.BookReservations.AddObject(new BookReservation {AccountId = selected.AccountId, BookId = BookId, Qty = Qty });
                db.SaveChanges();
                  
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete()
        {
            using (var db = new MyEntities())
            {

                foreach(var ent in db.BookReservations)
                {
                    db.BookReservations.DeleteObject(ent);
                }
               
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
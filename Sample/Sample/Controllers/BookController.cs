using Sample.Entity;
using Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            Info info = new Info();
            using (var x = new MyDbEntities())
            {
                var allAccount = x.Accounts.OrderBy(xxx => Guid.NewGuid()).FirstOrDefault();
                var allBook = x.Books.ToList();
                var allResrv = x.BookReservations.ToList();
                info.Account.Add(allAccount);
                info.Book.AddRange(allBook);
                info.BookReservation.AddRange(allResrv);
                info.BookReservationInfo.AddRange(allResrv.Select(b =>
                    new BookReservationInfo
                    {
                        Id = b.Id,
                        BookName = allBook.FirstOrDefault(book => book.BookId == b.BookId).Name,
                        BorrowersName = x.Accounts.FirstOrDefault(account => account.AccountId == b.AccountId).Name,
                        Qty = b.Qty
                    }));
            }
            return View(info);
        }
        public ActionResult Reserve(int AccountId, int BookId, int Qty)
        {
            using (var db = new MyDbEntities())
            {
                var selected = db.Accounts.FirstOrDefault(x => x.AccountId == AccountId);
                db.BookReservations.Add(new BookReservation {AccountId = selected.AccountId, BookId = BookId, Qty = Qty });
                db.SaveChanges();
                  
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete()
        {
            using (var db = new MyDbEntities())
            {
                db.BookReservations.RemoveRange(db.BookReservations);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
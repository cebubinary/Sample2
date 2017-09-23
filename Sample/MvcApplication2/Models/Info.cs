
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication2.Entity;

namespace Sample.Models
{
    public class Info
    {
        public List<Account> Account { get; set; }
        public List<Book> Book { get; set; }
        public List<BookReservation> BookReservation { get; set; }
        public List<BookReservationInfo> BookReservationInfo { get; set; }

        public Info()
        {
            Account = new List<Account>();
            Book = new List<Book>();
            BookReservation = new List<BookReservation>();
            BookReservationInfo = new List<Models.BookReservationInfo>();
        }
    }

    //public class Account {
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    //public class Book
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public int? Qty { get; set; }
    //}
    public class BookReservationInfo
    {
        public int Id { get; set; }
        public string BorrowersName { get; set; }
        public string BookName { get; set; }
        public int? Qty { get; set; }
    }
}
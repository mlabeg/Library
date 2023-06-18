using MenuUITools;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence
{
    public class BooksRepository
    {
        public BooksRepository()
        {
            _database.Add(new Book("Stary człowiek i morze", "Ernest Hemingway", 1986, "AAAA", 10, 19.99m));
            _database.Add(new Book("Komu bije dzwon", "Ernest Hemingway", 1997, "BBBB", 0, 119.99m));
            _database.Add(new Book("Alicja w krainie czarów", "C.K. Lewis", 1998, "CCCC", 53, 39.99m));
            _database.Add(new Book("Opowieści z Narnii", "C.K. Lewis", 1999, "DDDD", 33, 49.99m));
            _database.Add(new Book("Harry Potter", "J.K. Rowling", 2000, "EEEE", 23, 69.99m));
            _database.Add(new Book("Paragraf 22", "Joseph Heller", 2001, "FFFF", 5, 45.99m));
            _database.Add(new Book("Lalka", "Bolesław Prus", 2002, "GGGG", 7, 76.99m));
            _database.Add(new Book("To", "Stephen King", 2003, "HHHH", 2, 12.99m));
            _database.Add(new Book("Idiota", "Fiodor Dostojewski", 1950, "IIII", 89, 25.99m));
            _database.Add(new Book("Mistrz i Małgorzata", "Michaił Bułhakow", 1965, "JJJJ", 41, 36.99m));
        }
        //

        readonly List<Book> _database= new List<Book>();
		readonly Menu _wybor =new Menu();
      //_wybor.Kon        TODO  dlaczego nie mogę użyć zmiennej zadeklarowanej linijkę wyżej?
        
     



        public void Insert(Book book)
        {
            _database.Add(book);
        }
        public List<Book> GetAll()
        {
            return _database;
        }
        public bool RemoveByTitle(string title)
        {
            try
            {
                _database.Remove(_database.First(x => x.Title == title));
            }
            catch
            {
                return false;
            }

            return true;

        }
        public void ChangeState(string title, int stateChange)
        {
            _database.First(x => x.Title == title).State = stateChange;

        }
        public Book BookInfo(int id)
        {
            return _database[id];
        }
    }
}
//TODO dodać połączenie do EF
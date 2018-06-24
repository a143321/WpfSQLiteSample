using System.Collections.Generic;

namespace WPFSample4.Models
{
    public class BookList
    {
        /// <summary>
        /// 書籍リストクラス
        /// </summary>
        private List<Book> books;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BookList()
        {
            this.books = new List<Book>();
        }

        /// <summary>
        /// リストを取得する
        /// </summary>
        /// <returns></returns>
        public List<Book> GetList()
        {
            return new List<Book>(books);
        }

        /// <summary>
        /// 指定データをリストに追加する
        /// </summary>
        /// <param name="someBook"></param>
        /// <returns></returns>
        public List<Book> Add(Book someBook)
        {
            books.Add(someBook);

            return new List<Book>(books);
        }

        /// <summary>
        /// 指定データをリストから削除する
        /// </summary>
        /// <param name="someBook"></param>
        /// <returns></returns>
        public List<Book> Del(Book someBook)
        {
            foreach(var book in books)
            {
                if(book.ID == someBook.ID)
                {
                    books.Remove(book);
                    break;
                }

            }
            return new List<Book>(books);
        }

        /// <summary>
        /// 全データをリストから削除する
        /// </summary>
        public void DelAll()
        {
            books.Clear();
        }
    }
}

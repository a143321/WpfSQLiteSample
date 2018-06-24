using Prism.Mvvm;

namespace WPFSample4.Models
{
    /// <summary>
    /// 書籍クラス
    /// </summary>
    public class Book : BindableBase
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { this.SetProperty(ref this._ID, value); }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { this.SetProperty(ref this._Title, value); }
        }

        private string _Author;
        public string Author
        {
            get { return _Author; }
            set { this.SetProperty(ref this._Author, value); }
        }

        private int _Price;
        public int Price
        {
            get { return _Price; }
            set { this.SetProperty(ref this._Price, value); }
        }

        public Book()
        {

        }

        public Book(int someID, string someTitle, string someAuthor, int somePrice)
        {
            this.ID = someID;
            this.Title = someTitle;
            this.Author = someAuthor;
            this.Price = somePrice;
        }

    }
}

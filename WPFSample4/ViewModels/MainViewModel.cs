using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using WPFSample4.Models;

namespace WPFSample4.ViewModels
{
    /// <summary>
    /// ViewModel用クラス
    /// </summary>
    public class MainViewModel : BindableBase
    {
        /// <summary>
        /// 入力フィールド用の書籍インスタンス
        /// </summary>
        private Book _Book;
        public Book Book
        {
            get { return _Book; }
            set { this.SetProperty(ref this._Book, value); }
        }

        /// <summary>
        /// 書籍リスト
        /// </summary>
        private BookList books;

        /// <summary>
        /// 書籍データを格納するデータベース
        /// </summary>
        private DataBaseManager DataBaseManager;

        /// <summary>
        /// バインディング用コレクションリスト
        /// </summary>
        public ObservableCollection<Book> CollectionList { get; set; }

        /// <summary>
        /// 追加コマンドデリゲート
        /// </summary>
        private DelegateCommand _AddComamnd;
        public DelegateCommand AddCommand
        {
            get { return _AddComamnd = _AddComamnd ?? new DelegateCommand(AddCommandExecute); }
        }

        /// <summary>
        /// 削除コマンドデリゲート
        /// </summary>
        private DelegateCommand<Book> _DelComamnd;
        public DelegateCommand<Book> DelComamnd
        {
            get { return _DelComamnd = _DelComamnd ?? new DelegateCommand<Book>(DelCommandExecute); }
        }

        /// <summary>
        /// 全削除コマンドデリゲート
        /// </summary>
        private DelegateCommand _DelAllComamnd;
        public DelegateCommand DelAllComamnd
        {
            get { return _DelAllComamnd = _DelAllComamnd ?? new DelegateCommand(DelAllCommandExecute); }
        }

        /// <summary>
        /// 追加コマンドを実行する
        /// </summary>
        private void AddCommandExecute()
        {
            Book addBook = new Book(DataBaseManager.GetIDNextData(), Book.Title, Book.Author, Book.Price);
            books.Add(addBook);
            DataBaseManager.AddDataBase(addBook);


            CollectionList.Add(addBook);
        }

        /// <summary>
        /// 削除コマンドを実行する
        /// </summary>
        /// <param name="someBook"></param>
        private void DelCommandExecute(Book someBook)
        {
            books.Del(someBook);

            DataBaseManager.DelDataBase(someBook);

            CollectionList.Clear();

            foreach(var book in books.GetList())
            {
                CollectionList.Add(book);
            }
        }

        /// <summary>
        /// 全削除コマンドを実行する
        /// </summary>
        /// <param name="someBook"></param>
        private void DelAllCommandExecute()
        {
            books.DelAll();

            DataBaseManager.DelAllDataBase();

            CollectionList.Clear();
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainViewModel()
        {
            Book = new Book();

            books = new BookList();

            DataBaseManager = new DataBaseManager();

            if (!DataBaseManager.IsExistDataBaseFile())
            {
                DataBaseManager.CreateDataBase();

                books.Add(new Book() { ID = 1, Title = "簡単すぎる本", Author = "アンドロイド1号", Price = 100 });
                books.Add(new Book() { ID = 2, Title = "普通すぎる本", Author = "アンドロイド2号", Price = 200 });
                books.Add(new Book() { ID = 3, Title = "難しすぎる本", Author = "アンドロイド3号", Price = 300 });

                foreach(var book in books.GetList())
                {
                    DataBaseManager.AddDataBase(book);
                }
            }
            else
            {
                foreach(var book in DataBaseManager.GetDataBase())
                {
                    books.Add(book);
                }
            }



            CollectionList = new ObservableCollection<Book>(books.GetList());
        }
    }
}

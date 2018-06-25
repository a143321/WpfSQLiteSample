using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace WPFSample4.Models
{
    /// <summary>
    /// データベース管理クラス
    /// </summary>
    public class DataBaseManager
    {
        private static readonly string FileName = @"BookList.db";
        private readonly string ConnectionString = null;
        private readonly string DataBaseFilePath = System.AppDomain.CurrentDomain.BaseDirectory + FileName;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataBaseManager()
        {
            var builder = new SQLiteConnectionStringBuilder()
            {
                DataSource = FileName
            };

            ConnectionString = builder.ToString();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataBaseManager(BookList someList)
        {
            var builder = new SQLiteConnectionStringBuilder()
            {
                DataSource = FileName
            };

            ConnectionString = builder.ToString();
        }

        /// <summary>
        /// データベースは存在するかどうか
        /// </summary>
        /// <returns></returns>
        public bool IsExistDataBaseFile()
        {
            return System.IO.File.Exists(DataBaseFilePath);
        }

        /// <summary>
        /// データベースを作成する
        /// </summary>
        public void CreateDataBase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                // データベースに接続
                connection.Open();
                // コマンドの実行
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE T_BOOKLIST(ID int PRIMARY KEY, Title string, Author string, Price int)";
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// データベースの全情報を取得する
        /// </summary>
        /// <returns></returns>
        public List<Book> GetDataBase()
        {
            var list = new List<Book>();

            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // SQLの設定
                    command.CommandText = @"SELECT * FROM T_BOOKLIST";

                    // SQLの実行
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            Int32.TryParse(reader["ID"].ToString(), out int id);
                            Int32.TryParse(reader["Price"].ToString(), out int price);

                            list.Add(new Book()
                            {
                                ID = id,
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                Price = price
                            });
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    throw;
                }
            }
            return list;
        }

        /// <summary>
        /// データベースにデータを追加する
        /// </summary>
        /// <returns></returns>
        public void AddDataBase(Book someBook)
        {
            var list = new List<Book>();

            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // SQLの設定
                    command.CommandText = @"INSERT INTO T_BOOKLIST (ID, Title, Author, Price) VALUES (@ID, @Title, @Author, @Price)";
                    command.Parameters.Add(new SQLiteParameter("@ID", someBook.ID));
                    command.Parameters.Add(new SQLiteParameter("@Title", someBook.Title));
                    command.Parameters.Add(new SQLiteParameter("@Author", someBook.Author));
                    command.Parameters.Add(new SQLiteParameter("@Price", someBook.Price));
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// データベースの指定データを削除する
        /// </summary>
        public void DelDataBase(Book someBook)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // SQLの設定
                    command.CommandText = @"DELETE FROM T_BOOKLIST WHERE ID=@ID";

                    command.Parameters.Add(new SQLiteParameter("@ID", someBook.ID));

                    // SQLの実行
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// データベースの全データを削除する
        /// </summary>
        public void DelAllDataBase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // SQLの設定
                    command.CommandText = "DELETE FROM T_BOOKLIST";

                    // SQLの実行
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// データベースに格納する次のデータのID値を返す
        /// </summary>
        /// <remarks>
        /// データベースにデータがない場合は、戻り値として1を返す
        /// </remarks>
        /// <returns></returns>
        public int GetIDNextData()
        {
            var resultID = 0;

            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // SQLの設定
                    command.CommandText = @"SELECT MAX(ID) FROM T_BOOKLIST";

                    // SQLの実行
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            resultID = reader.GetInt32(0);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultID + 1;
        }
    }
}

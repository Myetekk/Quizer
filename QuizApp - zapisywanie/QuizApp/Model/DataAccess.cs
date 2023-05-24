using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace QuizApp.Model
{
    class DataAccess
    {
        static SQLiteConnection conn = new SQLiteConnection(@"Data Source= .\..\..\..\..\data_base.db; Version=3");

        public DataAccess()
        {

        }

        public void DeleteIfExists(SQLiteConnection conn, string chosenName)
        {
            SQLiteCommand command;
            command = conn.CreateCommand();

            command.CommandText = $"DROP TABLE IF EXISTS {chosenName};";
            command.ExecuteNonQuery();

            command.CommandText = $"CREATE TABLE {chosenName} (" +
                $"id INTEGER PRIMARY KEY," +
                $"question TEXT," +
                $"answer_1 TEXT," +
                $"answer_2 TEXT," +
                $"answer_3 TEXT," +
                $"answer_4 TEXT," +
                $"correct_answer TEXT);";
            command.ExecuteNonQuery();

                //$"correct_answer INTEGER);";
        }

        private void ReadData(SQLiteConnection conn, List<Quiz> quizzes, string chosenName)
        {
            DeleteIfExists(conn, chosenName);

            //SQLiteDataReader reader;
            SQLiteCommand command;

            var key = "b14ca5898a4e4133bbce2ea2315a1916";

            command = conn.CreateCommand();
            foreach (Quiz quizQuest in quizzes)
            {
                quizQuest.QuizQuestion = EncryptString(key, quizQuest.QuizQuestion);
                quizQuest.AAnswer = EncryptString(key, quizQuest.AAnswer);
                quizQuest.BAnswer = EncryptString(key, quizQuest.BAnswer);
                quizQuest.CAnswer = EncryptString(key, quizQuest.CAnswer);
                quizQuest.DAnswer = EncryptString(key, quizQuest.DAnswer);
                string Correct = EncryptString(key, quizQuest.Correct.ToString());


                command.CommandText = $"INSERT INTO {chosenName} (question, answer_1, answer_2, answer_3, answer_4, correct_answer)" +
                //$"VALUES ('{quizQuest.QuizQuestion}', '{quizQuest.AAnswer}', '{quizQuest.BAnswer}', '{quizQuest.CAnswer}', '{quizQuest.DAnswer}', '{quizQuest.Correct}');";
                $"VALUES ('{quizQuest.QuizQuestion}', '{quizQuest.AAnswer}', '{quizQuest.BAnswer}', '{quizQuest.CAnswer}', '{quizQuest.DAnswer}', '{Correct}');";
                command.ExecuteNonQuery();
            }

            

            /*command.CommandText = "SELECT * FROM quiz_1";

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                long id = (long)reader["id"];
                string question = (string)reader["question"];
                string answer_1 = (string)reader["answer_1"];
                string answer_2 = (string)reader["answer_2"];
                string answer_3 = (string)reader["answer_3"];
                string answer_4 = (string)reader["answer_4"];
                long correct_answer = (long)reader["correct_answer"];
                //kolejne atyrbuty
                Console.WriteLine($"{id} {question} {answer_1} {answer_2} {answer_3} {answer_4} {correct_answer}");
            }*/

           
        }

        public void ReadData(List<Quiz> quizzes, string chosenName)
        {
            try
            {
                conn.Open();
                ReadData(conn, quizzes, chosenName);
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

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
                $"correct_answer INTEGER);";
            command.ExecuteNonQuery();

        }

        private void ReadData(SQLiteConnection conn, List<Quiz> quizzes, string chosenName)
        {
            DeleteIfExists(conn, chosenName);

            //SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            foreach (Quiz quizQuest in quizzes)
            {
                command.CommandText = $"INSERT INTO {chosenName} (question, answer_1, answer_2, answer_3, answer_4, correct_answer)" +
                $"VALUES ('{quizQuest.QuizQuestion}', '{quizQuest.AAnswer}', '{quizQuest.BAnswer}', '{quizQuest.CAnswer}', '{quizQuest.DAnswer}', {quizQuest.Correct});";
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

        
    }
}

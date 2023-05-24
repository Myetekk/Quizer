using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Quizer.Model
{
    class Read_data
    {
        SQLiteConnection conn = new SQLiteConnection(@"Data Source= .\..\..\..\..\data_base.db; Version=3");
        SQLiteDataReader reader;
        SQLiteCommand command;

        public Read_data()
        {

        }

        public string[] read_question(int number_of_info, string quiz_selected, int integer, long question_number)
        {
            string[] result = new string[number_of_info];

            conn.Open();
            command = conn.CreateCommand();
            command.CommandText = $"SELECT * FROM {quiz_selected} where id is {integer}";
            reader = command.ExecuteReader();
            reader.Read();

            result[0] = quiz_selected;
            result[1] = "Pytanie nr: " + (long)reader["id"] + "/" + question_number;
            result[3] = (string)reader["question"];
            result[4] = (string)reader["answer_1"];
            result[5] = (string)reader["answer_2"];
            result[6] = (string)reader["answer_3"];
            result[7] = (string)reader["answer_4"];

            conn.Close();

            return result;
        }

        public long correct_answer(string quiz_selected, int integer)
        {
            conn.Open();
            command = conn.CreateCommand();
            command.CommandText = $"SELECT * FROM {quiz_selected} where id is {integer}";
            reader = command.ExecuteReader();
            reader.Read();
            long answer_correct = (long)reader["correct"];
            conn.Close();

            return answer_correct;
        }

        public long count_questions(string quiz_selected)
        {
            conn.Open();
            command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) as ile FROM {quiz_selected}";
            reader = command.ExecuteReader();
            reader.Read();
            long result = (long)reader["ile"];
            conn.Close();

            return result;
        }


        public long count_quizes()
        {
            long ile;

            conn.Open();
            command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) as ile FROM sqlite_master WHERE name not like 'sqlite_sequence'";
            reader = command.ExecuteReader();
            reader.Read();
            ile = (long)reader["ile"];
            conn.Close();

            return ile;
        }

        public string[] items_to_combobox(long ile)
        {
            string[] result = new string[ile];
            int i = 0;
            conn.Open();
            command = conn.CreateCommand();
            command.CommandText = $"SELECT name FROM sqlite_master WHERE type = 'table' and name not like 'sqlite_sequence' ";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                result[i] = (string)reader["name"];
                i++;
            }
            conn.Close();

            return result;
        }
    }
}

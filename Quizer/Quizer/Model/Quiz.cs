using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace Quizer.Model
{
    class Quiz
    {
        static SQLiteConnection conn = new SQLiteConnection(@"Data Source=D:\MYETEK\Pulpit\data_base.db; Version=3");
        public int number_of_info = 10;
        public int integer = 1;
        public long question_number = 0;

        public Quiz()
        {
        }

        public string[] read()
        {
            string[] result = new string[number_of_info];
            long score = 0;

            SQLiteDataReader reader;
            SQLiteCommand command;

            conn.Open();

            command = conn.CreateCommand();
            command.CommandText = $"SELECT count(*) as ile FROM quiz_2";
            reader = command.ExecuteReader();
            reader.Read();
            question_number = (long)reader["ile"];


            if (integer < 1 || integer > question_number)
            {
                result[3] = "KONIEC";
            }
            else
            {
                command = conn.CreateCommand();
                command.CommandText = $"SELECT * FROM quiz_2 where id is {integer}";
                reader = command.ExecuteReader();

                reader.Read();

                result[1] = "Pytanie nr: " + (long)reader["id"] + "/" + question_number;
                result[2] = "Wynik: " + score;
                result[3] = (string)reader["question"];
                result[4] = (string)reader["answer_1"];
                result[5] = (string)reader["answer_2"];
                result[6] = (string)reader["answer_3"];
                result[7] = (string)reader["answer_4"];
            }


            conn.Close();
            return result;
        }

        public string[] clear()
        {
            integer = 1;
            string[] result = new string[number_of_info];
            for(int i=0; i< result.Length; i++)
            {
                result[i] = "";
            }
            return result;
        }




        public void next_question()
        {
            integer++;
        }


        public void previous_question()
        {
            integer--;
        }



        
    }
}

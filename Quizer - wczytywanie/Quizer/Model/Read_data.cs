using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.IO;

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
            var key = "b14ca5898a4e4133bbce2ea2315a1916";

            conn.Open();
            command = conn.CreateCommand();
            command.CommandText = $"SELECT * FROM {quiz_selected} where id is {integer}";
            reader = command.ExecuteReader();
            reader.Read();

            result[0] = quiz_selected;
            result[1] = "Pytanie nr: " + (long)reader["id"] + "/" + question_number;
            //result[3] = (string)reader["question"];
            //result[4] = (string)reader["answer_1"];
            //result[5] = (string)reader["answer_2"];
            //result[6] = (string)reader["answer_3"];
            //result[7] = (string)reader["answer_4"];

            result[3] = DecryptString(key, (string)reader["question"]);
            result[4] = DecryptString(key, (string)reader["answer_1"]);
            result[5] = DecryptString(key, (string)reader["answer_2"]);
            result[6] = DecryptString(key, (string)reader["answer_3"]);
            result[7] = DecryptString(key, (string)reader["answer_4"]);

            conn.Close();

            return result;
        }

        public long correct_answer(string quiz_selected, int integer)
        {
            var key = "b14ca5898a4e4133bbce2ea2315a1916";

            conn.Open();
            command = conn.CreateCommand();
            command.CommandText = $"SELECT * FROM {quiz_selected} where id is {integer}";
            reader = command.ExecuteReader();
            reader.Read();
            //long answer_number = (long)reader["correct_answer"];
            //string answer_number = (string)reader["correct_answer"];
            string answer_number = DecryptString(key, (string)reader["correct_answer"]);
            conn.Close();

            int d = int.Parse(answer_number.Substring(6,2));
            int c = int.Parse(answer_number.Substring(4,2));
            int b = int.Parse(answer_number.Substring(2,2));
            int a = int.Parse(answer_number.Substring(0,2));

            int answer_correct = 0;
            if (a % 5 == 0)
                answer_correct = 1;
            else if (b % 5 == 0)
                answer_correct = 2;
            else if (c % 5 == 0)
                answer_correct = 3;
            else if (d % 5 == 0)
                answer_correct = 4;

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

        private static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}

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
        public Model.Read_data reading_data = new Model.Read_data();

        static SQLiteConnection conn = new SQLiteConnection(@"Data Source= .\..\..\..\data_base.db; Version=3");
        int number_of_info = 8;
        public int integer = 1;
        public long question_number = 0;
        public string quiz_selected = "quiz_1";
        long score = 0;

        static int max_question_number = 100;

        bool[] answers = new bool[max_question_number];
        public long[] answers_checked = new long[max_question_number];
        long answer_correct = 0;


        public Quiz()
        {
            for (int i = 0; i < max_question_number; i++)
            {
                answers[i] = false;
            }
        }

        public string[] read()
        {
            question_number = reading_data.count_questions(quiz_selected);


            string[] result = reading_data.read_question(number_of_info, quiz_selected, integer, question_number);
            answer_correct = reading_data.correct_answer(quiz_selected, integer);

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

        public string[] finish()
        {
            string[] result = new string[number_of_info];
            for(int i=0; i< max_question_number; i++)
            {
                if (answers[i] == true)
                    score++;
            }
            result[3] = "Wynik: " + score + " / " + question_number;
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



        
        public void answering(long answer_user)
        {
            answers_checked[integer] = answer_user;

            if (answer_user == answer_correct)
                answers[integer] = true;
            else
                answers[integer] = false;
        }
    }
}

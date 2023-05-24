using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Model
{
    class Quiz
    {
        public string QuizQuestion { get; set; }
        public string AAnswer { get; set; }
        public string BAnswer { get; set; }
        public string CAnswer { get; set; }
        public string DAnswer { get; set; }
        public int Correct { get; set; }

        public Quiz(string question, string aAnswer, string bAnswer, string cAnswer, string dAswer, int correct)
        {
            this.QuizQuestion = question;
            this.AAnswer = aAnswer;
            this.BAnswer = bAnswer;
            this.CAnswer = cAnswer;
            this.DAnswer = dAswer;
            this.Correct = correct;
        }
    }
}

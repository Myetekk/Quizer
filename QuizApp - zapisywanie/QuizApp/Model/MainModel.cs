using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Model
{
    class MainModel
    {
        public bool FieldContainsText(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                return false;
            }
            return true;
        }

        public bool AtLeastOneChecked(bool a, bool b, bool c, bool d)
        {
            if (a || b || c || d)
            {
                return true;
            }
            return false;
        }

        public int CheckedAnswersToInt(bool a, bool b, bool c, bool d) // zamienia zaznaczine na osmiocyfrowa liczbe
        { 
            // jesli bedzie prawdziwe to liczba dwucyfowa odpowiadajaca musi byc podziela przez 5
            Random rnd = new Random();

            int finalNumber = 0;

            bool[] answers = { d, c, b, a }; // kolejnosc odwrotna zeby na koncu po dodaniu bylo D a na poczatku A
            int leap = 1;
            
            for (int i = 0; i <= 3; i++)
            {
                int unitDig; // cyfra jednosci
                if (answers[i])
                {
                    unitDig = rnd.Next(0, 2) * 5;
                }
                else
                {
                    unitDig = rnd.Next(0, 10);
                    while (unitDig == 0 || unitDig == 5)
                    {
                        unitDig = rnd.Next(0, 10);
                    }
                }

                int tensDigit = rnd.Next(1, 10);

                unitDig += tensDigit * 10;

                finalNumber += unitDig * leap;
                leap *= 100;
            }

            return finalNumber;
        }

        public bool[] WhichCorrect(int liczba)
        {
            int aNum, bNum, cNum, dNum;

            dNum = liczba % 100;
            cNum = liczba / 100 % 100;
            bNum = liczba / 10000 % 100;
            aNum = liczba / 1000000 % 100;

            bool[] wynik = { aNum % 5 == 0, bNum % 5 == 0, cNum % 5 == 0, dNum % 5 == 0 };

            return wynik;

        }
    }
}

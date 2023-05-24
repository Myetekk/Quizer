using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Timers;

namespace QuizApp.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Model.DataAccess databaseAccess = new Model.DataAccess();
        public Model.MainModel _model = new Model.MainModel();


        // tworzenie listy na pytania, ktora nastepnie zapisuje do bazy danych
        public List<Model.Quiz> quizList = new List<Model.Quiz>();
        public int listIndex = 0;

        public MainViewModel()
        {

        }

        private string dodajZmien = "Dodaj Pytanie";

        public string DodajZmien
        {
            get => dodajZmien;
            set
            {
                dodajZmien = value;
                //zgłoszenie zmiany właściwości
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DodajZmien)));
            }
        }


        private string quizName;

        public string QuizName
        {
            get => quizName;

            set
            {
                quizName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuizName)));
            }
        }


        private int questionIndex = 1;

        public int QuestionIndex
        {
            get => questionIndex;
            set
            {
                questionIndex = value;
                //zgłoszenie zmiany właściwości
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionIndex)));
            }
        }

        private string question;

        public string Question
        {
            get => question;
            set
            {
                question = value;
                //zgłoszenie zmiany właściwości
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Question)));
            }
        }



        private string a;

        public string A
        {
            get => a;
            set
            {
                a = value;
                //zgłoszenie zmiany właściwości
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(A)));
            }
        }



        private string b;

        public string B
        {
            get => b;
            set
            {
                b = value;
                //zgłoszenie zmiany właściwości
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(B)));
            }
        }



        private string c;

        public string C
        {
            get => c;
            set
            {
                c = value;
                //zgłoszenie zmiany właściwości
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(C)));
            }
        }



        private string d;

        public string D
        {
            get => d;
            set
            {
                d = value;
                //zgłoszenie zmiany właściwości
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(D)));
            }
        }


        // Zmienna do sprawdzania czy A to poprawna odpowiedz
        private bool aChecked = false;
        public bool AChecked
        {
            get => aChecked;
            set
            {
                aChecked = value;
                //zgłoszenie zmiany właściwości
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AChecked)));
            }
        }

        // Zmienna do sprawdzania czy B to poprawna odpowiedz
        private bool bChecked = false;
        public bool BChecked
        {
            get => bChecked;
            set
            {
                bChecked = value;
                //zgłoszenie zmiany właściwości
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BChecked)));
            }
        }

        // Zmienna do sprawdzania czy C to poprawna odpowiedz
        private bool cChecked = false;
        public bool CChecked
        {
            get => cChecked;
            set
            {
                cChecked = value;
                //zgłoszenie zmiany właściwości
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CChecked)));
            }
        }

        // Zmienna do sprawdzania czy D to poprawna odpowiedz
        private bool dChecked = false;
        public bool DChecked
        {
            get => dChecked;
            set
            {
                dChecked = value;
                //zgłoszenie zmiany właściwości
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DChecked)));
            }
        }



        private ICommand dodaj;

        public ICommand Dodaj
        {
            get
            {
                if (dodaj == null)
                    dodaj = new RelayCommand(

                    (o) =>
                    {
                        if (listIndex == quizList.Count)
                        {
                            //BChecked = CChecked = DChecked = false; // sprawiało, że tylko odpowaiedź "a" może być poprawna
                            quizList.Add(new Model.Quiz(Question, A, B, C, D, _model.CheckedAnswersToInt(AChecked, BChecked, CChecked, DChecked)));
                            Question = A = B = C = D = "";
                            AChecked = BChecked = CChecked = DChecked = false;
                        }
                        else
                        {
                            quizList[listIndex] = new Model.Quiz(Question, A, B, C, D, _model.CheckedAnswersToInt(AChecked, BChecked, CChecked, DChecked));
                        }

                        
                        if (listIndex == quizList.Count() - 1)
                        {
                            
                            listIndex++;
                            QuestionIndex++;
                        }
                    }
                    ,

                    // sprawdza czy wszystkie pola są wypełnione i conajmniej jedna z odpowiedzi zaznaczona jako prawidlowa

                    (o) => _model.FieldContainsText(A) &&
                    _model.FieldContainsText(B) &&
                    _model.FieldContainsText(C) &&
                    _model.FieldContainsText(D) &&
                    _model.FieldContainsText(Question) &&
                    _model.AtLeastOneChecked(AChecked, BChecked, CChecked, DChecked)

                    );
                return dodaj;
            }
        }


        private ICommand zapisz;

        public ICommand Zapisz
        {
            get
            {
                if (zapisz == null)
                    zapisz = new RelayCommand(

                    (o) =>
                    {
                        databaseAccess.ReadData(quizList, quizName);
                        quizList.Clear();
                        listIndex = 0;
                        Question = A = B = C = D = QuizName = "";
                        AChecked = BChecked = CChecked = DChecked = false;
                    }
                    ,
                    (o) => _model.FieldContainsText(QuizName) && (quizList.Count() != 0) // jezeli lista jest pusta, to nie mozna jej zapisac
                    );
                return zapisz;
            }
        }

        private ICommand nastepny;

        public ICommand Nastepny
        {
            get
            {
                if (nastepny == null)
                    nastepny = new RelayCommand(

                    (o) =>
                    {
                        listIndex++;
                        QuestionIndex = listIndex + 1;
                        if (listIndex == quizList.Count())
                        {
                            DodajZmien = "Dodaj pytanie";
                            Question = A = B = C = D = "";
                            AChecked = BChecked = CChecked = DChecked = false;
                        }
                        else
                        {
                            Question = quizList[listIndex].QuizQuestion;
                            A = quizList[listIndex].AAnswer;
                            B = quizList[listIndex].BAnswer;
                            C = quizList[listIndex].CAnswer;
                            D = quizList[listIndex].DAnswer;
                            bool[] answers = _model.WhichCorrect(quizList[listIndex].Correct);
                            AChecked = answers[0];
                            BChecked = answers[1];
                            CChecked = answers[2];
                            DChecked = answers[3];
                            DodajZmien = "Zmień pytanie";
                        }

                    }
                    ,
                    (o) => listIndex < quizList.Count()
                    );
                return nastepny;
            }
        }

        private ICommand poprzedni;

        public ICommand Poprzedni
        {
            get
            {
                if (poprzedni == null)
                    poprzedni = new RelayCommand(

                    (o) =>
                    {
                        listIndex--;
                        QuestionIndex = listIndex + 1;
                        if (listIndex != quizList.Count())
                        {
                            Question = quizList[listIndex].QuizQuestion;
                            A = quizList[listIndex].AAnswer;
                            B = quizList[listIndex].BAnswer;
                            C = quizList[listIndex].CAnswer;
                            D = quizList[listIndex].DAnswer;
                            bool[] answers = _model.WhichCorrect(quizList[listIndex].Correct);
                            AChecked = answers[0];
                            BChecked = answers[1];
                            CChecked = answers[2];
                            DChecked = answers[3];
                            DodajZmien = "Zmień pytanie";
                        }
                    }
                    ,
                    (o) => listIndex > 0
                    );
                return poprzedni;
            }
        }

    }
}

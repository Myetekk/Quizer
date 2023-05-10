using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Quizer.ViewModel
{
    using BaseClass;
    class MainViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Model.Quiz info_class = new Model.Quiz();

        private static bool isRun_start = true;
        private static bool isRun_stop = false;
        private static bool isRun_next_question = false;
        private static bool isRun_previous_question = false;
        private static bool isRun_answers = false;


        private string[] info;
        public string[] Info
        {
            get { return info; }
            private set
            {
                info = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Info)));
            }
        }




        private ICommand info_read;
        public ICommand Info_read
        {
            get { return info_read ?? (info_read = new BaseClass.RelayCommand( (p) => { 
                Info = info_class.read();

                isRun_start = false;
                isRun_stop = true;
                isRun_next_question = true;
                isRun_previous_question = false;
                isRun_answers = true;

                MainWindow.time = Convert.ToDateTime("5/7/2023 0:0:0");
                MainWindow.timer.Start();
            }, p => isRun_start) ); }
        }


        private ICommand info_clear;
        public ICommand Info_clear
        {
            get { return info_clear ?? (info_clear = new BaseClass.RelayCommand( (p) => { 
                //info_class = new Model.Quiz();
                Info = info_class.clear();
                Info = info_class.finish();

                isRun_start = false;
                isRun_stop = false;
                isRun_next_question = false;
                isRun_previous_question = false;
                isRun_answers = false;

                MainWindow.timer.Stop();
            } ,  p => isRun_stop) ); }
        }




        private ICommand info_next_question;
        public ICommand Info_next_question
        {
            get { return info_next_question ?? (info_next_question = new BaseClass.RelayCommand( (p) => { 
                info_class.next_question();

                Info = info_class.read();

                if (info_class.integer == info_class.question_number)
                    isRun_next_question = false;
                else
                    isRun_next_question = true;

                if (info_class.integer == 1)
                    isRun_previous_question = false;
                else
                    isRun_previous_question = true;
            },  p => isRun_next_question) ); }
        }


        private ICommand info_previous_question;
        public ICommand Info_previous_question
        {
            get { return info_previous_question ?? (info_previous_question = new BaseClass.RelayCommand( (p) => { 
                info_class.previous_question(); 

                Info = info_class.read();

                if (info_class.integer == 1)
                    isRun_previous_question = false;
                else
                    isRun_previous_question = true;

                if (info_class.integer == info_class.question_number)
                    isRun_next_question = false;
                else
                    isRun_next_question = true;
            },  p => isRun_previous_question) ); }
        }




        private ICommand answer_1;
        public ICommand Answer_1
        {
            get
            {
                return answer_1 ?? (answer_1 = new BaseClass.RelayCommand((p) => {
                    info_class.answering(1);
                    //info_class.answers_checked[info_class.integer]
                    Info = info_class.read();
                }, p => isRun_answers));
            }
        }

        private ICommand answer_2;
        public ICommand Answer_2
        {
            get
            {
                return answer_2 ?? (answer_2 = new BaseClass.RelayCommand((p) => {
                    info_class.answering(2);
                    Info = info_class.read();
                }, p => isRun_answers));
            }
        }

        private ICommand answer_3;
        public ICommand Answer_3
        {
            get
            {
                return answer_3 ?? (answer_3 = new BaseClass.RelayCommand((p) => {
                    info_class.answering(3);
                    Info = info_class.read();
                }, p => isRun_answers));
            }
        }

        private ICommand answer_4;
        public ICommand Answer_4
        {
            get
            {
                return answer_4 ?? (answer_4 = new BaseClass.RelayCommand((p) => {
                    info_class.answering(4);
                    Info = info_class.read();
                }, p => isRun_answers));
            }
        }



        private ICommand answer_1_color;
        public ICommand Answer_1_color
        {
            get
            {
                return answer_1_color ?? (answer_1_color = new BaseClass.RelayCommand((p) => {
                    //info_class.answering(1);
                    //info_class.answers_checked[info_class.integer]



                    //Info = info_class.read();
                }, p => isRun_answers));
            }
        }
    }
}

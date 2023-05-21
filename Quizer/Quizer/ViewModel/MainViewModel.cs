using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace Quizer.ViewModel
{
    using BaseClass;
    using Quizer.Model;

    class MainViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Model.Quiz info_class = new Model.Quiz();
        public Model.Read_data reading_data = new Model.Read_data();
        public string selected;
        public string[] items_in_combobox;

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
                Quizes.Clear();

                isRun_start = false;
                isRun_stop = true;
                isRun_next_question = true;
                isRun_previous_question = false;
                isRun_answers = true;
                check_next_prev();

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

                check_next_prev();
            },  p => isRun_next_question) ); }
        }


        private ICommand info_previous_question;
        public ICommand Info_previous_question
        {
            get { return info_previous_question ?? (info_previous_question = new BaseClass.RelayCommand( (p) => { 
                info_class.previous_question(); 
                Info = info_class.read();

                check_next_prev();
            },  p => isRun_previous_question) ); }
        }


        private void check_next_prev()
        {
            if (info_class.integer == 1)
                isRun_previous_question = false;
            else
                isRun_previous_question = true;

            if (info_class.integer == info_class.question_number)
                isRun_next_question = false;
            else
                isRun_next_question = true;

            if (info_class.question_number == 1)
            {
                isRun_next_question = false;
                isRun_previous_question = false;
            }
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




        private ObservableCollection<Quiz_list> quizes = new ObservableCollection<Quiz_list>();
        public ObservableCollection<Quiz_list> Quizes
        {
            get { return quizes; }
            set { quizes = value; }
        }
        private Quiz_list selectedItem = new Quiz_list();
        public Quiz_list SelectedItem
        {
            get { return selectedItem; }
            set { 
                selectedItem = value;
                if (value != null)
                {
                    info_class.quiz_selected = value.ToString();
                    selected = value.ToString();
                }
            }
        }



        public MainViewModel()
        {
            long ile = reading_data.count_quizes();
            items_in_combobox = new string[ile];

            string[] result = reading_data.items_to_combobox(ile);

            for (int i=0; i<ile; i++)
            {
                items_in_combobox[i] = result[i];
                Quizes.Add(new Quiz_list() { Name = result[i] });
            }


        }
    }
}

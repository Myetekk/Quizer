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

        private static bool isRun = false;
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
                isRun = !isRun;
                isRun_next_question = true;
                isRun_previous_question = false;
                isRun_answers = true;

                MainWindow.time = Convert.ToDateTime("5/7/2023 0:0:0");
                MainWindow.timer.Start();
            }, p => !isRun) ); }
        }


        private ICommand info_clear;
        public ICommand Info_clear
        {
            get { return info_clear ?? (info_clear = new BaseClass.RelayCommand( (p) => { 
                Info = info_class.clear(); 
                isRun = !isRun;
                isRun_next_question = false;
                isRun_previous_question = false;
                isRun_answers = false;

                MainWindow.timer.Stop();
            } ,  p => isRun) ); }
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
                    
                }, p => isRun_answers));
            }
        }








        //private string _currentTime;
        //private void DispatcherTimerSetup()
        //{
        //    DispatcherTimer dispatcherTimer = new DispatcherTimer();
        //    dispatcherTimer.Interval = TimeSpan.FromMinutes(1);
        //    dispatcherTimer.Tick += new EventHandler(CurrentTimeText);
        //    dispatcherTimer.Start();
        //}

        //private void CurrentTimeText(object sender, EventArgs e)
        //{
        //    CurrentTime = DateTime.Now.ToString("HH:mm");
        //}

        //public string CurrentTime
        //{
        //    get { return _currentTime; }
        //    set
        //    {
        //        if (_currentTime != value)
        //            _currentTime = value;

        //        OnPropertyChanged("CurrentTime");
        //    }
        //}

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}

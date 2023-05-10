using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Windows.Threading;

namespace Quizer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DispatcherTimer timer = new DispatcherTimer();
        public static DateTime time = new DateTime();
        private TimeSpan time_span = new TimeSpan(0, 0, 0, 1);

        

        public bool answer_1_colored = false;
        public bool answer_2_colored = false;
        public bool answer_3_colored = false;
        public bool answer_4_colored = false;



        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            //ComboBoxDataBinding();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = time.ToString("HH:mm:ss");
            time += time_span;
        }

        private void answer_1_checked_Click(object sender, RoutedEventArgs e)
        {
            clear_colors();
            if (answer_1_colored == false)
                answer_1_checked.Background = Brushes.Gray;
            else
                answer_1_checked.Background = Brushes.LightGray;
            answer_1_colored = !answer_1_colored;
        }

        private void answer_2_checked_Click(object sender, RoutedEventArgs e)
        {
            clear_colors();
            if (answer_2_colored == false)
                answer_2_checked.Background = Brushes.Gray;
            else
                answer_2_checked.Background = Brushes.LightGray;
            answer_2_colored = !answer_2_colored;
        }

        private void answer_3_checked_Click(object sender, RoutedEventArgs e)
        {
            clear_colors();
            if (answer_3_colored == false)
                answer_3_checked.Background = Brushes.Gray;
            else
                answer_3_checked.Background = Brushes.LightGray;
            answer_3_colored = !answer_3_colored;
        }

        private void answer_4_checked_Click(object sender, RoutedEventArgs e)
        {
            clear_colors();
            if (answer_4_colored == false)
                answer_4_checked.Background = Brushes.Gray;
            else
                answer_4_checked.Background = Brushes.LightGray;
            answer_4_colored = !answer_4_colored;
        }

        private void clear_colors_Click(object sender, RoutedEventArgs e)
        {
            clear_colors();
        }

        private void clear_colors()
        {
            answer_1_checked.Background = Brushes.LightGray;
            answer_2_checked.Background = Brushes.LightGray;
            answer_3_checked.Background = Brushes.LightGray;
            answer_4_checked.Background = Brushes.LightGray;

            answer_1_colored = false;
            answer_2_colored = false;
            answer_3_colored = false;
            answer_4_colored = false;
        }

        //public void ComboBoxDataBinding()
        //{
        //    SQLiteConnection conn = new SQLiteConnection(@"Data Source=D:\MYETEK\Pulpit\data_base.db; Version=3");
        //    SQLiteDataReader reader;
        //    SQLiteCommand command;
        //    string test;

        //    conn.Open();
            
        //    command = conn.CreateCommand();
        //    command.CommandText = $"SELECT name FROM sqlite_master WHERE type = 'table' and name like 'quiz%' ";
        //    reader = command.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        test = (string)reader["name"];
        //        question_list.Items.Add(test);
        //    }

        //    conn.Close();
        //}

        //private void question_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //Model.Quiz info_class = new Model.Quiz();
        //    //info_class.quiz_selected = question_list.SelectedItem.ToString();

        //    //ViewModel.MainViewModel info_class.quiz_selected = question_list.SelectedItem.ToString();


        //    string selected = question_list.SelectedItem.ToString();

        //    MessageBox.Show(question_list.SelectedItem.ToString());
        //}
    }
}

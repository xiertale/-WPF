using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace MyTasks
{
    /// <summary>
    /// Логика взаимодействия для EditData.xaml
    /// </summary>
    public partial class EditData : Window
    {

        public Data Data { get; set; } = new Data();

        // В конструктор окна мы передаём задачу, с которым будем работать
        public EditData(Data data)
        {
            InitializeComponent();
            TitleTask.Text = data.Title;
            Description.Text = data.Description;
            Date.SelectedDate = data.Date;
            Data = data;
        }

        public EditData()
        {
            InitializeComponent();
            Data = new Data(TitleTask.Text, Description.Text, Date.SelectedDate);
        }

        // При подтверждении изменений мы перезаписываем Data 
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Data = new Data(TitleTask.Text, Description.Text, Date.SelectedDate);
        }

        // При нажатии на выйти возвращаем ДиалогРезалт фолз
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Windows.Automation.Peers;


namespace MyTasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string _sortingType = "По дате";

        public MainWindow()
        {
            InitializeComponent();
            GetData();
        }

        // Обновление текстблока Remember с сортировкой по дате, в случае отсутствия элементов просто выходим из функции 
        private void UpdateRemember()
        {
            if (DataList.Items.Count <= 0)
            {
                Remember.Text = "";
                return;
            }
            List<Data> data = Sort("По дате");
            Remember.Text = data[0].ToString();
            SaveData();
        }

        // Добавление нового задачи в ListBox с сохранением данных с помощью нового окна
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            EditData editData = new EditData();
            bool? result = editData.ShowDialog();

            if (result == true)
            {
                DataList.Items.Add(editData.Data);
            }
            UpdateRemember();
            FillListBox(Sort(_sortingType));
            SaveData();
        }

        // Сортировка по Дате или по Задаче
        private List<Data> Sort(string type)
        {
            List<Data> data = DataList.Items.Cast<Data>().ToList();

            if (type == "По задаче")
            {
                return Data.SortByTitle(data);
            }
            return Data.SortByDate(data);
        }

        // Заполнение ЛистБокса задачи
        private void FillListBox(List<Data> data)
        {
            DataList.Items.Clear();
            foreach (Data dataItem in data)
            {
                DataList.Items.Add(dataItem);
            }
        }

        // Показ выбранной задачи
        private void DataList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = DataList.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            UpdateSelectedInfo((Data)DataList.Items[index]);
            SaveData();
        }

        // Изменение типа сортировки
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _sortingType = ((TextBlock)SelectTypeSort.SelectedValue).Text;
            FillListBox(Sort(_sortingType));
        }

        // Сохранение данных в json-файл
        private void SaveData()
        {
            List<Data> data = DataList.Items.Cast<Data>().ToList();
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText("list.json", json);
        }

        // Получение данных из json-файла и обновление ListBox и текста в Remember
        private void GetData()
        {
            string readJson = File.ReadAllText("list.json");
            List<Data>? data = JsonConvert.DeserializeObject<List<Data>?>(readJson);
            if (data == null)
            {
                return;
            }
            FillListBox(data);
            UpdateRemember();
        }

        // Функция для изменения выбранной задачи
        private void Edit_Click(object sender, RoutedEventArgs e)
        {

            int index = DataList.SelectedIndex;

            if (index == -1 || DataList.Items.Count <= 0)
            {
                return;
            }

            Data data = (Data)DataList.Items[index];
            EditData editData = new EditData(data);

            bool? result = editData.ShowDialog();

            if (result == true)
            {
                data = editData.Data;
            }
            DataList.Items[index] = data;
            UpdateSelectedInfo(data);
            SaveData();
        }

        // Обновление данных о выбранной задачи
        private void UpdateSelectedInfo(Data data)
        {
            if (DataList.Items.Count <= 0)
            {
                Title.Text = string.Empty;
                Discription.Text = string.Empty;
                return;
            }

            Title.Text = data.Title;
            Discription.Text = data.Description;
        }

        // Функция удаления задачи из списка
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            int index = DataList.SelectedIndex;

            if (DataList.Items.Count <= 0 || index == -1)
            {
                return;
            }

            DataList.Items.RemoveAt(index);
            UpdateRemember();
            UpdateSelectedInfo(new Data());
            SaveData();
        }
    }
}
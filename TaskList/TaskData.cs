using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasks
{
    // Класс для хранения данных о задаче
    public class Data
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.Today;

        public Data(string title, string description, DateTime? date)
        {
            Title = title;
            Description = description;
            Date = date ?? DateTime.Today;
        }

        public Data() { }

        // Переопределение ТуСтринг для вывода в ЛистБокс
        public override string ToString()
        {
            int days = (Date - DateTime.Today).Days;
            return Title + $" (Срок {days} {GetCurrenKindDay(days)})";
        }

        // Сортировка по Дате через Linq
        public static List<Data> SortByDate(List<Data> data)
        {
            return data.OrderBy(x => x.Date).ToList();
        }

        // Сортировка по названию через Linq
        public static List<Data> SortByTitle(List<Data> data)
        {
            return data.OrderBy(x => x.Title).ToList();
        }

        public static string GetCurrenKindDay(int day)
        {
            if (day % 10 == 1)
            {
                return "день";
            }

            if (day % 10 == 2 || day % 10 == 3 || day % 10 == 4)
            {
                return "дня";
            }

            return "дней";
        }

    }
}
//вы лучшая:)

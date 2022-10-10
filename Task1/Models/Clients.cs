using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows;

namespace Task1
{
    public class Clients : ObservableCollection<Client>, INotifyCollectionChanged
    {
        public Clients() {  }

        public Clients(string path = "data.csv")  {  LoadData(path);  }

        /// <summary>
        /// Возвращает копию коллекции
        /// </summary>
        /// <returns>Копия Clients</returns>
        public Clients Clone()
        {
            var rep =  new Clients();

            foreach (var item in this) 

                  {  rep.Add(item); }

            return rep; 
        }

        /// <summary>
        /// Заменяет клиента по указанному индексу
        /// </summary>
        /// <param name="index">Индекс (с нуля) элемента, который требуется заменить</param>
        /// <param name="editClient">Отредактируемый клиент по указанному индексу</param>
        public void EditClient(int index, Client editClient)
        {   
            SetItem(index, editClient);
        }

        /// <summary>
        /// Загружает данные о клиентах из файла data.csv
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        private void LoadData(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        while (!reader.EndOfStream)
                        {
                            string[] line = reader.ReadLine().Split('\t');

                            this.Add(new Client(firstName: line[1],
                                               middleName: line[2],
                                               secondName: line[3],
                                                  telefon: line[4],
                                  seriesAndPassportNumber: line[5]));
                        }
                    }
                   
                }
                else
                {
                    MessageBox.Show("Не найден файл с данными",
                    caption: "Ощибка в чтении данных",
                    MessageBoxButton.OK,
                    icon: MessageBoxImage.Error);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), caption: "Не удалось получить данные");
            }

        }
    }
}

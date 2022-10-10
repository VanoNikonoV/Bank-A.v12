using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Consultant : IClientDataMonitor
    {
        public Consultant() {  }
      
        /// <summary>
        /// Метод редактирования номера телефона
        /// </summary>
        /// <param name="client">Клент чей номер необходимо отредактировать</param>
        /// <param name="newData">Новый номер</param>
        /// <returns>Клент с новым номером</returns>
        public Client EditeClient(Client client, string newData)
        {
            client.Telefon = newData;

            return client;
        }

        /// <summary>
        /// Возвращает коллекцию клиентов со скрытими данными
        /// </summary>
        /// <returns>ObservableCollection<Client></returns>
        public ObservableCollection<Client> ViewClientsData(ObservableCollection<Client> clients)
        { 
            ObservableCollection<Client> clientsForConsultant = new ObservableCollection<Client>();

            foreach (Client client in clients)
            {
                string concealment = ConcealmentOfSeriesAndPassportNumber(client.SeriesAndPassportNumber);

                Client temp = new Client(firstName: client.FirstName,
                                        middleName: client.MiddleName,
                                        secondName: client.SecondName,
                                           telefon: client.Telefon,
                           seriesAndPassportNumber: concealment,
                                         currentId: client.ID);

                clientsForConsultant.Add(temp);
            }

            return clientsForConsultant;
        }

        /// <summary>
        /// Сокрыте паспортных данных клиента
        /// </summary>
        /// <param name="number">Паспорные данные</param>
        /// <returns>Скрытые данные либо "нет данных"</returns>
        private string ConcealmentOfSeriesAndPassportNumber(string number)
        {
            if (number.Length > 0 && number != null && number != String.Empty)
            {
                string data = number;

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < number.Length; i++)
                {
                    if (data[i] != ' ')
                    {
                        sb.Append('*');
                    }
                    else sb.Append(data[i]);
                }
                return sb.ToString();
            }

            else return "нет данных";
        }
    }

    
}

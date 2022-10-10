using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;

namespace Task2
{
    public class Meneger:Consultant, IClientDataMonitor
    {
        /// <summary>
        /// Возвращает коллекцию клиентов со скрытими данными
        /// </summary>
        /// <returns>ObservableCollection<Client></returns>
        public new ObservableCollection<Client> ViewClientsData(ObservableCollection<Client> clients)
        {
            return clients;
        }

        /// <summary>
        /// Метод редактирования имени
        /// </summary>
        /// <param name="client">Клент чьё имя необходимо отредактировать</param>
        /// <param name="newName">Новое имя</param>
        /// <returns>Клиент с новым именем</returns>
        public Client EditNameClient(Client client, string newName)
        {
            return new Client(firstName: newName,
                             middleName: client.MiddleName,
                             secondName: client.SecondName,
                                telefon: client.Telefon,
                seriesAndPassportNumber: client.SeriesAndPassportNumber,
                              currentId: client.ID);
        }

        public Client EditMiddleNameClient(Client client, string newMiddleName)
        {
            return new Client(firstName: client.FirstName,
                             middleName: newMiddleName,
                             secondName: client.SecondName,
                                telefon: client.Telefon,
                seriesAndPassportNumber: client.SeriesAndPassportNumber,
                              currentId: client.ID);
        }

        public Client EditSecondNameClient(Client client, string newSecondName)
        {
            return new Client( firstName: client.FirstName,
                              middleName: client.MiddleName,
                              secondName: newSecondName,
                                 telefon: client.Telefon,
                 seriesAndPassportNumber: client.SeriesAndPassportNumber,
                               currentId: client.ID);
        }
    }
}

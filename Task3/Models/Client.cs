using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Task3

/// <summary>
/// Класс описывающий модель клиента
/// </summary>
{
    public class Client : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Статический конструктор
        private static int id;

        private static int NextID()
        {
            id++;
            return id;
        }
        static Client()
        {
            id = 0;
        }
        #endregion

        public Client() : this("Имя", "Отчество", "Фамилия", "79000000000", "66 00 000000") { --id; } //для нового клиента

        /// <summary>
        /// Вызывается при создании нового клиента
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="middleName"></param>
        /// <param name="secondName"></param>
        /// <param name="telefon"></param>
        /// <param name="seriesAndPassportNumber"></param>
        public Client(string firstName, string middleName,
                      string secondName, string telefon,
                      string seriesAndPassportNumber) =>

                    (this.FirstName, this.MiddleName,
                     this.SecondName, this.Telefon,
                     this.SeriesAndPassportNumber, this.DateOfEntry, 
                     this.ID, this.IsChanged) =

                    (firstName, middleName,
                     secondName, telefon,
                     seriesAndPassportNumber, DateTime.Now, 
                     Client.NextID(), false);

        /// <summary>
        /// Вызывается при редактировании, перезаписывании клиента
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="middleName"></param>
        /// <param name="secondName"></param>
        /// <param name="telefon"></param>
        /// <param name="seriesAndPassportNumber"></param>
        /// <param name="currentId"></param>
        public Client(string firstName, string middleName,
                          string secondName, string telefon,
                          string seriesAndPassportNumber, 
                          int currentId, DateTime dateTime, 
                          bool isChanged)

                          : this(firstName, middleName, secondName, 
                                 telefon, seriesAndPassportNumber) 
            {
                this.ID = currentId; 
                --id; 
                this.DateOfEntry = dateTime;
                this.IsChanged = isChanged;
            }

        // для загрузки данных
        public Client(string firstName, string middleName,
                          string secondName, string telefon,
                          string seriesAndPassportNumber,
                          DateTime dateTime,
                          bool isChanged)

                          : this(firstName, middleName, secondName,
                                 telefon, seriesAndPassportNumber)
        {

            this.DateOfEntry = dateTime;
            this.IsChanged = isChanged;
        }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string FirstName
        {
            get { return this.firstName; }
            private set
            {
                    this.firstName = value;
                    OnPropertyChanged(nameof(FirstName));
            }
        }
        /// <summary>
        /// Отчество клиента
        /// </summary>
        public string MiddleName
        {
            get { return this.middleName; }
            private set
            { this.middleName = value;
                OnPropertyChanged(nameof(MiddleName)); }
        }
        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string SecondName
        {
            get { return this.secondName; }

            private set { this.secondName = value;
                  OnPropertyChanged(nameof(SecondName));}
        }
        /// <summary>
        /// Телефон клиента
        /// </summary>
        public string Telefon
        {
            get { return this.telefon; }

            set
            {
                this.telefon = value;
                OnPropertyChanged(nameof(Telefon));
            }
        }
        public int ID { get; private set; }
        /// <summary>
        /// Серия и номер паспотра клиента
        /// </summary>
        public string SeriesAndPassportNumber
        {
            get {return this.seriesAndPassportNumber;}
            private set
            {
                this.seriesAndPassportNumber = value;
                OnPropertyChanged(nameof(SeriesAndPassportNumber));
            }
        }

        #region InfoChanges
        private ObservableCollection<InformationAboutChanges> infoChanges = new ObservableCollection<InformationAboutChanges>();

        public ObservableCollection<InformationAboutChanges> InfoChanges 
        { 
            get { return this.infoChanges; }
            set 
            { 
                this.infoChanges = value;
                OnPropertyChanged(nameof(InfoChanges));
            }
        }
        #endregion

        #region IsChanged
        private bool isChanged;
        /// <summary>
        /// Индикатор наличия измнений
        /// </summary>
        public bool IsChanged 
        { 
            get { return this.isChanged; }
            set
            {
                if (isChanged == value) return;
                {
                    this.isChanged = value;
                    OnPropertyChanged(nameof(IsChanged));
                }
               
            }
        }
        #endregion

        #region DateOfEntry
        private DateTime dateOfEntry;
        /// <summary>
        /// Дата внесения изменений
        /// </summary>
        public DateTime DateOfEntry 
        { 
            get { return this.dateOfEntry; } 
            set
            {
                this.dateOfEntry = value;
                OnPropertyChanged(nameof(DateOfEntry));
            }
        }
        #endregion

        #region Поля
        string firstName;
        string secondName;  
        string middleName;
        string telefon;
        string seriesAndPassportNumber;
        string error;

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if (propName != nameof(IsChanged))
            {
                this.IsChanged = true;

            }
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        /// <summary>
        /// Переопределяю для записи данных в файл .csv
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ID.ToString() + "\t" 
                    + FirstName + "\t" 
                    + MiddleName  + "\t"
                    + SecondName + "\t" 
                    + Telefon + "\t" 
                    + SeriesAndPassportNumber + "\t"
                    + DateOfEntry;
        }

        /// <summary>
        /// Сообщает о наличии ощибки в поле
        /// </summary>
        public string Error => error;

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;

                error = string.Empty; 

                switch (columnName)
                {
                    case nameof(Telefon):

                        if (this.Telefon.Length == 0)
                        {
                            error = "Нужно заполнить поле";
                            return result = "Нужно заполнить поле";
                        }

                        else if (!decimal.TryParse(this.Telefon, out decimal number))
                        {
                            error = "Нужны числа";
                            return result = "Нужны числа";
                        }

                        else if (this.Telefon.Length > 11 || this.Telefon.Length < 11)
                        {
                            error = "Номер должен состоять из 11 цифр";
                            return result = "Номер должен состоять из 11 цифр";
                        }
                        break;

                    case nameof(SeriesAndPassportNumber):

                        if (this.SeriesAndPassportNumber.Length == 0)
                        {
                            error = "Нужно заполнить поле";
                            return result = "Нужно заполнить поле";
                        }
                       
                        break;

                    case nameof(FirstName):

                        if (this.FirstName.Length == 0)
                        {
                            error = "Нужно заполнить поле";
                            return result = "Нужно заполнить поле";
                        }
                        break;

                    case nameof(SecondName):

                        if (this.SecondName.Length == 0)
                        {
                            error = "Нужно заполнить поле";
                            return result = "Нужно заполнить поле";
                        }
                        break;

                    case nameof(MiddleName):

                        if (this.MiddleName.Length == 0)
                        {
                            error = "Нужно заполнить поле";
                            return result = "Нужно заполнить поле";
                        }
                        break;
                }
                return result;
            }
        }
    }
}


using System;
using System.ComponentModel;

namespace Task3
{
        /// <summary>
        /// Информаци об измененияих в записи о клиенте
        /// </summary>
    public class InformationAboutChanges : INotifyPropertyChanged
{
        public InformationAboutChanges( DateTime dateTime, 
                                        string whatChanges = "", 
                                        string typeOfChanges = "", 
                                        string whoChangedIt = "") =>
            
            (this.DateChenges, 
            this.WhatChanges, this.TypeOfChanges, 
            this.WhoChangedIt) =

            (dateTime, whatChanges, 
            typeOfChanges, whoChangedIt);
            
        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime DateChenges { get; set; }


        private string whatChanges;
        /// <summary>
        /// Какое поле измненилось
        /// </summary>
        public string WhatChanges
        { 
            get { return this.whatChanges; }
            set {
                    this.whatChanges = value;
                    OnPropertyChanged(nameof(WhatChanges));
                }
        }
        /// <summary>
        /// Тип изменений
        /// </summary>
        public string TypeOfChanges { get; set; }
        /// <summary>
        /// Кто произмел изменение
        /// </summary>
        public string WhoChangedIt { get; set; }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

    }

}

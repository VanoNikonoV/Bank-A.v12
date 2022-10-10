using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Task1
{
    public partial class MainWindow : Window
    {
        public Clients ClientsBank { get; set; }

        public Consultant Consultant { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ClientsBank = new Clients("data.csv");

            Consultant = new Consultant();

            DataClients.ItemsSource = Consultant.ViewClientsData(ClientsBank.Clone());

            #region Сокрытие не функциональных кнопок

            EditName_Button.IsEnabled = false;
            EditMiddleName_Button.IsEnabled =false;
            EditSecondName_Button.IsEnabled = false;
            EditSeriesAndPassportNumber_Button.IsEnabled=false;
            NewClient_Button.IsEnabled = false;
            #endregion
        }

        /// <summary>
        /// Метод редактирования номера телефона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTelefon_Button(object sender, RoutedEventArgs e)
        {
            var client = DataClients.SelectedItem as Client;

            if (client != null)
            {
                //изменения в коллекции клиентов
                Consultant.EditeClient(client, EditTelefon_TextBox.Text);

                //изменения в коллекции банка, по ссылке менаджера
                Client editClient = ClientsBank.First(i => i.ID == client.ID);

                editClient.Telefon = EditTelefon_TextBox.Text;
            }

            else ShowStatusBarText("Выберите клиента");
        }
        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            e.CanExecute = true;
        }
        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var saveDlg = new SaveFileDialog { Filter = "Text files|*.csv" };

            if (true == saveDlg.ShowDialog())
            {
                string fileName = saveDlg.FileName;

                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Unicode))
                {
                    foreach (var emp in DataClients.ItemsSource)
                    {
                        sw.WriteLine(emp.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Метод удаляющий текст сообщения в StatusBar
        /// </summary>
        /// <param name="message">Текст информационного сообщения</param>
        private void ShowStatusBarText(string message)
        {
            StatusBarText.Text = message;

            var timer = new System.Timers.Timer();

            timer.Interval = 2000;

            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                timer.Stop();
                //удалите текст сообщения о состоянии с помощью диспетчера, поскольку таймер работает в другом потоке
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    StatusBarText.Text = "";
                }));
            };
            timer.Start();
        }
        private void CloseWindows(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
    }
}

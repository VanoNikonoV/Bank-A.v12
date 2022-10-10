using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Task1;

namespace Task2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Clients ClientsBank { get; set; }

        public Consultant Consultant { get; set; }

        public Meneger Meneger { get; set; }     

        public MainWindow()
        {

            ClientsBank = new Clients("data.csv");

            Consultant = new Consultant(); 
            
            Meneger = new Meneger();

            InitializeComponent();

            DataClients.ItemsSource = Consultant.ViewClientsData(ClientsBank.Clone());

            #region Сокрытие не функциональных кнопок

            EditName_Button.IsEnabled = false;
            EditMiddleName_Button.IsEnabled = false;
            EditSecondName_Button.IsEnabled = false;
            EditSeriesAndPassportNumber_Button.IsEnabled = false;
            NewClient_Button.IsEnabled = false;
            #endregion
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

        private void CloseWindows(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AccessLevel_ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (AccessLevel_ComboBox.SelectedIndex)
            {
                case 0: //консультант

                    #region Сокрытие не функциональных кнопок
                    EditName_Button.IsEnabled = false;
                    EditMiddleName_Button.IsEnabled = false;
                    EditSecondName_Button.IsEnabled = false;
                    EditSeriesAndPassportNumber_Button.IsEnabled = false;
                    NewClient_Button.IsEnabled = false;
                    #endregion
                    
                    DataClients.ItemsSource = Consultant.ViewClientsData(ClientsBank.Clone()); 

                    break;

                case 1: //менждер

                    #region Активация функциональных кнопок   
                    EditName_Button.IsEnabled = true;
                    EditMiddleName_Button.IsEnabled = true;
                    EditSecondName_Button.IsEnabled = true;
                    EditSeriesAndPassportNumber_Button.IsEnabled = true;
                    NewClient_Button.IsEnabled = true;
                    #endregion

                    DataClients.ItemsSource = Meneger.ViewClientsData(ClientsBank);

                    break;

                default:
                    break;

            }
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
                Consultant.EditeClient(client, EditTelefon_TextBox.Text.Trim());

                // если присутствуют ощибки изменения на примутся
                if (client.Error == String.Empty)
                {
                    //изменения в коллекции банка, по ID клиента
                    Client editClient = ClientsBank.First(i => i.ID == client.ID);

                    editClient.Telefon = EditTelefon_TextBox.Text;
                }
                else ShowStatusBarText("Исправте не корректные данные");
            }

            else ShowStatusBarText("Выберите клиента");

        }

        /// <summary>
        /// Метод редактирования имени клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditName_Button_Clik(object sender, RoutedEventArgs e)
        {
            var client = DataClients.SelectedItem as Client;

            if (client != null)
            {
                Client changedClient = Meneger.EditNameClient(client, EditName_TextBox.Text);

                ClientsBank.EditClient(ClientsBank.IndexOf(client), changedClient);
            }

            else ShowStatusBarText("Выберите клиента");
        }

        /// <summary>
        /// Метод редактирования отчества клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMiddleName_Button_Clik(object sender, RoutedEventArgs e)
        {
            var client = DataClients.SelectedItem as Client;

            if (client != null)
            {
                Client changedClient = Meneger.EditMiddleNameClient(client, EditMiddleName_TextBox.Text);

                ClientsBank.EditClient(ClientsBank.IndexOf(client), changedClient);
            }
            else ShowStatusBarText("Выберите клиента");
        }

        /// <summary>
        /// Метод редактирования фамилии клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditSecondName_Button_Clik(object sender, RoutedEventArgs e)
        {
            var client = DataClients.SelectedItem as Client;

            if (client != null)
            {
                Client changedClient = Meneger.EditSecondNameClient(client, EditSecondName_TextBox.Text);

                ClientsBank.EditClient(ClientsBank.IndexOf(client), changedClient);
            }
            else ShowStatusBarText("Выберите клиента");
        }

        /// <summary>
        /// Метод заполняющий панель данными выбранного клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientViewSelection(object sender, SelectionChangedEventArgs e)
        {
            PanelInfo.DataContext = DataClients.SelectedItem as Client;
        }

        /// <summary>
        /// Метод добавления нового клиенита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewClient_Button_Click(object sender, RoutedEventArgs e)
        {
            NewClientWindow _windowNewClient = new NewClientWindow();

            _windowNewClient.Owner = this;

            _windowNewClient.ShowDialog();

            if (_windowNewClient.DialogResult == true)
            {
                ClientsBank.Add(_windowNewClient.NewClient);
            }
        }

    }
}

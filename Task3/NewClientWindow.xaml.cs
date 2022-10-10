using System.Windows;
using System.Windows.Controls;
using Task1;

namespace Task3
{
    /// <summary>
    /// Логика взаимодействия для NewClientWindow.xaml
    /// </summary>
    public partial class NewClientWindow : Window
    {
        public Client NewClient { get; private set; }

        private Client temp;

        public NewClientWindow()
        {
            InitializeComponent();

            temp = new Client();

            NewClientPanel.DataContext = temp;
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void AddClient(object sender, RoutedEventArgs e)
        {
            if (temp.Error == string.Empty)
            {
                NewClient = new Client( FirstNameTextBox.Text.Trim(),
                                        MidlleNameTextBox.Text.Trim(),
                                        SecondNameTextBox.Text.Trim(),
                                        TelefonTextBox.Text.Trim(),
                                        SeriesAndPassportNumberTextBox.Text.Trim());
                DialogResult = true;
            }

           else MessageBox.Show(messageBoxText: temp.Error, 
                            caption: "Ощибка в данных", 
                            MessageBoxButton.OK, 
                            icon: MessageBoxImage.Error);
            
        }

        private bool isFocused = false;
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            isFocused = true;
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (isFocused)
            {
                isFocused = false;
                (sender as TextBox).SelectAll();
            }
        }
    }
}

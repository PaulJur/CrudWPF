using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using CRUDFunctionsWPF;

namespace CrudWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _connectionString = "Server=localhost;Database=CRUDEntityFramework;Trusted_Connection=True;TrustServerCertificate=True;";

        private readonly CRUDFunctions _crudFunctions;
        public MainWindow()
        {
            InitializeComponent();
            _crudFunctions = new CRUDFunctions();
            _crudFunctions.SetConnectionString(_connectionString);

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            //Textbox text equals to person variables, regex already set so no need for filtering.
            string name = name_txt.Text;
            int age = int.Parse(age_txt.Text);
            string gender = gender_txt.Text;


            //Insert Person data into database based on provided input.
            _crudFunctions.Insert(name, age, gender);

        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InputCheck_RegexLetters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");

            //Check if the entered text matches the pattern.
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void InputCheck_RegexNumbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");

            //Check if the entered text matches the pattern.
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
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

        private readonly CRUDContext _context;

        public static DataGrid _datagrid;
        public MainWindow()
        {
            InitializeComponent();
            _crudFunctions = new CRUDFunctions();
            _crudFunctions.SetConnectionString(_connectionString);

            _context = new CRUDContext(_connectionString);

        }

        private void LoadDatagrid()//Loads Person data into the grid
        {
            //Sets the Source for datagrid.
            datagrid.ItemsSource = _context.Person.ToList();
            _datagrid = datagrid;
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(id_txt.Text, out int id))
            {
                if (_crudFunctions.Delete(id))
                {
                    MessageBox.Show("Record deleted successfully.", "Successful", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("No record found with the provided ID.", "Record not found", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            //Textbox text equals to person variables, regex already set so no need for filtering.
            string name = name_txt.Text;
            int age;
            string gender = gender_txt.Text;

            // Check if age text is not empty and can be parsed to integer
            bool isAgeValid = int.TryParse(age_txt.Text, out age);

            // Check if any of the input fields are empty or null
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(gender) || !isAgeValid)
            {
                MessageBox.Show("Please fill all the fields correctly.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Stop further execution
            }

            // Insert Person data into the database based on provided input.
            _crudFunctions.Insert(name, age, gender);
            MessageBox.Show("Record inserted successfully!", "Inserted", MessageBoxButton.OK);

        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDatagrid();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InputCheck_RegexLetters(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, 0) && !char.IsControl(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Only letters are allowed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void InputCheck_RegexNumbers(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) && !char.IsControl(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are allowed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
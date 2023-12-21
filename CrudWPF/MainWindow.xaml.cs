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
using System.Collections.ObjectModel;
using System.Data;

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

            LoadDatagrid();

            search_txt.TextChanged += search_txt_TextChanged;

        }

        private void LoadDatagrid()//Loads Person data into the grid
        {
            //Sets the Source for datagrid.
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = _context.Person.ToList();
            _datagrid = datagrid;

            

        }

        //Button reponsible for deleting record based on ID.
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //Checks if the provided ID can be parsed into an int.
            if (int.TryParse(id_txt.Text, out int id))
            {
                //If parsed, delete the current record based on id.
                if (_crudFunctions.Delete(id))
                {
                    MessageBox.Show("Record deleted successfully.", "Successful", MessageBoxButton.OK);
                    ClearTextBox();
                }
                //Otherwise throw message box error if record is not found.
                else
                {
                    MessageBox.Show("No record found with the provided ID.", "Record not found", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearTextBox();
                }
            }
            //If an invalid ID(not an int) is provided, throw an error.
            else
            {
                MessageBox.Show("Please enter a valid ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ClearTextBox();
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
            ClearTextBox();
            

        }

        //Loads person data into the datagrid.
        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDatagrid();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //Textbox text equals to person variables, regex already set so no need for filtering.
            string name = name_txt.Text;
            string gender = gender_txt.Text;

            int age;
            int id;
            


            // Check if age text is not empty and can be parsed to integer
            bool isAgeValid = int.TryParse(age_txt.Text, out age);
            bool isIdValid = int.TryParse(id_txt.Text, out id);

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(gender) || !isAgeValid || !isIdValid)
            {
                MessageBox.Show("Please fill all the fields correctly.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Stop further execution
            }

            _crudFunctions.Update(id, name, age, gender);
            MessageBox.Show("Record updated successfully!", "Updated", MessageBoxButton.OK);
            LoadDatagrid();
            ClearTextBox();

        }


        //Regex for making sure the input field is only letters.
        private void InputCheck_RegexLetters(object sender, TextCompositionEventArgs e)
        {
            //Checks every letter that is written at the index(0) if it's not a number or a control character
            //Otherwise throw messagebox error.
            if (!char.IsLetter(e.Text, 0) && !char.IsControl(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Only letters are allowed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Regex for making sure the input field is only numbers.
        private void InputCheck_RegexNumbers(object sender, TextCompositionEventArgs e)
        {
            //Checks every letter that is written at the index(0) if it's not a letter or a control character
            //Otherwise throw messagebox error.
            if (!char.IsDigit(e.Text, 0) && !char.IsControl(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are allowed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Clear the textbox texts
        private void ClearTextBox()
        {
            name_txt.Text = null;
            age_txt.Text = null;
            gender_txt.Text = null;
            id_txt.Text = null;
        }

        private void search_txt_TextChanged(object sender, TextChangedEventArgs e)
        {

            //Gets the text from inputted text in the textbox.
            string searchText = search_txt.Text.Trim();

            //Filter data based on the search text.
            if (!string.IsNullOrEmpty(searchText))
            {
                //LAMBDA experssion to filter out based on name and puts it into a list which then updates the datagrid with the list.
                var filteredData = _context.Person.Where(p => p.Name.Contains(searchText)).ToList();
                _datagrid.ItemsSource = filteredData;
            }
            else
            {
                // If the search text is empty, reload the original data
                LoadDatagrid();
            }
        }
     
    }
}
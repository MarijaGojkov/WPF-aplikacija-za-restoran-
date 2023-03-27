using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.Serialization;

namespace Restoran
{
    /// <summary>
    /// Interaction logic for DostavljacPage.xaml
    /// </summary>
    public partial class DostavljacPage : Window
    {
        public DostavljacPage()
        {
            InitializeComponent();
            prikaziDostavljaca();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            AdministratorPage ObjLogin = new AdministratorPage();
            ObjLogin.Show();
            this.Close();
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE [Dostavljac] SET nazivDostavljaca = @nazivDostavljaca, brojTelefona = @brojTelefona WHERE idDostavljaca = @idDostavljaca";
            command.Parameters.AddWithValue("@idDostavljaca", txtIdDostavljaca.Text);
            command.Parameters.AddWithValue("@nazivDostavljaca", txtDostavljac.Text);
            command.Parameters.AddWithValue("@brojTelefona", txtBrojTelefona.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno izmenjeni.");
                prikaziDostavljaca();
            }
            ponistiUnosTxt();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM [Dostavljac] WHERE idDostavljaca = @idDostavljaca";
            command.Parameters.AddWithValue("@idDostavljaca", txtIdDostavljaca.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno obrisani.");
                prikaziDostavljaca();
            }
            ponistiUnosTxt();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Dostavljac] (nazivDostavljaca, brojTelefona) VALUES (@nazivDostavljaca, @brojTelefona) ";
            command.Parameters.AddWithValue("@nazivDostavljaca", txtDostavljac.Text);
            command.Parameters.AddWithValue("@brojTelefona", txtBrojTelefona.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Dostavljač je uspešno dodat.");
                prikaziDostavljaca();
            }
            ponistiUnosTxt();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void prikaziDostavljaca()
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM [Dostavljac]";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Dostavljac");
            dataAdapter.Fill(dataTable);

            DataGridDostavljaci.ItemsSource = dataTable.DefaultView;
        }

        private void DataGridDostavljaci_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtDostavljac.Text = dr["nazivDostavljaca"].ToString();
                txtIdDostavljaca.Text = dr["idDostavljaca"].ToString();
                txtBrojTelefona.Text = dr["brojTelefona"].ToString();
            }
        }

        private void ponistiUnosTxt()
        {

            txtDostavljac.Text = "";
            txtIdDostavljaca.Text = "";
            txtBrojTelefona.Text = "";


        }

        private void txtDostavljaci_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBrojTelefona_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

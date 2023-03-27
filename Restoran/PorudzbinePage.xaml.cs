using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace Restoran
{
    /// <summary>
    /// Interaction logic for PorudzbinePage.xaml
    /// </summary>
    public partial class PorudzbinePage : Window
    {
        public PorudzbinePage()
        {
            InitializeComponent();
            prikaziPorudzbine();
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
            LoginOsoblje ObjLogin = new LoginOsoblje();
            ObjLogin.Show();
            this.Close();
        }

        private void prikaziPorudzbine()
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT Porudzbina.idPorudzbine, Proizvod.naziv, Porudzbina.adresa, Proizvod.cena, Dostavljac.nazivDostavljaca" +
                " FROM [Porudzbina], [Proizvod], [Dostavljac] WHERE Porudzbina.idProizvoda = Proizvod.idProizvoda and Porudzbina.idDostavljaca = Dostavljac.idDostavljaca";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Porudzbina");
            dataAdapter.Fill(dataTable);
            DataGridPorudzbine.ItemsSource = dataTable.DefaultView;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Porudzbina] (adresa, idProizvoda, idDostavljaca)" +
                " VALUES (@adresa, (SELECT idProizvoda FROM Proizvod WHERE naziv=@naziv)," +
                "(SELECT idDostavljaca FROM Dostavljac WHERE nazivDostavljaca = @nazivDostavljaca)) ";
            command.Parameters.AddWithValue("@adresa", txtAdresa.Text);
            command.Parameters.AddWithValue("@naziv", cbxProizvod.Text);
            command.Parameters.AddWithValue("@nazivDostavljaca", cbxDostavljac.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Porudzbina je uspešno dodata");
                prikaziPorudzbine();
            }
            ponistiUnosTxt();
        }
        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE [Porudzbina] SET adresa = @adresa, " +
                "idProizvoda = (SELECT idProizvoda FROM Proizvod WHERE naziv = @naziv), " +
                "idDostavljaca = (SELECT idDostavljaca FROM Dostavljac WHERE nazivDostavljaca = @nazivDostavljaca)" +
                "WHERE idPorudzbine = @idPorudzbine";
            command.Parameters.AddWithValue("@idPorudzbine", txtIdPorudzbine.Text);
            command.Parameters.AddWithValue("@adresa", txtAdresa.Text);
            command.Parameters.AddWithValue("@naziv", cbxProizvod.Text);
            command.Parameters.AddWithValue("@nazivDostavljaca", cbxDostavljac.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno izmenjeni");
                prikaziPorudzbine();
            }
            ponistiUnosTxt();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM [Porudzbina] WHERE idPorudzbine = @idPorudzbine";
            command.Parameters.AddWithValue("@idPorudzbine", txtIdPorudzbine.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Porudzbina je uspešno obrisana");
                prikaziPorudzbine();
            }
            ponistiUnosTxt();
        }

       

        private void txtCena_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtAdresa_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGridPorudzbine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtIdPorudzbine.Text = dr["idPorudzbine"].ToString();
                cbxDostavljac.Text = dr["nazivDostavljaca"].ToString();
                cbxProizvod.Text = dr["naziv"].ToString();
                txtAdresa.Text = dr["adresa"].ToString();
            }
        }

        private void CbxProizvod_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand commandCbx = new SqlCommand();
            commandCbx.CommandText = "SELECT * FROM [Proizvod] ORDER BY idProizvoda";
            commandCbx.Connection = connection;
            SqlDataAdapter dataAdapterCbx = new SqlDataAdapter(commandCbx);
            DataTable dataTableCbx = new DataTable("Proizvod");
            dataAdapterCbx.Fill(dataTableCbx);

            for (int i = 0; i < dataTableCbx.Rows.Count; i++)
            {
                cbxProizvod.Items.Add(dataTableCbx.Rows[i]["naziv"]);
            }
        }

        private void CbxDostavljac_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand commandCbx = new SqlCommand();
            commandCbx.CommandText = "SELECT * FROM [Dostavljac] ORDER BY idDostavljaca";
            commandCbx.Connection = connection;
            SqlDataAdapter dataAdapterCbx = new SqlDataAdapter(commandCbx);
            DataTable dataTableCbx = new DataTable("Dostavljac");
            dataAdapterCbx.Fill(dataTableCbx);

            for (int i = 0; i < dataTableCbx.Rows.Count; i++)
            {
                cbxDostavljac.Items.Add(dataTableCbx.Rows[i]["nazivDostavljaca"]);
            }
        }
        private void ponistiUnosTxt()
        {
            cbxDostavljac.Text = "";
            cbxProizvod.Text = "";
            txtAdresa.Text = "";
            txtCena.Text = "";
            txtIdDostavljaca.Text = "";
            txtIdPorudzbine.Text = "";
        }
    }
}

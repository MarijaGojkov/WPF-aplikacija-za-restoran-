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

namespace Restoran
{
    /// <summary>
    /// Interaction logic for KorisniciPage.xaml
    /// </summary>
    public partial class KorisniciPage : Window
    {
        public KorisniciPage()
        {
            InitializeComponent();
            prikaziKorisnike();
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

        private void DataGridKorisnici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtIdKorisnika.Text = dr["idKorisnika"].ToString();
                txtIme.Text = dr["ime"].ToString();
                txtPrezime.Text = dr["prezime"].ToString();
                txtKorisnickoIme.Text = dr["korisnickoIme"].ToString();
                txtLozinka.Text = dr["lozinka"].ToString();
                cbxTipKorisnika.Text = dr["idTipaKorisnika"].ToString();
                dtDatum.Text = dr["datumZaposlenja"].ToString();
            }
        }

        private void prikaziKorisnike()
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT Korisnik.idKorisnika, Korisnik.ime, Korisnik.prezime, Korisnik.korisnickoIme, korisnik.lozinka," +
                "Korisnik.datumZaposlenja, korisnik.idTipaKorisnika, TipKorisnika.naziv FROM [Korisnik], [TipKorisnika] WHERE korisnik.idTipaKorisnika = TipKorisnika.idTipaKorisnika";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Korisnik");
            dataAdapter.Fill(dataTable);
            DataGridKorisnici.ItemsSource = dataTable.DefaultView;
        }

        private void CbxTipKorisnika_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand commandCbx = new SqlCommand();
            commandCbx.CommandText = "SELECT * FROM [TipKorisnika] ORDER BY idTipaKorisnika";
            commandCbx.Connection = connection;
            SqlDataAdapter dataAdapterCbx = new SqlDataAdapter(commandCbx);
            DataTable dataTableCbx = new DataTable("TipKorisnika");
            dataAdapterCbx.Fill(dataTableCbx);

            for (int i = 0; i < dataTableCbx.Rows.Count; i++)
            {
                cbxTipKorisnika.Items.Add(dataTableCbx.Rows[i]["naziv"]);
            }
        }

      
        private void txtIme_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtKorisnickoIme_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtLozinka_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            DateTime datum = Convert.ToDateTime(dtDatum.Text);
            SqlCommand command = new SqlCommand();
            command.CommandText = "UPDATE [Korisnik] SET ime = @ime, prezime = @prezime, korisnickoIme = @korisnickoIme," +
                " lozinka = @lozinka, datumZaposlenja = @datumZaposlenja, " +
                " idTipaKorisnika = (SELECT idTipaKorisnika FROM TipKorisnika WHERE naziv = @naziv) WHERE idKorisnika = @idKorisnika";
            command.Parameters.AddWithValue("@idKorisnika", txtIdKorisnika.Text);
            command.Parameters.AddWithValue("@ime", txtIme.Text);
            command.Parameters.AddWithValue("@prezime", txtPrezime.Text);
            command.Parameters.AddWithValue("@korisnickoIme", txtKorisnickoIme.Text);
            command.Parameters.AddWithValue("@lozinka", txtLozinka.Text);
            command.Parameters.AddWithValue("@datumZaposlenja", datum);
            command.Parameters.AddWithValue("@naziv", cbxTipKorisnika.Text);

            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno izmenjeni");
                prikaziKorisnike();
            }
            ponistiUnosTxt();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM [Korisnik] WHERE idKorisnika = @idKorisinka";
            command.Parameters.AddWithValue("@idKorisinka", txtIdKorisnika.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Korisnik je uspešno obrisan");
                prikaziKorisnike();
            }
            ponistiUnosTxt();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            DateTime datum = Convert.ToDateTime(dtDatum.Text);
            connection.Open();           
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Korisnik] (ime, prezime, korisnickoIme, lozinka, datumZaposlenja, idTipaKorisnika) VALUES (@ime, @prezime, @korisnickoIme, @lozinka, @datumZaposlenja, (SELECT idTipaKorisnika FROM TipKorisnika WHERE naziv=@naziv)) ";
            command.Parameters.AddWithValue("@ime", txtIme.Text);
            command.Parameters.AddWithValue("@prezime", txtPrezime.Text);
            command.Parameters.AddWithValue("@korisnickoIme", txtKorisnickoIme.Text);
            command.Parameters.AddWithValue("@lozinka", txtLozinka.Text);
            command.Parameters.AddWithValue("@datumZaposlenja", datum);
            command.Parameters.AddWithValue("@naziv", cbxTipKorisnika.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Korisnik je uspešno dodat");
                prikaziKorisnike();
            }
            ponistiUnosTxt();
        }

        private void ponistiUnosTxt()
        {
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtKorisnickoIme.Text = "";
            txtLozinka.Text = "";
            cbxTipKorisnika.Text = "";
            dtDatum.Text = "";
        }




    }
}

﻿using System;
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
    /// Interaction logic for HranaPage.xaml
    /// </summary>
    public partial class HranaPage : Window
    {
        public HranaPage()
        {
            InitializeComponent();
            prikaziHranu();
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
            command.CommandText = "UPDATE [Proizvod] SET naziv = @naziv, cena = @cena, idTipaProizvoda = @idTipaProizvoda WHERE idProizvoda = @idProizvoda";
            command.Parameters.AddWithValue("@idProizvoda", txtIdProizvoda.Text);
            command.Parameters.AddWithValue("@naziv", txtNazivHrane.Text);
            command.Parameters.AddWithValue("@cena", txtCena.Text);
            command.Parameters.AddWithValue("@idTipaProizvoda", 1);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno izmenjeni");
                prikaziHranu();
            }
            ponistiUnosTxt();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE FROM [Proizvod] WHERE idProizvoda = @idProizvoda";
            command.Parameters.AddWithValue("@idProizvoda", txtIdProizvoda.Text);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci su uspešno obrisani");
                prikaziHranu();
            }
            ponistiUnosTxt();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "INSERT INTO [Proizvod] (naziv, cena, idTipaProizvoda) VALUES (@naziv, @cena, @idTipaProizvoda) ";


            command.Parameters.AddWithValue("@naziv", txtNazivHrane.Text);
            command.Parameters.AddWithValue("@cena", txtCena.Text);
            command.Parameters.AddWithValue("@idTipaProizvoda", 1);
            command.Connection = connection;
            int provera = command.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Hrana je uspešno dodata");
                prikaziHranu();
            }
            ponistiUnosTxt();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void prikaziHranu()
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["connRestoranApp"].ConnectionString;
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM [Proizvod] WHERE idTipaProizvoda =  1";
            command.Connection = connection;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable("Proizvod");
            dataAdapter.Fill(dataTable);

            DataGridHrana.ItemsSource = dataTable.DefaultView;
        }

        private void DataGridHrana_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtIdProizvoda.Text = dr["idProizvoda"].ToString();
                txtNazivHrane.Text = dr["naziv"].ToString();
                txtCena.Text = dr["cena"].ToString();
                txtTipaProizvoda.Text = dr["idTipaProizvoda"].ToString();
            }
        }

        private void ponistiUnosTxt()
        {
           
            txtNazivHrane.Text = "";
            txtTipaProizvoda.Text = "";


        }

        private void txtNazivHrane_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


       
    }


}

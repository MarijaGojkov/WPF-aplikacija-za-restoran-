using System;
using System.Collections.Generic;
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
    /// Interaction logic for LoginAdminPage.xaml
    /// </summary>
    public partial class LoginAdminPage : Window
    {
        public LoginAdminPage()
        {
            InitializeComponent();
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

        private void btnPrijava_Click(object sender, RoutedEventArgs e)
        {
            
            
                SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-C1R5IH8\SQLEXPRESS;Initial Catalog=RestoranApp;Integrated Security=True");
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    String query = "SELECT COUNT (1) FROM Korisnik WHERE korisnickoIme=@korisnickoIme and lozinka=@lozinka";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@korisnickoIme", txtUser.Text);
                    sqlCmd.Parameters.AddWithValue("@lozinka", txtPass.Password);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                        AdministratorPage ObjLogin = new AdministratorPage();
                        ObjLogin.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Something is wrong");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }
            
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ObjLogin = new MainWindow();
            ObjLogin.Show();
            this.Close();
        }

        private void txtUser_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }
}

using System;
using System.Collections.Generic;
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
    /// Interaction logic for AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Window
    {
        public AdministratorPage()
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

        private void btnHrana_Click(object sender, RoutedEventArgs e)
        {
            HranaPage ObjLogin = new HranaPage();
            ObjLogin.Show();
            this.Close();
        }

        private void btnPice_Click(object sender, RoutedEventArgs e)
        {
            PicePage ObjLogin = new PicePage();
            ObjLogin.Show();
            this.Close();
        }

        private void btnOsoblje_Click(object sender, RoutedEventArgs e)
        {
            KorisniciPage ObjLogin = new KorisniciPage();
            ObjLogin.Show();
            this.Close();
        }

        private void btnDostavljaci_Click(object sender, RoutedEventArgs e)
        {
            DostavljacPage ObjLogin = new DostavljacPage();
            ObjLogin.Show();
            this.Close();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            LoginAdminPage ObjLogin = new LoginAdminPage();
            ObjLogin.Show();
            this.Close();
        }
    }
}

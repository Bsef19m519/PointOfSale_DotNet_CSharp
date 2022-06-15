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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginClick(object sender, RoutedEventArgs e)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\Bs software engineering\6th semester\EAD\project\PointOfSale\Database1.mdf';Integrated Security=True";
            if(id.Text!="" && password.Password!="")    //if email and password are not empty
            {
                SqlConnection con = new SqlConnection(cs);
                try
                {
                    con.Open();
                    string query = "Select * from Account where Email='" + id.Text + "' and password='" + password.Password+"'";
                    SqlCommand command = new SqlCommand(query, con);
                    SqlDataReader dataset = command.ExecuteReader();
                    if (dataset.Read())
                    {
                        if ((bool)dataset[6] == true) //admin side
                        {
                            AdminHome win = new AdminHome();
                            win.Show();
                            this.Close();
                        }
                        else                    //user side
                        {
                        }
                    }
                    else
                    {
                        loginStatus.Visibility = Visibility.Visible;
                        con.Close();
                        return;
                    }
                }
                catch(Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }
            }
            else loginStatus.Visibility = Visibility.Visible;
        }

        private void FocusFunction(object sender, RoutedEventArgs e)
        {
            loginStatus.Visibility = Visibility.Hidden;
        }

        private void IdWatermarkFocusFunction(object sender, RoutedEventArgs e)
        {
            id_watermark.IsEnabled = false;
            id_watermark.Visibility = Visibility.Hidden;
            id.IsEnabled = true;
            id.Visibility = Visibility.Visible;
            id.Focus();
        }

        private void PasswordWatermarkFocusFunction(object sender, RoutedEventArgs e)
        {
            password_watermark.IsEnabled = false;
            password_watermark.Visibility = Visibility.Hidden;
            password.IsEnabled = true;
            password.Visibility = Visibility.Visible;
            password.Focus();
        }

        private void IdFocusFunction(object sender, RoutedEventArgs e)
        {
            if(id.Text=="") {
                id_watermark.IsEnabled = true;
                id_watermark.Visibility = Visibility.Visible;
                id.IsEnabled = false;
                id.Visibility = Visibility.Hidden;
            }
        }

        private void PasswordFocusFunction(object sender, RoutedEventArgs e)
        {
            if (password.Password == "") {
                password_watermark.IsEnabled = true;
                password_watermark.Visibility = Visibility.Visible;
                password.IsEnabled = false;
                password.Visibility = Visibility.Hidden;
            }
        }
    }
}

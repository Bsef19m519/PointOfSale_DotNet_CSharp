using System;
using System.Collections.Generic;
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

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
        }
        private void signupClick(object sender, RoutedEventArgs e)
        {
            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\Bs software engineering\6th semester\EAD\project\PointOfSale\Database1.mdf';Integrated Security=True";
            if (name.Text == "" || password.Password == "" || password_retype.Password == "" || email.Text == "") {
                ErrorLabel.Content = "Important Input Fields are empty!";
            }
            else if(password.Password!=password_retype.Password) {
                ErrorLabel.Content = "Password and confirm password doesn't match!";
            }
            else if (password.Password.Length < 8) {
                ErrorLabel.Content = "Password must contain atleast 8 characters!";
                return;
            }
            //more cases for data cleanliness
            else {
                SqlConnection con = new SqlConnection(cs);
                try {
                    con.Open();

                    string q = "Select * from Account where Email='" + email.Text+"'";
                    SqlCommand comm = new SqlCommand(q, con);
                    SqlDataReader dataset = comm.ExecuteReader();
                    if (dataset.Read())     //if email id already exists
                    {
                        ErrorLabel.Content = "Email Id already exists";
                        return;
                    }
                    dataset.Close();

                    byte adm;
                    if ((bool)admin.IsChecked) adm = 1;
                    else adm = 0;
                    string query = "Insert into Account(Name,Email,Password,Phone,Address,Admin) Values('"+name.Text+ "','"+email.Text+"','"+ password.Password + "','" + phone.Text + "','" + address.Text + "'," + adm + ")";
                    SqlCommand command = new SqlCommand(query, con);
                    int res = command.ExecuteNonQuery();
                    if (res>0) {
                        this.Close();
                        MessageBox.Show("Signup successfull");
                    }
                    else {
                        MessageBox.Show("Signup Failed!");
                    }
                }
                catch (Exception exc) {
                    MessageBox.Show(exc.ToString());
                }
                finally {
                    con.Close();
                }
            }              
       
        }


        //On getting focus of a field
        private void FocusFunction(object sender, RoutedEventArgs e)
        {
            ErrorLabel.Content = "";
        }

        //On getting focus of a watermark
        private void NameWatermarkFocusFunction(object sender, RoutedEventArgs e)
        {
            name_watermark.IsEnabled = false;
            name_watermark.Visibility = Visibility.Hidden;
            name.IsEnabled = true;
            name.Visibility = Visibility.Visible;
            name.Focus();
        }

        private void EmailWatermarkFocusFunction(object sender, RoutedEventArgs e)
        {
            email_watermark.IsEnabled = false;
            email_watermark.Visibility = Visibility.Hidden;
            email.IsEnabled = true;
            email.Visibility = Visibility.Visible;
            email.Focus();
        }

        private void PasswordWatermarkFocusFunction(object sender, RoutedEventArgs e)
        {
            password_watermark.IsEnabled = false;
            password_watermark.Visibility = Visibility.Hidden;
            password.IsEnabled = true;
            password.Visibility = Visibility.Visible;
            password.Focus();
        }
        private void ConfirmPasswordWatermarkFocusFunction(object sender, RoutedEventArgs e)
        {
            password_retype_watermark.IsEnabled = false;
            password_retype_watermark.Visibility = Visibility.Hidden;
            password_retype.IsEnabled = true;
            password_retype.Visibility = Visibility.Visible;
            password_retype.Focus();
        }
        private void PhoneWatermarkFocusFunction(object sender, RoutedEventArgs e)
        {
            phone_watermark.IsEnabled = false;
            phone_watermark.Visibility = Visibility.Hidden;
            phone.IsEnabled = true;
            phone.Visibility = Visibility.Visible;
            phone.Focus();
        }
        private void AddressWatermarkFocusFunction(object sender, RoutedEventArgs e)
        {
            address_watermark.IsEnabled = false;
            address_watermark.Visibility = Visibility.Hidden;
            address.IsEnabled = true;
            address.Visibility = Visibility.Visible;
            address.Focus();
        }


        //On lossing focus functions
        private void NameFocusFunction(object sender, RoutedEventArgs e)
        {
            if (name.Text == "") {
                name_watermark.IsEnabled = true;
                name_watermark.Visibility = Visibility.Visible;
                name.IsEnabled = false;
                name.Visibility = Visibility.Hidden;
            }
        }
        private void EmailFocusFunction(object sender, RoutedEventArgs e)
        {
            if (email.Text == "") {
                email_watermark.IsEnabled = true;
                email_watermark.Visibility = Visibility.Visible;
                email.IsEnabled = false;
                email.Visibility = Visibility.Hidden;
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
        private void ConfirmPasswordFocusFunction(object sender, RoutedEventArgs e)
        {
            if (password_retype.Password == "") {
                password_retype_watermark.IsEnabled = true;
                password_retype_watermark.Visibility = Visibility.Visible;
                password_retype.IsEnabled = false;
                password_retype.Visibility = Visibility.Hidden;
            }
        }
        private void PhoneFocusFunction(object sender, RoutedEventArgs e)
        {
            if (phone.Text == "") {
                phone_watermark.IsEnabled = true;
                phone_watermark.Visibility = Visibility.Visible;
                phone.IsEnabled = false;
                phone.Visibility = Visibility.Hidden;
            }
        }
        private void AddressFocusFunction(object sender, RoutedEventArgs e)
        {
            if (address.Text == "") {
                address_watermark.IsEnabled = true;
                address_watermark.Visibility = Visibility.Visible;
                address.IsEnabled = false;
                address.Visibility = Visibility.Hidden;
            }
        }
    }
}


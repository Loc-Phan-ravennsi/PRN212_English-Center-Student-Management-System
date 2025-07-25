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
using Project.Models;

namespace Project
{
    /// <summary>  
    /// Interaction logic for LoginWindow.xaml  
    /// </summary>  
    public partial class LoginWindow : Window
    {
        EnglishCenterDbContext context = new();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text == "" || pbPassword.Password == "")
            {
                tbError.Text = "Please enter your email and password!";
                return;
            }
            if (context.Teachers.Any(u => u.Email == tbEmail.Text && u.Password == pbPassword.Password))
            {
                Teacher teacher = context.Teachers.FirstOrDefault(u => u.Email == tbEmail.Text && u.Password == pbPassword.Password);
                MainWindow main = new MainWindow(teacher);
                main.Show();
                this.Close();
            }
            if (context.Students.Any(u => u.Email == tbEmail.Text && u.Password == pbPassword.Password))
            {
                Student student = context.Students.FirstOrDefault(u => u.Email == tbEmail.Text && u.Password == pbPassword.Password);
                StudentWindow studentWindow = new StudentWindow(student);
                studentWindow.Show();
                this.Close();
            }
            else
            {
                tbError.Text = "Invalid email or password.";
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
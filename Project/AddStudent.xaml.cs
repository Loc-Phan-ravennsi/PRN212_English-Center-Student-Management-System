using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project.Models;

namespace Project
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        private EnglishCenterDbContext context = new();

        public AddStudent()
        {
            InitializeComponent();
            LoadClasses();
        }

        private void LoadClasses()
        {
            var classes = context.Classes.ToList();
            cbClass.ItemsSource = classes;
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim().ToLower();
            string password = pbPassword.Password.Trim();
            Class selectedClass = cbClass.SelectedItem as Class;

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email))
            {
                txtStatus.Text = "Please fill in all fields.";
                return;
            }

            if (selectedClass == null)
            {
                txtStatus.Text = "Please select a class.";
                return;
            }
            bool emailExists = context.Students.Any(s => s.Email.ToLower() == email);

            if (emailExists)
            {
                txtStatus.Text = "Email already exists.";
                return;
            }
            var student = new Student
                {
                    FullName = fullName,
                    Email = email,
                    Password = password,
                    ClassId = selectedClass.ClassId
                };
            context.Students.Add(student);
            context.SaveChanges();
            System.Windows.MessageBox.Show("Student added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}

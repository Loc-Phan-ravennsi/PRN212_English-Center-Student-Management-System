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
using Project.Models;

namespace Project
{
    /// <summary>
    /// Interaction logic for DeleteStudent.xaml
    /// </summary>
    public partial class DeleteStudent : Window
    {
        EnglishCenterDbContext context = new EnglishCenterDbContext();
        public DeleteStudent()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            cbStudents.ItemsSource = context.Students.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (cbStudents.SelectedItem is Student student)
            {
                var confirm = MessageBox.Show($"Are you sure you want to delete {student.FullName}?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirm == MessageBoxResult.Yes)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                    MessageBox.Show("Student deleted.");
                    LoadStudents();
                }
            }
            else
            {
                MessageBox.Show("Please select a student.");
            }
        }
    }
}

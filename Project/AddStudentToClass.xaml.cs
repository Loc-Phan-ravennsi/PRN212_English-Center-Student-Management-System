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
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project
{
    /// <summary>
    /// Interaction logic for AddStudentToClass.xaml
    /// </summary>
    public partial class AddStudentToClass : Window
    {
        public AddStudentToClass()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new EnglishCenterDbContext())
            {
                var students = context.Students
                                      .Where(s => s.ClassId == null)
                                      .ToList();
                cbStudents.ItemsSource = students;

                var classes = context.Classes.ToList();
                cbClasses.ItemsSource = classes;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = cbStudents.SelectedItem as Student;
            var selectedClass = cbClasses.SelectedItem as Class;

            if (selectedStudent == null || selectedClass == null)
            {
                MessageBox.Show("Please select both student and class.");
                return;
            }

            using (var context = new EnglishCenterDbContext())
            {
                var student = context.Students.FirstOrDefault(s => s.StudentsId == selectedStudent.StudentsId);
                if (student != null)
                {
                    student.ClassId = selectedClass.ClassId;
                    context.SaveChanges();
                    MessageBox.Show("Student added to class.");
                    this.DialogResult = true;
                    this.Close();
                }
            }
        }
    }
}

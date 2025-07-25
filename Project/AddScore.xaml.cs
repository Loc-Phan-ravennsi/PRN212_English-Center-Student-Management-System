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
    /// Interaction logic for AddScore.xaml
    /// </summary>
    public partial class AddScore : Window
    {
        EnglishCenterDbContext context = new();
        public AddScore()
        {
            InitializeComponent();
            LoadClasses();
        }

        private void LoadClasses()
        {
            var classes = context.Classes.ToList();
            cbClass.ItemsSource = classes;
        }

        private void LoadStudents(int classId)
        {
            var students = context.Students
                .Where(s => s.ClassId == classId)
                .ToList();
            cbStudent.ItemsSource = students;
            cbStudent.DisplayMemberPath = "FullName";
            cbStudent.SelectedValuePath = "StudentsId";
        }
        private void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbClass.SelectedItem is Class selectedClass)
            {
                LoadStudents(selectedClass.ClassId);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (cbClass.SelectedItem == null || cbStudent.SelectedItem == null)
            {
                txtStatus.Text = "Please select a class and a student.";
                return;
            }

            if (!int.TryParse(txtWeek.Text, out int weekNumber) || weekNumber <= 0)
            {
                txtStatus.Text = "Invalid week number.";
                return;
            }

            if (!double.TryParse(txtListening.Text, out double listening) ||
                !double.TryParse(txtSpeaking.Text, out double speaking) ||
                !double.TryParse(txtReading.Text, out double reading) ||
                !double.TryParse(txtWriting.Text, out double writing))
            {
                txtStatus.Text = "Scores must be numbers.";
                return;
            }

            if (listening < 0 || listening > 9 ||
                speaking < 0 || speaking > 9 ||
                reading < 0 || reading > 9 ||
                writing < 0 || writing > 9)
            {
                txtStatus.Text = "Scores must be between 0 and 9.";
                return;
            }

            var student = cbStudent.SelectedItem as Student;

            //Kiểm tra nếu điểm tuần đó đã tồn tại
            bool alreadyExists = context.Scores.Any(s =>
                s.StudentId == student.StudentsId && s.WeekNumber == weekNumber);

            if (alreadyExists)
            {
                txtStatus.Text = $"Score for week {weekNumber} already exists.";
                return;
            }

            var score = new Score
            {
                StudentId = student.StudentsId,
                WeekNumber = weekNumber,
                Listening = listening,
                Speaking = speaking,
                Reading = reading,
                Writing = writing,
                Feedback = txtFeedback.Text,
            };

            context.Scores.Add(score);
            context.SaveChanges();

            txtStatus.Foreground = Brushes.Green;
            txtStatus.Text = "Score saved successfully.";
        }

    }
}

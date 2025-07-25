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
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        EnglishCenterDbContext context = new EnglishCenterDbContext();
        private Student _student;

        public StudentWindow(Student student)
        {
            InitializeComponent();
            _student = student;
            tbGreeting.Text = $"Hello, {_student.FullName}!";
            txtFullName.Text = _student.FullName;
            txtStudentId.Text = _student.StudentsId.ToString();
            LoadWeeks();
        }

        private void LoadWeeks()
        {
            var weeks = context.Scores
            .Where(sc => sc.Student.StudentsId == _student.StudentsId)
            .Select(sc => sc.WeekNumber)
            .Distinct()
            .OrderByDescending(w => w)
            .ToList();

            cbWeek.ItemsSource = weeks;
            if (weeks.Any())
            {
                cbWeek.SelectedIndex = 0;
            }

        }

        private void LoadScoreForWeek(int selectedWeek)
        {
            var score = context.Scores
                .FirstOrDefault(sc => sc.Student.StudentsId == _student.StudentsId && sc.WeekNumber == selectedWeek);

            if (score != null)
            {
                txtListening.Text = score.Listening.ToString("0.0");
                txtSpeaking.Text = score.Speaking.ToString("0.0");
                txtReading.Text = score.Reading.ToString("0.0");
                txtWriting.Text = score.Writing.ToString("0.0");
                txtFeedback.Text = score.Feedback ?? "No feedback yet.";
            }
            else
            {
                txtListening.Text = "";
                txtSpeaking.Text = "";
                txtReading.Text = "";
                txtWriting.Text = "";
                txtFeedback.Text = "No data for this week.";
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void cbWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbWeek.SelectedItem is int selectedWeek)
            {
                LoadScoreForWeek(selectedWeek);
            }
        }
    }

}

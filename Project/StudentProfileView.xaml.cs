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
using System.Xml.Linq;
using Project.Models;

namespace Project
{
    /// <summary>
    /// Interaction logic for StudentProfileView.xaml
    /// </summary>
    public partial class StudentProfileView : UserControl
    {
        private int _studentId;
        private int _weekNumber;
        EnglishCenterDbContext context = new EnglishCenterDbContext();

        public StudentProfileView(int studentId, int weekNumber)
        {
            InitializeComponent();
            _studentId = studentId;
            _weekNumber = weekNumber;

            LoadStudentData();
        }

        private void LoadStudentData()
        {
            var student = context.Students.FirstOrDefault(s => s.StudentsId == _studentId);
            var score = context.Scores.FirstOrDefault(sc => sc.StudentId == _studentId && sc.WeekNumber == _weekNumber);

            if (student != null && score != null)
            {
                txtFullName.Text = student.FullName;
                txtStudentId.Text = student.StudentsId.ToString();
                txtListening.Text = score.Listening.ToString();
                txtSpeaking.Text = score.Speaking.ToString();
                txtReading.Text = score.Reading.ToString();
                txtWriting.Text = score.Writing.ToString();
                txtFeedback.Text = score.Feedback ?? string.Empty;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var score = context.Scores
        .FirstOrDefault(sc => sc.StudentId == _studentId && sc.WeekNumber == _weekNumber);

            if (score != null)
            {
                if (double.TryParse(txtListening.Text, out double listening)) score.Listening = listening;
                if (double.TryParse(txtSpeaking.Text, out double speaking)) score.Speaking = speaking;
                if (double.TryParse(txtReading.Text, out double reading)) score.Reading = reading;
                if (double.TryParse(txtWriting.Text, out double writing)) score.Writing = writing;

                score.Feedback = txtFeedback.Text;

                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Save failed:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Score data not found for this student and week.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

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
using Project.Models;

namespace Project
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : UserControl
    {
        EnglishCenterDbContext context = new();

        public StatisticsView()
        {
            InitializeComponent();
            LoadWeeks();
        }

        private void LoadStudents(int weekNumber)
        {
            var students = context.Students
            .Select(s => new
            {
                s.StudentsId,
                s.FullName,
                Scores = s.Scores
                    .Where(sc => sc.WeekNumber == weekNumber)
                    .Select(sc => (sc.Listening + sc.Speaking + sc.Reading + sc.Writing) / 4.0)
                    .ToList()
            })
            .ToList()
            .Select(s => new
            {
                s.StudentsId,
                s.FullName,
                AverageScore = s.Scores.Any() ? s.Scores.Average() : 0,
                Info = new StudentWeekInfo
                {
                    StudentId = s.StudentsId,
                    WeekNumber = weekNumber
                }
            })
            .OrderByDescending(s => s.AverageScore).ToList();

            StudentDataGrid.ItemsSource = students;
        }

        private void LoadWeeks()
        {
            var weeks = context.Scores
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

        private void cbWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbWeek.SelectedItem is int week)
            {
                LoadStudents(week);
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn?.Tag is StudentWeekInfo info)
            {
                var profileView = new StudentProfileView(info.StudentId, info.WeekNumber);

                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainContent.Content = profileView;
                }
            }
        }
    }
}

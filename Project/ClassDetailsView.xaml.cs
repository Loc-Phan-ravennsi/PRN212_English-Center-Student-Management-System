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
    /// Interaction logic for ClassDetailsView.xaml
    /// </summary>
    public partial class ClassDetailsView : UserControl
    {
        EnglishCenterDbContext context = new();
        private int _classId;

        public ClassDetailsView(int classId)
        {
            InitializeComponent();
            _classId = classId;
            LoadWeeks();
        }

        private void LoadStudents(int weekNumber)
        {
            var students = context.Students
                  .Where(s => s.ClassId == _classId)
                  .Select(s => new
                  {
                      s.StudentsId,
                      s.FullName,
                      AverageScore = s.Scores
                          .Where(sc => sc.WeekNumber == weekNumber)
                          .Select(sc => (sc.Listening + sc.Speaking + sc.Reading + sc.Writing) / 4.0)
                          .FirstOrDefault(),
                      Info = new StudentWeekInfo
                      {
                          StudentId = s.StudentsId,
                          WeekNumber = weekNumber
                      }
                  })
                  .ToList();

            StudentDataGrid.ItemsSource = students;
        }

        private void LoadWeeks()
        {
            var weeks = context.Scores
            .Where(sc => sc.Student.ClassId == _classId)
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

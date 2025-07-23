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
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        EnglishCenterDbContext context = new();
        public DashboardView()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var classList = context.Classes
                .Select(c => new
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    TeacherName = c.Teacher.FullName,
                    StudentCount = c.Students.Count()
                })
                .ToList();

            dgClassList.ItemsSource = classList;
        }


        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag != null)
            {
                int classId = (int)button.Tag;

                var classDetailsView = new ClassDetailsView(classId);

                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainContent.Content = classDetailsView;
                }
            }
        }
    }
}

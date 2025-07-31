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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Teacher teacher;
        public MainWindow(Teacher t)
        {
            InitializeComponent();
            teacher = t;
            MainContent.Content = new DashboardView(); // default view
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new DashboardView();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddStudent();
            addWindow.ShowDialog();
        }

        private void AddToClass_Click(object sender, RoutedEventArgs e)
        {
            var addToClassWindow = new AddStudentToClass();
            addToClassWindow.ShowDialog();
        }

        private void AddScore_Click(object sender, RoutedEventArgs e)
        {
            var addScoreWindow = new AddScore();
            addScoreWindow.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            var deleteStudentWindow = new DeleteStudent();
            deleteStudentWindow.ShowDialog();
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new StatisticsView();
        }
    }

}

using System.Windows;
using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;

namespace DemoArchitecture_SE192160
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEmployeeService _employeeService;

        public MainWindow()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            this.Loaded += MainWindow_Loaded; // đăng ký sự kiện Loaded
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgEmployees.ItemsSource = await _employeeService.GetAllAsync(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
            }
        }

        /// <summary>
        /// Xử lý tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var key = TxtSearch.Text;
            dgEmployees.ItemsSource = await _employeeService.GetAllAsync(key);
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedItem is Employee employee)
            {
                var result = MessageBox.Show(
                    "Do you want to delete this employee?",
                    "Warning",
                    MessageBoxButton.YesNo, MessageBoxImage.Question
                );

                if (result == MessageBoxResult.No) return;

                await _employeeService.DeleteAsync(employee);
                dgEmployees.ItemsSource = await _employeeService.GetAllAsync(null);
            }
        }

        private void BtnCreate_Click()
        {
        }
    }

    // Architecture: 3-layer
    // Presentation layer (UI): Giao diện người dùng
    // Business logic layer (BLL): Xử lý yêu cầu nghiệp vụ
    // Data access layer (DAL): Lấy dữ liệu từ database
}
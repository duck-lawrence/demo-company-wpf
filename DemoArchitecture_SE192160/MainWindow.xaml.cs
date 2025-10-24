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
        private readonly IDepartmentService _departmentService;

        public MainWindow()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _departmentService = new DepartmentService();
            this.Loaded += MainWindow_Loaded; // đăng ký sự kiện Loaded
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dgEmployees.ItemsSource = await _employeeService.GetAllAsync(null);
                CbxDepartment.ItemsSource = await _departmentService.GetAllAsync();
                CbxDepartment.DisplayMemberPath = "Name";
                CbxDepartment.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when get data: {ex.Message}");
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

        private async void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            // Khởi tạo 1 employee mới
            var employee = new Employee()
            {
                Name = TxtName.Text,
                Address = TxtAddress.Text,
                Age = int.TryParse(TxtAge.Text, out int age) ? age : null,
                DepartmentId = (int?)CbxDepartment.SelectedValue
            };

            await _employeeService.AddAsync(employee);
            dgEmployees.ItemsSource = await _employeeService.GetAllAsync(null);
        }

        private void dgEmployees_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // click dòng nào thì đổ data dòng đó vào form nhập
            if (dgEmployees.SelectedItem is Employee employee)
            {
                TxtName.Text = employee.Name;
                TxtAddress.Text = employee.Address;
                TxtAge.Text = employee.Age?.ToString() ?? "";
                CbxDepartment.SelectedValue = employee.DepartmentId;
            }
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var employee = new Employee()
            {
                //Id = ((Employee)dgEmployees.SelectedItem).Id,
                Name = TxtName.Text,
                Address = TxtAddress.Text,
                Age = int.TryParse(TxtAge.Text, out int age) ? age : null,
                DepartmentId = (int?)CbxDepartment.SelectedValue
            };

            if (dgEmployees.SelectedItem is Employee selectedEmployee)
            {
                employee.Id = selectedEmployee.Id;
            }

            await _employeeService.UpdateAsync(employee);
            dgEmployees.ItemsSource = await _employeeService.GetAllAsync(null);
        }

        // Architecture: 3-layer
        // Presentation layer (UI): Giao diện người dùng
        // Business logic layer (BLL): Xử lý yêu cầu nghiệp vụ
        // Data access layer (DAL): Lấy dữ liệu từ database
    }
}
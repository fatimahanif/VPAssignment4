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

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int updateID;
        public MainWindow()
        {
            InitializeComponent();
            EmployeeDBEntities dB = new EmployeeDBEntities();
            var employees = from employee in dB.Employees
                            select employee;
            employees_list.ItemsSource = employees.ToList();
        }

        private void refresh_btn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDBEntities dB = new EmployeeDBEntities();
            var employees = from employee in dB.Employees
                            select employee;
            employees_list.ItemsSource = employees.ToList();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDBEntities db = new EmployeeDBEntities();
            Employee employee = new Employee()
            {
                FirstName = firstName_txtbox.Text,
                LastName = lastName_txtbox.Text,
                CompanyURL = company_txtbox.Text,
                Desigation = designation_txtbox.Text,
                DOB = dob_picker.SelectedDate,
                Email = email_txtbox.Text,
                Image = img_txtbox.Text,
                Mobile = mobile_txtbox.Text,
                Salary = int.Parse(salary_txtbox.Text),
                Telephone = telephone_txtbox.Text
            };
            db.Employees.Add(employee);
            db.SaveChanges();
            employees_list.Items.Refresh();
        }

        private void employee_list_selectionchange(object sender, SelectionChangedEventArgs e)
        {
            Employee employee = (Employee)employees_list.SelectedItem;
            if (employee != null)
            {
                firstName_txtbox.Text = employee.FirstName;
                lastName_txtbox.Text = employee.LastName;
                company_txtbox.Text = employee.CompanyURL;
                designation_txtbox.Text = employee.Desigation;
                dob_picker.SelectedDate = employee.DOB;
                email_txtbox.Text = employee.Email;
                img_txtbox.Text = employee.Image;
                mobile_txtbox.Text = employee.Mobile;
                salary_txtbox.Text = employee.Salary.ToString();
                telephone_txtbox.Text = employee.Telephone;
                updateID = employee.EmpID;
            }
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDBEntities db = new EmployeeDBEntities();
            var employees = from employee in db.Employees
                            where employee.EmpID == updateID
                            select employee;
            Employee emp = employees.SingleOrDefault();
            if (emp != null)
            {
                emp.FirstName = firstName_txtbox.Text;
                emp.LastName = lastName_txtbox.Text;
                emp.CompanyURL = company_txtbox.Text;
                emp.Desigation = designation_txtbox.Text;
                emp.DOB = dob_picker.SelectedDate;
                emp.Email = email_txtbox.Text;
                emp.Image = img_txtbox.Text;
                emp.Mobile = mobile_txtbox.Text;
                emp.Salary = (decimal?)double.Parse(salary_txtbox.Text);
                emp.Telephone = telephone_txtbox.Text;

                db.SaveChanges();
                employees_list.Items.Refresh();

            }
        }

        private void remove_btn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDBEntities db = new EmployeeDBEntities();
            var employees = from employee in db.Employees
                            where employee.EmpID == updateID
                            select employee;
            Employee emp = employees.SingleOrDefault();
            if (emp != null)
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
                employees_list.Items.Refresh();

            }
        }

        private void filter_button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDBEntities Db = new EmployeeDBEntities();
            var employees = from employee in Db.Employees
                                where employee.FirstName.Contains(filter_txtbox.Text) || employee.LastName.Contains(filter_txtbox.Text)
                                orderby employee.EmpID
                                select employee;
            employees_list.ItemsSource = employees.ToList();
            employees_list.Items.Refresh();
        }
    }
}

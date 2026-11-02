using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PharmacyManagementSystem.AdministratorUC
{
    public partial class UC_AddUser : UserControl
    {
        Function fn = new Function();
        string query;
        public UC_AddUser()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string role = txtUserRole.Text;
            string name = txtName.Text;
            string dob = txtDob.Text;
            Int64 mobile = Int64.Parse(txtMobileNo.Text);
            string email = txtEmail.Text;
            string username = txtUsername.Text;
            string pass = txtPassword.Text;

            try
            {
                query = "insert into users (userRole,name,dob,mobile,email,username,pass) values ('" +role+ "','" +name+ "','" +dob+ "'," +mobile+ ",'" +email+ "','" +username+ "','" +pass+ "')";
                fn.setData(query, "Sign Up Successfully.");
                
            }
            catch (Exception)
            {
                MessageBox.Show("Username Allready exist." ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        public void clearAll()
        {
            txtUserRole.SelectedIndex = -1;
            txtName.Clear();
            txtDob.ResetText();
            txtMobileNo.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            query = "select * from users where username='" + txtUsername.Text + "'";
            DataSet ds = fn.GetData(query);

            if (ds.Tables[0].Rows.Count == 0)
            {
                pictureBox2.ImageLocation = @"C:\\Users\\Ashikur Rahman\\Downloads\\C# Pharmacy Management System\\Pharmacy Management System in C#\yes.png";
            }
            else
            {
                pictureBox2.ImageLocation = @"C:\\Users\\Ashikur Rahman\\Downloads\\C# Pharmacy Management System\\Pharmacy Management System in C#\no.png";
            }
        }
    }
}

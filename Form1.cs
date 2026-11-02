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

namespace PharmacyManagementSystem
{
    public partial class Form1 : Form
    {
        Function fn = new Function();
        string query;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            query = "select * from users";
            ds = fn.GetData(query);
            if (ds.Tables[0].Rows.Count == 0)
            {
                if (txtUsername.Text == "root" && txtPassword.Text == "root")
                {
                    Adminstrator admin = new Adminstrator();
                    admin.Show();
                    this.Hide();
                }
            }
            else
            { 
                query="select * from users where username= '"+txtUsername.Text+"' and pass= '"+txtPassword.Text+"'";
                ds = fn.GetData(query);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    string role = ds.Tables[0].Rows[0][1].ToString();
                    if (role == "Administrator")
                    {
                        Adminstrator admin = new Adminstrator(txtUsername.Text);
                        admin.Show();
                        this.Hide();
                    }
                    else if (role == "Pharmacist")
                    {
                        Pharmacist pharm = new Pharmacist();
                        pharm.Show();
                        this.Hide();
                    }
                    

                }
                else
                {
                   MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            //if (txtUsername.Text == "admin" && txtPassword.Text == "admin123")
            //{
            //    MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    // Proceed to the next form or main application window
            //    Adminstrator am = new Adminstrator();
            //    am.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}

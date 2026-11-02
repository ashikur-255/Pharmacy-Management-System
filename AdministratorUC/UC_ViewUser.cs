using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementSystem.AdministratorUC
{
    
    public partial class UC_ViewUser : UserControl
    {
        Function fn = new Function();
        string query;
        string currentUser = "";
        public UC_ViewUser()
        {
            InitializeComponent();
        }

        public string ID
        {
            set { currentUser = value; }
        }

        private void UC_ViewUser_Load(object sender, EventArgs e)
        {
            query="select * from users";
            DataSet ds=fn.GetData(query);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            query = "select * from users where username like '" + txtUserName.Text + "%'";  //a%
            DataSet ds = fn.GetData(query);
            dataGridView1.DataSource = ds.Tables[0];
        }

        string userName;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                userName = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            catch
            {
                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure?", "Delete Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (currentUser != userName)
                {
                    query = "delete from users where username= '" + userName + "'";
                    fn.setData(query, "User Deleted Successfully.");
                    UC_ViewUser_Load(this, null);
                }
                else
                {
                    MessageBox.Show("You cannot delete your own account.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_ViewUser_Load(this, null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementSystem.PharmacistUC
{
    public partial class UC_P_MedicineValidityCheck : UserControl
    {
        Function fn = new Function();
        string query;
        public UC_P_MedicineValidityCheck()
        {
            InitializeComponent();
        }

        private void txtCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtCheck.SelectedIndex == 0)
            {
                query = "select * from medic where eDate >= GETDATE()";
                //DataSet ds = fn.GetData(query);
                //dataGridView2.DataSource = ds.Tables[0];
                //setLabel.Text = "Valid Medicines:";
                //setLabel.ForeColor = Color.Green;

                setDataGridView(query, "Valid Medicines:", Color.Green);

            }
            else if (txtCheck.SelectedIndex == 1)
            {
                query = "select * from medic where eDate < GETDATE()";              
                setDataGridView(query, "Expired Medicines:", Color.Red);
            }
            else if (txtCheck.SelectedIndex == 2)
            {
                query = "select * from medic";
                setDataGridView(query, "", Color.Black);
            }

        }
        private void setDataGridView(string query,string labelName,Color col)
        {
            DataSet ds = fn.GetData(query);
            dataGridView2.DataSource = ds.Tables[0];
            setLabel.Text = labelName;
            setLabel.ForeColor = col;
        }

        private void UC_P_MedicineValidityCheck_Load(object sender, EventArgs e)
        {
            setLabel.Text = "";
        }
    }
}

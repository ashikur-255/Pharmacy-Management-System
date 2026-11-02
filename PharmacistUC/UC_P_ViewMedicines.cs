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
    public partial class UC_P_ViewMedicines : UserControl
    {
        Function fn = new Function();
        string query;
        public UC_P_ViewMedicines()
        {
            InitializeComponent();
        }

        private void UC_P_ViewMedicines_Load(object sender, EventArgs e)
        {
            query = "select * from medic";
            setDataGridView2(query);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            query = "select * from medic where mname like '" + txtSearch.Text + "%'";
            setDataGridView2(query);
        }
        private void setDataGridView2(string query)
        {
            DataSet ds = fn.GetData(query);
            dataGridView2.DataSource = ds.Tables[0];
        }

        string medicineId;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                medicineId = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You want Delete Medicine?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                query = "delete from medic where mid = '" + medicineId + "'";
                fn.setData(query, "Medicine Deleted Successfully.");
                UC_P_ViewMedicines_Load(this, null);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_P_ViewMedicines_Load(this, null);
        }
    }
}

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

namespace PharmacyManagementSystem.PharmacistUC
{
    public partial class UC_P_AddMedicine : UserControl
    {
        Function fn = new Function();
        string query;       
        public UC_P_AddMedicine()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtMediId.Text != "" && txtMediName.Text != "" && txtQuantity.Text != "" && txtMediNumber.Text != "" && txtPricePerUnit.Text != "")
            {
                String mid = txtMediId.Text;
                String mname = txtMediName.Text;            
                String mnumber = txtMediNumber.Text;           
                String mDate = txtManufacturingDate.Value.ToString("MM/dd/yyyy");
                String eDate = txtExpireDate.Value.ToString("MM/dd/yyyy");
                Int64 quantity = Int64.Parse(txtQuantity.Text);
                Int64 perunit = Int64.Parse(txtPricePerUnit.Text);
                query = "insert into medic(mid,mname,mnumber,mDate,eDate,quantity,perUnit) values('" + mid + "','" + mname + "'," + mnumber + ",'" + mDate + "','" + eDate + "','" + quantity + "','" + perunit + "')";
                fn.setData(query, "Medicine Added Successfully.");
                clearAll();
            }
            else
            {
                MessageBox.Show("Please fill all the fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
           clearAll();
        }

        public void clearAll()
        {
            txtMediId.Clear();
            txtMediName.Clear();
            txtQuantity.Clear();
            txtMediNumber.Clear();
            txtPricePerUnit.Clear();
            txtManufacturingDate.ResetText();
            txtExpireDate.ResetText();        
           
        }

    }
}

using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementSystem.PharmacistUC
{
    public partial class UC_P_SellMedicine : UserControl
    {
        Function fn = new Function();
        String query;
        DataSet ds;
        public UC_P_SellMedicine()
        {
            InitializeComponent();
        }

        private void UC_P_SellMedicine_Load(object sender, EventArgs e)
        {
            ListBoxMedicines.Items.Clear();
            query = "select mname from medic where eDate >= getdate() and quantity >'0'";
            ds = fn.GetData(query);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ListBoxMedicines.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_P_SellMedicine_Load(this, null);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ListBoxMedicines.Items.Clear();
            query = "select mname from medic where eDate >= getdate() and quantity >'0' and mname like '" + txtSearch.Text + "%'";
            ds = fn.GetData(query);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ListBoxMedicines.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        private void ListBoxMedicines_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNoOfUnites.Clear();

            string name = ListBoxMedicines.GetItemText(ListBoxMedicines.SelectedItem);
            txtMediName.Text = name;
            query = "select mid,eDate,perUnit from medic where mname = '" + name + "'";
            ds = fn.GetData(query);
            txtMediId.Text = ds.Tables[0].Rows[0][0].ToString();
            txtExpireDate.Text = ds.Tables[0].Rows[0][1].ToString();
            txtPricePerUnite.Text = ds.Tables[0].Rows[0][2].ToString();
        }

        private void txtNoOfUnites_TextChanged(object sender, EventArgs e)
        {
            if (txtNoOfUnites.Text != "")
            {
                Int64 unitPrice = Int64.Parse(txtPricePerUnite.Text);
                Int64 noOfUnits = Int64.Parse(txtNoOfUnites.Text);
                Int64 totalAmount = unitPrice * noOfUnits;
                txtTotalPrice.Text = totalAmount.ToString();
            }
            else
            {
                txtTotalPrice.Clear();
            }
        }

        protected int n, totalAmount = 0;
        protected Int64 quantity, newQuantity;

        
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (txtMediId.Text !="")
            {
                query = "select quantity from medic where mid = '" +txtMediId.Text+ "'";
                ds = fn.GetData(query);

                quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                newQuantity = quantity - Int64.Parse(txtNoOfUnites.Text);

                if (newQuantity >= 0)
                {
                    n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells[0].Value = txtMediId.Text;
                    dataGridView2.Rows[n].Cells[1].Value = txtMediName.Text;
                    dataGridView2.Rows[n].Cells[2].Value = txtExpireDate.Text;
                    dataGridView2.Rows[n].Cells[3].Value = txtPricePerUnite.Text;
                    dataGridView2.Rows[n].Cells[4].Value = txtNoOfUnites.Text;
                    dataGridView2.Rows[n].Cells[5].Value = txtTotalPrice.Text;

                    totalAmount = totalAmount + int.Parse(txtTotalPrice.Text);
                    totalLabel.Text = "BDT. " + totalAmount.ToString();

                    query = "update medic set quantity = '" + newQuantity + "' where mid = '" + txtMediId.Text + "'";
                    fn.setData(query, "Medicine Added.");

                }
                else
                {
                    MessageBox.Show("Required Medicine is not available in stock.\n Only " + quantity + " Left", "Warning !!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                clearAll();
                UC_P_SellMedicine_Load(this, null);

            }
            else
            {
                MessageBox.Show("Please select medicine to add in cart", "Information !!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        int valueAmount;
        string valueId;
        protected Int64 noOfUnit;

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                valueAmount = int.Parse(dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString());
                valueId = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                noOfUnit = Int64.Parse(dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString());
            }
            catch (Exception)
            {
                //MessageBox.Show("Please select a valid cell", "Error !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (valueId != null)
            {
                try
                {
                    dataGridView2.Rows.RemoveAt(this.dataGridView2.SelectedRows[0].Index);
                }
                catch
                {

                }
                finally
                {
                    query = "select quantity from medic where mid = '" + valueId + "'";
                    ds = fn.GetData(query);
                    quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    newQuantity = quantity + noOfUnit;

                    query = "update medic set quantity = '" + newQuantity + "' where mid = '" + valueId + "'";
                    fn.setData(query,"Medicine Removed from Cart.");
                    totalAmount = totalAmount - valueAmount;
                    totalLabel.Text = "BDT. " + totalAmount.ToString();
                }
                
                UC_P_SellMedicine_Load(this, null);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DGVPrinter print = new DGVPrinter();
            print.Title = "lazz pharma Ltd";
            print.SubTitle = String.Format("Date: {0}",DateTime.Now);
            print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            print.PageNumbers = true;
            print.PageNumberInHeader = false;
            print.PorportionalColumns = true;
            print.HeaderCellAlignment = StringAlignment.Near;
            print.Footer = "Total Payable Amount: " + totalLabel.Text;
            print.FooterSpacing = 15;
            print.PrintDataGridView(dataGridView2);

            totalAmount = 0;
            totalLabel.Text = "BDT. 00";
            dataGridView2.DataSource = 0;
        }

        private void clearAll()
        {
            txtMediId.Clear();
            txtMediName.Clear();
            txtExpireDate.ResetText();
            txtPricePerUnite.Clear();
            txtNoOfUnites.Clear();
            
        }

    }
}

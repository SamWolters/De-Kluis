using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De_kluis
{
    public partial class Form1 : Form
    {
        private SafeEngine safeEngine;

        public Form1()
        {
            InitializeComponent();

            safeEngine = new SafeEngine();
            txtPincode.Text = safeEngine.getDisplayText();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            safeEngine.Open();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            safeEngine.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(safeEngine.getDisplayText());
        }

        private void txtPincode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            safeEngine.numberPressed(e);
            txtPincode.Text = safeEngine.getDisplayText();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPincode.Clear();
            safeEngine.reset();
            txtPincode.Text = safeEngine.getDisplayText();
        }


        private void btnChange_Click(object sender, EventArgs e)
        {
            //Form testDialog = new Form();
            Form2 testDialog = new Form2();

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                safeEngine = new SafeEngine(testDialog.txtNew.Text.Length, testDialog.txtNew.Text);
                txtPincode.Text = safeEngine.getDisplayText();
            }
            testDialog.Dispose();
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            MessageBox.Show("De kluis is: " + safeEngine.getStatus() + "\n" + "Bool kluis open: " + safeEngine.isOpen());
        }

        private void txtPincode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomerInfoApp.Model;
using CustomerInfoApp.BLL;
using System.Data.SqlClient;

namespace CustomerInfoApp
{
    public partial class CustomerInfoUI : Form
    {
        string connectionString = @"Server = HABIB; Database = CustomerInfo; Integrated Security = true";
        int id;

        CustomerManager _customerManager = new CustomerManager();
        DistrictManager _districtManager = new DistrictManager();

        Customer customer = new Customer();
        District district = new District();

        public CustomerInfoUI()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string code = codeTextBox.Text;
            string name = nameTextBox.Text;
            string address = addressTextBox.Text;
            string contact = contactTextBox.Text;
            string district = districtComboBox.Text;

            customer.Code = codeTextBox.Text;
            customer.Name = nameTextBox.Text;
            customer.Address = addressTextBox.Text;
            customer.Contact = contactTextBox.Text;
            customer.DistrictId = Convert.ToInt32(districtComboBox.SelectedValue);

            if (addButton.Text == "Update")
            {
                if (_customerManager.Update(customer, id))
                {
                    MessageBox.Show("Updated");
                    showDataGridView.DataSource = _customerManager.Display();
                    addButton.Text = "Add";
                    return;
                }
            }

            if (String.IsNullOrEmpty(code))
            {
                codeLabel.ForeColor = Color.Red;
                codeLabel.Text = "Please Code field must not be empty..";
                return;
            }
            else if (!_customerManager.CheckIfNumeric(code))
            {
                codeLabel.ForeColor = Color.Red;
                codeLabel.Text = "Please Code field required numeric value..";
                return;
            }
            else if (code.Length != 4)
            {
                codeLabel.ForeColor = Color.Red;
                codeLabel.Text = "Please Code field required 4 length Value..";
                return;
            }
            else
            {
                codeLabel.Text = "";
            }

            if (String.IsNullOrEmpty(name))
            {
                nameLabel.ForeColor = Color.Red;
                nameLabel.Text = "Please Name field must  be required..";
                return;
            }
            else if (_customerManager.IsNameExist(customer))
            {
                nameLabel.ForeColor = Color.Red;
                nameLabel.Text = "Customer is already exist..";
                return;
            }
            else
            {
                nameLabel.Text = "";
            }
            //if (String.IsNullOrEmpty(address))
            //{
            //    addressLabel.ForeColor = Color.Red;
            //    addressLabel.Text = "Please Address field must  be required..";
            //}
            if (String.IsNullOrEmpty(contact))
            {
                contactLabel.ForeColor = Color.Red;
                contactLabel.Text = "Please Contact field must  be required..";
                return;
            }
            else if (!_customerManager.CheckIfNumeric(contact))
            {
                contactLabel.ForeColor = Color.Red;
                contactLabel.Text = "Please Contact field required numeric value..";
                return;
            }
            else if (contact.Length != 11)
            {
                contactLabel.ForeColor = Color.Red;
                contactLabel.Text = "Please Code field required 11 length Value..";
                return;
            }
            else if (_customerManager.IsContactExist(customer))
            {
                contactLabel.ForeColor = Color.Red;
                contactLabel.Text = "Contact is already exist..";
                return;
            }
            else
            {
                contactLabel.Text = "";
            }

            if (String.IsNullOrEmpty(district))
            {
                distLabel.ForeColor = Color.Red;
                distLabel.Text = "Please Select any district..";
                return;
            }
            else
            {
                distLabel.Text = "";
            }

            if (code!=""|| name != ""|| contact != "" ||district!="")
            {
                if (_customerManager.Add(customer))
                {
                    MessageBox.Show("Added");
                    showDataGridView.DataSource = _customerManager.Display();
                    addButton.Text = "Update";
                    return;
                }
                else
                {
                    MessageBox.Show("Not Added");
                    return;
                }
            }
        }

        private void CustomerInfoUI_Load(object sender, EventArgs e)
        {
            districtComboBox.DataSource = _districtManager.LoadDistrict();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            customer.Code = codeTextBox.Text;
            customer.Name = nameTextBox.Text;
            customer.Address = addressTextBox.Text;
            customer.Contact = contactTextBox.Text;
            customer.DistrictId = Convert.ToInt32(districtComboBox.SelectedValue);

            showDataGridView.DataSource = "";
            showDataGridView.DataSource = _customerManager.Search(customer);
        }

        private void showDataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (showDataGridView.CurrentRow.Index != -1)
            {
                id = Convert.ToInt32(showDataGridView.CurrentRow.Cells[0].Value.ToString());
                codeTextBox.Text = showDataGridView.CurrentRow.Cells[1].Value.ToString();
                nameTextBox.Text = showDataGridView.CurrentRow.Cells[2].Value.ToString();
                addressTextBox.Text = showDataGridView.CurrentRow.Cells[3].Value.ToString();
                districtComboBox.Text = showDataGridView.CurrentRow.Cells[4].Value.ToString();
                contactTextBox.Text = showDataGridView.CurrentRow.Cells[5].Value.ToString();
            }
        }
    }
}

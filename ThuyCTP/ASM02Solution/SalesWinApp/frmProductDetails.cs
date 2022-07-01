using DataAccess.DataAccess;
using DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmProductDetails : Form
    {
        public frmProductDetails()
        {
            InitializeComponent();
        }
        //------------------------------
        public IProductRepository ProductRepository { get; set; }
        public bool InsertOrUpdate { get; set; } //False: Insert, True: Update
        public Product ProductInfo { get; set; }
        //------------------------------

        private void frmProductDetails_Load(object sender, EventArgs e)
        {
            cboCategoryId.SelectedIndex = 0;
            txtProductId.Enabled = !InsertOrUpdate;
            if(InsertOrUpdate == true) //update mode
            {
                //Show product to perform updating
                txtProductId.Text = ProductInfo.ProductId.ToString();
                cboCategoryId.Text = ProductInfo.CategoryId.ToString();
                txtProductName.Text = ProductInfo.ProductName.ToString();
                txtWeight.Text = ProductInfo.Weight.ToString();
                txtUnitPrice.Text = ProductInfo.UnitPrice.ToString("0#");
                txtUnitInStock.Text = ProductInfo.UnitsInStock.ToString();
            }
        }

        //------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            try{
                var Product = new Product
                {
                    ProductId = Convert.ToInt32(txtProductId.Text),
                    CategoryId = Convert.ToInt32(cboCategoryId.Text),
                    ProductName = txtProductName.Text,
                    Weight = txtWeight.Text,
                    UnitPrice = Convert.ToInt32(txtUnitPrice.Text),
                    UnitsInStock = Convert.ToInt32(txtUnitInStock.Text)
                };

                if(InsertOrUpdate == false)
                {
                    ProductRepository.InsertProduct(Product);
                    MessageBox.Show("Added product successfully", "Adding product");
                }
                else
                {
                    ProductRepository.UpdateProduct(Product);
                    MessageBox.Show("Updated product successfully", "Updating product");
                }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, InsertOrUpdate == false ? "Add a new product" : "Update a product");
            }
            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e) => Close();
    }
}

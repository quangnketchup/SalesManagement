using DataAccess.DataAccess;
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
    public partial class frmProductManagements : Form
    {
        public bool isAdmin { get; set; }
        public frmProductManagements()
        {
            InitializeComponent();
        }


        public frmMain Main { get; set; }

        IProductRepository ProductRepository = new ProductRepository();

        BindingSource Source;

        private void frmProductManagement_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            dgvProductList.CellDoubleClick += dbvProductList_CellDoubleClick;
        }


        private void dbvProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmProductDetails frmProductDetails = new frmProductDetails
            {
                Text = "Updating Product",
                InsertOrUpdate = true,
                ProductInfo = GetProductObject(),
                ProductRepository = ProductRepository
            };
            if(frmProductDetails.ShowDialog() == DialogResult.OK)
            {
                LoadProductList();

                Source.Position = Source.Position - 1;
            }
        }


        private Product GetProductObject()
        {
            Product product = null;
            try
            {
                product = new Product
                {
                    ProductId = int.Parse(txtProductId.Text),
                    CategoryId = int.Parse(txtCategoryId.Text),
                    ProductName = txtProductName.Text,
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitslnStock = int.Parse(txtUnitInStock.Text)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Product");
            }
            return product;
        } 

        private void ClearText()
        {
            txtProductId.Text = string.Empty;
            txtCategoryId.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
            txtUnitInStock.Text = string.Empty;
        }


        private void LoadProductList()
        {
            var products = ProductRepository.GetProducts();
            try
            {
                Source = new BindingSource();
                Source.DataSource = products;

                txtProductId.DataBindings.Clear();
                txtCategoryId.DataBindings.Clear();
                txtProductName.DataBindings.Clear();
                txtWeight.DataBindings.Clear();
                txtUnitPrice.DataBindings.Clear();
                txtUnitInStock.DataBindings.Clear();

                txtProductId.DataBindings.Add("Text", Source, "ProductId");
                txtCategoryId.DataBindings.Add("Text", Source, "CategoryId");
                txtProductName.DataBindings.Add("Text", Source, "ProductName");
                txtWeight.DataBindings.Add("Text", Source, "Weight");
                txtUnitPrice.DataBindings.Add("Text", Source, "UnitPrice");
                txtUnitInStock.DataBindings.Add("Text", Source, "UnitInStock");

                dgvProductList.DataSource = null;
                dgvProductList.DataSource = Source;
                if (products.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Product List");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadProductList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmProductDetails frmProductDetails = new frmProductDetails
            {
                Text = "Adding a Product",
                InsertOrUpdate = false,
                ProductRepository = ProductRepository
            };

            if (frmProductDetails.ShowDialog() == DialogResult.OK)
            {
                LoadProductList();

                Source.Position = Source.Position - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = GetProductObject();
                ProductRepository.DeleteProduct(product.ProductId);
                LoadProductList();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message, "Deleting product");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Main.Show();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var products = ProductRepository.GetFilteredProducts(txtFilter.Text);
            try
            {
                Source = new BindingSource();
                Source.DataSource = products;

                txtProductId.DataBindings.Clear();
                txtCategoryId.DataBindings.Clear();
                txtProductName.DataBindings.Clear();
                txtWeight.DataBindings.Clear();
                txtUnitPrice.DataBindings.Clear();
                txtUnitInStock.DataBindings.Clear();

                txtProductId.DataBindings.Add("Text", Source, "ProductId");
                txtCategoryId.DataBindings.Add("Text", Source, "CategoryId");
                txtProductName.DataBindings.Add("Text", Source, "ProductName");
                txtWeight.DataBindings.Add("Text", Source, "Weight");
                txtUnitPrice.DataBindings.Add("Text", Source, "UnitPrice");
                txtUnitInStock.DataBindings.Add("Text", Source, "UnitInStock");

                dgvProductList.DataSource = null;
                dgvProductList.DataSource = Source;
                if (products.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load filtered Product List");
            }
        }
    }
}

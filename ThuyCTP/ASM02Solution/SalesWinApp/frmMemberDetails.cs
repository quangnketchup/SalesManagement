using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess.DataAccess;
using DataAccess.Repository;
namespace SalesWinApp
{
    public partial class frmMemberDetails : Form
    {
        public IMemberRepository MemberRepository { get; set; }
        public bool InsertOrUpdate { get; set; }
        public Member MemberInfor { get; set; }
        public frmMemberDetails()
        {
            InitializeComponent();
        }

        private void frmMemberDetail_Load(object sender, EventArgs e)
        {
            txtMemberID.Enabled = !InsertOrUpdate;
            if (InsertOrUpdate == true)//update mode
            {
                //Show member to perform updating
                txtMemberID.Text = MemberInfor.MemberId.ToString();
                txtCompanyName.Text = MemberInfor.CompanyName;
                cboCity.Text = MemberInfor.City;
                txtEmail.Text = MemberInfor.Email;
                cboCountry.Text = MemberInfor.Country;
                txtPassword.Text = MemberInfor.Password;
            }
        }

        private void cboFillter_SelectCity(object sender, EventArgs e)
        {
            cboCountry.Text = cboCountry.GetItemText(cboCountry.SelectedItem);
            if (cboCountry.Text.Equals("United State"))
            {
                cboCity.SelectedIndex = -1;
                cboCity.Items.Clear();
                cboCity.Items.Add("New York");
                cboCity.Items.Add("San Diego");
                cboCity.Items.Add("Houston");
            }
            else if (cboCountry.Text.Equals("Viet Nam"))
            {
                cboCity.SelectedIndex = -1;
                cboCity.Items.Clear();
                cboCity.Items.Add("Phu Quoc");
                cboCity.Items.Add("Da Nang");
                cboCity.Items.Add("Sai gon");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var member = new Member
                {
                    MemberId = int.Parse(txtMemberID.Text),
                    CompanyName = txtCompanyName.Text,
                    City = cboCity.Text,
                    Email = txtEmail.Text,
                    Country = cboCountry.Text,
                    Password = txtPassword.Text,
                    Status = 1
                };
                if (InsertOrUpdate == false)
                {
                    MemberRepository.InsertMember(member);
                }
                else
                {
                    MemberRepository.UpdateMember(member);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new Member" : "Update a member");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

    }
}


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
    public partial class frmMemberManagements : Form
    {
        public bool IsAdmin { get; set; }
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        public frmMemberManagements()
        {
            InitializeComponent();
        }

        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            IsAdmin = true;
            if(IsAdmin == false)
            {
                btnDelete.Enabled = false;
                btnNew.Enabled = false;

                txtCity.Enabled = false;
                txtCountry.Enabled = false;
                txtEmail.Enabled = false;
                txtMemberID.Enabled = false;
                txtCompanyName.Enabled = false;
                txtPassword.Enabled = false;
                txtStatus.Enabled = false;
                btnDelete.Enabled = false;
                btnFind.Enabled = false;
                cboFillterName.Enabled = false;
                cboFillter.Enabled = false;
                dgvMemberList.CellDoubleClick += null;
            }
            else
            {
                btnDelete.Enabled = false;               
                dgvMemberList.CellDoubleClick += DgvMemberList_CellDoubleClick;
            }
        }

        private void DgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails



            {
                Text = "Update member",
                InsertOrUpdate = true,
                MemberInfor = GetMemberObject(),
                MemberRepository = memberRepository
            };
            if (frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                //Set focus member updated
                source.Position = source.Count - 1;
            }
        }

        private Member GetMemberObject()
        {
            Member member = null;
            try
            {
                member = new Member
                {
                    MemberId = int.Parse(txtMemberID.Text),
                    CompanyName = txtCompanyName.Text,
                    Password = txtPassword.Text,
                    Email = txtEmail.Text,
                    Country = txtCountry.Text,
                    City = txtCity.Text,
                    Status = int.Parse(txtStatus.Text)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member");
            }
            return member;
        }

        private void ClearText()
        {
            txtMemberID.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtStatus.Text = string.Empty;
        }

        public void LoadMemberList()
        {
            var members = memberRepository.GetMembers();

            try
            {
                //The BindingSource omponent is designed to simplify
                //the process of binding controls to an underlying data source
                source = new BindingSource();
                source.DataSource = members.OrderByDescending(member => member.CompanyName);
                txtMemberID.DataBindings.Clear();
                txtCompanyName.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtCountry.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtStatus.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtCompanyName.DataBindings.Add("Text", source, "CompanyName");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtCountry.DataBindings.Add("Text", source, "Country");
                txtCity.DataBindings.Add("Text", source, "City");
                txtStatus.DataBindings.Add("Text", source, "Status");


                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;
                if (IsAdmin == false)
                {
                    if (members.Count() == 0)
                    {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                    }
                }
                else
                {
                    if (members.Count() == 0)
                    {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else
                    {
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void SearchMember()
        {
            var members = memberRepository.GetMembers();
            List<Member> listmem = new List<Member>();
            foreach (var member in members)
            {
                if (member.MemberId.ToString().Equals(txtSearch.Text))
                {
                    listmem.Add(member);
                }
                else if (member.CompanyName.ToUpper().Contains((txtSearch.Text).ToUpper()))
                {
                    listmem.Add(member);
                }
            }
            try
            {
                source = new BindingSource();
                source.DataSource = listmem;

                txtMemberID.DataBindings.Clear();
                txtCompanyName.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtCountry.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtStatus.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtCompanyName.DataBindings.Add("Text", source, "CompanyName");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtCountry.DataBindings.Add("Text", source, "Country");
                txtCity.DataBindings.Add("Text", source, "City");
                txtStatus.DataBindings.Add("Text", source, "Status");


                dgvMemberList.DataSource = null;
                if (listmem.Count == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    dgvMemberList.DataSource = null;
                    dgvMemberList.DataSource = source;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void FillterByCoutryCity()
        {
            var members = memberRepository.GetMembers();
            List<Member> listFill = new List<Member>();

            foreach (Member member in members)
            {
                if (member.City.Contains(cboFillterName.Text) && member.Country.Contains(cboFillter.Text))
                {
                    listFill.Add(member);
                }
            }
            try
            {
                if (listFill.Count == 0)
                {
                    MessageBox.Show("No member matched", "No result");
                }
                else if (listFill.Count != 0)
                {
                    source = new BindingSource();
                    source.DataSource = listFill.OrderByDescending(member => member.CompanyName);
                    txtMemberID.DataBindings.Clear();
                    txtCompanyName.DataBindings.Clear();
                    txtPassword.DataBindings.Clear();
                    txtEmail.DataBindings.Clear();
                    txtCountry.DataBindings.Clear();
                    txtCity.DataBindings.Clear();
                    txtStatus.DataBindings.Clear();

                    txtMemberID.DataBindings.Add("Text", source, "MemberID");
                    txtCompanyName.DataBindings.Add("Text", source, "CompanyName");
                    txtPassword.DataBindings.Add("Text", source, "Password");
                    txtEmail.DataBindings.Add("Text", source, "Email");
                    txtCountry.DataBindings.Add("Text", source, "Country");
                    txtStatus.DataBindings.Add("Text", source, "Status");


                    dgvMemberList.DataSource = null;
                    dgvMemberList.DataSource = source;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void cboFillter_SelectCity(object sender, EventArgs e)
        {
            cboFillter.Text = cboFillter.GetItemText(cboFillter.SelectedItem);
            if (cboFillter.Text.Equals("American"))
            {
                cboFillterName.SelectedIndex = -1;
                cboFillterName.Items.Clear();
                cboFillterName.Items.Add("New York");
                cboFillterName.Items.Add("San Diego");
                cboFillterName.Items.Add("Houston");
            }
            else if (cboFillter.Text.Equals("Viet Nam"))
            {
                cboFillterName.SelectedIndex = -1;
                cboFillterName.Items.Clear();
                cboFillterName.Items.Add("HCM");
                cboFillterName.Items.Add("Da Nang");
                cboFillterName.Items.Add("Ha Noi");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadMemberList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails
            {
                Text = "Add member",
                InsertOrUpdate = false,
                MemberRepository = memberRepository
            };
            if (frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                //Set focus member inserted
                source.Position = source.Count - 1;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var member = GetMemberObject();
                memberRepository.DeleteMember(member.MemberId);
                LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a member");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMember();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FillterByCoutryCity();
        }

    }
}

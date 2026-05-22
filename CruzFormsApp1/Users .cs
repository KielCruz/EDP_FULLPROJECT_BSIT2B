using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CruzFormsApp1
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }
        MYDATABASEcs DB = new MYDATABASEcs();
        private int selectedUserID = -1;
        private int selectedLoginID = -1;
        private bool isUpdating = false;

        private void Users_Load(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnDeactivate.Enabled = false;

            string query = "SELECT tbluser_information.userID, tbllogin_credentials.LoginID, tbluser_information.firstname, tbluser_information.middlename, tbluser_information.lastname, tbluser_information.emailAddress, tbluser_information.homeAddress, tbluser_information.birthDate, tbllogin_credentials.user_username as 'Username', tbllogin_credentials.user_password as 'Password' FROM tbllogin_credentials INNER JOIN tbluser_information ON tbllogin_credentials.userID = tbluser_information.userID";

            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvUsers.DataSource = DB.ExecuteReturnQuery(query);
            dgvUsers.Columns[0].Visible = false;
            dgvUsers.Columns[1].Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string insertUser = "INSERT INTO tbluser_information (firstname, middlename, lastname, emailAddress, homeAddress, birthDate) " +
                         "VALUES (@fname, @mname, @lname, @email, @hadd, @bDate);";

            DB.ExecuteNoReturnQuery(insertUser,
                new MySqlParameter("@fname", tbFname.Text),
                new MySqlParameter("@mname", tbMname.Text),
                new MySqlParameter("@lname", tbLname.Text),
                new MySqlParameter("@email", tbEmail.Text),
                new MySqlParameter("@hadd", tbHome.Text),
                new MySqlParameter("@bDate", dtpBirth.Value)
            );

            string insertLogin = "INSERT INTO tbllogin_credentials (userID, user_username, user_password) " +
                         "VALUES (LAST_INSERT_ID(), @username, @password);";

            int affectedRowCount = DB.ExecuteNoReturnQuery(insertLogin,
                new MySqlParameter("@username", tbUser.Text),
                new MySqlParameter("@password", tbPass.Text)
            );

            if (affectedRowCount > 0)
            {
                MessageBox.Show("Data Inserted!");
                ClearFields();
                Users_Load(null, null);
            }
        }

        private void Users_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

            selectedUserID = Convert.ToInt32(row.Cells[0].Value);
            selectedLoginID = Convert.ToInt32(row.Cells[1].Value);

            tbFname.Text = row.Cells[2].Value?.ToString();
            tbMname.Text = row.Cells[3].Value?.ToString();
            tbLname.Text = row.Cells[4].Value?.ToString();
            tbEmail.Text = row.Cells[5].Value?.ToString();
            tbHome.Text = row.Cells[6].Value?.ToString();
            dtpBirth.Value = Convert.ToDateTime(row.Cells[7].Value);
            tbUser.Text = row.Cells[8].Value?.ToString();
            tbPass.Text = row.Cells[9].Value?.ToString();


            isUpdating = true;
            btnUpdate.Enabled = true;
            btnSave.Enabled = false;
            btnDeactivate.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedUserID == -1)
            {
                MessageBox.Show("Please select a user from the table first.");
                return;
            }

            string query = "UPDATE tbluser_information SET firstname = @fname, middlename = @mname, lastname = @lname, emailAddress = @email, homeAddress = @hadd, birthDate = @bDate WHERE userID = @userID; UPDATE tbllogin_credentials SET user_username = @username, user_password = @password WHERE LoginID = @loginID;";

            int affectedRowCount = DB.ExecuteNoReturnQuery(query,
                new MySqlParameter("@fname", tbFname.Text),
                new MySqlParameter("@mname", tbMname.Text),
                new MySqlParameter("@lname", tbLname.Text),
                new MySqlParameter("@email", tbEmail.Text),
                new MySqlParameter("@hadd", tbHome.Text),
                new MySqlParameter("@bDate", dtpBirth.Value),
                new MySqlParameter("@username", tbUser.Text),
                new MySqlParameter("@password", tbPass.Text),
                new MySqlParameter("@userID", selectedUserID),
                new MySqlParameter("@loginID", selectedLoginID)
            );

            if (affectedRowCount > 0)
            {
                MessageBox.Show("User updated successfully!");
                ClearFields();
                Users_Load(null, null);
            }
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (selectedUserID == -1)
            {
                MessageBox.Show("Please select a user from the table first.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to deactivate this user?",
                "Confirm Deactivate",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes) return;


            string query = "UPDATE tbllogin_credentials SET is_active = 0 WHERE userID = @userID;";

            int affectedRowCount = DB.ExecuteNoReturnQuery(query,
                new MySqlParameter("@userID", selectedUserID)
            );

            if (affectedRowCount > 0)
            {
                MessageBox.Show("User deactivated successfully!");
                ClearFields();
                Users_Load(null, null);
            }
        }

        private void ClearFields()
        {
            selectedUserID = -1;
            selectedLoginID = -1;
            isUpdating = false;         // back to Save mode
            btnSave.Enabled = true;     // re-enable Save
            btnUpdate.Enabled = false;  // disable Update

            tbFname.Text = "";
            tbMname.Text = "";
            tbLname.Text = "";
            tbEmail.Text = "";
            tbHome.Text = "";
            dtpBirth.Value = DateTime.Today;
            tbUser.Text = "";
            tbPass.Text = "";
        }
    }
}

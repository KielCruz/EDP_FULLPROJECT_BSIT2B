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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MYDATABASEcs DB = new MYDATABASEcs();
        private void Form1_Load(object sender, EventArgs e)
        {
            if (DB.TestConnection() == true)
            {
                MessageBox.Show("Connected Successfully");
            }
            else 
            {
                MessageBox.Show("Not Connected");
            }
        }

        string[,] UserCredentials =
        {
         { "Dunkin", "kiel cruz"},
         {"banana", "karl son" }

        };

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbUser.Text == "")
            {
                MessageBox.Show("Please enter username!", "Validation");
                tbUser.Focus();
            }
            else if (tbPass.Text == "")
            {
                MessageBox.Show("Please enter password!", "Validation");
                tbPass.Focus();
            }
            else
            {
                DataTable dt = DB.ExecuteReturnQuery("Select * from tbllogincredentials where user_username = @uname and user_password = @upass",
                    new MySqlParameter("@uname", tbUser.Text),
                    new MySqlParameter("@upass", tbPass.Text));


                if (dt.Rows.Count == 1) {
                    Form2 frm = new Form2();
                    this.Hide();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Username/Password");
                }

            }


        }
    }
}

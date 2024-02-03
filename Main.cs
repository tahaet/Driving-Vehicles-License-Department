using MySolution.GlobalClasses;
using MySolution.Login;
using MySolution.People;
using MySolution.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;

        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;

        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void manageLocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ManageInternationaDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void peopleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void employeesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmChangePassword frm=new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }
    }
}

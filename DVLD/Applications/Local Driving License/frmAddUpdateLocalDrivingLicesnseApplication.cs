﻿using DVLD_Buisness;
using DVLD_Business;
using MySolution.GlobalClasses;
using MySolution.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySolution.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _SelectedPersonID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
            _Mode=enMode.AddNew;
        }
        public frmAddUpdateLocalDrivingLicesnseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }
        private void _FillLicenseClassesInComoboBox()
        {
            DataTable dt =clsLicenseClass.GetAllLicenseClasses();
            foreach(DataRow row in dt.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }
        private void _ResetDefualtValues()
        {
            _FillLicenseClassesInComoboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                ctrlPersonCardWithFilters1.FilterFocus();
                tpApplicationInfo.Enabled = false;
                cbLicenseClass.SelectedIndex = 2;
                lblFees.Text=clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        private void _LoadData()
        {
            ctrlPersonCardWithFilters1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }
            ctrlPersonCardWithFilters1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(_LocalDrivingLicenseApplication.LicenseClassInfo.ClassName);
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        private void ctrlPersonCardWithFilters1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }
        //???
        private void DataBackEvent(object sender, int PersonID)
        {
            _SelectedPersonID = PersonID;
            ctrlPersonCardWithFilters1.LoadPersonInfo(PersonID);
        }

        private void frmAddUpdateApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnApplicationInfoNext_Click(object sender, EventArgs e)
        {

            if(_Mode == enMode.Update)
            {
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }
            if (ctrlPersonCardWithFilters1.PersonID != -1)
            {

                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilters1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;
           
            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);
            
            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            // this checks if he has active license only 
            //if his license is not active we should redirect user to create Renew application
            if (clsLicense.IsLicenseExistByPersonID(ctrlPersonCardWithFilters1.PersonID, LicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilters1.PersonID; 
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;   
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;
            


            if (_LocalDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();

                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void frmAddUpdateApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilters1.FilterFocus();

        }

    }
}

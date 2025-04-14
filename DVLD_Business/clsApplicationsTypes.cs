using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Business;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsApplicationsTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int ID { set; get; }
        public string Title { set; get; }
        public float Fees { set; get; }

        public clsApplicationsTypes()
        {
            this.ID = -1;
            this.Title = "";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }

        private clsApplicationsTypes(int ApplicationID, string ApplicationTitle, float ApplicationFees)
        {
            this.ID = ApplicationID;
            this.Title = ApplicationTitle;
            this.Fees = ApplicationFees;
            Mode = enMode.Update;
        }

        public static clsApplicationsTypes Find(int ID)
        {
            string Title = "";
            float Fees = 0;

            if (clsApplicationsTypeData.GetApplicationInfoByID(ID, ref Title, ref Fees))
            {
                return new clsApplicationsTypes(ID, Title, Fees);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewApplicationType()
        {
            //call DataAccess Layer 

            this.ID = clsApplicationsTypeData.AddNewApplicationType(this.Title, this.Fees);


            return (this.ID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsTypeData.UpdateApplication(this.ID, this.Title, this.Fees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplication();

            }

            return false;
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationsTypeData.GetAllApplications();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsTestTypes
    {
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestTypes.enTestType ID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public double Fees { set; get; }

        public clsTestTypes()
        {
            this.ID = clsTestTypes.enTestType.VisionTest;
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.Fees = 0.0;
        }

        public clsTestTypes(clsTestTypes.enTestType TestID, string TestTitle, string TestDescription, double TestFees)
        {
            this.ID = TestID;
            this.Title = TestTitle;
            this.Description = TestDescription;
            this.Fees = TestFees;
        }

        public static clsTestTypes Find(clsTestTypes.enTestType TestID)
        {
            string TestTitle = "";
            string TestDescription = "";
            double TestFees = 0.0;

            if (clsTestTypeData.GetTestInfoByID((int)TestID, ref TestTitle, ref TestDescription, ref TestFees))
            {
                return new clsTestTypes(TestID, TestTitle, TestDescription, TestFees);
            }
            else
            {
                return null;
            }
        }

        private bool _UpdateTest()
        {
            return clsTestTypeData.UpdateTest((int)this.ID, this.Title, this.Description, this.Fees);
        }

        public bool Save()
        {
            return _UpdateTest();
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }
    }
}

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modern_Auto
{
    public partial class Form_Bill_Customer_Purchasing : Form
    {
        int Bill_ID;
        string custome_name, Bill_Details,car_kilo,car_number,car_shaseh;
        double Bill_Total, Discout, old, payment;
        public Form_Bill_Customer_Purchasing(int bill_ID, string customername, string BillDetails, double billtotal, double discount, double old, double payment,string car_kilo,string car_number,string car_shaseh)
        {
            InitializeComponent();
            this.Bill_ID = bill_ID;
            this.custome_name = customername;
            this.Bill_Details = BillDetails;
            this.Bill_Total = billtotal;
            this.Discout = discount;
            this.old = old;
            this.payment = payment;
            this.car_kilo = car_kilo;
            this.car_number = car_number;
            this.car_shaseh = car_shaseh;
        }

        private void Form_Bill_Customer_Purchasing_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath;
            string directory = Path.GetDirectoryName(path); //without file name
            string oneUp = Path.GetDirectoryName(directory); // Temp folder
            ReportDocument cryRpt = new ReportDocument();
           // cryRpt.Load(Application.StartupPath + @"\Crystal Customer Purchasing.rpt");
            cryRpt.Load(@"C:\Users\3ZT\source\repos\Modern Auto\Modern Auto\CrystalReport1.rpt");


            ParameterFieldDefinitions crParameterFieldDefinitions;
            ParameterFieldDefinition crParameterFieldDefinition;
            ParameterValues crParameterValues = new ParameterValues();
            ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();




            crParameterDiscreteValue.Value = Bill_ID;
            crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["@Bill_ID"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);



            crParameterDiscreteValue.Value = Bill_ID;
            crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["ID"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);



            crParameterDiscreteValue.Value = custome_name;
            crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["Customer_Name"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);




            crParameterDiscreteValue.Value = car_number;
            crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["car_number"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);




            crParameterDiscreteValue.Value = car_kilo;
            crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["car_kilo"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);




            crParameterDiscreteValue.Value = car_shaseh;
            crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["car_shaseh"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);



          
            crystalReportViewer1.ReportSource = cryRpt;
            crystalReportViewer1.Refresh();
        }
    }
}

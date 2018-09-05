using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Vodafone_Essar1.Data_Access_Layer;
using Vodafone_Essar1.ViewModels;
using System.Data.SqlClient;

namespace Vodafone_Essar1.Models
{
    public class EditEssarBusinessLayer
    {
        EmployeeBusinessLayer eb = new EmployeeBusinessLayer();
        SqlConnection con = new SqlConnection("Server=150.0.150.17;Database=Training;User Id=TRAINEE;Password=TRAINEE");



        public Boolean UpdateSapId(string sapid, Employee objTodo)
        {
            List<Essar_Users_Bill> bill = new List<Essar_Users_Bill>();
            DataTable dt = new DataTable();
            using (var ctx = new Training())
            {

                using (DbContextTransaction transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        dt.Columns.Add("SAP_ID", typeof(string));
                        dt.Columns.Add("Mobile_no", typeof(string));
                        dt.Columns.Add("Year", typeof(string));
                        dt.Columns.Add("Months", typeof(string));
                        dt.Columns.Add("Sub_total", typeof(decimal));
                        dt.Columns.Add("GST_18", typeof(decimal));
                        dt.Columns.Add("Limit", typeof(int));
                        dt.Columns.Add("Total", typeof(decimal));
                        dt.Columns.Add("Deduction", typeof(decimal));
                        dt.Columns.Add("status", typeof(string));
                        bill = eb.GetBill(sapid);
                        foreach (Essar_Users_Bill bill1 in bill)
                        {
                            DataRow dr = dt.NewRow();
                            dr["SAP_ID"] = objTodo.SAP_ID;
                            dr["Mobile_no"] = bill1.Mobile_no;
                            dr["Year"] = bill1.Year;
                            dr["Months"] = bill1.Months;
                            dr["Sub_total"] = bill1.Sub_total;
                            dr["GST_18"] = bill1.GST_18;
                            dr["Limit"] = bill1.Limit;
                            dr["Total"] = bill1.Total;
                            dr["Deduction"] = bill1.Deduction;
                            dt.Rows.Add(dr);
                        }

                        int noOfRowUpdated1 = ctx.Database.ExecuteSqlCommand("Delete from Essar_users_Bill where SAP_ID='" + sapid + "' and Mobile_no='" + objTodo.Mobile_no + "';");
                        noOfRowUpdated1 = ctx.Database.ExecuteSqlCommand("Update Essar_users1 set SAP_ID='" + objTodo.SAP_ID + "' where SAP_ID='" + sapid + "' and Mobile_no='" + objTodo.Mobile_no + "';");
                        transaction.Commit();
                        using (SqlBulkCopy objbulk = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.CheckConstraints))
                        {

                            objbulk.DestinationTableName = "Essar_users_Bill";

                            objbulk.ColumnMappings.Add("SAP_ID", "SAP_ID");

                            objbulk.ColumnMappings.Add("Mobile_no", "Mobile_no");

                            objbulk.ColumnMappings.Add("Year", "Year");

                            objbulk.ColumnMappings.Add("Months", "Months");

                            objbulk.ColumnMappings.Add("Sub_total", "Sub_total");

                            objbulk.ColumnMappings.Add("GST_18", "GST_18");

                            objbulk.ColumnMappings.Add("Limit", "Limit");

                            objbulk.ColumnMappings.Add("Total", "Total");

                            objbulk.ColumnMappings.Add("Deduction", "Deduction");

                       


                            con.Open();

                            objbulk.WriteToServer(dt);
                        }
                        

                         con.Close();
                         return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }

}
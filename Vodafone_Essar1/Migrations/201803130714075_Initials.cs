namespace Vodafone_Essar1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initials : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Essar_users_Billing_Sheet",
                c => new
                    {
                        Billed_To = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Billed_To);
            
            CreateTable(
                "dbo.Essar_users_Email_Temp",
                c => new
                    {
                        Email_code = c.String(nullable: false, maxLength: 128),
                        Email_Sub = c.String(unicode: false, storeType: "text"),
                        Email_Body = c.String(unicode: false, storeType: "text"),
                        Email_To = c.String(),
                        Email_Cc = c.String(),
                        Email_Bcc = c.String(),
                    })
                .PrimaryKey(t => t.Email_code);
            
            CreateTable(
                "dbo.Essar_users1",
                c => new
                    {
                        SAP_ID = c.String(nullable: false, maxLength: 128),
                        Mobile_no = c.String(nullable: false, maxLength: 128),
                        Sr_no = c.Int(nullable: false, identity: true),
                        Employees_Name = c.String(),
                        Levels = c.String(),
                        Sim_no = c.String(),
                        email_id = c.String(),
                        Limit = c.Int(nullable: false),
                        Billed_To = c.String(),
                        Department = c.String(),
                        Scheme = c.String(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SAP_ID, t.Mobile_no });
            
            CreateTable(
                "dbo.Essar_Users_Bill",
                c => new
                    {
                        SAP_ID = c.String(nullable: false, maxLength: 128),
                        Mobile_no = c.String(nullable: false, maxLength: 128),
                        Sr_no = c.Int(nullable: false, identity: true),
                        Year = c.String(),
                        Months = c.String(),
                        Sub_total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GST_18 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Limit = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deduction = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Sr_no)
                .ForeignKey("dbo.Essar_users1", t => new { t.SAP_ID, t.Mobile_no }, cascadeDelete: true)
                .Index(t => new { t.SAP_ID, t.Mobile_no });
            
            CreateTable(
                "dbo.Essar_users_Mobile_no",
                c => new
                    {
                        Mobile_no = c.String(nullable: false, maxLength: 128),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Mobile_no);
            
            CreateTable(
                "dbo.Essar_users_Plans",
                c => new
                    {
                        Level = c.String(nullable: false, maxLength: 128),
                        Plan = c.String(),
                        Limit = c.Int(nullable: false),
                        Descrp = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.Level);
            
            CreateTable(
                "dbo.Essar_users_Logs",
                c => new
                    {
                        Sr_no = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Employees_Name = c.String(),
                        SAP_ID = c.String(),
                        Levels = c.String(),
                        Mobile_no = c.String(),
                        Sim_no = c.String(),
                        Type = c.String(),
                        Reason = c.String(),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.Sr_no);
            
            CreateTable(
                "dbo.Essar_users_Sim_Numbers",
                c => new
                    {
                        Sim_no = c.String(nullable: false, maxLength: 128),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Sim_no);
            
            CreateTable(
                "dbo.Essar_User_credentials",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Essar_Users_Bill", new[] { "SAP_ID", "Mobile_no" }, "dbo.Essar_users1");
            DropIndex("dbo.Essar_Users_Bill", new[] { "SAP_ID", "Mobile_no" });
            DropTable("dbo.Essar_User_credentials");
            DropTable("dbo.Essar_users_Sim_Numbers");
            DropTable("dbo.Essar_users_Logs");
            DropTable("dbo.Essar_users_Plans");
            DropTable("dbo.Essar_users_Mobile_no");
            DropTable("dbo.Essar_Users_Bill");
            DropTable("dbo.Essar_users1");
            DropTable("dbo.Essar_users_Email_Temp");
            DropTable("dbo.Essar_users_Billing_Sheet");
        }
    }
}

namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Sport = c.String(),
                        Achievements = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StudentInfoes");
        }
    }
}

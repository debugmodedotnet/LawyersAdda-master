namespace LawyersAdda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LawyerImages", "LawyerId", "dbo.Lawyers");
            DropIndex("dbo.LawyerImages", new[] { "LawyerId" });
            AlterColumn("dbo.LawyerImages", "LawyerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.LawyerImages", "LawyerId");
            AddForeignKey("dbo.LawyerImages", "LawyerId", "dbo.Lawyers", "Id");
            DropColumn("dbo.Lawyers", "City");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lawyers", "City", c => c.String());
            DropForeignKey("dbo.LawyerImages", "LawyerId", "dbo.Lawyers");
            DropIndex("dbo.LawyerImages", new[] { "LawyerId" });
            AlterColumn("dbo.LawyerImages", "LawyerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.LawyerImages", "LawyerId");
            AddForeignKey("dbo.LawyerImages", "LawyerId", "dbo.Lawyers", "Id", cascadeDelete: true);
        }
    }
}

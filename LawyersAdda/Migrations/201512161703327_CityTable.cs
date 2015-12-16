namespace LawyersAdda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 100),
                        CityName = c.String(nullable: false),
                        isVerified = c.Boolean(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        isPriority = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Courts", "CityId", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Lawyers", "CityId", c => c.String());
            AddColumn("dbo.Lawyers", "City", c => c.String());
            CreateIndex("dbo.Courts", "CityId");
            AddForeignKey("dbo.Courts", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
            DropColumn("dbo.Courts", "City");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courts", "City", c => c.String(nullable: false));
            DropForeignKey("dbo.Courts", "CityId", "dbo.Cities");
            DropIndex("dbo.Courts", new[] { "CityId" });
            DropColumn("dbo.Lawyers", "City");
            DropColumn("dbo.Lawyers", "CityId");
            DropColumn("dbo.Courts", "CityId");
            DropTable("dbo.Cities");
        }
    }
}

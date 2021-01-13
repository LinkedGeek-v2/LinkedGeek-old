namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCompanyLocationFromExperienceModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Experience", "CompanyLocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Experience", "CompanyLocation", c => c.String());
        }
    }
}

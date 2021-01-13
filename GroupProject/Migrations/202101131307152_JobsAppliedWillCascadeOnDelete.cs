namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobsAppliedWillCascadeOnDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JobsApplied", "JobID", "dbo.Job");
            AddForeignKey("dbo.JobsApplied", "JobID", "dbo.Job", "JobID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobsApplied", "JobID", "dbo.Job");
            AddForeignKey("dbo.JobsApplied", "JobID", "dbo.Job", "JobID");
        }
    }
}

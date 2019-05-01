namespace PokemonIndex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchCsv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gyms", "LeaderName", c => c.String());
            DropColumn("dbo.Gyms", "LeaderID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gyms", "LeaderID", c => c.Int(nullable: false));
            DropColumn("dbo.Gyms", "LeaderName");
        }
    }
}

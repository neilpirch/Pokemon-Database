namespace PokemonIndex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PokemonTypes", "PokemonId", "dbo.Pokemons");
            DropForeignKey("dbo.TrainerPokemons", "PokemonId", "dbo.Pokemons");
            DropForeignKey("dbo.Trainers", "GymLocation", "dbo.Gyms");
            DropForeignKey("dbo.TrainerPokemons", "TrainerId", "dbo.Trainers");
            DropIndex("dbo.PokemonTypes", new[] { "PokemonId" });
            DropIndex("dbo.TrainerPokemons", new[] { "TrainerId" });
            DropIndex("dbo.TrainerPokemons", new[] { "PokemonId" });
            DropIndex("dbo.Trainers", new[] { "GymLocation" });
            AlterColumn("dbo.Trainers", "GymLocation", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainers", "GymLocation", c => c.String(maxLength: 128));
            CreateIndex("dbo.Trainers", "GymLocation");
            CreateIndex("dbo.TrainerPokemons", "PokemonId");
            CreateIndex("dbo.TrainerPokemons", "TrainerId");
            CreateIndex("dbo.PokemonTypes", "PokemonId");
            AddForeignKey("dbo.TrainerPokemons", "TrainerId", "dbo.Trainers", "TrainerID", cascadeDelete: true);
            AddForeignKey("dbo.Trainers", "GymLocation", "dbo.Gyms", "GymLocation");
            AddForeignKey("dbo.TrainerPokemons", "PokemonId", "dbo.Pokemons", "PokemonId", cascadeDelete: true);
            AddForeignKey("dbo.PokemonTypes", "PokemonId", "dbo.Pokemons", "PokemonId", cascadeDelete: true);
        }
    }
}

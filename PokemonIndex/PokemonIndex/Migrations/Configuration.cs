namespace PokemonIndex.Migrations
{
    using CsvHelper;
    using PokemonIndex.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<PokemonIndex.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PokemonIndex.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "PokemonIndex.Domain.SeedData.evolutions.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    var evolutions = csvReader.GetRecords<Evolution>().ToArray();
                    context.Evolutions.AddOrUpdate(c => c.Id, evolutions);
                }
            }
            resourceName = "PokemonIndex.Domain.SeedData.gyms.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    var gyms = csvReader.GetRecords<Gym>().ToArray();
                    context.Gyms.AddOrUpdate(c => c.GymLocation, gyms);
                }
            }
            resourceName = "PokemonIndex.Domain.SeedData.pokemons.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    var pokemons = csvReader.GetRecords<Pokemon>().ToArray();
                    context.Pokemons.AddOrUpdate(c => c.PokemonId, pokemons);
                }
            }
            resourceName = "PokemonIndex.Domain.SeedData.pokemontypes.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    var pokemontypes = csvReader.GetRecords<PokemonType>().ToArray();
                    context.PokemonTypes.AddOrUpdate(c => c.Id, pokemontypes);
                }
            }
            resourceName = "PokemonIndex.Domain.SeedData.trainers.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    var trainers = csvReader.GetRecords<Trainer>().ToArray();
                    context.Trainers.AddOrUpdate(c => c.TrainerID, trainers);
                }
            }
            resourceName = "PokemonIndex.Domain.SeedData.trainerpokemons.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    var trainerpokemons = csvReader.GetRecords<TrainerPokemon>().ToArray();
                    context.TrainerPokemons.AddOrUpdate(c => c.Id, trainerpokemons);
                }
            }

            //resourceName = "PokemonIndex.Domain.SeedData.gyms.csv";
            //using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            //{
            //    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            //    {
            //        CsvReader csvReader = new CsvReader(reader);
            //        csvReader.Configuration.WillThrowOnMissingField = false;
            //        while (csvReader.Read())
            //        {
            //            var provinceState = csvReader.GetRecord<ProvinceState>();
            //            var countryCode = csvReader.GetField<string>("CountryCode");
            //            provinceState.Country = context.Countries.Local.Single(c => c.Code == countryCode);
            //            context.ProvinceStates.AddOrUpdate(p => p.Code, provinceState);
            //        }
            //    }
            //}
        }
    }
}

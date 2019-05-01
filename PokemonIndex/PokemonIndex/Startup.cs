using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PokemonIndex.Startup))]
namespace PokemonIndex
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

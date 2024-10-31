using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Core.Interfaces;

namespace MvcMovie.Core.ServiceHelpers
{
    /// <summary>
    /// Permet d'accéder à la configuration pour un type de DbContext donné. Utiliser AddRepositoryDefaultContextOptions pour ajouter une nouvelle configuration.
    /// Wooo une collection en static
    /// </summary>
    //internal static class DbContextConfiguration {
    //    internal static Dictionary<Type, DbContextOptionsBuilder > DefaultOptionsBuilder = new Dictionary<Type, DbContextOptionsBuilder>();
    //}

    public static class ServicesServiceCollectionExtensions
    {
        public static void AddIServices(this IServiceCollection services)
        {
            var test = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes());
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IService).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

            ////On supprime les types qui ont déjà été mis dans la DI
            var alreadyIn = services.Where(z => types.Contains(z.ServiceType));
            alreadyIn.ToList().ForEach(z => services.Remove(z));

            //On ajoute des services
            types.ToList().ForEach(z => services.AddScoped(z, z));

        }
    }
}
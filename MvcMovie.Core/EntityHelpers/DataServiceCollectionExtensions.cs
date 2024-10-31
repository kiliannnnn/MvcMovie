using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MvcMovie.Core.Helpers
{
    public static class DataServiceCollectionExtensions
    {
         /// <summary>
        /// Registre un nouveau DbContext à l'inversion de dépendance. S'utilise comme le services.AddDbContext de aspnet Core. Détermine les options du dbContext utilisé par les Repository si ce DbContext n'est pas fourni par inversion de dépendance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        public static void AddMvcMovieDbContext<T>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction) where T : DbContext
        {
            services.AddOptions();
            services.AddDbContext<T>(optionsAction);

            services.Configure<ContextProviderOptions<T>>(options => options.SetContextOptions(optionsAction));

            if (!services.Any(z => z.ServiceType == typeof(IContextProvider)))
                services.AddTransient<IContextProvider, ContextProvider>();

        }
    }
}
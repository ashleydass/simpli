namespace Simpli.SEO.Web.Services
{
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Models;

	public static class Configuration
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			return services
				.Configure<AppOptions>(configuration)
				.AddTransient<ISearchService, SearchService>()
				.AddTransient<ISearchExecutor, SearchExecutor>();
		}
	}
}

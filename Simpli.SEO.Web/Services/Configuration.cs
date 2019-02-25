namespace Simpli.SEO.Web.Services
{
	using Microsoft.Extensions.DependencyInjection;

	public static class Configuration
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			return services
				.AddTransient<ISearchServiceFactory, SearchServiceFactory>()
				.AddTransient<ISearchEngineService, GoogleSearchEngineService>()
				.AddTransient<ISearchEngineService, BingSearchEngineService>();
		}
	}
}

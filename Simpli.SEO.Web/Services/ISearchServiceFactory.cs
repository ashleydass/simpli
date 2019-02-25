namespace Simpli.SEO.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Models;

	public interface ISearchServiceFactory
	{
		ISearchEngineService GetSearchService(SearchEngineEnum searchEngine);
	}

	public class SearchServiceFactory : ISearchServiceFactory
	{
		private readonly IEnumerable<ISearchEngineService> _searchServices;

		public SearchServiceFactory(IEnumerable<ISearchEngineService> searchServices)
		{
			_searchServices = searchServices;
		}

		public ISearchEngineService GetSearchService(SearchEngineEnum searchEngine) =>
			_searchServices.Single(s => s.Uses(searchEngine));
	}
}
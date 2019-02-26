namespace Simpli.SEO.Web.Models
{
	using System.ComponentModel.DataAnnotations;

	public class SearchRequestModel
	{
		[Required]
		public string SearchSource { get; set; }

		[Required]
		public string Query { get; set; }

		[Required]
		public string UrlPartMatch { get; set; }
	}
}
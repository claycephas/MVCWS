
namespace MLAPI.Example.Search.Models
{
	/// <summary>
	/// Represents a paged list of articles based on search criteria.
	/// </summary>
	public class ArticleList
	{
	// Properties
		/// <summary>
		/// The total number of articles found based on the (non-paging) search criteria.
		/// </summary>
		public int TotalFound;

		/// <summary>
		/// The list of articles.
		/// </summary>
		public Article[] Articles;
	}
}

namespace MLAPI.Example.Search.Models
{
	/// <summary>
	/// Represents a doctor or dentist.
	/// </summary>
	public class Article
	{
	// Properties
		/// <summary>
		/// The full article text formatted using HTML.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// The tagline.
		/// </summary>
		public string Headline { get; set; }

		/// <summary>
		/// The unique identifier.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// The topics associated to this article.
		/// </summary>
		public Topic[] Topics { get; set; }
	}
}
using System.Linq;

namespace System.Xml.Linq
{
	/// <summary>
	/// Provides extension methods to the <see cref="T:XContainer"/> class.
	/// </summary>
	public static class XContainerExtensions
	{
		/// <summary>
		/// Returns the full string contents of the container including text nodes and element nodes.
		/// </summary>
		/// <param name="container">An xml container.</param>
		/// <returns>The full string contents of the container including text nodes and element nodes.</returns>
		public static string InnerText(this XContainer container)
		{
			return container.Nodes().Aggregate(String.Empty, (x, node) => x += node.ToString());
		}
	}
}

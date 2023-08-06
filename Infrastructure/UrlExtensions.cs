
namespace SportsStore.Infrastructure
{
	public static class UrlExtensions
	{
		public static string PathAndQuery(this HttpRequest request) =>
		request.QueryString.HasValue ? $"{request.QueryString}" : request.Path.ToString();
	}
}

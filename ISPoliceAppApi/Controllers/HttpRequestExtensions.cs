using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ISPoliceAppApi.Controllers
{
  public static class HttpRequestExtensions
  {
    public static Dictionary<string, string> ToDictionary(this IQueryCollection query)
    {
      return query.Keys.ToDictionary(k => k.ToLower(), v => (string)query[v]);
    }
  }
}

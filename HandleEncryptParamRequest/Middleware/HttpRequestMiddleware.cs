using HandleEncryptParamRequest.Encrypt;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandleEncryptParamRequest.Middleware
{
    public class HttpRequestMiddleware
    {
        private readonly RequestDelegate _next;
        public HttpRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //var api = new ApiRequestInputViewModel
            //{
            //    HttpType = httpContext.Request.Method,
            //    Query = httpContext.Request.QueryString.Value,
            //    RequestUrl = httpContext.Request.Path,
            //    RequestName = "",
            //    RequestIP = httpContext.Request.Host.Value
            //};

            string stringQuery = httpContext.Request.QueryString.Value;
            int indexOfda = stringQuery.IndexOf("=");
            int lends = stringQuery.Length;
            string actualParam = stringQuery.Substring(indexOfda + 1);
            if (actualParam != "")
            {
                var actualQuery = AESEncryption.DecryptFromHexa(actualParam);
                var requestData = new StringBuilder(actualQuery);
                httpContext.Request.QueryString = new QueryString(requestData.ToString());

            }



            //var queryitems = httpContext.Request.Query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();
            //List<KeyValuePair<string, string>> queryparameters = new List<KeyValuePair<string, string>>();
            //foreach (var item in queryitems)
            //{
            //    var value = item.Value.ToString().Replace("x", "y");
            //    KeyValuePair<string, string> newqueryparameter = new KeyValuePair<string, string>(item.Key, value);
            //    queryparameters.Add(newqueryparameter);
            //}

            //var contentType = httpContext.Request.ContentType;

            //if (contentType != null && contentType.Contains("multipart/form-data"))
            //{
            //    var formitems = httpContext.Request.Form.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();

            //    Dictionary<string, StringValues> formparameters = new Dictionary<string, StringValues>();
            //    foreach (var item in formitems)
            //    {
            //        var value = item.Value.ToString().Replace("x", "y");
            //        formparameters.Add(item.Key, value);
            //    };

            //    var qb1 = new QueryBuilder(queryparameters);
            //    var qb2 = new FormCollection(formparameters);
            //    httpContext.Request.QueryString = qb1.ToQueryString();
            //    httpContext.Request.Form = qb2;

            //    var items2 = httpContext.Request.Query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();
            //    var items3 = httpContext.Request.Form.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();
            //}

            await _next(httpContext);

        }
    }
}

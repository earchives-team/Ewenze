using Microsoft.AspNetCore.Mvc;

namespace Ewenze.API.Helpers
{
    public class CustomProblemDetails : ProblemDetails
    {
        public IDictionary<string, string[]> ErrorDetails { get; set; } = new Dictionary<string, string[]>();
    }
}

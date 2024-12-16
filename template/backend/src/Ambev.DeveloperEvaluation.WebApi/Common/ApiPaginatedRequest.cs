using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Reflection.Context;
using System.Reflection.Emit;

namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    public class ApiPaginatedRequest
    {

        [FromQuery(Name = "_page")]
        public int? Page { get; set; } = 1;

        [FromQuery(Name = "_size")]
        public int? Size { get; set; } = 10;

        [FromQuery(Name = "_order")]
        public string? Order { get; set; }
    }
}

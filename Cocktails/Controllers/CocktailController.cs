using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Cocktails.Convertors;
using Cocktails.Services.Models;
using Cocktails.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Cocktail = Cocktails.Contracts.Cocktail;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Cocktails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class CocktailController : ControllerBase
    {
        private readonly ICocktailService _cocktailService;
        private readonly ILogService _logService;

        public CocktailController(ICocktailService cocktailService, ILogService logService)
        {
            _cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        [HttpGet]
        public IEnumerable<Cocktail> GetCocktailsBySearchQuery([FromQuery] string searchKeyword, [FromQuery] int count)
        {
            AddLogInData(nameof(GetCocktailsBySearchQuery), JsonSerializer.Serialize(new
            {
                SearchKeyword = searchKeyword,
                ItemsCount = count.ToString(),
            }));

            if (string.IsNullOrWhiteSpace(searchKeyword))
            {
                throw new ValidationException("Please provide a valid search query.");
            }

            if (count <= 0)
            {
                throw new ValidationException("Please provide a count of cocktails to be returned.");
            }

            var cocktails = _cocktailService.GetCocktails(searchKeyword, count).ToContract();
            AddLog(JsonConvert.SerializeObject(cocktails));
            return cocktails;
        }

        private void AddLog(string outData)
        {
            HttpContext.Items.TryGetValue(Constants.InDataKey, out var indData);
            HttpContext.Items.TryGetValue(Constants.MethodNameKey, out var methodName);
            HttpContext.Items.TryGetValue(Constants.RequestTimeKey, out var requestDate);

            _logService.AddLog(new CocktailLog
            {
                OutData = outData,
                ReplyDateTime = DateTime.UtcNow,
                InData = indData?.ToString() ?? string.Empty,
                MethodName = methodName?.ToString() ?? string.Empty,
                RequestDateTime = DateTime.Parse(requestDate?.ToString() ?? DateTime.UtcNow.ToString(CultureInfo.InvariantCulture))
            });
        }

        private void AddLogInData(string methodName, string inData)
        {
            HttpContext.Items.TryAdd(Constants.RequestTimeKey, DateTime.UtcNow);
            HttpContext.Items.TryAdd(Constants.MethodNameKey, methodName);
            HttpContext.Items.TryAdd(Constants.InDataKey, inData);
        }
    }
}

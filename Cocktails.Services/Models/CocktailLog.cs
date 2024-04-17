namespace Cocktails.Services.Models
{
    public class CocktailLog
    {
        public string MethodName { get; set; }

        public DateTime RequestDateTime { get; set; }

        public string InData { get; set; }
        public DateTime ReplyDateTime { get; set; }

        public string OutData { get; set; }

        public string ErrorMessage { get; set; }
    }
}
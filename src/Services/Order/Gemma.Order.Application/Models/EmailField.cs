namespace Gemma.Order.Application.Models
{
    public class EmailField
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public EmailField(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}

using System.Collections;
using System.Text.Json;
using OnlineStore.Models.Domain;

namespace OnlineStore.Services.Implementations
{
    public class JsonDBService : IDatabaseService
    {
        private string _filePath = "reviews.json";

        public void AddItems(IEnumerable items)
        {
            string itemsStr = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, itemsStr);
        }

        public IEnumerable GetItems(int? amount)
        {
            if (File.Exists(_filePath))
            {
                string itemsStr = File.ReadAllText(_filePath);
                List<Review> items = JsonSerializer.Deserialize<List<Review>>(itemsStr);
                if (items != null)
                {
                    return (amount == null || items.Count <= amount) ? items : items.GetRange(0, (int)amount);
                }
            }
            return new List<Review>();
        }
    }
}

using System.Collections;

namespace OnlineStore.Services
{
    public interface IDatabaseService
    {
        public IEnumerable GetItems(int? amount);
        public void AddItems(IEnumerable items);
    }
}

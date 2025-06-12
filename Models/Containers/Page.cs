namespace OnlineStore.Models.Containers
{
    public class Page<T>
    {
        public required int CurPage {  get; set; }
        public required int MaxPage { get; set; }
        public required int ItemAmount { get; set; }
        public required List<T> Items { get; set; }
    }
}

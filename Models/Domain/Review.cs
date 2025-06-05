namespace OnlineStore.Models.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public required int ProductID { get; set; }
        public DateTime Date { get; set; }
        public required string Username { get; set; }
        public required string Text { get; set; }

        private byte _rating;
        public required byte Rating
        {
            get => _rating;
            set
            {
                if (value < 1 || value > 5) throw new ArgumentOutOfRangeException("Rating must be between 1 and 5");
                _rating = value;
            }
        }
    }
}

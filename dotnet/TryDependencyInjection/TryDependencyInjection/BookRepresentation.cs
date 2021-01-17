namespace TryDependencyInjection
{
    class BookRepresentation
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Available { get; set; } = string.Empty;
        public int Price { get; set; }
        public string ExpensiveLevel { get; set; } = string.Empty;
    }
}

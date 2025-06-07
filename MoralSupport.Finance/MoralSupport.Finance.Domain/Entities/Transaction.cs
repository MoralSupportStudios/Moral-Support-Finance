namespace MoralSupport.Finance.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; }
        public int? PayeeId { get; set; }
        public Payee? Payee { get; set; }
        public int? PropertyId { get; set; }
        public Property? Property { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string Description { get; set; } = string.Empty;
        public bool IsExpense { get; set; }
    }
}

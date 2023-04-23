namespace JointExpenses
{
    public class Bill
    {
        public decimal Cost { get; set; }
        public bool IsBoth { get; set; }

        public Bill(decimal Cost , bool IsBoth) {
            this.Cost = Cost;
            this.IsBoth = IsBoth;
        }
    }
}

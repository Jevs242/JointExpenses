namespace JointExpenses
{

    public class BillList
    {
        public List<Bill> Bill { get; set; }
        public BillList() {
            Bill = new List<Bill>();
        }
    }
}

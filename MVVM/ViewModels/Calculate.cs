namespace JointExpenses
{
    internal class Calculate
    {
        private decimal percentTax = 0;
        private decimal totalTax = 0;
        private int totalPerson = 2;
        private decimal tip = 0;

        public decimal PercentTax
        {
            get { return percentTax;}
            set
            {
                if(percentTax != value)
                {
                    percentTax = value;
                }
            }
        }

        public decimal TotalTax
        {
            get { return totalTax;}
            set
            {
                if(totalTax != value)
                {
                    totalTax = value;
                }
            }
        }
        public decimal Tip
        {
            get { return tip; }
            set
            {
                if (tip != value)
                {
                    tip = value;
                }
            }
        }

        public int TotalPerson
        {
            get { return totalPerson;}
            set
            {
                if (totalPerson != value)
                {
                    totalPerson = value;
                }
            }
        }
    }
}

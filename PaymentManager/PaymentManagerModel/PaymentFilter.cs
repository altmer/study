namespace PaymentManagerModel
{
	public class PaymentFilter
	{
		private readonly Payment from = new Payment();
		private readonly Payment to = new Payment();
		public decimal SumFrom = decimal.MinValue;
		public decimal SumTo = decimal.MinValue;

		public Payment From{
			get{
				return from;
			}
		}

		public Payment To{
			get{
				return to;
			}
		}
	}
}

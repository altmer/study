using System;

namespace PaymentManagerModel
{
	[Serializable]
	public class Payment
	{
		private readonly Guid id;
		private DateTime paymentDate = DateTime.MinValue;
		private DateTime monthPaid = DateTime.MinValue;
		private Decimal amount = -1;
		private Decimal rate = -1;
		private Guid customerID = Guid.Empty;

		public Payment(Guid newID) {
			id = newID;
		}

		public Payment(){
			id = Guid.NewGuid();
		}

		public DateTime PaymentDate{
			get{
				return paymentDate;
			}

			set{
				paymentDate = value;
			}
		}

		public DateTime MonthPaid{
			get{
				return monthPaid;
			}
			set{
				monthPaid = value;
			}
		}

		public Decimal Amount{
			get{
				return amount;
			}
			set{
				amount = value;
			}
		}

		public Decimal Rate{
			get{
				return rate;
			}
			set{
				rate = value;
			}
		}

		public Guid ID{
			get{
				return id;
			}
		}

		public Guid CustomerID{
			get{
				return customerID;
			}
			set{
				customerID = value;
			}
		}

		public override bool Equals(object obj) {
			if ( ReferenceEquals ( null, obj ) ) return false;
			if ( ReferenceEquals ( this, obj ) ) return true;
			if ( obj.GetType() != typeof (Payment) ) return false;
			return Equals ( (Payment) obj );
		}

		public bool Equals (Payment obj){
			if ( ReferenceEquals ( null, obj ) ) return false;
			if ( ReferenceEquals ( this, obj ) ) return true;
			return obj.id.Equals ( id ) && obj.paymentDate.Equals ( paymentDate ) && obj.monthPaid.Equals ( monthPaid ) 
				&& obj.amount == amount && obj.rate == rate && obj.customerID.Equals ( customerID );
		}

		public override int GetHashCode(){
			unchecked{
				int result = id.GetHashCode();
				result = ( result * 397 ) ^ paymentDate.GetHashCode();
				result = ( result * 397 ) ^ monthPaid.GetHashCode();
				result = ( result * 397 ) ^ amount.GetHashCode();
				result = ( result * 397 ) ^ rate.GetHashCode();
				result = ( result * 397 ) ^ customerID.GetHashCode();
				return result;
			}
		}
	}
}

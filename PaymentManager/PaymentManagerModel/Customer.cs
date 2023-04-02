using System;

namespace PaymentManagerModel
{
	[Serializable]
	public class Customer : IComparable<Customer>
	{
		private readonly Guid id;

		private string lastName;
		private string firstName;
		private string middleName;
		private string number;

		public Customer(){
			id = Guid.NewGuid();
		}

		public Customer(Guid newID){
			id = newID;
		}

		public string LastName{
			get{
				return lastName;
			}
			set{
				lastName = value;
			}
		}

		public string FirstName{
			get{
				return firstName;
			}
			set{
				firstName = value;
			}
		}

		public string MiddleName{
			get{
				return middleName;
			}
			set{
				middleName = value;
			}
		}

		public string Number{
			get{
				return number;
			}
			set{
				number = value;
			}
		}

		public Guid ID{
			get{
				return id;
			}
		}

		public override bool Equals(object obj){
			if ( ReferenceEquals ( null, obj ) ) return false;
			if ( ReferenceEquals ( this, obj ) ) return true;
			if ( obj.GetType() != typeof (Customer) ) return false;
			return Equals ( (Customer) obj );
		}

		public bool Equals (Customer obj){
			if ( ReferenceEquals ( null, obj ) ) return false;
			if ( ReferenceEquals ( this, obj ) ) return true;
			return Equals ( obj.lastName, lastName ) && Equals ( obj.firstName, firstName ) 
				&& Equals ( obj.middleName, middleName ) && obj.id.Equals ( id ) && Equals ( obj.number, number );
		}

		public override int GetHashCode(){
			unchecked{
				int result = ( lastName != null ? lastName.GetHashCode() : 0 );
				result = ( result * 397 ) ^ ( firstName != null ? firstName.GetHashCode() : 0 );
				result = ( result * 397 ) ^ ( middleName != null ? middleName.GetHashCode() : 0 );
				result = ( result * 397 ) ^ id.GetHashCode();
				result = ( result * 397 ) ^ ( number != null ? number.GetHashCode() : 0 );
				return result;
			}
		}

		public int CompareTo(Customer other){
			return ToString().CompareTo ( other.ToString() );
		}

		public override string ToString() {
			return lastName + " " + firstName + " " + middleName;
		}
	}
}

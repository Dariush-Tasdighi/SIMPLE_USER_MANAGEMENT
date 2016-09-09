namespace Models
{
	public abstract class BaseEntity : System.Object
	{
		public BaseEntity()
			: base()
		{
			Id = System.Guid.NewGuid();
		}

		// **********
		public System.Guid Id { get; set; }
		// **********
	}
}

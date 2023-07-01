namespace Utilities;

public class Optional
{
	//additional task for 5
	public class Maybe<T>
	{
		private T value;

		public Maybe(T value)
		{
			this.value = value;
		}

		public T Value
		{
			get
			{
				if (!HasValue)
				{
					throw new InvalidOperationException("null exception");
				}
				return value;
			}
		}

		public bool HasValue => this.value == null ? false : true;

		public override string ToString()
		{
			return HasValue ? Value.ToString() : "";
		}
	}
}


using System;

namespace UniUnitTest
{
	
	public enum AssertErrorTypeEnum
	{
		Failed = 0,
		Error
	}
	
	/*
	 * Assert Exception class.
	 * This class will support serialization.
	 */
	[Serializable]
	public class AssertException : Exception
	{
		public AssertErrorTypeEnum ErrCode;
		public AssertException(AssertErrorTypeEnum in_errCode) : base()
		{
			ErrCode = in_errCode;
		}
		
		public AssertException(AssertErrorTypeEnum in_errCode, string message) : base(message)
		{
			ErrCode = in_errCode;
		}
		
		public AssertException(AssertErrorTypeEnum in_errCode, string format, params object[] args) : base(string.Format(format, args))
		{
			ErrCode = in_errCode;
		}
		
		public AssertException(AssertErrorTypeEnum in_errCode, string message, Exception innerException) : base(message, innerException)
		{
			ErrCode = in_errCode;
		}
		
		public AssertException(AssertErrorTypeEnum in_errCode, Exception innerException, string format, params object[] args) : base(string.Format(format, args), innerException)
		{
			ErrCode = in_errCode;
		}
	}
}


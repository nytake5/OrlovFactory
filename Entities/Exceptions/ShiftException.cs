namespace Entities.Exceptions
{
    public class ShiftException : Exception
    {
        public ShiftException() : base() { }
        public ShiftException(string message) : base(message) { }
        public ShiftException(string message, Exception innerException) : base(message, innerException) { }
    }
}

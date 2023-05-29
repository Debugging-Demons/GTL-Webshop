using Webshop.Order.Domain.Common;

namespace Webshop.Order.Domain.ValueObjects
{
    public sealed class Error : ValueObject
    {
        public string Code { get; }
        public string Message { get; }
        public int StatusCode { get; }

        internal Error(string code, string message, int statusCode = 400)
        {
            Code = code;
            Message = message;
            StatusCode = statusCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Code);
        }
    }
}
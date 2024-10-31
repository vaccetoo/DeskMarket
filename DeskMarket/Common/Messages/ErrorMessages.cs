using static DeskMarket.Common.Constants.ValidationConstants;

namespace DeskMarket.Common.Messages
{
    public static class ErrorMessages
    {
        public const string StringLengthErrorMessage = "The field {0} mus be between {2} and {1} symbols!";
        public const string RequiredErrorMessage = "The field {0} is required";
        public const string RangeErrorMessage = "The value must be between {1} and {2}";
        public const string InvalidDateFormatErrorMessage = $"The date format must be {DateFormat}";
    }
}

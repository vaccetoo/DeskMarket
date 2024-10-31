namespace DeskMarket.Common.Constants
{
    public static class ValidationConstants
    {
        // Product validation constants
        public const int ProductNameMinLength = 2;
        public const int ProductNameMaxLength = 60;

        public const int ProductDescriptionMinLength = 10;
        public const int ProductDescriptionMaxLength = 250;

        public const decimal ProductMinPrice = 1.00m;
        public const decimal ProductMaxPrice = 3000.00m;

        // Category validation constants
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 20;

        // Common validation constants
        public const string DateFormat = "dd-MM-yyyy";
        public const int PricePrecision = 18;
        public const int PriceScale = 2;
    }
}

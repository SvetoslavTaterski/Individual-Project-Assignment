namespace GepardOOD.Common
{
    public static class EntityValidationConstants
    {
        public static class BeerCategory
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 3;
        }

        public static class Beer
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 5;

            public const int DescriptionMaxLength = 200;
            public const int DescriptionMinLength = 20;

            public const int ManufacturerMaxLength = 50; 
            public const int ManufacturerMinLength = 5;

            public const int ImageUrlMaxLength = 2048;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "20";
        }

        public static class Associate
        {
            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }

        public static class Soda
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 5;

            public const int DescriptionMaxLength = 200;
            public const int DescriptionMinLength = 20;

            public const int ManufacturerMaxLength = 50;
            public const int ManufacturerMinLength = 5;

            public const int ImageUrlMaxLength = 2048;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "20";
        }

        public static class SodaCategory
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 3;
        }

        public static class WineCategory
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 3;
        }

        public static class Wine
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 5;

            public const int DescriptionMaxLength = 200;
            public const int DescriptionMinLength = 20;

            public const int ManufacturerMaxLength = 50;
            public const int ManufacturerMinLength = 5;

            public const int ImageUrlMaxLength = 2048;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "2000";
        }

        public static class Whiskey
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 5;

            public const int DescriptionMaxLength = 200;
            public const int DescriptionMinLength = 20;

            public const int ManufacturerMaxLength = 50;
            public const int ManufacturerMinLength = 5;

            public const int ImageUrlMaxLength = 2048;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "2000";
        }

        public static class WhiskeyCategory
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 3;
        }
    }
}

namespace BackendRestAPI
{
    public static class Consts
    {
        public static string NoSubcategoryName { get; }
        public static int DefaultStringSizeInDb { get; }
        public static int TokenValidTimeInMinutes { get; }

        public static int MinPasswordLen { get; }
        public static int MaxPasswordLen { get; }
        public static bool RequireSpecialCharInPassword { get; }
        public static bool RequireNumberInPassword { get; }
        public static bool RequireSmallLetterInPassword { get; }
        public static bool RequireBigLetterInPassword { get; }
        public static string SpecialCharacters { get; }
        public static bool HideContactsPasswordOnDetailsRequest { get; }
        public static string TokenCookieName { get; }
        public static string TokenCookieOptions { get; }

        static Consts()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            NoSubcategoryName = configuration["Database:NoSubcategoryName"];
            DefaultStringSizeInDb = int.Parse(configuration["Database:DefaultStringSizeInDb"]);
            TokenValidTimeInMinutes = int.Parse(configuration["Jwt:TokenValidTimeInMinutes"]);
            MinPasswordLen = int.Parse(configuration["PasswordPolicy:MinPasswordLen"]);
            MaxPasswordLen = int.Parse(configuration["PasswordPolicy:MaxPasswordLen"]);
            RequireSpecialCharInPassword = bool.Parse(configuration["PasswordPolicy:RequireSpecialCharInPassword"]);
            RequireNumberInPassword = bool.Parse(configuration["PasswordPolicy:RequireNumberInPassword"]);
            RequireSmallLetterInPassword = bool.Parse(configuration["PasswordPolicy:RequireSmallLetterInPassword"]);
            RequireBigLetterInPassword = bool.Parse(configuration["PasswordPolicy:RequireBigLetterInPassword"]);
            SpecialCharacters = configuration["PasswordPolicy:SpecialCharacters"];
            HideContactsPasswordOnDetailsRequest = bool.Parse(configuration["PasswordPolicy:HideContactsPasswordOnDetailsRequest"]);
            TokenCookieName = configuration["Jwt:TokenCookieName"];
        }
    }
}

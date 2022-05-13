namespace ApiAppShop.Domain.Constants
{
    public static class ErrorMessageConstants
    {
        public const string APP_DOESNT_EXIST = "App doesn't exist";

        public const string USER_DOESNT_EXIST = "User doesn't exist";

        public const string USER_NOT_AUTHENTICATED = "User not Authenticated";

        public const string NOT_A_NEW_USER = "Not a new user. UserId: {0}";

        public const string ROLLED_BACK_USER_ACCOUNT_0_IN_CACHE = "Rolled back user account {0} in cache";
    }
}

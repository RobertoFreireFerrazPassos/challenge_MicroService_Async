namespace ApiAppShop.Domain.Constants
{
    public static class ErrorMessageConstants
    {
        public const string APP_DOESNT_EXIST = "App doesn't exist";

        public const string USER_DOESNT_EXIST = "User doesn't exist";

        public const string USER_NOT_AUTHENTICATED = "User not Authenticated";

        public const string NOT_A_NEW_USER = "Not a new user. UserId: {0}";

        public const string ROLLING_BACK_CHANGES_FOR_USER_ACCOUNT_0_IN_CACHE = "Rolling back changes for user account {0} in cache";

        public const string RETRYING_SETTING_USER_ACCOUNT_0 = "Retrying setting user account {0}";
    }
}

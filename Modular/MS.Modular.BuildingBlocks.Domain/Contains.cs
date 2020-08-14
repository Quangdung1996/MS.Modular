namespace MS.Modular.BuildingBlocks.Domain
{
    public static class Contains
    {
        public struct StoredProcedures_Account
        {
            public const string CreateAccount = "usp_Account_Insert";
            public const string GetAccount = "usp_Account_Get";
            public const string UpdateAccount = "usp_Account_Update";
        }

        public struct StoredProcedures_User
        {
            public const string CreateUser = "usp_User_Insert";
            public const string GetUserByEmail = "usp_User_GetByEmail";
            public const string GetUserById = "usp_User_GetById";
            public const string UpdateUser = "usp_User_Update";
        }
    }
}
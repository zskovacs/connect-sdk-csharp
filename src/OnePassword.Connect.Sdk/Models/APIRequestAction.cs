namespace OnePassword.Connect.Sdk.Models;

public enum APIRequestAction
{

    [System.Runtime.Serialization.EnumMember(Value = @"READ")]
    READ = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"CREATE")]
    CREATE = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"UPDATE")]
    UPDATE = 2,

    [System.Runtime.Serialization.EnumMember(Value = @"DELETE")]
    DELETE = 3,

}
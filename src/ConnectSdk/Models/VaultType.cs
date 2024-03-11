namespace ConnectSdk.Models;

public enum VaultType
{

    [System.Runtime.Serialization.EnumMember(Value = @"USER_CREATED")]
    USER_CREATED = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"PERSONAL")]
    PERSONAL = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"EVERYONE")]
    EVERYONE = 2,

    [System.Runtime.Serialization.EnumMember(Value = @"TRANSFER")]
    TRANSFER = 3,

}
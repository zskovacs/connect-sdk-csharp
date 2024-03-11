namespace ConnectSdk.Models;

public enum FieldType
{

    [System.Runtime.Serialization.EnumMember(Value = @"STRING")]
    STRING = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"EMAIL")]
    EMAIL = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"CONCEALED")]
    CONCEALED = 2,

    [System.Runtime.Serialization.EnumMember(Value = @"URL")]
    URL = 3,

    [System.Runtime.Serialization.EnumMember(Value = @"TOTP")]
    TOTP = 4,

    [System.Runtime.Serialization.EnumMember(Value = @"DATE")]
    DATE = 5,

    [System.Runtime.Serialization.EnumMember(Value = @"MONTH_YEAR")]
    MONTH_YEAR = 6,

    [System.Runtime.Serialization.EnumMember(Value = @"MENU")]
    MENU = 7,
}
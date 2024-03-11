namespace ConnectSdk.Models;

public enum Op
{

    [System.Runtime.Serialization.EnumMember(Value = @"add")]
    Add = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"remove")]
    Remove = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"replace")]
    Replace = 2,

}
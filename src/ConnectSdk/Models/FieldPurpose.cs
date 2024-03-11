namespace ConnectSdk.Models;

public enum FieldPurpose
{
    [System.Runtime.Serialization.EnumMember(Value = @"")]
    Empty = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"USERNAME")]
    USERNAME = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"PASSWORD")]
    PASSWORD = 2,

    [System.Runtime.Serialization.EnumMember(Value = @"NOTES")]
    NOTES = 3,
}
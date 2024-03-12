namespace OnePassword.Connect.Sdk.Models;

public enum ItemCategory
{
    [System.Runtime.Serialization.EnumMember(Value = @"LOGIN")]
    LOGIN = 0,

    [System.Runtime.Serialization.EnumMember(Value = @"PASSWORD")]
    PASSWORD = 1,

    [System.Runtime.Serialization.EnumMember(Value = @"API_CREDENTIAL")]
    API_CREDENTIAL = 2,

    [System.Runtime.Serialization.EnumMember(Value = @"SERVER")]
    SERVER = 3,

    [System.Runtime.Serialization.EnumMember(Value = @"DATABASE")]
    DATABASE = 4,

    [System.Runtime.Serialization.EnumMember(Value = @"CREDIT_CARD")]
    CREDIT_CARD = 5,

    [System.Runtime.Serialization.EnumMember(Value = @"MEMBERSHIP")]
    MEMBERSHIP = 6,

    [System.Runtime.Serialization.EnumMember(Value = @"PASSPORT")]
    PASSPORT = 7,

    [System.Runtime.Serialization.EnumMember(Value = @"SOFTWARE_LICENSE")]
    SOFTWARE_LICENSE = 8,

    [System.Runtime.Serialization.EnumMember(Value = @"OUTDOOR_LICENSE")]
    OUTDOOR_LICENSE = 9,

    [System.Runtime.Serialization.EnumMember(Value = @"SECURE_NOTE")]
    SECURE_NOTE = 10,

    [System.Runtime.Serialization.EnumMember(Value = @"WIRELESS_ROUTER")]
    WIRELESS_ROUTER = 11,

    [System.Runtime.Serialization.EnumMember(Value = @"BANK_ACCOUNT")]
    BANK_ACCOUNT = 12,

    [System.Runtime.Serialization.EnumMember(Value = @"DRIVER_LICENSE")]
    DRIVER_LICENSE = 13,

    [System.Runtime.Serialization.EnumMember(Value = @"IDENTITY")]
    IDENTITY = 14,

    [System.Runtime.Serialization.EnumMember(Value = @"REWARD_PROGRAM")]
    REWARD_PROGRAM = 15,

    [System.Runtime.Serialization.EnumMember(Value = @"DOCUMENT")]
    DOCUMENT = 16,

    [System.Runtime.Serialization.EnumMember(Value = @"EMAIL_ACCOUNT")]
    EMAIL_ACCOUNT = 17,

    [System.Runtime.Serialization.EnumMember(Value = @"SOCIAL_SECURITY_NUMBER")]
    SOCIAL_SECURITY_NUMBER = 18,

    [System.Runtime.Serialization.EnumMember(Value = @"MEDICAL_RECORD")]
    MEDICAL_RECORD = 19,

    [System.Runtime.Serialization.EnumMember(Value = @"SSH_KEY")]
    SSH_KEY = 20,

    [System.Runtime.Serialization.EnumMember(Value = @"CUSTOM")]
    CUSTOM = 21,
}
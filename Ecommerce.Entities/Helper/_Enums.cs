using System.ComponentModel;

namespace Entities.Helper;

public enum SystemRoles : byte
{
    SuperAdmin = 1,
    Admin = 2,
    Client = 3
}

public enum ProductSort : byte
{
    New = 1,
    Star = 2,
    LowToHighPrice = 3,
    HighToLowPrice = 4,
    Bestsellers = 5
}

public enum Grade : byte
{
    ندارد = 1,
    عالی = 2,
    یک = 3
}

public enum CustomHttpStatus
{
    [Description("token expired")] TokenExpired = 601,
    Unauthorized = 603,
    [Description("username is not valid")] InvalidUsername = 610,

    [Description("password is not strong")]
    PasswordIsNotStrong = 611,

    [Description("username or password aren't valid")]
    UsernameOrPasswordAreNotValid = 612,
    [Description("token isn't valid")] InvalidToken = 613,

    [Description("must change default password at first-login")]
    MustChangeDefaultPassword = 614,

    [Description("old and new password not be same")]
    OldAndNewPasswordNotBeSame = 615,

    [Description("The role could not be found")]
    RoleCouldNotFound = 616,

    [Description("The user could not be found")]
    UserCouldNotFound = 617,

    [Description("The user doesn't have this role")]
    UserDoesNotHaveRole = 618,

    [Description("this role was assinged to some users")]
    UsersHaveRole = 619,
    [Description("role name is invalid")] RoleNameIsInvalid = 620,

    [Description("allow or restrict http verbs has conflicts")]
    HttpVerbsHaveConflict = 621,

    [Description("permission could not found")]
    PermissionCouldNotFound = 622,

    [Description("this permission was assinged to some users")]
    UsersHavePermission = 623,
    [Description("two factor is enabled")] TwoFactorIsEnabled = 624,

    [Description("two factor pin is not valid")]
    TwoFactorPinIsNotValid = 625,

    [Description("user has already this role")]
    UserHasRole = 626,

    [Description("user has already this permission")]
    UserHasPermission = 627,

    [Description("this role has already exist")]
    RoleNameIsDuplicate = 628,

    [Description("this permission has already exist")]
    PermissionNameIsDuplicate = 629,

    [Description("'search' field must be longer than three character")]
    SearchMustLongerThanThreeCharacter = 630,

    [Description("this permissionGroup was assinged to some users")]
    UsersHavePermissionGroup = 631,

    [Description("refresh token isn't valid, maybe it's invoked or expired")]
    RefreshTokenIsInvalid = 632,

    [Description("allow or restrict http verbs has conflicts")]
    PermissionsHaveConflict = 633,

    [Description("permissionGroup could not found")]
    PermissionGroupCouldNotFound = 634,

    [Description("two factor is not enabled")]
    TwoFactorIsNotEnabled = 635,

    [Description("this UserGroup was assinged to some users")]
    UsersInThisGroup = 636
}
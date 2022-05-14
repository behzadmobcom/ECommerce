using Microsoft.AspNetCore.Identity;

namespace PersianTranslation.Identity;

public class PersianIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new()
        {
            Code = nameof(DuplicateEmail),
            Description = $"ایمیل '{email}' قبلا توسط شخص دیگری انتخاب شده است"
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new()
        {
            Code = nameof(DuplicateUserName),
            Description = $"نام کاربری {userName} قبلا توسط شخص دیگری انتخاب شده است"
        };
    }

    public override IdentityError InvalidEmail(string email)
    {
        return new()
        {
            Code = nameof(InvalidEmail),
            Description = $"ایمیل '{email}' ، یک ایمیل معتبر نیست"
        };
    }

    public override IdentityError DuplicateRoleName(string role)
    {
        return new()
        {
            Code = nameof(DuplicateRoleName),
            Description = $"مقام '{role}' قبلا ثبت شده است"
        };
    }

    public override IdentityError InvalidRoleName(string role)
    {
        return new()
        {
            Code = nameof(InvalidRoleName),
            Description = $"نام '{role}' معتبر نیست"
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new()
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "رمز عبور باید حداقل دارای یک عدد باشد"
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new()
        {
            Code = nameof(PasswordRequiresLower),
            Description = "رمز عبور باید حداقل دارای یک کاراکتر انگلیسی کوچک باشد ('a'-'z')"
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new()
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "رمز عبور باید حداقل دارای یک کاراکتر انگلیسی بزرگ باشد ('A'-'Z')"
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new()
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "رمز عبور باید حداقل دارای یک کاراکتر ویژه باشد مثل '@#%^&'"
        };
    }

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
    {
        return new()
        {
            Code = nameof(PasswordRequiresUniqueChars),
            Description = $"رمز عبور باید حداقل دارای {uniqueChars} کاراکتر منحصر به فرد باشد"
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new()
        {
            Code = nameof(PasswordTooShort),
            Description = $"رمز عبور نباید کمتر از {length} کاراکتر باشد"
        };
    }

    public override IdentityError InvalidUserName(string userName)
    {
        return new()
        {
            Code = nameof(InvalidUserName),
            Description = $"نام کاربری '{userName}' معتبر نیست، نام کاربری فقط میتواند دارای حروف یا عدد باشد"
        };
    }

    public override IdentityError UserNotInRole(string role)
    {
        return new()
        {
            Code = nameof(UserNotInRole),
            Description = $"کاربر در مقام '{role}' نیست"
        };
    }

    public override IdentityError UserAlreadyInRole(string role)
    {
        return new()
        {
            Code = nameof(UserAlreadyInRole),
            Description = $"کاربر در مقام '{role}' است"
        };
    }

    public override IdentityError DefaultError()
    {
        return new()
        {
            Code = nameof(DefaultError),
            Description = "خطای پیشبینی نشده رخ داد"
        };
    }
}
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GovDigitalApp.Infrastructure.Persistence;

public class EncryptedStringConverter : ValueConverter<string, string>
{
    public EncryptedStringConverter(IDataProtector protector)
        : base(
            plain => plain == null ? string.Empty : protector.Protect(plain),
            cipher => string.IsNullOrEmpty(cipher) ? string.Empty : TryUnprotect(protector, cipher))
    {
    }

    private static string TryUnprotect(IDataProtector protector, string cipher)
    {
        try
        {
            return protector.Unprotect(cipher);
        }
        catch
        {
            return cipher;
        }
    }
}

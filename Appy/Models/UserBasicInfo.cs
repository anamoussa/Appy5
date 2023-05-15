namespace Appy.Models;

public class UserBasicInfo
{
    public long Id { get; set; }
    public string Avatar { get; set; }
    public string PhoneId { get; set; }
    public DateTimeOffset LastMobileLogin { get; set; }
    public long AdminId { get; set; }
    public long ArchiveId { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Expired { get; set; }
    public string Title { get; set; }
    public string Role { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public long Ruolo { get; set; }
    public string Sedi { get; set; }
    public string Livello { get; set; }
    public string AllowedArchives { get; set; }
    public string WpUsername { get; set; }
    public string MonthlyFee { get; set; }
    public long Status { get; set; }
    public string PasswordReset { get; set; }
    public long FavArchive { get; set; }
}

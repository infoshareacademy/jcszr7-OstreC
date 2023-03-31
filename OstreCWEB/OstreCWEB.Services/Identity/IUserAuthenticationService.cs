using OstreCWEB.DomainModels.Identity;

namespace OstreCWEB.Services.Identity
{
    public interface IUserAuthenticationService
    {
        Task<StatusIdentity> LoginAsync(Login model);
        Task<StatusIdentity> RegisterAsync(Registration model);
        Task<StatusIdentity> ChangePasswordAsync(ChangePassword model, int userId);
        public bool sendEmailSMTP(int emailType, Registration registration);
        Task LogoutAsync();
    }
}

using Microsoft.AspNetCore.Identity;
using OstreCWEB.DomainModels.Identity;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace OstreCWEB.Services.Identity
{

    internal class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserAuthenticationService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<StatusIdentity> LoginAsync(Login model)
        {
            var status = new StatusIdentity();
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid Username";
                return status;
            }
            // matching password
            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User locked out";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on loggin in";
                return status;
            }
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<StatusIdentity> RegisterAsync(Registration model)
        {
            var status = new StatusIdentity();
            var userExists = await userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exists";
                return status;
            }

            User user = new User
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = model.Name,
                Email = model.Email,
                UserName = model.UserName,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));

            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            status.StatusCode = 1;
            status.Message = "You have registered successfully";
            return status;

        }
        public async Task<StatusIdentity> ChangePasswordAsync(ChangePassword model, string userId)
        {
            var status = new StatusIdentity();
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                status.Message = "User does not exist";
                status.StatusCode = 0;
                return status;
            }
            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                status.Message = "Password has updated successfully";
                status.StatusCode = 1;
            }
            else
            {
                status.Message = "Some error occurred";
                status.StatusCode = 0;
            }
            return status;
        }

        public bool sendEmailSMTP(int emailType, Registration registration, out string feedback)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("ostreCGame@gmail.com", "jgkeyglxajjymsft"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("ostreCGame@gmail.com"),
                Subject = "",
                Body = "",
                IsBodyHtml = true,
            };

            //Forgot Password template
            if (emailType == 1)
            {
                mailMessage.Subject = "Ostre C Game password recovery email";
                mailMessage.Body = $"<h1>Hello,</h1> <br> <h2> dear {registration.UserName}</h2><br> You forgot your password. <br> For now the best I can do is send you your password. Here it is : <br>" +
                    $"Your username: {registration.UserName} <br> Your password: {registration.Password} <br> Please don't forget your password going forward. <br> <b>Regards</b>,<br><b> Ostre C team</b>";

                mailMessage.To.Add(registration.Email);

                feedback = "Email sent on the email adress assigned to your existing account :" + registration.Email;
                smtpClient.Send(mailMessage);
                return true;
            }
            else
            {
                feedback = "An non existing email template was chosen";
                throw new Exception(feedback);
                return false;

            }
        }
    }
}

using IdentityServerHost.Quickstart.UI;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.MainModule.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string? Username { get; set; }

        [Required] 
        public string? Password { get;set; }

        [Required]
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ReturnUrl { get; set; }

        // Don't let the user choose the Role in a real project.
        public string? RoleName { get; set; } 
        public bool AllowRememberLogin { get; set; }
        public bool EnableLocalLogin { get; set; }
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
        public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders;
    }
}

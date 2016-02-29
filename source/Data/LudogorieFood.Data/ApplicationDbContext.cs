namespace LudogorieFood.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("LudogorieFoodDb", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

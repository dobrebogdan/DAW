using DAW.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DAW.Startup))]
namespace DAW
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            	ApplicationDbContext context = new ApplicationDbContext(); 
             
            	var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)); 
             
            	var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)); 
             
            // Se adauga rolurile aplicatiei
            	if (!roleManager.RoleExists("Administrator"))
            	{
            		// Se adauga rolul de administrator
            		var role = new IdentityRole();
            		role.Name = "Administrator";
            		roleManager.Create(role); 
             
                // se adauga utilizatorul administrator
            		var user = new ApplicationUser();
            		user.UserName = "admin@admin.com";
            		user.Email = "admin@admin.com"; 
             
                var adminCreated = userManager.Create(user, "Administrator1!");
            		if (adminCreated.Succeeded)
            		{
            			userManager.AddToRole(user.Id, "Administrator");
            		}
            	} 
             
            if (!roleManager.RoleExists("Moderator"))
            	{
            		var role = new IdentityRole();
            		role.Name = "Moderator";
            		roleManager.Create(role);
            	} 
             
            	if (!roleManager.RoleExists("User"))
            	{
            		var role = new IdentityRole();
            		role.Name = "User";
            		roleManager.Create(role);
            	}

            if (!roleManager.RoleExists("Guest"))
            {
                var role = new IdentityRole();
                role.Name = "Guest";
                roleManager.Create(role);
            }
        }
    }
}

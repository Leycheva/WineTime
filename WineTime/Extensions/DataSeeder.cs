using Microsoft.AspNetCore.Identity;
using WineTime.Infrastructure.Data;

namespace WineTime.Extensions
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext applicationDbContext;

        public DataSeeder(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void Seed()
        {
            if (!applicationDbContext.Categories.Any())
            {
                var categories = new List<Category>()
                {
                    new Category { Name = "White" },
                    new Category { Name = "Rose" },
                    new Category { Name = "Red" },
                };

                applicationDbContext.Categories.AddRange(categories);
                applicationDbContext.SaveChanges();
            }


            if (!applicationDbContext.Regions.Any())
            {
                var regions = new List<Region>()
                {
                    new Region { Country = "Bulgaria" },
                    new Region { Country = "Italy" },
                    new Region { Country = "Spain" },
                    new Region { Country = "Portugal" },
                    new Region { Country = "Germany" },
                };

                applicationDbContext.Regions.AddRange(regions);
                applicationDbContext.SaveChanges();
            }

            if (!applicationDbContext.Manufactures.Any())
            {
                var manufactures = new List<Manufacture>()
                {
                    new Manufacture { ManufactureName = "Starosel" , RegionId = 1 , ImageUrl = "https://taraba.bg/image/catalog/1234/alkohol/sarosel.png"},
                    new Manufacture { ManufactureName = "Katarzyna Estate" , RegionId = 1 , ImageUrl = "https://sommelier.bg/media/k2/items/cache/963c54073e784a324883122381877c85_M.webp"},
                    new Manufacture { ManufactureName = "Montevertrano" , RegionId = 2 , ImageUrl = "https://www.paolocoloniali.it/pimages/Az-Agr-Montevetrano-image-192-779.png"},
                    new Manufacture { ManufactureName = "Casanova di Neri" , RegionId = 2 , ImageUrl = "https://www.casanovadinerirelais.com/wp-content/uploads/2017/04/bg-logo-casanovadineri-1170x552.png"},
                    new Manufacture { ManufactureName = "Bodega La Encina" , RegionId = 3 , ImageUrl = "https://www.bodegalaencina.com/wp-content/uploads/logo-web1.png"},
                    new Manufacture { ManufactureName = "Domino Del Soto" , RegionId = 3 , ImageUrl = "https://www.dominiodelsoto.com/wp-content/uploads/2017/01/cropped-logo_ombre.png"},
                    new Manufacture { ManufactureName = "Monte D'Agualva" , RegionId = 4 , ImageUrl = "https://vinhomontedagualva.pt/wp-content/uploads/2021/02/logo-vinho-monte-da-agualva-200x163.png"},
                    new Manufacture { ManufactureName = "Burmester" , RegionId = 4 , ImageUrl = "https://getvectorlogo.com/wp-content/uploads/2018/12/burmester-audiosysteme-vector-logo.png"},
                    new Manufacture { ManufactureName = "Eva Fricke" , RegionId = 5 , ImageUrl = "https://liebl-pr.de/cms/upload/eva-fricke/EvaFricke_Logo_Image.jpg"},
                    new Manufacture { ManufactureName = "7 Hats" , RegionId = 5 , ImageUrl = "https://scontent.fsof8-1.fna.fbcdn.net/v/t1.18169-9/10712943_1505180926403772_8666357332833920348_n.jpg?_nc_cat=105&ccb=1-5&_nc_sid=09cbfe&_nc_ohc=m1GhEfbwiaEAX8NEQ6I&_nc_ht=scontent.fsof8-1.fna&oh=00_AT9RkQqHD2s35nno3IZFx8om_4BOq2OL6telioq7yKNlMQ&oe=6259E3B4"},
                };

                applicationDbContext.Manufactures.AddRange(manufactures);
                applicationDbContext.SaveChanges();
            }
        }

        public void SeedAdmin(IServiceProvider service)
        {
            const string AdminName = "Administrator";
            const string email = "admin @wt.com";
            const string userName = "admin@wt.com";
            const string password = "pass123";

            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run( async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminName))
                    {
                        return;
                    }

                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = AdminName
                    });

                    var user = new ApplicationUser
                    {
                        UserName = userName,
                        Email = email,
                        FullName = "Owner",
                        EmailConfirmed = true,
                    };

                    await userManager.CreateAsync(user, password);    
                    await userManager.AddToRoleAsync(user, AdminName);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}




namespace Vidionic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
			Sql(@"
			INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'0d2b27d0-6164-49f3-9dae-6d73885fe377', N'admin@vidionic.com', 0, N'ADYelWi8cmbFjDaByS9OyUu5cM0E9HCKMyiCVgdj7Ctcrryw3DWJvlYPZo2DyWAvIA==', N'a4ac3e33-5d04-41f2-a258-9be1cfc3234a', NULL, 0, 0, NULL, 1, 0, N'admin@vidionic.com')
			INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c0d7b2e9-fa17-48bd-a736-74195dcbb9ee', N'guest@vidionic.com', 0, N'ACaYRc+LyamNWza3OOCMx2Nghrr4ncO28tlkbq4OQZ+ddCV2/C5b6ZTyxOFxzDhIKA==', N'85370a28-bcdc-43c9-a5eb-73e789b351c7', NULL, 0, 0, NULL, 1, 0, N'guest@vidionic.com')
			INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'15af452e-1902-4486-b339-7a7f3ff5aba0', N'CanManageMovies')			
			INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0d2b27d0-6164-49f3-9dae-6d73885fe377', N'15af452e-1902-4486-b339-7a7f3ff5aba0')
			");
        }
        
        public override void Down()
        {
        }
    }
}

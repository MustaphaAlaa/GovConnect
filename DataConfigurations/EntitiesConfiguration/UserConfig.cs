using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Drivers;
using Models.Users;

namespace DataConfigurations.EntitiesConfiguration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var users = new User[]
            {
            new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                FirstName = "Test",
                SecondName = "User",
                ThirdName = "One",
                FourthName = "First",
                NationalNo = "1111111111",
                Gender = enGender.Male,
                Address = "Test Address 1",
                ImagePath = "null",
                CountryId = 1,
                BirthDate = new DateTime(2000, 1, 1),
                UserName = "testuser1",
                NormalizedUserName = "TESTUSER1",
                Email = "test1@test.com",
                NormalizedEmail = "TEST1@TEST.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==",
                SecurityStamp = "K2MDKSUEXFG6QCHLCWJLVREVWT7545X2",
                ConcurrencyStamp = "b3e27cb1-7bfb-4722-bc67-0c9c96c51d4e",
                PhoneNumber = "0777777771",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                LockoutEnd = null
            },
            new User
            {
                Id = new Guid("22222222-2222-2222-2222-222222222222"),
                FirstName = "Test",
                SecondName = "User",
                ThirdName = "Two",
                FourthName = "Second",
                NationalNo = "2222222222",
                Gender = enGender.Female,
                Address = "Test Address 2",
                ImagePath = "null",
                CountryId = 1,
                BirthDate = new DateTime(2000, 1, 2),
                UserName = "testuser2",
                NormalizedUserName = "TESTUSER2",
                Email = "test2@test.com",
                NormalizedEmail = "TEST2@TEST.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==",
                SecurityStamp = "K2MDKSUEXFG6QCHLCWJLVREVWT7545X3",
                ConcurrencyStamp = "44f69173-726f-44d8-b4b0-7695e4bec210",
                PhoneNumber = "0777777772",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                LockoutEnd = null
            },
            new User
            {
                Id = new Guid("33333333-3333-3333-3333-333333333333"),
                FirstName = "Test",
                SecondName = "User",
                ThirdName = "Three",
                FourthName = "Third",
                NationalNo = "3333333333",
                Gender = enGender.Male,
                Address = "Test Address 3",
                ImagePath = "null",
                CountryId = 1,
                BirthDate = new DateTime(2000, 1, 3),
                UserName = "testuser3",
                NormalizedUserName = "TESTUSER3",
                Email = "test3@test.com",
                NormalizedEmail = "TEST3@TEST.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==",
                SecurityStamp = "K2MDKSUEXFG6QCHLCWJLVREVWT7545X4",
                ConcurrencyStamp = "2f511f41-c3a1-4ebe-a55b-377487fac8b8",
                PhoneNumber = "0777777773",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                LockoutEnd = null
            },
            new User
            {
                Id = new Guid("44444444-4444-4444-4444-444444444444"),
                FirstName = "Test",
                SecondName = "User",
                ThirdName = "Four",
                FourthName = "Fourth",
                NationalNo = "4444444444",
                Gender = enGender.Female,
                Address = "Test Address 4",
                ImagePath = "null",
                CountryId = 1,
                BirthDate = new DateTime(2000, 1, 4),
                UserName = "testuser4",
                NormalizedUserName = "TESTUSER4",
                Email = "test4@test.com",
                NormalizedEmail = "TEST4@TEST.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==",
                SecurityStamp = "K2MDKSUEXFG6QCHLCWJLVREVWT7545X5",
                ConcurrencyStamp = "a98af5d4-731c-489b-851c-86f23c66b3d1",
                PhoneNumber = "0777777774",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                LockoutEnd = null
            },
            new User
            {
                Id = new Guid("55555555-5555-5555-5555-555555555555"),
                FirstName = "Test",
                SecondName = "User",
                ThirdName = "Five",
                FourthName = "Fifth",
                NationalNo = "5555555555",
                Gender = enGender.Male,
                Address = "Test Address 5",
                ImagePath = "null",
                CountryId = 1,
                BirthDate = new DateTime(2000, 1, 5),
                UserName = "testuser5",
                NormalizedUserName = "TESTUSER5",
                Email = "test5@test.com",
                NormalizedEmail = "TEST5@TEST.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEPahXcemRXGRxKE3LxhXh/jtLc5gQV31AVLNkX2GAh0xArP6aYtL2TFmDaxZHUXKQw==",
                SecurityStamp = "K2MDKSUEXFG6QCHLCWJLVREVWT7545X6",
                ConcurrencyStamp = "7c8f2e9b-4c1a-483d-b832-d35cb3f3f287",
                PhoneNumber = "0777777775",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                LockoutEnd = null
            }
            };

            builder.HasData(users);
        }
    }
}

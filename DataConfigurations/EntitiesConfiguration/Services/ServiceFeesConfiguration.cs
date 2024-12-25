using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ApplicationModels;

namespace DataConfigurations.EntitiesConfiguration;

public class ServiceFeesConfiguration : IEntityTypeConfiguration<ServiceFees>
{
    public void Configure(EntityTypeBuilder<ServiceFees> builder)
    {
        builder.HasKey(appFees => new { appFees.ServicePurposeId, ApplicationForId = appFees.ServiceCategoryId });

        // Local Driving License
        builder.HasData(new ServiceFees[]
        {
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.Local_Driving_License,
                ServicePurposeId = (int)EnServicePurpose.New,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.Local_Driving_License,
                ServicePurposeId = (int)EnServicePurpose.Renew,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.Local_Driving_License,
                ServicePurposeId = (int)EnServicePurpose.Release,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.Local_Driving_License,
                ServicePurposeId = (int)EnServicePurpose.Retake_Test,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.Local_Driving_License,
                ServicePurposeId = (int)EnServicePurpose.Replacement_For_Damage,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.Local_Driving_License,
                ServicePurposeId = (int)EnServicePurpose.Replacement_For_Lost,
                Fees = 0
            },

            // International Driving License
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.International_Driving_License,
                ServicePurposeId = (byte)EnServicePurpose.New,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.International_Driving_License,
                ServicePurposeId = (byte)EnServicePurpose.Renew,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.International_Driving_License,
                ServicePurposeId = (byte)EnServicePurpose.Replacement_For_Damage,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.International_Driving_License,
                ServicePurposeId = (byte)EnServicePurpose.Replacement_For_Lost,
                Fees = 0
            },

            // Passport
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.Passport,
                ServicePurposeId = (byte)EnServicePurpose.New,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.Passport,
                ServicePurposeId = (byte)EnServicePurpose.Renew,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.Passport,
                ServicePurposeId = (byte)EnServicePurpose.Replacement_For_Damage,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.Passport,
                ServicePurposeId = (byte)EnServicePurpose.Replacement_For_Lost,
                Fees = 0
            },

            // National Identity Card
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.National_Identity_Card,
                ServicePurposeId = (byte)EnServicePurpose.New,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.National_Identity_Card,
                ServicePurposeId = (byte)EnServicePurpose.Renew,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.National_Identity_Card,
                ServicePurposeId = (byte)EnServicePurpose.Replacement_For_Damage,
                Fees = 0
            },
            new ServiceFees {
                ServiceCategoryId = (short)EnServiceCategory.National_Identity_Card,
                ServicePurposeId = (byte)EnServicePurpose.Replacement_For_Lost,
                Fees = 0
            }
        });
    }
}

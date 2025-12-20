using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Tests;
using Models.Tests.Enums;

namespace DataConfigurations.EntitiesConfiguration;

public class TestTypesConfiguration : IEntityTypeConfiguration<TestType>
{
    public void Configure(EntityTypeBuilder<TestType> builder)
    {
        builder.HasData(new []
        {
            new TestType()
            {
                TestTypeId = (int)EnTestTypes.Vision,
                TestTypeTitle = EnTestTypes.Vision.ToString(),
                TestTypeDescription = "This assesses the applicant's visual acuity to ensure they have sufficient vision to drive safely.",
                TestTypeFees = 100 
            },
            new TestType()
            {
                TestTypeId = (int)EnTestTypes.Written_Theory,
                TestTypeTitle = EnTestTypes.Written_Theory.ToString().Replace("_"," "),
                TestTypeDescription =  "This test assesses the applicant's knowledge of traffic rules, road signs, and driving regulations. It typically consists of multiple-choice questions, and the applicant must select the correct answer(s). The written test aims to ensure that the applicant understands the rules of the road and can apply them in various driving scenarios.",
                TestTypeFees = 150 
            },
            new TestType()
            {
                TestTypeId = (int)EnTestTypes.Practical_Street,
                TestTypeTitle = EnTestTypes.Practical_Street.ToString().Replace("_"," "),
                TestTypeDescription = "This test evaluates the applicant's driving skills and ability to operate a motor vehicle safely on public roads. A licensed examiner accompanies the applicant in the vehicle and observes their driving performance.",
                TestTypeFees = 250 
            },
        });
    }
}
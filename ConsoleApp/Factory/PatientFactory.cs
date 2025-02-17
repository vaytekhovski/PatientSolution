using Bogus;
using Core.DTOs;

namespace ConsoleApp.Factory;

public class PatientFactory : IPatientFactory
{
    private readonly Faker<Create> Faker;

    public PatientFactory()
    {
        Faker = new Faker<Create>()
            .RuleFor(p => p.Family, f => f.Name.LastName())
            .RuleFor(p => p.Given, f => [f.Name.FirstName()])
            .RuleFor(p => p.Gender, f => f.PickRandom(new[] { "male", "female" }))
            .RuleFor(p => p.BirthDate, f => DateOnly.FromDateTime(f.Date.Past(80, DateTime.Today.AddYears(-18))))
            .RuleFor(p => p.Active, f => f.Random.Bool());
    }

    public Create Create() => Faker.Generate();

    public List<Create> Create(int count) => Faker.Generate(count);
}

﻿using Bogus;
using Core.DTOs;

public static class PatientGenerator
{
    public static List<Create> GeneratePatients(int count)
    {
        var faker = new Faker<Create>()
            .RuleFor(p => p.Family, f => f.Name.LastName())
            .RuleFor(p => p.Given, f => [f.Name.FirstName()])
            .RuleFor(p => p.Gender, f => f.PickRandom(new[] { "male", "female" }))
            .RuleFor(p => p.BirthDate, f => DateOnly.FromDateTime(f.Date.Past(80, DateTime.Today.AddYears(-18))))
            .RuleFor(p => p.Active, f => f.Random.Bool());

        return faker.Generate(count);
    }
}

using System.Net;
using Bogus;
using NUnit.Framework;
using Scada.FakeRestApi.Api.Apis;
using Scada.FakeRestApi.Api.Models;

namespace Scada.FakeRestApi.Tests.Authors;

public class CreateAuthorTests : BaseTest<CreateAuthorRepository>
{
    private readonly AuthorsApi _authorsApi = new AuthorsApi(TestConfiguration.BaseUrl);
    
    protected override string JsonFileName => "Authors/CreateAuthorData.json";

    [TestCase(TestName = "Verify that author is created.")]
    public void VerifyThatAuthorIsCreated()
    {
        var authorFaker = new Faker<AuthorEntity>()
            .RuleFor(author => author.Id, faker => faker.Random.Int(Repository.MinAuthorId, Repository.MaxAuthorId))
            .RuleFor(author => author.IdBook, faker => faker.Random.Int(Repository.MinBookId, Repository.MaxBookId))
            .RuleFor(author => author.FirstName, faker => faker.Name.FirstName())
            .RuleFor(author => author.LastName, faker => faker.Name.LastName());
        var author = authorFaker.Generate();
        
        var response = _authorsApi.CreateAuthor(author);
        
        Assert.Multiple(() =>
        {
            AssertThatResponseIsSuccessful(response, HttpStatusCode.OK);
            Assert.That(response.JsonData!.Id, Is.EqualTo(author.Id), "Incorrect author id.");
            Assert.That(response.JsonData!.IdBook, Is.EqualTo(author.IdBook), "Incorrect book id.");
            Assert.That(response.JsonData!.FirstName, Is.EqualTo(author.FirstName), "Incorrect author first name.");
            Assert.That(response.JsonData!.LastName, Is.EqualTo(author.LastName), "Incorrect author last name.");
        });
    }
}

public class CreateAuthorRepository
{
    public int MinAuthorId { get; init; }
    public int MinBookId { get; init; }
    public int MaxAuthorId { get; init; }
    public int MaxBookId { get; init; }
}
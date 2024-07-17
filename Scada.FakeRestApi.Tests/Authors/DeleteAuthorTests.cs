using System.Net;
using Bogus;
using NUnit.Framework;
using Scada.FakeRestApi.Api.Apis;
using Scada.FakeRestApi.Api.Models;

namespace Scada.FakeRestApi.Tests.Authors;

public class DeleteAuthorTests : BaseTest<DeleteAuthorRepository>
{
    private readonly AuthorsApi _authorsApi = new AuthorsApi(TestConfiguration.BaseUrl);
    
    protected override string JsonFileName => "Authors/DeleteAuthorData.json";

    [TestCase(TestName = "Verify that author is deleted.")]
    public void VerifyThatAuthorIsDeleted()
    {
        var authorFaker = new Faker<AuthorEntity>()
            .RuleFor(author => author.Id, faker => faker.Random.Int(Repository.MinAuthorId, Repository.MaxAuthorId))
            .RuleFor(author => author.IdBook, faker => faker.Random.Int(Repository.MinBookId, Repository.MaxBookId))
            .RuleFor(author => author.FirstName, faker => faker.Name.FirstName())
            .RuleFor(author => author.LastName, faker => faker.Name.LastName());
        var author = authorFaker.Generate();
        
        // Form TA standpoint, it is better to not rely on test data created in another test and not have such dependence
        // However, if that is the case that client wants, it might be implemented next:
        // Adding [Order] attribute and group such test in single fixture.
        _authorsApi.CreateAuthor(author);
        var deleteResponse = _authorsApi.DeleteAuthor(author.Id);
        var getResponse = _authorsApi.GetAuthor(author.Id);
        
        Assert.Multiple(() =>
        {
            AssertThatResponseIsSuccessful(deleteResponse, HttpStatusCode.OK);
            AssertThatResponseIsSuccessful(getResponse, HttpStatusCode.NotFound);
        });
    }
}

public class DeleteAuthorRepository
{
    public int MinAuthorId { get; init; }
    public int MinBookId { get; init; }
    public int MaxAuthorId { get; init; }
    public int MaxBookId { get; init; }
}
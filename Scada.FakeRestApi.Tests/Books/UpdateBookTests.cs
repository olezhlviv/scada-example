using System.Net;
using Bogus;
using NUnit.Framework;
using Scada.FakeRestApi.Api.Apis;
using Scada.FakeRestApi.Api.Models;

namespace Scada.FakeRestApi.Tests.Books;

public class UpdateBookTests : BaseTest<UpdateBookRepository>
{
    private readonly BooksApi _booksApi = new BooksApi(TestConfiguration.BaseUrl);
    
    protected override string JsonFileName => "Books/UpdateBookData.json";

    [TestCase(TestName = "Verify that book is updated.")]
    public void VerifyThatBookIsUpdated()
    {
        var booksFaker = new Faker<BookEntity>()
            .RuleFor(book => book.Id, faker => faker.Random.Int(Repository.MinBookId, Repository.MaxBookId))
            .RuleFor(book => book.Title, faker => faker.Lorem.Sentence(3))
            .RuleFor(book => book.Description, faker => faker.Lorem.Paragraph())
            .RuleFor(book => book.PageCount, faker => faker.Random.Int(1, 1000))
            .RuleFor(book => book.Excerpt, faker => faker.Lorem.Paragraph())
            .RuleFor(book => book.PublishDate, faker => faker.Date.Past());
        var book = booksFaker.Generate();
        var updatedBook = booksFaker.Generate();
        _booksApi.CreateBook(book);
        
        var response = _booksApi.UpdateBook(book.Id, updatedBook);
        
        Assert.Multiple(() =>
        {
            AssertThatResponseIsSuccessful(response, HttpStatusCode.OK);
            Assert.That(response.JsonData!.Id, Is.EqualTo(updatedBook.Id), "Incorrect author id.");
            Assert.That(response.JsonData!.Title, Is.EqualTo(updatedBook.Title), "Incorrect title.");
            Assert.That(response.JsonData!.Description, Is.EqualTo(updatedBook.Description), "Incorrect description.");
            Assert.That(response.JsonData!.PageCount, Is.EqualTo(updatedBook.PageCount), "Incorrect page count.");
            Assert.That(response.JsonData!.Excerpt, Is.EqualTo(updatedBook.Excerpt), "Incorrect excerpt.");
            Assert.That(response.JsonData!.PublishDate, Is.EqualTo(updatedBook.PublishDate), "Incorrect publish date.");
        });
    }
}

public class UpdateBookRepository
{
    public int MinBookId { get; init; }
    public int MaxBookId { get; init; }
}
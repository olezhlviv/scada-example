using System.Net;
using Bogus;
using Newtonsoft.Json;
using NUnit.Framework;
using Scada.FakeRestApi.Api.Apis;
using Scada.FakeRestApi.Api.Models;

namespace Scada.FakeRestApi.Tests.Books;

public class GetBookTests : BaseTest<GetBookRepository>
{
    private readonly BooksApi _booksApi = new BooksApi(TestConfiguration.BaseUrl);
    
    protected override string JsonFileName => "Books/GetBookData.json";
    
    [TestCaseSource(nameof(GetGetBookTestCases))]
    public void VerifyThatBookDoesExistWithCorrectPageCount(int id, int expectedPageCount)
    {
        var response = _booksApi.GetBook(id);
        
        Assert.Multiple(() =>
        {
            AssertThatResponseIsSuccessful(response, HttpStatusCode.OK);
            Assert.That(response.JsonData!.Id, Is.EqualTo(id), "Incorrect book id.");
            Assert.That(response.JsonData!.PageCount, Is.EqualTo(expectedPageCount), "Incorrect page count.");
        });
    }
    
    private static IEnumerable<TestCaseData> GetGetBookTestCases()
    {
        var jsonText = File.ReadAllText("Books/GetBookData.json");
        var repository = JsonConvert.DeserializeObject<GetBookRepository>(jsonText);
        foreach (var book in repository.Books)
        {
            yield return new TestCaseData(book.Id, book.PageCount)
                .SetName($"Verify that book with id [{book.Id}] does exist with [{book.PageCount}] page count.");
        }
    }
}

public class GetBookRepository
{
    public List<BookEntity> Books { get; init; }
}
namespace Scada.FakeRestApi.Api.Models;

[Serializable]
public sealed class AuthorEntity
{
    public int Id { get; set; }
    public int IdBook { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
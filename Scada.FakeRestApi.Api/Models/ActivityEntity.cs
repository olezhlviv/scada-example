namespace Scada.FakeRestApi.Api.Models;

[Serializable]
public sealed class ActivityEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public bool Completed { get; set; }
}
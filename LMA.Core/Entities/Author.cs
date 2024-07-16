namespace LMA.Core.Entities;

public class Author : Base
{
    public string Name { get; set; }
    public List<BookAuthors> BookAuthors { get; set; }

}

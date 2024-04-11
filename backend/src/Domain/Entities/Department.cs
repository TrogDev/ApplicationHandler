using ApplicationHandler.Domain.Common;

namespace ApplicationHandler.Domain.Entities;

// Модель отдела
public class Department : BaseEntity<long>
{
    public string Title { get; set; }
    
    public IEnumerable<Keyword> Keywords { get; set; } = new List<Keyword>();
}

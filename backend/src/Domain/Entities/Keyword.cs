using ApplicationHandler.Domain.Common;

namespace ApplicationHandler.Domain.Entities;

// Модель ключевого слова, по которому определятся в какой отдел направляется запрос
public class Keyword : BaseEntity<long>
{
    public string Text { get; set; }
    
    public long DepartmentId { get; set; }
    public Department Department { get; set; }
}

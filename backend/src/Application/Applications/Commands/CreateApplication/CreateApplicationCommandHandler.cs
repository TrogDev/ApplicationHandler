namespace ApplicationHandler.Application.Applications.Commands.CreateApplication;

using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Domain.Entities;
using ApplicationHandler.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

// Создание заявки, определение отдела
public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, Guid>
{
    private readonly IUser currentUser;
    private readonly IApplicationDbContext context;

    public CreateApplicationCommandHandler(IUser currentUser, IApplicationDbContext context)
    {
        this.currentUser = currentUser;
        this.context = context;
    }

    public async Task<Guid> Handle(
        CreateApplicationCommand request,
        CancellationToken cancellationToken
    )
    {
        var application = new Application()
        {
            Title = request.Title,
            Description = request.Description,
            Status = ApplicationStatus.Processing,
            UserId = currentUser.Id,
            DepartmentId = await getDepartment(request)
        };

        await context.Applications.AddAsync(application, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return application.Id;
    }

    // Определение отдела по ключевым словам
    private async Task<long> getDepartment(CreateApplicationCommand request)
    {
        IEnumerable<Department> departments = await context.Departments.ToListAsync();
        IEnumerable<Keyword> keywords = await context.Keywords.ToListAsync();
        var departmentCounts = new Dictionary<long, int>();
        
        foreach (Department department in departments)
        {
            departmentCounts.Add(department.Id, 0);
        }

        foreach (Keyword keyword in keywords)
        {
            // Если ключевое слово найдено в названии добавляем 3 балла отделу
            departmentCounts[keyword.DepartmentId] += new Regex(keyword.Text, RegexOptions.IgnoreCase)
                .Matches(request.Title)
                .Count * 3;
            
            // Если ключевое слово найдено в описании добавляем 1 балл отделу
            departmentCounts[keyword.DepartmentId] += new Regex(keyword.Text, RegexOptions.IgnoreCase)
                .Matches(request.Description)
                .Count;
        }
        
        // Возвращем отдел с максимальный колличеством баллов
        return departmentCounts.MaxBy(e => e.Value).Key;
    }
}

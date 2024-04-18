namespace ApplicationHandler.Infrastructure.Data;

using ApplicationHandler.Application.Common.Interfaces;
using ApplicationHandler.Domain.Entities;
using ApplicationHandler.Domain.Enums;
using ApplicationHandler.Infrastructure.Auth.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext context;
    private readonly IHasher hasher;

    public ApplicationDbContextInitializer(ApplicationDbContext context, IHasher hasher)
    {
        this.context = context;
        this.hasher = hasher;
    }

    public void Initialize()
    {
        context.Database.Migrate();
        addAdmin();
        addDepartments();
        context.SaveChanges();
    }

    private void addAdmin()
    {
        User? admin = context.Users.FirstOrDefault(e => e.Email == "admin@admin.admin");

        if (admin is null)
        {
            admin = new User
            {
                Email = "admin@admin.admin",
                PasswordHash = hasher.Hash("admin"),
                Phone = "77777777777",
                Role = UserRole.Admin
            };
            context.Users.Add(admin);
        }
    }

    private void addDepartments()
    {
        Department[] departments =
        [
            new Department()
            {
                Title = "Отдел не определён"
            },
            new Department()
            {
                Title = "Проектный отдел",
                Keywords =
                [
                    new Keyword() { Text = "проект" },
                    new Keyword() { Text = "задач" },
                    new Keyword() { Text = "компания" },
                    new Keyword() { Text = "разработка" },
                    new Keyword() { Text = "иноваци" },
                    new Keyword() { Text = "решение" },
                    new Keyword() { Text = "план" }
                ]
            },
            new Department()
            {
                Title = "WEB-отдел",
                Keywords =
                [
                    new Keyword() { Text = "веб" },
                    new Keyword() { Text = "web" },
                    new Keyword() { Text = "сайт" },
                    new Keyword() { Text = "дизайн" },
                    new Keyword() { Text = "програм" },
                    new Keyword() { Text = "оптимиз" },
                    new Keyword() { Text = "SEO" },
                    new Keyword() { Text = "СЕО" },
                    new Keyword() { Text = "разработ" },
                ]
            },
            new Department()
            {
                Title = "Отдел продаж",
                Keywords =
                [
                    new Keyword() { Text = "продукт" },
                    new Keyword() { Text = "клиент" },
                    new Keyword() { Text = "коммер" },
                    new Keyword() { Text = "маркет" },
                    new Keyword() { Text = "сделк" },
                    new Keyword() { Text = "цен" },
                    new Keyword() { Text = "выручка" },
                    new Keyword() { Text = "реклам" },
                ]
            },
            new Department()
            {
                Title = "Отдел кадров",
                Keywords =
                [
                    new Keyword() { Text = "карьер" },
                    new Keyword() { Text = "труд" },
                    new Keyword() { Text = "документ" },
                    new Keyword() { Text = "найм" },
                    new Keyword() { Text = "работу" },
                    new Keyword() { Text = "устроит" },
                    new Keyword() { Text = "работник" },
                ]
            },
            new Department()
            {
                Title = "Бухгалтерия",
                Keywords =
                [
                    new Keyword() { Text = "финанс" },
                    new Keyword() { Text = "отчёт" },
                    new Keyword() { Text = "баланс" },
                    new Keyword() { Text = "налог" },
                    new Keyword() { Text = "зарплат" },
                    new Keyword() { Text = "расход" },
                    new Keyword() { Text = "доход" },
                    new Keyword() { Text = "договор" },
                ]
            },
            new Department()
            {
                Title = "Отдел внедрения",
                Keywords =
                [
                    new Keyword() { Text = "1с" },
                    new Keyword() { Text = "установ" },
                    new Keyword() { Text = "подключ" },
                    new Keyword() { Text = "внедр" },
                    new Keyword() { Text = "програм" },
                ]
            },
            new Department()
            {
                Title = "Отдел сопровождения",
                Keywords =
                [
                    new Keyword() { Text = "сломал" },
                    new Keyword() { Text = "упал" },
                    new Keyword() { Text = "не работ" },
                    new Keyword() { Text = "поддержка" },
                    new Keyword() { Text = "администри" },
                    new Keyword() { Text = "запус" },
                    new Keyword() { Text = "развер" },
                    new Keyword() { Text = "верси" },
                    new Keyword() { Text = "обнов" },
                ]
            },
            new Department()
            {
                Title = "Администрация офиса",
                Keywords =
                [
                    new Keyword() { Text = "босс" },
                    new Keyword() { Text = "менеджер" },
                    new Keyword() { Text = "глав" },
                    new Keyword() { Text = "администрац" },
                ]
            },
        ];

        Department[] existingDepartments = context.Departments.ToArray();

        foreach (Department department in departments)
        {
            if (!existingDepartments.Any(e => e.Title == department.Title))
            {
                context.Departments.Add(department);
            }
        }
    }
}

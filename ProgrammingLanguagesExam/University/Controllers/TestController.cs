using Microsoft.AspNetCore.Mvc;
using University.DataAccess;

namespace University.Controllers;

[Route("{controller}")]
public class TeacherController
{
    private readonly UniversityDbContext _universityDbContext;

    public TeacherController(UniversityDbContext universityDbContext) => _universityDbContext = universityDbContext;

    [HttpGet]
    public string Test(string message) => $"Echo reply for: {message}";
}
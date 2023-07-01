using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.DataAccess;

namespace University.Controllers;

//for grade 5
[Route("{controller}")]
public class TestController : ControllerBase
{
    private readonly UniversityDbContext _universityDbContext;

    public TestController(UniversityDbContext universityDbContext) => _universityDbContext = universityDbContext;

    [HttpGet]
    public string Test(string message) => $"Test: database contains {_universityDbContext.Teachers.Count()} teachers with {_universityDbContext.Skills.Count()} skills in total";


	[HttpGet("/Teacher/All")]
	public IActionResult GetAllTeachers()
	{
		return Ok(_universityDbContext.Teachers.ToList());
	}

	[HttpGet("/Teacher/{id}")]
	public IActionResult GetTeacher(int id)
	{
		Teacher teacher = _universityDbContext.Teachers.Include(t => t.Skills).FirstOrDefault(t => t.Id == id);

		if (teacher == null)
		{
			return NotFound();
		}

		return Ok(teacher);
	}

	[HttpPost("/Teacher")]
	public IActionResult CreateTeacher([FromBody] Teacher InputTeacher)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		// Create the teacher
		Teacher teacher = new Teacher
		{
			FirstName = InputTeacher.FirstName,
			LastName = InputTeacher.LastName,
			Age = InputTeacher.Age,
			Skills = new List<Skill>()
		};

		foreach (Skill skillName in InputTeacher.Skills)
		{
			Skill existingSkill = _universityDbContext.Skills.FirstOrDefault(s => s.Name == skillName.Name);
			if (existingSkill != null)
			{
				teacher.Skills.Add(existingSkill);
			}
			else
			{
				Skill newSkill = new Skill
				{
					Name = skillName.Name
				};
				teacher.Skills.Add(newSkill);
			}
		}
		_universityDbContext.Teachers.Add(teacher);
		_universityDbContext.SaveChanges();

		return Created("/Teacher/" + teacher.Id, teacher);
	}

	[HttpPut("/Teacher/{id}")]
	public IActionResult UpdateTeacher(int id, [FromBody] Teacher inputTeacher)
	{
		Teacher teacher = _universityDbContext.Teachers.Include(t => t.Skills).FirstOrDefault(t => t.Id == id);
		if (teacher == null)
		{
			return NotFound();
		}

		teacher.FirstName = inputTeacher.FirstName;

		teacher.LastName = inputTeacher.LastName;

		teacher.Age = inputTeacher.Age;

		teacher.Skills.Clear();

		foreach (Skill skillName in inputTeacher.Skills)
		{
			Skill skill = new Skill
			{
				Name = skillName.Name,
				TeacherId = teacher.Id
			};
			teacher.Skills.Add(skill);
		}

		_universityDbContext.SaveChanges();

		return Ok(teacher);
	}

	[HttpDelete("/Teacher/{id}")]
	public IActionResult DeleteTeacher(int id)
	{
		Teacher teacher = _universityDbContext.Teachers.FirstOrDefault(t => t.Id == id);
		if (teacher == null)
		{
			return NotFound();
		}

		_universityDbContext.Teachers.Remove(teacher);
		_universityDbContext.SaveChanges();

		return NoContent();
	}
}
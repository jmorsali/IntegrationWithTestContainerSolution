using API.Domain;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Database;

public class DatabaseInitializer
{
    private readonly SaleDbStore _context;

    public DatabaseInitializer(SaleDbStore context)
    {
        _context = context;
    }
    public  void Initialize()
    {
        _context.Database.EnsureCreated();
        //_context.Database.Migrate();


        //Look for any students.
        if (_context.Students.Any())
        {
            return;   // DB has been seeded
        }

        var students = new Student[]
        {
            new(){FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new() { FirstMidName = "Meredith", LastName = "Alonso", EnrollmentDate = DateTime.Parse("2002-09-01") },
            new() { FirstMidName = "Arturo", LastName = "Anand", EnrollmentDate = DateTime.Parse("2003-09-01") },
            new() { FirstMidName = "Gytis", LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2002-09-01") },
            new() { FirstMidName = "Yan", LastName = "Li", EnrollmentDate = DateTime.Parse("2002-09-01") },
            new() { FirstMidName = "Peggy", LastName = "Justice", EnrollmentDate = DateTime.Parse("2001-09-01") },
            new() { FirstMidName = "Laura", LastName = "Norman", EnrollmentDate = DateTime.Parse("2003-09-01") },
            new() { FirstMidName = "Nino", LastName = "Olivetto", EnrollmentDate = DateTime.Parse("2005-09-01") }
        };
        foreach (Student s in students)
        {
            _context.Students.Add(s);
        }
        _context.SaveChanges();

        var courses = new Course[]
        {
            new() {CourseID=1050,Title="Chemistry",Credits=3},
            new() {CourseID=4022,Title="Microeconomics",Credits=3},
            new() { CourseID = 4041, Title = "Macroeconomics", Credits = 3 },
            new (){CourseID=1045,Title="Calculus",Credits=4},
            new (){CourseID=3141,Title="Trigonometry",Credits=4},
            new (){CourseID=2021,Title="Composition",Credits=3},
            new (){CourseID=2042,Title="Literature",Credits=4}
        };
        foreach (Course c in courses)
        {
            _context.Courses.Add(c);
        }
        _context.SaveChanges();

        var enrollments = new Enrollment[]
        {
            new (){StudentID=1,CourseID=1050,Grade=Grade.A},
            new() { StudentID = 1, CourseID = 4022, Grade = Grade.C },
            new() { StudentID = 1, CourseID = 4041, Grade = Grade.B },
            new() { StudentID = 2, CourseID = 1045, Grade = Grade.B },
            new() { StudentID = 2, CourseID = 3141, Grade = Grade.F },
            new() { StudentID = 2, CourseID = 2021, Grade = Grade.F },
            new(){StudentID=3,CourseID=1050},
            new() { StudentID = 4, CourseID = 1050 },
            new() { StudentID = 4, CourseID = 4022, Grade = Grade.F },
            new() { StudentID = 5, CourseID = 4041, Grade = Grade.C },
            new() { StudentID = 6, CourseID = 1045 },
            new() { StudentID = 7, CourseID = 3141, Grade = Grade.A },
        };
        foreach (Enrollment e in enrollments)
        {
            _context.Enrollments.Add(e);
        }
        _context.SaveChanges();


        //var customers = new Customer[]
        //{
        //    new(){ FullName="Carson", GitHubUsername="Alexander", Email="", DateOfBirth=DateTime.Parse("2005-09-01")},
        //    new() {  FullName = "Meredith", GitHubUsername = "Alonso", Email="",  DateOfBirth = DateTime.Parse("2002-09-01") },
        //    new() { FullName = "Arturo", GitHubUsername = "Anand",Email="", DateOfBirth = DateTime.Parse("2003-09-01") },
        //    new() { FullName = "Gytis", GitHubUsername = "Barzdukas",Email="", DateOfBirth = DateTime.Parse("2002-09-01") },
        //    new() { FullName = "Yan", GitHubUsername = "Li",Email="", DateOfBirth = DateTime.Parse("2002-09-01") },
        //    new() { FullName = "Peggy", GitHubUsername = "Justice",Email="", DateOfBirth = DateTime.Parse("2001-09-01") },
        //    new() { FullName = "Laura", GitHubUsername = "Norman", Email = "", DateOfBirth = DateTime.Parse("2003-09-01") },
        //    new() {FullName = "Nino", GitHubUsername = "Olivetto", Email = "", DateOfBirth = DateTime.Parse("2005-09-01")}
        //};
        //foreach (Customer c in customers)
        //{
        //    _context.Customers.Add(c);
        //}
        //_context.SaveChanges();
    }
}


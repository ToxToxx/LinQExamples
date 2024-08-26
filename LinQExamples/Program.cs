// Примеры данных
using LinQExamples;

var users = new List<User>
            {
                new() { Id = 1, Name = "Alice", Age = 30 },
                new() { Id = 2, Name = "Bob", Age = 25 },
                new() { Id = 3, Name = "Charlie", Age = 35 }
            };

var orders = new List<Order>
            {
                new() { Id = 1, UserId = 1, Amount = 250 },
                new() { Id = 2, UserId = 2, Amount = 150 },
                new() { Id = 3, UserId = 1, Amount = 50 },
                new() { Id = 4, UserId = 3, Amount = 500 }
            };

// Примеры использования LINQ
Console.WriteLine("SIMPLE EXAMPLES");
ShowWhereExample(users);
ShowSelectExample(users);
ShowOrderByExample(users);
ShowGroupByExample(orders);
ShowJoinExample(users, orders);
ShowAggregateExample(orders);
ShowFirstExample(users);
ShowDistinctExample();
ShowSetOperationsExample();

#region simple linq operations
// Where
static void ShowWhereExample(List<User> users)
{
    var adults = users.Where(u => u.Age >= 30);
    Console.WriteLine("Users aged 30 or older:");
    foreach (var user in adults)
    {
        Console.WriteLine($"{user.Name}, {user.Age}");
    }
}

// Select
static void ShowSelectExample(List<User> users)
{
    var names = users.Select(u => u.Name);
    Console.WriteLine("\nUser names:");
    foreach (var name in names)
    {
        Console.WriteLine(name);
    }
}

// OrderBy
static void ShowOrderByExample(List<User> users)
{
    var sortedUsers = users.OrderBy(u => u.Age);
    Console.WriteLine("\nUsers sorted by age:");
    foreach (var user in sortedUsers)
    {
        Console.WriteLine($"{user.Name}, {user.Age}");
    }
}

// GroupBy
static void ShowGroupByExample(List<Order> orders)
{
    var ordersByUser = orders.GroupBy(o => o.UserId);
    Console.WriteLine("\nOrders grouped by UserId:");
    foreach (var group in ordersByUser)
    {
        Console.WriteLine($"User {group.Key}:");
        foreach (var order in group)
        {
            Console.WriteLine($"  Order {order.Id}, Amount: {order.Amount}");
        }
    }
}

// Join
static void ShowJoinExample(List<User> users, List<Order> orders)
{
    var userOrders = users.Join(
        orders,
        u => u.Id,
        o => o.UserId,
        (u, o) => new { u.Name, o.Amount });

    Console.WriteLine("\nJoin Users and Orders:");
    foreach (var item in userOrders)
    {
        Console.WriteLine($"{item.Name}, Order Amount: {item.Amount}");
    }
}

// Aggregate
static void ShowAggregateExample(List<Order> orders)
{
    var totalAmount = orders.Aggregate(0, (sum, order) => sum + order.Amount);
    Console.WriteLine($"\nTotal order amount: {totalAmount}");
}

// First
static void ShowFirstExample(List<User> users)
{
    var firstUser = users.First();
    Console.WriteLine($"\nFirst user: {firstUser.Name}, {firstUser.Age}");
}

// Distinct
static void ShowDistinctExample()
{
    var numbers = new List<int> { 1, 2, 2, 3, 4, 4, 5 };
    var distinctNumbers = numbers.Distinct();
    Console.WriteLine("\nDistinct numbers:");
    foreach (var number in distinctNumbers)
    {
        Console.WriteLine(number);
    }
}

// Set operations: Union, Intersect, Except
static void ShowSetOperationsExample()
{
    var set1 = new List<int> { 1, 2, 3 };
    var set2 = new List<int> { 3, 4, 5 };

    var union = set1.Union(set2);
    var intersect = set1.Intersect(set2);
    var except = set1.Except(set2);

    Console.WriteLine("\nUnion of sets:");
    foreach (var number in union)
    {
        Console.WriteLine(number);
    }

    Console.WriteLine("\nIntersect of sets:");
    foreach (var number in intersect)
    {
        Console.WriteLine(number);
    }

    Console.WriteLine("\nExcept of sets:");
    foreach (var number in except)
    {
        Console.WriteLine(number);
    }
}

#endregion

Console.WriteLine();
Console.WriteLine("COMPLEX EXAMPLES");

#region Aggregation GroupBy
var employees = new List<Employee>
{
    new Employee { Name = "John", Department = "HR" },
    new Employee { Name = "Jane", Department = "IT" },
    new Employee { Name = "Sam", Department = "HR" },
    new Employee { Name = "Sara", Department = "IT" }
};

var departmentGroups = employees
    .GroupBy(e => e.Department)
    .Select(g => new
    {
        Department = g.Key,
        EmployeeCount = g.Count()
    })
    .ToList();

foreach (var department in departmentGroups)
{
    Console.WriteLine(department);
}
Console.WriteLine();
#endregion

#region Inner LinQ Operations
var students = new List<Student>
{
    new Student { Name = "John", Grades = new List<int> { 85, 92, 78 } },
    new Student { Name = "Jane", Grades = new List<int> { 88, 79, 91 } },
    new Student { Name = "Sam", Grades = new List<int> { 60, 70, 68 } }
};

var topStudents = students
    .Where(s => s.Grades.Any(g => g > 90))
    .ToList();

foreach (var student in topStudents)
{
    Console.WriteLine(student.Name);
}
Console.WriteLine();
#endregion

#region GroupBy and Join
var courseStudents = new List<CourseStudents>
{
    new() { Id = 1, Name = "John" },
    new() { Id = 2, Name = "Jane" }
};

var courses = new List<Course>
{
    new Course { StudentId = 1, CourseName = "Math" },
    new Course { StudentId = 1, CourseName = "Science" },
    new Course { StudentId = 2, CourseName = "History" }
};

var studentCourses = courseStudents
    .Join(courses, s => s.Id, c => c.StudentId, (s, c) => new
    {
        StudentName = s.Name,
        CourseName = c.CourseName
    })
    .ToList();

foreach(var student in studentCourses)
{
    Console.WriteLine($"{student.StudentName} : {student.CourseName} ");
}
#endregion

Console.WriteLine();
Console.WriteLine("LIVE EXAMPLES ");

IEnumerable<int> collection = [1, 2, 3, 4, 5];
IEnumerable<string> collection2 = ["a", "b", "c", "d", "f"];

var resultsLINQ = collection.Select((x,i) => $"{i} {x}");

foreach(var result in resultsLINQ)
{
    Console.WriteLine(result);
}

Console.WriteLine("ZIP");
var resultsZip = collection.Zip(collection2);

foreach(var result in resultsZip)
{

    Console.WriteLine(result);
}

Console.WriteLine("\nJoin");

IEnumerable<Person> collectionPerson = [new(0, "Matvey", 22), new(1, "Tanya", 20)];
IEnumerable<Product> collectionProduct = [new(0, "Chitos"), new(0, "Doritos"), new(1, "Lays")];

var customersBoughtsSeparate = collectionPerson.Join(
    collectionProduct,
    person => person.Id,
    product => product.PersonId,
    (person, product) => $"{person.Name} bought {product.Name}");

foreach (var customer in customersBoughtsSeparate)
{

    Console.WriteLine(customer);
}

Console.WriteLine("\nGroup Join");
var customersBoughtsAll = collectionPerson.GroupJoin(
    collectionProduct,
    person => person.Id,
    product => product.PersonId,
    (person, products) =>
    $"{person.Name} " +
    $"bought " +
    $"{string.Join(',', products)}");

foreach (var customer in customersBoughtsAll)
{

    Console.WriteLine(customer);
}

Console.WriteLine("\nOrder By");
var orderedCustomers = collectionPerson.OrderBy(x => x.Age);

foreach (var customer in orderedCustomers)
{

    Console.WriteLine(customer);
}
record Person (int Id, string Name, int Age);
record Product (int PersonId, string Name);
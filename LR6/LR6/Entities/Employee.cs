namespace LR6.Entities
{
    public class Employee
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int ExperienceYears { get; set; }
        public bool IsManager { get; set; }

        public Employee()
        {
        }

        public Employee(string name, int age, int experienceYears, bool isManager)
        {
            Name = name;
            Age = age;
            ExperienceYears = experienceYears;
            IsManager = isManager;
        }
    }
}

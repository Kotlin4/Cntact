namespace Cntact.Models
{
    public class Contacts
    {
        public Contacts(string number, string firstName, string name, string? lastName) 
        {
            Number = number;
            FirstName = firstName;
            Name = name;
            LastName = lastName;
        }

        public Guid Id { get; init; }
        public string Number { get; init; }
        public string FirstName { get; init; }
        public string Name { get; init; }
        public string? LastName { get; init; }
        
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain;

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string GitHubUsername { get; set; } = default!;

    public string FullName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public DateTime DateOfBirth { get; set; } = default!;
}


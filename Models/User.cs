using System;
using System.ComponentModel.DataAnnotations;

namespace Ynov_Workshare.Models;

public class User
{
    public Guid Id { get; set; }

    [EmailAddress] [MaxLength(50)] public string Email { get; set; } = String.Empty;
    
    [MaxLength(50)]
    public string Pseudo { get; set; } = String.Empty;
    
    public string Avatar { get; set; } = String.Empty;
    
    
    [MaxLength(30)]
    public string FirstName { get; set; } = String.Empty;
    
    [MaxLength(30)]
    public string LastName { get; set; } = String.Empty;
    
    public string Token { get; set; } = String.Empty;
    
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set;}
}
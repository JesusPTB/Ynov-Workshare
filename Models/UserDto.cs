using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ynov_Workshare.Models;

public class UserDto
{
    public Guid Id { get; set; }
    
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; }
    
    [MaxLength(50)]
    public string Pseudo { get; set; }
    
    public string Avatar { get; set; }
    
    
    [MaxLength(30)]
    public string FirstName { get; set; }
    
    [MaxLength(30)]
    public string LastName { get; set; }
    
    public string Token { get; set; } = "";
    
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set;}
    
    //public ICollection<UserChannel>? UserChannels { get; set; }

    public UserDto()
    {

    }
    
  

    
   
    
}


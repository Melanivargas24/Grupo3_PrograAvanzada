using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CasoPractico.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Pass { get; set; } = null!; //Contiene el Hash

    public byte[]? Imagen { get; set; } = null!;

    private static readonly PasswordHasher<User> hasher = new PasswordHasher<User>();

    public void SetPassword(string password)
    {
        this.Pass = hasher.HashPassword(this, password);
    }

    public bool VerifyPassword(string password)
    {
        return hasher.VerifyHashedPassword(this, this.Pass, password) == PasswordVerificationResult.Success;
    }
}

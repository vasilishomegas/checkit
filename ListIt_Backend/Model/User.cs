using System;
using ListIt_Backend.Models;

public class User
{
    public int Id { get; set; }
    public virtual Country Country { get; set; }
    public virtual Language Language { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime Timestamp { get; set; }
}
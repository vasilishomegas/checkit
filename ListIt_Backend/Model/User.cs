using System;

public class User
{
    private int Id { get; set; }
    private Country Country { get; set; }
    private Language Language { get; set; }
    private string Nickname { get; set; }
    private string Email { get; set; }
    private string PasswordHash { get; set; }
    //private string SocialMediaLogIn { get; set; }
    private DateTime Timestamp { get; set; }
}

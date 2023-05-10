// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using System;
using TalentManagement.Core.Models.Foundations.Users;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Country { get; set; }
    public string PhoneNumber { get; set; }
    public string PlaceOfStudy { get; set; }
    public string DirectionOfStudy { get; set; }
    public Degree Degree { get; set; }
    public UserDirection UserDirection { get; set; }
    public UserSkills UserSkills { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }
}
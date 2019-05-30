using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Models
{
    public class FullEnrollmentData
    {
        public int EnrollmentID { get; set; } = 0;
        public int InsurancePlanID { get; set; } = 0;
        public int SubscriberID { get; set; } = 0;
        public int PlanYear { get; set; } = 0;
        public string ConfirmationCode { get; set; }
        public DateTime? TimeStamp { get; set; } = DateTime.Now;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string County { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string SocialSecurityNumber { get; set; } = string.Empty;
        public bool IsMilitary { get; set; } = false;
        public bool IsOnDisability { get; set; } = false;
        public bool IsOnMedicare { get; set; } = false;
        public bool IsStudent { get; set; } = false;
        public bool IsUSCitizen { get; set; } = false;
        public double AlimonyChildSupport { get; set; } = 0;
        public double EmploymentIncome { get; set; } = 0;
        public double InvestmentIncome { get; set; } = 0;
        public int HouseholdMemberID { get; set; } = 0;
        public Relationship Relationship { get; set; } = Relationship.Applicant;
        public string DateOfBirth { get; set; } = string.Empty;
        public Gender Gender { get; set; } = Gender.Male;
        public TobaccoUse TobaccoUse { get; set; } = TobaccoUse.Never;


        public static FullEnrollmentData GetFullEnrollmentData(InsuranceContext _context, int enrollmentID)
        {
            /*
                SELECT *
                FROM [dbo].[Enrollments] e
                JOIN [dbo].[Subscribers] s ON e.SubscriberID = s.SubscriberID
                JOIN [dbo].[HouseholdMembers] h ON s.SubscriberID = h.SubscriberID
            */

            var enrollments = (from e in _context.Enrollments
                               join s in _context.Subscribers on e.SubscriberID equals s.SubscriberID
                               join h in _context.HouseholdMembers on s.SubscriberID equals h.SubscriberID
                               where e.EnrollmentID == enrollmentID
                               select new FullEnrollmentData
                               {
                                   EnrollmentID = e.EnrollmentID,
                                   InsurancePlanID = e.InsurancePlanID,
                                   PlanYear = e.PlanYear,
                                   SubscriberID = e.SubscriberID,
                                   ConfirmationCode = e.ConfirmationCode,
                                   TimeStamp = e.TimeStamp,
                                   FirstName = s.FirstName,
                                   MiddleName = s.MiddleName,
                                   LastName = s.LastName,
                                   AddressLine1 = s.AddressLine1,
                                   AddressLine2 = s.AddressLine2,
                                   City = s.City,
                                   State = s.State,
                                   ZipCode = s.ZipCode,
                                   County = s.County,
                                   EmailAddress = s.EmailAddress,
                                   PhoneNumber = s.PhoneNumber,
                                   SocialSecurityNumber = s.SocialSecurityNumber,
                                   EmploymentIncome = s.EmploymentIncome,
                                   InvestmentIncome = s.InvestmentIncome,
                                   IsMilitary = s.IsMilitary,
                                   IsOnDisability = s.IsOnDisability,
                                   IsOnMedicare = s.IsOnMedicare,
                                   IsStudent = s.IsStudent,
                                   IsUSCitizen = s.IsUSCitizen,
                                   AlimonyChildSupport = s.AlimonyChildSupport,
                                   HouseholdMemberID = h.HouseholdMemberID,
                                   Relationship = h.Relationship,
                                   DateOfBirth = h.DateOfBirth,
                                   Gender = h.Gender,
                                   TobaccoUse = h.TobaccoUse
                               });

            return enrollments.First();
        }

        public static IQueryable<FullEnrollmentData> GetFullEnrollmentData(InsuranceContext _context)
        {
            /*
                SELECT *
                FROM [dbo].[Enrollments] e
                JOIN [dbo].[Subscribers] s ON e.SubscriberID = s.SubscriberID
                JOIN [dbo].[HouseholdMembers] h ON s.SubscriberID = h.SubscriberID
            */

            var enrollments = (from e in _context.Enrollments
                               join s in _context.Subscribers on e.SubscriberID equals s.SubscriberID
                               join h in _context.HouseholdMembers on s.SubscriberID equals h.SubscriberID
                               select new FullEnrollmentData
                               {
                                   EnrollmentID = e.EnrollmentID,
                                   InsurancePlanID = e.InsurancePlanID,
                                   PlanYear = e.PlanYear,
                                   SubscriberID = e.SubscriberID,
                                   ConfirmationCode = e.ConfirmationCode,
                                   TimeStamp = e.TimeStamp,
                                   FirstName = s.FirstName,
                                   MiddleName = s.MiddleName,
                                   LastName = s.LastName,
                                   AddressLine1 = s.AddressLine1,
                                   AddressLine2 = s.AddressLine2,
                                   City = s.City,
                                   State = s.State,
                                   ZipCode = s.ZipCode,
                                   County = s.County,
                                   EmailAddress = s.EmailAddress,
                                   PhoneNumber = s.PhoneNumber,
                                   SocialSecurityNumber = s.SocialSecurityNumber,
                                   EmploymentIncome = s.EmploymentIncome,
                                   InvestmentIncome = s.InvestmentIncome,
                                   IsMilitary = s.IsMilitary,
                                   IsOnDisability = s.IsOnDisability,
                                   IsOnMedicare = s.IsOnMedicare,
                                   IsStudent = s.IsStudent,
                                   IsUSCitizen = s.IsUSCitizen,
                                   AlimonyChildSupport = s.AlimonyChildSupport,
                                   HouseholdMemberID = h.HouseholdMemberID,
                                   Relationship = h.Relationship,
                                   DateOfBirth = h.DateOfBirth,
                                   Gender = h.Gender,
                                   TobaccoUse = h.TobaccoUse
                               });

            return enrollments;
        }
    }
}

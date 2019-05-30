IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [dbo].[InsurancePlans](
	[InsurancePlanId] [int] IDENTITY(1,1) NOT NULL,
	[ERVisitAfterDeductible] [float] NOT NULL,
	[FamilyDeductible] [float] NOT NULL,
	[FamilyOutOfPocketMax] [float] NOT NULL,
	[FreePrimaryCareVisits] [int] NOT NULL,
	[IndividualDeductible] [float] NOT NULL,
	[IndividualOutOfPocketMax] [float] NOT NULL,
	[Level] [int] NOT NULL,
	[PlanName] [nvarchar](max) NULL,
	[Premium] [float] NOT NULL,
	[PrimaryCareVisitCostAfterDeductible] [float] NOT NULL,
	[IsSpecial] [bit] NOT NULL,
 CONSTRAINT [PK_InsurancePlans] PRIMARY KEY CLUSTERED 
(
	[InsurancePlanId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[InsurancePlans] ADD  CONSTRAINT [DF_InsurancePlans_IsSpecial]  DEFAULT ((0)) FOR [IsSpecial]
GO

CREATE TABLE [dbo].[Subscribers](
	[SubscriberID] [int] IDENTITY(1,1) NOT NULL,
	[AddressLine1] [nvarchar](max) NULL,
	[AddressLine2] [nvarchar](max) NULL,
	[AlimonyChildSupport] [float] NOT NULL,
	[City] [nvarchar](max) NULL,
	[County] [nvarchar](max) NULL,
	[EmailAddress] [nvarchar](max) NULL,
	[EmploymentIncome] [float] NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[InvestmentIncome] [float] NOT NULL,
	[IsMilitary] [bit] NOT NULL,
	[IsOnDisability] [bit] NOT NULL,
	[IsOnMedicare] [bit] NOT NULL,
	[IsStudent] [bit] NOT NULL,
	[IsUSCitizen] [bit] NOT NULL,
	[LastName] [nvarchar](max) NULL,
	[MiddleName] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[SocialSecurityNumber] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[ZipCode] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Subscribers] PRIMARY KEY CLUSTERED 
(
	[SubscriberID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Subscribers] ADD  CONSTRAINT [DF_Subscribers_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO

CREATE TABLE [dbo].[Enrollments](
	[EnrollmentID] [int] IDENTITY(1,1) NOT NULL,
	[InsurancePlanID] [int] NOT NULL,
	[PlanYear] [int] NOT NULL,
	[SubscriberID] [int] NOT NULL,
	[ConfirmationCode] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NOT NULL,
 CONSTRAINT [PK_Enrollments] PRIMARY KEY CLUSTERED 
(
	[EnrollmentID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Enrollments] ADD  CONSTRAINT [DF_Enrollments_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO

CREATE TABLE [dbo].[HouseholdMembers](
	[HouseholdMemberID] [int] IDENTITY(1,1) NOT NULL,
	[DateOfBirth] [nvarchar](max) NULL,
	[Gender] [int] NOT NULL,
	[Relationship] [int] NOT NULL,
	[SubscriberID] [int] NULL,
	[TobaccoUse] [int] NOT NULL,
 CONSTRAINT [PK_HouseholdMembers] PRIMARY KEY CLUSTERED 
(
	[HouseholdMemberID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE INDEX [IX_Enrollments_InsurancePlanID] ON [Enrollments] ([InsurancePlanID]);

GO

CREATE INDEX [IX_Enrollments_SubscriberID] ON [Enrollments] ([SubscriberID]);

GO

CREATE INDEX [IX_HouseholdMembers_SubscriberID] ON [HouseholdMembers] ([SubscriberID]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170705101701_InitialCreate', N'1.1.2');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170706005142_IsSpecial', N'1.1.2');

GO

ALTER TABLE [Enrollments] ADD [ConfirmationCode] nvarchar(max);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170706030119_ConfirmationCode', N'1.1.2');

GO


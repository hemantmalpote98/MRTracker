IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'MRTracking') IS NULL EXEC(N'CREATE SCHEMA [MRTracking];');
GO

CREATE TABLE [MRTracking].[Doctor] (
    [DoctorId] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(100) NOT NULL,
    [MiddleName] nvarchar(100) NULL,
    [LastName] nvarchar(100) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Gender] nvarchar(10) NOT NULL,
    [Phone] nvarchar(15) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [Address] nvarchar(200) NOT NULL,
    [MedicalLicenseNumber] nvarchar(50) NOT NULL,
    [Specialty] nvarchar(100) NOT NULL,
    [HospitalAffiliation] nvarchar(100) NOT NULL,
    [Department] nvarchar(100) NOT NULL,
    [DateOfJoining] datetime2 NOT NULL,
    [HighestDegree] nvarchar(100) NOT NULL,
    [FieldOfStudy] nvarchar(100) NOT NULL,
    [MedicalSchool] nvarchar(100) NOT NULL,
    [GraduationYear] int NOT NULL,
    [AvailabilityForConsultation] bit NOT NULL,
    [OfficeHours] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Doctor] PRIMARY KEY ([DoctorId])
);
GO

CREATE TABLE [MRTracking].[MedicalRepresentative] (
    [MedicalRepresentativeId] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(100) NOT NULL,
    [MiddleName] nvarchar(100) NULL,
    [LastName] nvarchar(100) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Gender] nvarchar(10) NOT NULL,
    [Phone] nvarchar(15) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [Address] nvarchar(200) NOT NULL,
    [JobTitle] nvarchar(100) NOT NULL,
    [Department] nvarchar(100) NOT NULL,
    [EmployeeID] nvarchar(max) NOT NULL,
    [DateOfJoining] datetime2 NOT NULL,
    [LocationAssigned] nvarchar(100) NOT NULL,
    [ReportingManager] nvarchar(100) NOT NULL,
    [HighestDegree] nvarchar(100) NOT NULL,
    [FieldOfStudy] nvarchar(100) NOT NULL,
    [InstitutionName] nvarchar(100) NOT NULL,
    [GraduationYear] int NOT NULL,
    [AvailabilityForTravel] bit NOT NULL,
    [DriversLicense] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_MedicalRepresentative] PRIMARY KEY ([MedicalRepresentativeId])
);
GO

CREATE TABLE [MRTracking].[MedicalRepresentativeVisit] (
    [VisitId] uniqueidentifier NOT NULL,
    [MedicalRepresentativeId] uniqueidentifier NOT NULL,
    [DoctorId] uniqueidentifier NOT NULL,
    [VisitDate] datetime2 NOT NULL,
    [Purpose] nvarchar(200) NOT NULL,
    [Notes] nvarchar(500) NOT NULL,
    [FollowUpRequired] bit NOT NULL,
    [FollowUpDate] datetime2 NULL,
    CONSTRAINT [PK_MedicalRepresentativeVisit] PRIMARY KEY ([VisitId]),
    CONSTRAINT [FK_MedicalRepresentativeVisit_Doctor_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [MRTracking].[Doctor] ([DoctorId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MedicalRepresentativeVisit_MedicalRepresentative_MedicalRepresentativeId] FOREIGN KEY ([MedicalRepresentativeId]) REFERENCES [MRTracking].[MedicalRepresentative] ([MedicalRepresentativeId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MedicalRepresentativeVisit_DoctorId] ON [MRTracking].[MedicalRepresentativeVisit] ([DoctorId]);
GO

CREATE INDEX [IX_MedicalRepresentativeVisit_MedicalRepresentativeId] ON [MRTracking].[MedicalRepresentativeVisit] ([MedicalRepresentativeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240712095135_InitialCreate', N'9.0.0-preview.1.24081.2');
GO

COMMIT;
GO


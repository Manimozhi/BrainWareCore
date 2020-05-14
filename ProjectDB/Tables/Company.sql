CREATE TABLE [dbo].[Company]
(
	[CompanyId] UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    [Name] NCHAR(128) NOT NULL
)

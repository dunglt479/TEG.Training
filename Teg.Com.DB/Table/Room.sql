CREATE TABLE [dbo].[Room]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(255) NOT NULL,
	[Description] NVARCHAR(4000),
	[IsActive] bit not null,
	[CreatedBy] NVARCHAR(255) not null,
	[CreatedOn] DATETIME not null,
	[UpdatedBy] NVARCHAR(255) not null,
	[UpdatedOn] DATETIME not null
)

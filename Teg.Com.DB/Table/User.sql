CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[UserName] nvarchar(255) not null,
	[Password] nvarchar(255) not null 
)

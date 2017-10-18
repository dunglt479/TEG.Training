CREATE TABLE [dbo].[Booking]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] nvarchar(255) not null,
	[Company] nvarchar(255) not null,
	[Email] nvarchar(255) not null,
	[Phone] nvarchar(20),
	[From] datetime ,
	[To] datetime ,
	[Room] int not null,
	[CreatedBy] NVARCHAR(255) not null,
	[CreatedOn] DATETIME not null,
	[UpdatedBy] NVARCHAR(255) not null,
	[UpdatedOn] DATETIME not null,
	CONSTRAINT [FK_Booking_Room_ToBookingRoom] FOREIGN KEY ([Room]) REFERENCES [Room]([Id])
)

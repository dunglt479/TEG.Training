
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/17/2017 21:19:48
-- Generated from EDMX file: D:\TEG\Tranning\Teg.Com.Data\BookingModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Booking];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Booking_Room_ToBookingRoom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bookings] DROP CONSTRAINT [FK_Booking_Room_ToBookingRoom];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Bookings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bookings];
GO
IF OBJECT_ID(N'[dbo].[Rooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rooms];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Bookings'
CREATE TABLE [dbo].[Bookings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Company] nvarchar(255)  NOT NULL,
    [Email] nvarchar(255)  NOT NULL,
    [Phone] nvarchar(20)  NULL,
    [From] datetime  NULL,
    [To] datetime  NULL,
    [Room] int  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [CreatedBy] nvarchar(255)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [UpdatedBy] nvarchar(255)  NOT NULL,
    [UpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Description] nvarchar(4000)  NULL,
    [IsActive] bit  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [CreatedBy] nvarchar(255)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [UpdatedBy] nvarchar(255)  NOT NULL,
    [UpdatedOn] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(255)  NOT NULL,
    [Password] nvarchar(255)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [PK_Bookings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Room] in table 'Bookings'
ALTER TABLE [dbo].[Bookings]
ADD CONSTRAINT [FK_Booking_Room_ToBookingRoom]
    FOREIGN KEY ([Room])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Booking_Room_ToBookingRoom'
CREATE INDEX [IX_FK_Booking_Room_ToBookingRoom]
ON [dbo].[Bookings]
    ([Room]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
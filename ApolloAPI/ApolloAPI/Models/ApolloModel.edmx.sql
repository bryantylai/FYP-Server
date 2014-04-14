
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/14/2014 15:56:41
-- Generated from EDMX file: C:\Users\Lai\Documents\GitHub\FYP-Server\ApolloAPI\ApolloAPI\Models\ApolloModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ApolloDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserBMI]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BMIs] DROP CONSTRAINT [FK_UserBMI];
GO
IF OBJECT_ID(N'[dbo].[FK_Doctor_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People_Doctor] DROP CONSTRAINT [FK_Doctor_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_Trainer_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People_Trainer] DROP CONSTRAINT [FK_Trainer_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_User_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People_User] DROP CONSTRAINT [FK_User_inherits_Person];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BMIs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMIs];
GO
IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[People_Doctor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People_Doctor];
GO
IF OBJECT_ID(N'[dbo].[People_Trainer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People_Trainer];
GO
IF OBJECT_ID(N'[dbo].[People_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People_User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BMIs'
CREATE TABLE [dbo].[BMIs] (
    [Id] uniqueidentifier  NOT NULL,
    [Height] float  NOT NULL,
    [Weight] float  NOT NULL,
    [RecordTime] datetime  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [id] uniqueidentifier  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [DateOfBirth] datetime  NOT NULL
);
GO

-- Creating table 'People_Doctor'
CREATE TABLE [dbo].[People_Doctor] (
    [id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'People_Trainer'
CREATE TABLE [dbo].[People_Trainer] (
    [id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'People_User'
CREATE TABLE [dbo].[People_User] (
    [id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BMIs'
ALTER TABLE [dbo].[BMIs]
ADD CONSTRAINT [PK_BMIs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'People_Doctor'
ALTER TABLE [dbo].[People_Doctor]
ADD CONSTRAINT [PK_People_Doctor]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'People_Trainer'
ALTER TABLE [dbo].[People_Trainer]
ADD CONSTRAINT [PK_People_Trainer]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'People_User'
ALTER TABLE [dbo].[People_User]
ADD CONSTRAINT [PK_People_User]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'BMIs'
ALTER TABLE [dbo].[BMIs]
ADD CONSTRAINT [FK_UserBMI]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[People_User]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBMI'
CREATE INDEX [IX_FK_UserBMI]
ON [dbo].[BMIs]
    ([UserId]);
GO

-- Creating foreign key on [id] in table 'People_Doctor'
ALTER TABLE [dbo].[People_Doctor]
ADD CONSTRAINT [FK_Doctor_inherits_Person]
    FOREIGN KEY ([id])
    REFERENCES [dbo].[People]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [id] in table 'People_Trainer'
ALTER TABLE [dbo].[People_Trainer]
ADD CONSTRAINT [FK_Trainer_inherits_Person]
    FOREIGN KEY ([id])
    REFERENCES [dbo].[People]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [id] in table 'People_User'
ALTER TABLE [dbo].[People_User]
ADD CONSTRAINT [FK_User_inherits_Person]
    FOREIGN KEY ([id])
    REFERENCES [dbo].[People]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
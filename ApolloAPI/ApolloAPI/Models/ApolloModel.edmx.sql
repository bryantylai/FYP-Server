
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/18/2014 22:16:37
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
IF OBJECT_ID(N'[dbo].[FK_DoctorAppointment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Appointments] DROP CONSTRAINT [FK_DoctorAppointment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAppointment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Appointments] DROP CONSTRAINT [FK_UserAppointment];
GO
IF OBJECT_ID(N'[dbo].[FK_DiscussionComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_DiscussionComment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDiscussion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Discussions] DROP CONSTRAINT [FK_UserDiscussion];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_PersonComment];
GO
IF OBJECT_ID(N'[dbo].[FK_User_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People_User] DROP CONSTRAINT [FK_User_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_Doctor_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People_Doctor] DROP CONSTRAINT [FK_Doctor_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_Trainer_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People_Trainer] DROP CONSTRAINT [FK_Trainer_inherits_Person];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[BMIs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMIs];
GO
IF OBJECT_ID(N'[dbo].[Credentials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Credentials];
GO
IF OBJECT_ID(N'[dbo].[Appointments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Appointments];
GO
IF OBJECT_ID(N'[dbo].[Discussions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Discussions];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[People_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People_User];
GO
IF OBJECT_ID(N'[dbo].[People_Doctor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People_Doctor];
GO
IF OBJECT_ID(N'[dbo].[People_Trainer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People_Trainer];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] uniqueidentifier  NOT NULL,
    [DisplayName] nvarchar(max)  NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [DateOfBirth] datetime  NULL,
    [Gender] smallint  NULL,
    [Phone] nvarchar(max)  NULL
);
GO

-- Creating table 'BMIs'
CREATE TABLE [dbo].[BMIs] (
    [Id] uniqueidentifier  NOT NULL,
    [Height] float  NOT NULL,
    [Weight] float  NOT NULL,
    [RecordTime] datetime  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Credentials'
CREATE TABLE [dbo].[Credentials] (
    [Id] uniqueidentifier  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Role] smallint  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [PersonId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Appointments'
CREATE TABLE [dbo].[Appointments] (
    [Id] uniqueidentifier  NOT NULL,
    [DoctorId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [AppointmentTime] datetime  NOT NULL
);
GO

-- Creating table 'Discussions'
CREATE TABLE [dbo].[Discussions] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] uniqueidentifier  NOT NULL,
    [DiscussionId] uniqueidentifier  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [CommentTime] datetime  NOT NULL,
    [PersonId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'People_User'
CREATE TABLE [dbo].[People_User] (
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'People_Doctor'
CREATE TABLE [dbo].[People_Doctor] (
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'People_Trainer'
CREATE TABLE [dbo].[People_Trainer] (
    [Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BMIs'
ALTER TABLE [dbo].[BMIs]
ADD CONSTRAINT [PK_BMIs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [PersonId] in table 'Credentials'
ALTER TABLE [dbo].[Credentials]
ADD CONSTRAINT [PK_Credentials]
    PRIMARY KEY CLUSTERED ([Id], [PersonId] ASC);
GO

-- Creating primary key on [Id] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [PK_Appointments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Discussions'
ALTER TABLE [dbo].[Discussions]
ADD CONSTRAINT [PK_Discussions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'People_User'
ALTER TABLE [dbo].[People_User]
ADD CONSTRAINT [PK_People_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'People_Doctor'
ALTER TABLE [dbo].[People_Doctor]
ADD CONSTRAINT [PK_People_Doctor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'People_Trainer'
ALTER TABLE [dbo].[People_Trainer]
ADD CONSTRAINT [PK_People_Trainer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'BMIs'
ALTER TABLE [dbo].[BMIs]
ADD CONSTRAINT [FK_UserBMI]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[People_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserBMI'
CREATE INDEX [IX_FK_UserBMI]
ON [dbo].[BMIs]
    ([UserId]);
GO

-- Creating foreign key on [DoctorId] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [FK_DoctorAppointment]
    FOREIGN KEY ([DoctorId])
    REFERENCES [dbo].[People_Doctor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorAppointment'
CREATE INDEX [IX_FK_DoctorAppointment]
ON [dbo].[Appointments]
    ([DoctorId]);
GO

-- Creating foreign key on [UserId] in table 'Appointments'
ALTER TABLE [dbo].[Appointments]
ADD CONSTRAINT [FK_UserAppointment]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[People_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAppointment'
CREATE INDEX [IX_FK_UserAppointment]
ON [dbo].[Appointments]
    ([UserId]);
GO

-- Creating foreign key on [DiscussionId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_DiscussionComment]
    FOREIGN KEY ([DiscussionId])
    REFERENCES [dbo].[Discussions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DiscussionComment'
CREATE INDEX [IX_FK_DiscussionComment]
ON [dbo].[Comments]
    ([DiscussionId]);
GO

-- Creating foreign key on [UserId] in table 'Discussions'
ALTER TABLE [dbo].[Discussions]
ADD CONSTRAINT [FK_UserDiscussion]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[People_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDiscussion'
CREATE INDEX [IX_FK_UserDiscussion]
ON [dbo].[Discussions]
    ([UserId]);
GO

-- Creating foreign key on [PersonId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_PersonComment]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonComment'
CREATE INDEX [IX_FK_PersonComment]
ON [dbo].[Comments]
    ([PersonId]);
GO

-- Creating foreign key on [Id] in table 'People_User'
ALTER TABLE [dbo].[People_User]
ADD CONSTRAINT [FK_User_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'People_Doctor'
ALTER TABLE [dbo].[People_Doctor]
ADD CONSTRAINT [FK_Doctor_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'People_Trainer'
ALTER TABLE [dbo].[People_Trainer]
ADD CONSTRAINT [FK_Trainer_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/08/2014 02:22:56
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

IF OBJECT_ID(N'[dbo].[FK_DoctorAppointment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Appointments] DROP CONSTRAINT [FK_DoctorAppointment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAppointment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Appointments] DROP CONSTRAINT [FK_UserAppointment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDiscussion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Discussions] DROP CONSTRAINT [FK_UserDiscussion];
GO
IF OBJECT_ID(N'[dbo].[FK_DiscussionReply]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Replies] DROP CONSTRAINT [FK_DiscussionReply];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonReply]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Replies] DROP CONSTRAINT [FK_PersonReply];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonCredential]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Credentials] DROP CONSTRAINT [FK_PersonCredential];
GO
IF OBJECT_ID(N'[dbo].[FK_PostComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_PostComment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPost]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_UserPost];
GO
IF OBJECT_ID(N'[dbo].[FK_UserComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_UserComment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserAvatar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Avatars] DROP CONSTRAINT [FK_UserAvatar];
GO
IF OBJECT_ID(N'[dbo].[FK_AvatarRun]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Runs] DROP CONSTRAINT [FK_AvatarRun];
GO
IF OBJECT_ID(N'[dbo].[FK_Doctor_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People_Doctor] DROP CONSTRAINT [FK_Doctor_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_User_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People_User] DROP CONSTRAINT [FK_User_inherits_Person];
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
IF OBJECT_ID(N'[dbo].[Credentials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Credentials];
GO
IF OBJECT_ID(N'[dbo].[Appointments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Appointments];
GO
IF OBJECT_ID(N'[dbo].[Discussions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Discussions];
GO
IF OBJECT_ID(N'[dbo].[Replies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Replies];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Avatars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Avatars];
GO
IF OBJECT_ID(N'[dbo].[Runs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Runs];
GO
IF OBJECT_ID(N'[dbo].[People_Doctor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People_Doctor];
GO
IF OBJECT_ID(N'[dbo].[People_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People_User];
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
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [DateOfBirth] datetime  NULL,
    [Gender] smallint  NULL,
    [Phone] nvarchar(max)  NULL,
    [ProfileImage] nvarchar(max)  NULL
);
GO

-- Creating table 'Credentials'
CREATE TABLE [dbo].[Credentials] (
    [Id] uniqueidentifier  NOT NULL,
    [PersonId] uniqueidentifier  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Role] smallint  NOT NULL,
    [CreatedAt] datetime  NOT NULL
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
    [CreatedAt] datetime  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Replies'
CREATE TABLE [dbo].[Replies] (
    [Id] uniqueidentifier  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [RepliedAt] datetime  NOT NULL,
    [DiscussionId] uniqueidentifier  NOT NULL,
    [PersonId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [PostedAt] datetime  NOT NULL,
    [Photo] nvarchar(max)  NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] uniqueidentifier  NOT NULL,
    [PostId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [CommentedAt] datetime  NOT NULL
);
GO

-- Creating table 'Avatars'
CREATE TABLE [dbo].[Avatars] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Level] int  NOT NULL,
    [Points] int  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Runs'
CREATE TABLE [dbo].[Runs] (
    [Id] uniqueidentifier  NOT NULL,
    [RunningTime] time  NOT NULL,
    [Distance] float  NOT NULL,
    [Point] int  NOT NULL,
    [AvatarId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Scoresheets'
CREATE TABLE [dbo].[Scoresheets] (
    [Id] uniqueidentifier  NOT NULL,
    [TotalDistance] float  NOT NULL,
    [Points] int  NOT NULL
);
GO

-- Creating table 'GameSystems'
CREATE TABLE [dbo].[GameSystems] (
    [Id] uniqueidentifier  NOT NULL,
    [Level] int  NOT NULL,
    [Points] int  NOT NULL
);
GO

-- Creating table 'Gyms'
CREATE TABLE [dbo].[Gyms] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Location] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'People_Doctor'
CREATE TABLE [dbo].[People_Doctor] (
    [FieldOfExpertise] nvarchar(max)  NOT NULL,
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'People_User'
CREATE TABLE [dbo].[People_User] (
    [Status] nvarchar(max)  NULL,
    [Height] float  NULL,
    [Weight] float  NULL,
    [Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'People_Trainer'
CREATE TABLE [dbo].[People_Trainer] (
    [FieldOfExpertise] nvarchar(max)  NOT NULL,
    [GymId] uniqueidentifier  NOT NULL,
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

-- Creating primary key on [Id] in table 'Credentials'
ALTER TABLE [dbo].[Credentials]
ADD CONSTRAINT [PK_Credentials]
    PRIMARY KEY CLUSTERED ([Id] ASC);
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

-- Creating primary key on [Id] in table 'Replies'
ALTER TABLE [dbo].[Replies]
ADD CONSTRAINT [PK_Replies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Avatars'
ALTER TABLE [dbo].[Avatars]
ADD CONSTRAINT [PK_Avatars]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Runs'
ALTER TABLE [dbo].[Runs]
ADD CONSTRAINT [PK_Runs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Scoresheets'
ALTER TABLE [dbo].[Scoresheets]
ADD CONSTRAINT [PK_Scoresheets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GameSystems'
ALTER TABLE [dbo].[GameSystems]
ADD CONSTRAINT [PK_GameSystems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Gyms'
ALTER TABLE [dbo].[Gyms]
ADD CONSTRAINT [PK_Gyms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'People_Doctor'
ALTER TABLE [dbo].[People_Doctor]
ADD CONSTRAINT [PK_People_Doctor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'People_User'
ALTER TABLE [dbo].[People_User]
ADD CONSTRAINT [PK_People_User]
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

-- Creating foreign key on [DiscussionId] in table 'Replies'
ALTER TABLE [dbo].[Replies]
ADD CONSTRAINT [FK_DiscussionReply]
    FOREIGN KEY ([DiscussionId])
    REFERENCES [dbo].[Discussions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DiscussionReply'
CREATE INDEX [IX_FK_DiscussionReply]
ON [dbo].[Replies]
    ([DiscussionId]);
GO

-- Creating foreign key on [PersonId] in table 'Replies'
ALTER TABLE [dbo].[Replies]
ADD CONSTRAINT [FK_PersonReply]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonReply'
CREATE INDEX [IX_FK_PersonReply]
ON [dbo].[Replies]
    ([PersonId]);
GO

-- Creating foreign key on [PersonId] in table 'Credentials'
ALTER TABLE [dbo].[Credentials]
ADD CONSTRAINT [FK_PersonCredential]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonCredential'
CREATE INDEX [IX_FK_PersonCredential]
ON [dbo].[Credentials]
    ([PersonId]);
GO

-- Creating foreign key on [PostId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_PostComment]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostComment'
CREATE INDEX [IX_FK_PostComment]
ON [dbo].[Comments]
    ([PostId]);
GO

-- Creating foreign key on [UserId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_UserPost]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[People_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPost'
CREATE INDEX [IX_FK_UserPost]
ON [dbo].[Posts]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_UserComment]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[People_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserComment'
CREATE INDEX [IX_FK_UserComment]
ON [dbo].[Comments]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Avatars'
ALTER TABLE [dbo].[Avatars]
ADD CONSTRAINT [FK_UserAvatar]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[People_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserAvatar'
CREATE INDEX [IX_FK_UserAvatar]
ON [dbo].[Avatars]
    ([UserId]);
GO

-- Creating foreign key on [AvatarId] in table 'Runs'
ALTER TABLE [dbo].[Runs]
ADD CONSTRAINT [FK_AvatarRun]
    FOREIGN KEY ([AvatarId])
    REFERENCES [dbo].[Avatars]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AvatarRun'
CREATE INDEX [IX_FK_AvatarRun]
ON [dbo].[Runs]
    ([AvatarId]);
GO

-- Creating foreign key on [GymId] in table 'People_Trainer'
ALTER TABLE [dbo].[People_Trainer]
ADD CONSTRAINT [FK_GymTrainer]
    FOREIGN KEY ([GymId])
    REFERENCES [dbo].[Gyms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GymTrainer'
CREATE INDEX [IX_FK_GymTrainer]
ON [dbo].[People_Trainer]
    ([GymId]);
GO

-- Creating foreign key on [Id] in table 'People_Doctor'
ALTER TABLE [dbo].[People_Doctor]
ADD CONSTRAINT [FK_Doctor_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'People_User'
ALTER TABLE [dbo].[People_User]
ADD CONSTRAINT [FK_User_inherits_Person]
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
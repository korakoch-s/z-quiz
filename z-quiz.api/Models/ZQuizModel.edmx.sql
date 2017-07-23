
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/23/2017 11:08:07
-- Generated from EDMX file: C:\Users\Korakoch\documents\visual studio 2017\Projects\z-quiz\z-quiz.api\Models\ZQuizModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ZQuizDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Testers'
CREATE TABLE [dbo].[Testers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [RegistedDate] time  NOT NULL,
    [SubmittedDate] time  NOT NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Choices'
CREATE TABLE [dbo].[Choices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Score] int  NOT NULL,
    [Question_Id] int  NOT NULL
);
GO

-- Creating table 'QuizItems'
CREATE TABLE [dbo].[QuizItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Question_Id] int  NOT NULL,
    [Choice_Id] int  NOT NULL,
    [Tester_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Testers'
ALTER TABLE [dbo].[Testers]
ADD CONSTRAINT [PK_Testers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Choices'
ALTER TABLE [dbo].[Choices]
ADD CONSTRAINT [PK_Choices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuizItems'
ALTER TABLE [dbo].[QuizItems]
ADD CONSTRAINT [PK_QuizItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Question_Id] in table 'Choices'
ALTER TABLE [dbo].[Choices]
ADD CONSTRAINT [FK_QuestionsChoices]
    FOREIGN KEY ([Question_Id])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionsChoices'
CREATE INDEX [IX_FK_QuestionsChoices]
ON [dbo].[Choices]
    ([Question_Id]);
GO

-- Creating foreign key on [Question_Id] in table 'QuizItems'
ALTER TABLE [dbo].[QuizItems]
ADD CONSTRAINT [FK_QuestionsQuizItems]
    FOREIGN KEY ([Question_Id])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionsQuizItems'
CREATE INDEX [IX_FK_QuestionsQuizItems]
ON [dbo].[QuizItems]
    ([Question_Id]);
GO

-- Creating foreign key on [Choice_Id] in table 'QuizItems'
ALTER TABLE [dbo].[QuizItems]
ADD CONSTRAINT [FK_ChoicesQuizItems]
    FOREIGN KEY ([Choice_Id])
    REFERENCES [dbo].[Choices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChoicesQuizItems'
CREATE INDEX [IX_FK_ChoicesQuizItems]
ON [dbo].[QuizItems]
    ([Choice_Id]);
GO

-- Creating foreign key on [Tester_Id] in table 'QuizItems'
ALTER TABLE [dbo].[QuizItems]
ADD CONSTRAINT [FK_TestersQuizItems]
    FOREIGN KEY ([Tester_Id])
    REFERENCES [dbo].[Testers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestersQuizItems'
CREATE INDEX [IX_FK_TestersQuizItems]
ON [dbo].[QuizItems]
    ([Tester_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
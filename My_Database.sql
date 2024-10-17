USE [master]
GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'BorisBot')
CREATE DATABASE [BorisBot] 


USE [BorisBot]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- first section, tables

IF OBJECT_ID(N'dbo.articleAuthors', N'U') IS NULL
CREATE TABLE [dbo].[articleAuthors](
	[id] [uniqueidentifier] NOT NULL,
	[articleId] [uniqueidentifier] NOT NULL,
	[authorId] [bigint] NOT NULL,
	 CONSTRAINT [PK_articleAuthors] PRIMARY KEY CLUSTERED (
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


IF OBJECT_ID(N'dbo.articles', N'U') IS NULL
CREATE TABLE [dbo].[articles](
	[id] [uniqueidentifier] NOT NULL,
	[journalIssueId] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](max) NOT NULL,
	[contents] [varbinary](max) NOT NULL,
	 CONSTRAINT [PK_articles] PRIMARY KEY CLUSTERED (
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


IF OBJECT_ID(N'dbo.authors', N'U') IS NULL
CREATE TABLE [dbo].[authors](
	[id] [bigint] NOT NULL,
	[userName] [nvarchar](max) NOT NULL,
	[realName] [nvarchar](max) NOT NULL,
	 CONSTRAINT [PK_authors] PRIMARY KEY CLUSTERED 	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


IF OBJECT_ID(N'dbo.editors', N'U') IS NULL
CREATE TABLE [dbo].[editors](
	[id] [uniqueidentifier] NOT NULL,
	[authorId] [bigint] NOT NULL,
	[scientificJournalId] [uniqueidentifier] NOT NULL,
	 CONSTRAINT [PK_editors] PRIMARY KEY CLUSTERED 	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


IF OBJECT_ID(N'dbo.journalIssues', N'U') IS NULL
CREATE TABLE [dbo].[journalIssues](
	[id] [uniqueidentifier] NOT NULL,
	[scientificJournalId] [uniqueidentifier] NOT NULL,
	[date] [datetime2](7) NOT NULL,
	 CONSTRAINT [PK_journalIssues] PRIMARY KEY CLUSTERED (
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


IF OBJECT_ID(N'dbo.scientificJournals', N'U') IS NULL
CREATE TABLE [dbo].[scientificJournals](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	 CONSTRAINT [PK_scientificJournals] PRIMARY KEY CLUSTERED (
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


-- second section, keys


ALTER TABLE [dbo].[articleAuthors]  WITH CHECK ADD  CONSTRAINT [FK_articleAuthors_articles] FOREIGN KEY([articleId]) REFERENCES [dbo].[articles] ([id])
GO

ALTER TABLE [dbo].[articleAuthors] CHECK CONSTRAINT [FK_articleAuthors_articles]
GO

ALTER TABLE [dbo].[articleAuthors]  WITH CHECK ADD  CONSTRAINT [FK_articleAuthors_authors] FOREIGN KEY([authorId]) REFERENCES [dbo].[authors] ([id])
GO

ALTER TABLE [dbo].[articleAuthors] CHECK CONSTRAINT [FK_articleAuthors_authors]
GO

ALTER TABLE [dbo].[articles]  WITH CHECK ADD  CONSTRAINT [FK_articles_journalIssues] FOREIGN KEY([journalIssueId]) REFERENCES [dbo].[journalIssues] ([id])
GO

ALTER TABLE [dbo].[articles] CHECK CONSTRAINT [FK_articles_journalIssues]
GO

ALTER TABLE [dbo].[editors]  WITH CHECK ADD  CONSTRAINT [FK_editors_authors] FOREIGN KEY([authorId]) REFERENCES [dbo].[authors] ([id])
GO

ALTER TABLE [dbo].[editors] CHECK CONSTRAINT [FK_editors_authors]
GO

ALTER TABLE [dbo].[editors]  WITH CHECK ADD  CONSTRAINT [FK_editors_scientificJournals] FOREIGN KEY([scientificJournalId]) REFERENCES [dbo].[scientificJournals] ([id])
GO

ALTER TABLE [dbo].[editors] CHECK CONSTRAINT [FK_editors_scientificJournals]
GO

ALTER TABLE [dbo].[journalIssues]  WITH CHECK ADD  CONSTRAINT [FK_journalIssues_scientificJournals] FOREIGN KEY([scientificJournalId]) REFERENCES [dbo].[scientificJournals] ([id])
GO

ALTER TABLE [dbo].[journalIssues] CHECK CONSTRAINT [FK_journalIssues_scientificJournals]
GO

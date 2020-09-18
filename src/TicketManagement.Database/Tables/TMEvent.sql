CREATE TABLE [dbo].[TMEvent]
(
	[Id] int primary key identity,
	[Name] nvarchar(120) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[TMLayoutId] int NOT NULL,
	[StartEvent] datetime NOT NULL,
	[EndEvent] datetime NOT NULL
)

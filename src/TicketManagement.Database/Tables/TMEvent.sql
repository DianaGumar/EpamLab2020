CREATE TABLE [dbo].[TMEvent]
(
	[Id] int primary key identity,
	[Name] nvarchar(120) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[TMLayoutId] int NOT NULL,
	[StartEvent] datetime NOT NULL DEFAULT GETDATE(),
	[EndEvent] datetime NOT NULL DEFAULT GETDATE(),
	[Img] nvarchar(max) CONSTRAINT TMEvent_Img  DEFAULT 'https://upload.wikimedia.org/wikipedia/en/6/60/No_Picture.jpg'
)

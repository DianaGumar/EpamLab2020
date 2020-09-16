CREATE TABLE [dbo].[PublicEventArea]
(
	[Id] int identity primary key,
	[PublicEventId] int NOT NULL,
	[Description] nvarchar(200) NOT NULL,
	[CoordX] int NOT NULL,
	[CoordY] int NOT NULL,
	[Price] decimal NOT NULL
)

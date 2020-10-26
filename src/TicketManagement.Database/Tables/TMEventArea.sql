CREATE TABLE [dbo].[TMEventArea]
(
	[Id] int identity primary key,
	[TMEventId] int NOT NULL,
	[Description] nvarchar(200) NOT NULL,
	[CoordX] int NOT NULL,
	[CoordY] int NOT NULL,
	[Price] decimal(19,4) NOT NULL
)

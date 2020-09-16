CREATE TABLE [dbo].[PublicEventSeat]
(
	[Id] int identity primary key,
	[PublicEventAreaId] int NOT NULL,
	[Row] int NOT NULL,
	[Number] int NOT NULL,
	[State] int NOT NULL
)

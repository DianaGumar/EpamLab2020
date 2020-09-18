CREATE TABLE [dbo].[TMEventSeat]
(
	[Id] int identity primary key,
	[TMEventAreaId] int NOT NULL,
	[Row] int NOT NULL,
	[Number] int NOT NULL,
	[State] int NOT NULL
)

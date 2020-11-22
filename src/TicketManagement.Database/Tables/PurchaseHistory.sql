CREATE TABLE [dbo].[PurchaseHistory]
(
	[Id] [INT] NOT NULL PRIMARY KEY,
	[UserId] [nvarchar](128) NOT NULL,
	[TMEventSeatId] [int],
	[BookingDate] [datetime] NOT NULL,
)

CREATE PROCEDURE [dbo].[TMEvent_Delete]
	@Id int
AS
	BEGIN TRANSACTION;

	delete from  TMEventSeat where TMEventAreaId in
	(select Id from TMEventArea where TMEventArea.TMEventId = @Id)

	delete from TMEventArea where TMEventArea.TMEventId = @Id

	delete from TMEvent where Id = @Id

	COMMIT;
RETURN 0

CREATE PROCEDURE [dbo].[SP_Delete_TMEvent]
	@TMEventId int
AS
	BEGIN TRANSACTION;

	delete from  TMEventSeat where TMEventAreaId in
	(select Id from TMEventArea where TMEventArea.TMEventId = @TMEventId)

	delete from TMEventArea where TMEventArea.TMEventId = @TMEventId

	delete from TMEvent where Id = @TMEventId

	COMMIT;
RETURN 0

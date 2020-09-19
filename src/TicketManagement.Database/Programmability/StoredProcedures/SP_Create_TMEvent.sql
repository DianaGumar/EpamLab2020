CREATE PROCEDURE [dbo].[SP_Create_TMEvent]
	@Name nvarchar(120),
	@Description nvarchar(max),
	@TMLayoutId int,
	@StartEvent datetime,
	@EndEvent datetime
AS
	DECLARE @TMEventId int = 0
	BEGIN TRANSACTION;

	declare @LayoutId int = 1
	declare @TMEvent table (Id int)
	insert into TMEvent (Name, Description, TMLayoutId, StartEvent, EndEvent) 
		OUTPUT INSERTED.ID into @TMEvent
		values (@Name, @Description, @TMLayoutId, @StartEvent, @EndEvent)

	set @TMEventId = (SELECT TOP 1 Id FROM @TMEvent);
	declare @TMEventArea table (Id int)

	INSERT INTO TMEventArea (TMEventId, Description, CoordX, CoordY, Price) 
	OUTPUT INSERTED.ID into @TMEventArea
	SELECT @TMEventId, Area.Description, Area.CoordX, Area.CoordY, 0
	FROM Area WHERE Area.TMLayoutId = @LayoutId;

	select * from @TMEventArea;

	WITH tmeventArea_id_area_id AS
	(
		SELECT TMEventArea.Id as tmeventArea_id, Area.Id as area_id
		FROM TMEventArea, Area
		WHERE TMEventArea.Description = Area.Description and Area.TMLayoutId = @LayoutId 
		and TMEventArea.TMEventId = @TMEventId
	)

	INSERT INTO dbo.TMEventSeat (TMEventAreaId, Row, Number, State) 
	SELECT tmeventArea_id_area_id.tmeventArea_id, Seat.Row, Seat.Number, 0
	FROM dbo.Seat, tmeventArea_id_area_id  
	WHERE AreaId IN (select Id from Area WHERE Area.TMLayoutId = @LayoutId) and
	tmeventArea_id_area_id.area_id = Seat.AreaId
	
	COMMIT;

RETURN @TMEventId
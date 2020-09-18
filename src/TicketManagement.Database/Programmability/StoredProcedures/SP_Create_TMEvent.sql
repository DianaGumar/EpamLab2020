
CREATE PROCEDURE [dbo].[SP_Create_TMEvent]
	@Name nvarchar(120),
	@Description nvarchar(max),
	@TMLayoutId int,
	@StartEvent datetime,
	@EndEvent datetime,
	@Price money
AS

	BEGIN TRANSACTION;

	insert into TMEvent (Name, Description, TMLayoutId, StartEvent, EndEvent)
	values (@Name, @Description, @TMLayoutId, @StartEvent, @EndEvent)

	-- just copy area and seat in publiceventarea and publiceventseat

	-- находим значение id public
	declare @current_TMEvent_id int
	SELECT TOP 1 @current_TMEvent_id = dbo.TMEvent.Id FROM TMEvent ORDER BY id DESC

	--INSERT INTO dbo.PublicEventArea (PublicEventId, Description, CoordX, CoordY, Price)
	--SELECT @current_PublicEvent_id, Area.Description, Area.CoordX, Area.CoordY, @Price
	--FROM dbo.Area WHERE Area.LayoutId = @LayoutId


	--INSERT INTO dbo.PublicEventSeat (PublicEventAreaId, Row, Number, State) 
	--SELECT @current_PublicArea_id, Seat.Row, Seat.Number, 0
	--FROM dbo.Seat WHERE AreaId = @current_PublicArea_id

	-- сравнивать по параметрам
	
	--WHILE @i < @Weidth
 --   BEGIN



	--END

	
	COMMIT;


RETURN 0
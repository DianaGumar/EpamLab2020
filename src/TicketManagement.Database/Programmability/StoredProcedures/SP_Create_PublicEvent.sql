CREATE PROCEDURE [dbo].[CreatePublicEvent]
	@Name nvarchar(120),
	@Description nvarchar(max),
	@LayoutId int,
	@StartEvent datetime,
	@EndEvent datetime,
	@Price money
AS
	insert into PublicEvent (Name, Description, LayoutId, StartEvent, EndEvent)
	values (@Name, @Description, @LayoutId, @StartEvent, @EndEvent)

	-- just copy area and seat in publiceventarea and publiceventseat
	declare @current_PublicEvent_id int
	select @current_PublicEvent_id = dbo.PublicEvent.Id 
		from PublicEvent 
		where PublicEvent.Name = @Name and 
			PublicEvent.Description = @Description and 
			PublicEvent.LayoutId = @LayoutId and 
			PublicEvent.StartEvent = @StartEvent and 
			PublicEvent.EndEvent = @EndEvent


	--SELECT * INTO dbo.PublicEventArea FROM dbo.Area WHERE Area.LayoutId = @LayoutId

	INSERT INTO dbo.PublicEventArea (PublicEventId, Description, CoordX, CoordY, Price)
	SELECT @current_PublicEvent_id, Area.Description, Area.CoordX, Area.CoordY, @Price
	FROM dbo.Area WHERE Area.LayoutId = @LayoutId


	INSERT INTO dbo.PublicEventSeat (PublicEventAreaId, Row, Number, State) 
	SELECT @current_PublicArea_id, Seat.Row, Seat.Number, 0
	FROM dbo.Seat WHERE AreaId = @current_PublicArea_id

	-- сравнивать по параметрам


		[Id] int identity primary key,
	[AreaId] int NOT NULL,
	[Row] int NOT NULL,
	[Number] int NOT NULL


		[Id] int identity primary key,
	[PublicEventAreaId] int NOT NULL,
	[Row] int NOT NULL,
	[Number] int NOT NULL,
	[State] int NOT NULL

	
	WHILE @i < @Weidth
    BEGIN



	END

	



RETURN 0
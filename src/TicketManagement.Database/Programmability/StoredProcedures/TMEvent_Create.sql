CREATE PROCEDURE [dbo].[TMEvent_Create]
	@Name nvarchar(120),
	@Description nvarchar(max),
	@TMLayoutId int,
	@StartEvent datetime,
	@EndEvent datetime,
	@Img nvarchar(max)
AS
	DECLARE @Id int = 0
	BEGIN TRANSACTION;

	declare @TMEvent table (Id int)
	insert into TMEvent (Name, Description, TMLayoutId, StartEvent, EndEvent, Img) 
		OUTPUT INSERTED.Id into @TMEvent
		values (@Name, @Description, @TMLayoutId, @StartEvent, @EndEvent, @Img)

	set @Id = (SELECT TOP 1 Id FROM @TMEvent);
	declare @TMEventArea table (Id int)

	INSERT INTO TMEventArea (TMEventId, Description, CoordX, CoordY, Price) 
	OUTPUT INSERTED.Id into @TMEventArea
	SELECT @Id, Area.Description, Area.CoordX, Area.CoordY, 0
	FROM Area WHERE Area.TMLayoutId = @TMLayoutId;

	WITH tmeventArea_id_area_id AS
	(
		SELECT TMEventArea.Id as tmeventArea_id, Area.Id as area_id
		FROM TMEventArea, Area
		WHERE TMEventArea.Description = Area.Description and Area.TMLayoutId = @TMLayoutId 
		and TMEventArea.TMEventId = @Id
	)

	INSERT INTO dbo.TMEventSeat (TMEventAreaId, Row, Number, State) 
	SELECT tmeventArea_id_area_id.tmeventArea_id, Seat.Row, Seat.Number, 0
	FROM dbo.Seat, tmeventArea_id_area_id  
	WHERE AreaId IN (select Id from Area WHERE Area.TMLayoutId = @TMLayoutId) and
	tmeventArea_id_area_id.area_id = Seat.AreaId

	COMMIT;
	select @Id AS Id
	SELECT IDENT_CURRENT('TMEvent') AS Id
	


GO


---
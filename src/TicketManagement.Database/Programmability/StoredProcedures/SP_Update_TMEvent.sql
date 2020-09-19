CREATE PROCEDURE [dbo].[SP_Update_TMEvent]
	@TMEventId int,
	@Name nvarchar(120),
	@Description nvarchar(max),
	@LayoutId int,
	@StartEvent datetime,
	@EndEvent datetime
AS
	
	BEGIN TRANSACTION;
	
	declare @pastLayoutId int
	select @pastLayoutId = TMEvent.TMLayoutId from TMEvent where Id =  @TMEventId

	update TMEvent 
	set 
	Name = @Name, 
	Description = @Description, 
	TMLayoutId = @LayoutId, 
	StartEvent = @StartEvent, 
	EndEvent = @EndEvent


	if @pastLayoutId <> @LayoutId 
	begin

		delete from  TMEventSeat where TMEventAreaId in
			(select Id from TMEventArea where TMEventArea.TMEventId = @TMEventId)
		delete from TMEventArea where TMEventArea.TMEventId = @TMEventId



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


	end

	COMMIT;


RETURN 0

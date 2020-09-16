CREATE PROCEDURE [dbo].[SP_Create_Venue]
	@Description nvarchar(120),
	@Address nvarchar(200),
	@Phone nvarchar(30),
	@Weidth int,
	@Lenght int
AS
	--- Venue
	insert into dbo.Venue (Description, Address, Phone, Weidth, Lenght)
	values (@Description, @Address, @Phone, @Weidth, @Lenght)

	--- Layout
	declare @current_Venue_id int
	select @current_Venue_id = dbo.Venue.Id 
		from Venue 
		where Venue.Description = @Description and Venue.Address = @Address and Venue.Phone = @Phone

	insert into dbo.Layout
	values (@current_Venue_id, 'Default layout')

	--- Area
	declare @current_Layout_id int
	select @current_Layout_id = dbo.Layout.Id 
		from Layout 
		where Layout.VenueId = @current_Venue_id


	insert into dbo.Area
	values (@current_Layout_id, 'All area of default layout', 1, 1)

	-- create count seats by real venue size
	--- Seat
	declare @current_Area_id int
	select @current_Area_id = dbo.Area.Id 
		from Area 
		where Area.LayoutId = @current_Layout_id

	declare @i int = 0
	declare @j int = 0

	WHILE @i < @Weidth
    BEGIN
		WHILE @j < @Lenght
		BEGIN
			insert into dbo.Seat values (@current_Area_id, @i, @j)
			SET @j = @j + 1
		END
		set @j = 0
        SET @i = @i + 1
    END

RETURN 1



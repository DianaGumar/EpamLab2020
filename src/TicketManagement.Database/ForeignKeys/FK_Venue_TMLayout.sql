ALTER TABLE dbo.TMLayout
ADD CONSTRAINT FK_Venue_TMLayout FOREIGN KEY (VenueId)     
    REFERENCES dbo.Venue (Id)
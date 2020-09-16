ALTER TABLE dbo.PublicEventSeat
ADD CONSTRAINT FK_Area_PublicEventSeat FOREIGN KEY ([PublicEventAreaId])     
    REFERENCES dbo.PublicEventArea (Id)
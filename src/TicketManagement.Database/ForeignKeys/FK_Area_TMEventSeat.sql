ALTER TABLE dbo.TMEventSeat
ADD CONSTRAINT FK_Area_TMEventSeat FOREIGN KEY ([TMEventAreaId])     
    REFERENCES dbo.TMEventArea (Id)
ALTER TABLE dbo.PublicEventArea
ADD CONSTRAINT FK_PublicEvent_PublicEventArea FOREIGN KEY ([PublicEventId])     
    REFERENCES dbo.[PublicEvent] (Id)
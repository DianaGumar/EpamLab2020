ALTER TABLE dbo.TMEventArea
ADD CONSTRAINT FK_TMEvent_TMEventArea FOREIGN KEY ([TMEventId])     
    REFERENCES dbo.[TMEvent] (Id)
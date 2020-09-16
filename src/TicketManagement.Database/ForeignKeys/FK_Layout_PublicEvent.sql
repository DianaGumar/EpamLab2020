ALTER TABLE dbo.[PublicEvent]
ADD CONSTRAINT FK_Layout_PublicEvent FOREIGN KEY (LayoutId)     
    REFERENCES dbo.Layout (Id)
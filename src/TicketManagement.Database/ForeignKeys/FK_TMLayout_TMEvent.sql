﻿ALTER TABLE dbo.[TMEvent]
ADD CONSTRAINT FK_TMLayout_TMEvent FOREIGN KEY (TMLayoutId)     
    REFERENCES dbo.TMLayout (Id)
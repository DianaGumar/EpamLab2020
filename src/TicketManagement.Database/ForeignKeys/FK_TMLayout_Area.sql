﻿ALTER TABLE dbo.Area
ADD CONSTRAINT FK_TMLayout_Area FOREIGN KEY (TMLayoutId)     
    REFERENCES dbo.TMLayout (Id)
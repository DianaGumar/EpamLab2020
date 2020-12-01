ALTER TABLE [dbo].[PurchaseHistory] 
ADD  CONSTRAINT [FK_PurchaseHistory_TMUser] FOREIGN KEY([UserId])
    REFERENCES [dbo].[TMUser] ([UserId])
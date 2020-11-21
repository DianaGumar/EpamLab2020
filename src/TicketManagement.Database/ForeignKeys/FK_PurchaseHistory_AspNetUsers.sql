ALTER TABLE [dbo].[PurchaseHistory] 
ADD  CONSTRAINT [FK_PurchaseHistory_AspNetUsers] FOREIGN KEY([UserId])
    REFERENCES [dbo].[AspNetUsers] ([Id])
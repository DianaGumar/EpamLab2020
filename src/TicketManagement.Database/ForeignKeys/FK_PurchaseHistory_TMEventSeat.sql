ALTER TABLE [dbo].[PurchaseHistory] 
ADD CONSTRAINT [FK_PurchaseHistory_TMEventSeat] FOREIGN KEY([TMEventSeatId])
    REFERENCES [dbo].[TMEventSeat] ([Id])
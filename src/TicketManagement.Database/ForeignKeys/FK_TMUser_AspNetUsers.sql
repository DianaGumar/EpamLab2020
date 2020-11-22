ALTER TABLE [dbo].[TMUser] ADD  CONSTRAINT [FK_TMUser_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
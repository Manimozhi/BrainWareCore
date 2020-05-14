CREATE TABLE [dbo].[SalesOrderDetail]
(
	[SalesOrderId] UNIQUEIDENTIFIER NOT NULL,
	[ProductId] UNIQUEIDENTIFIER NOT NULL,
	[Price] decimal(18,2) NOT NULL,
	[Quantity] int NOT NULL, 
    CONSTRAINT [PK_SalesOrderDetail] PRIMARY KEY ([SalesOrderId], [ProductId]), 
    CONSTRAINT [FK_SalesOrderDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([ProductId]), 
    CONSTRAINT [FK_SalesOrderDetail_Order] FOREIGN KEY ([SalesOrderId]) REFERENCES [SalesOrder]([SalesOrderId])
)

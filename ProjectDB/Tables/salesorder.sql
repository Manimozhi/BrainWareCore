CREATE TABLE [dbo].[SalesOrder]
(
	[SalesOrderId] UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	[Description] NVARCHAR(1000) NOT NULL, 
    [CompanyId] UNIQUEIDENTIFIER NOT NULL, 
    [Date] DATE NOT NULL default GETDATE(), 
    CONSTRAINT [FK_SalesOrder_to_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]),
)

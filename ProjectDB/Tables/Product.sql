﻿CREATE TABLE [dbo].[Product]
(
	[ProductId] UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	[Name] NVARCHAR(128) NOT NULL, 
    [Price] DECIMAL(18, 2) NOT NULL,

)

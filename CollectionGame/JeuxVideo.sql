CREATE TABLE [dbo].[JeuxVideo]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Nom] NVARCHAR(100) NOT NULL,
    [Plateforme] NVARCHAR(50),
    [DateDeSortie] DATE,
    [CategorieId] INT,

    CONSTRAINT [FK_JeuxVideo_Categories] FOREIGN KEY ([CategorieId]) REFERENCES [dbo].[Categories]([Id])
)

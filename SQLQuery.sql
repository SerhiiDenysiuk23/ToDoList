CREATE TABLE ToDo(
	Id INT PRIMARY KEY NOT NULL IDENTITY,
	Title NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	CategoryId INT FOREIGN KEY REFERENCES Category(Id) NULL,
	DueDate DATE NULL,
	[Status] NVARCHAR(15) CHECK ([Status] IN ('In progress', 'Completed')) DEFAULT 'In progress' NOT NULL
)

CREATE TABLE Category(
	Id INT PRIMARY KEY NOT NULL IDENTITY,
	[Name] NVARCHAR(15) NOT NULL
)

select * from ToDo

SELECT t.*, c.* FROM ToDo t LEFT JOIN Category c ON t.CategoryId = c.Id
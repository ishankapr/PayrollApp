CREARE DATABASE PayrollApp;

CREATE TABLE EmployeeInfo (
    EmployeeInfoID INT PRIMARY KEY IDENTITY (1, 1),
    EmployeeID INT,
    DepartmentID INT,
	Date DateTime,
	Amount DECIMAL,
	Status VARCHAR(20)
);

CREATE TABLE FileHistory (
    FileHistoryID INT PRIMARY KEY IDENTITY (1, 1),
    FileName VARCHAR(50),
    CreatedOn DateTime
);
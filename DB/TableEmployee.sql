CREATE TABLE Employee
(
    id INT IDENTITY(1,1) NOT NULL,
    name VARCHAR(100) NULL,
    age INT NULL,
    DateOfJoining DATE NULL -- Change the data type according to your needs
);
SELECT * FROM Employee
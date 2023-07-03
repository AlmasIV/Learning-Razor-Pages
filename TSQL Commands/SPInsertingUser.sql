USE MainDatabase;
GO
CREATE PROCEDURE InsertUser
    @_Name NVARCHAR(20),
    @_Surname NVARCHAR(20),
    @_Email VARCHAR(40),
    @_PhoneNumber VARCHAR(15),
    @_Age TINYINT,
    @_Password VARCHAR(32)
AS BEGIN
    INSERT INTO Users ([Name], [Surname], [Email], [PhoneNumber], [Age], [Password]) VALUES (@_Name, @_Surname, @_Email, @_PhoneNumber, @_Age, @_Password)
    
    INSERT INTO Bank(UserEmail) VALUES (@_Email)
END;
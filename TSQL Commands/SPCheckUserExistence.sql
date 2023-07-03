USE MainDatabase
GO
CREATE PROCEDURE CheckUserExistence
    @_Email VARCHAR(40),
    @_PhoneNumber VARCHAR(15),
    @_EmailNum TINYINT OUTPUT,
    @_PhoneNumberNum TINYINT OUTPUT
AS BEGIN
    SELECT 
        @_EmailNum = COUNT(CASE WHEN Email = @_Email THEN 1 END),
        @_PhoneNumberNum = COUNT(CASE WHEN PhoneNumber = @_PhoneNumber THEN 1 END)
    FROM 
        Users 
    WHERE 
        Email = @_Email OR PhoneNumber = @_PhoneNumber;
END;
USE MainDatabase
GO
CREATE PROCEDURE GetBalance
    @_Email VARCHAR(40),
    @_Balance DECIMAL(18, 2) OUTPUT
AS BEGIN 
    SELECT @_Balance = Deposit FROM Bank WHERE UserEmail = @_Email
END;
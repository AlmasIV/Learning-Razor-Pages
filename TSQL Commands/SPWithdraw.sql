USE MainDatabase
GO
CREATE PROCEDURE Withdraw
    @_Email VARCHAR(40),
    @_Amount DECIMAL(18, 2)
AS BEGIN
    UPDATE Bank
    SET Deposit -= @_Amount WHERE Bank.UserEmail = @_Email
END;
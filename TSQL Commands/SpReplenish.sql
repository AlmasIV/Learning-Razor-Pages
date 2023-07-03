USE MainDatabase
GO
CREATE PROCEDURE Replenish
    @_Email VARCHAR(40),
    @_Amount DECIMAL(18, 2)
AS BEGIN
    UPDATE Bank
    SET Deposit += @_Amount WHERE Bank.UserEmail = @_Email
END;
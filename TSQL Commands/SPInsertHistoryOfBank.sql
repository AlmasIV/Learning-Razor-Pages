USE MainDatabase
GO
CREATE PROCEDURE InsertHistoryOfBank
    @_Email VARCHAR(40),
    @_OperationType VARCHAR(30),
    @_Amount DECIMAL(18, 2),
    @_FromWhom VARCHAR(40) = NULL,
    @_ToWhom VARCHAR(40) = NULL
AS BEGIN
    INSERT INTO HistoryOfBank(UserEmail, OperationType, Amount, FromWhom, ToWhom) VALUES (@_Email, @_OperationType, @_Amount, @_FromWhom, @_ToWhom)
END;
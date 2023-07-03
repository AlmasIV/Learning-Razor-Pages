USE MainDatabase
GO
CREATE PROCEDURE GetHistoryOfBank
    @_Email VARCHAR(40)
AS BEGIN
    SELECT OperationDate, OperationType, Amount, FromWhom, ToWhom FROM HistoryOfBank WHERE UserEmail = @_Email
END;
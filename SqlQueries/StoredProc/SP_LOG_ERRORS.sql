USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOG_ERRORS]    Script Date: 1/26/2018 10:50:30 PM ******/
DROP PROCEDURE [dbo].[SP_LOG_ERRORS]
GO

/****** Object:  StoredProcedure [dbo].[SP_LOG_ERRORS]    Script Date: 1/26/2018 10:50:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_LOG_ERRORS]
(
	@MESSAGE NVARCHAR(MAX),
	@STACK_TRACE NVARCHAR(MAX),
	@EXCEPTION_TYPE NVARCHAR(MAX),
	@SOURCE NVARCHAR(MAX),
	@USER_ID BIGINT
)
AS

BEGIN
	INSERT INTO [dbo].[ErrorLog]
           ([Message]
           ,[StackTrace]
           ,[ExceptionType]
           ,[Source]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@MESSAGE
		   , @STACK_TRACE
		   , @EXCEPTION_TYPE
		   , @SOURCE
		   , @USER_ID
		   , GETUTCDATE())

END




GO



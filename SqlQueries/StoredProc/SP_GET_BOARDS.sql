USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_BOARDS]    Script Date: 7/12/2018 11:08:51 PM ******/
DROP PROCEDURE [dbo].[SP_GET_BOARDS]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_BOARDS]    Script Date: 7/12/2018 11:08:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_BOARDS]
(
	@BOARD_ID BIGINT = NULL,
	@BOARD NVARCHAR(MAX) = NULL,
	@ACTIVE BIT = NULL
)
AS

BEGIN

--EXEC [SP_GET_BOARDS]
--EXEC [SP_GET_BOARDS] 1 
--EXEC [SP_GET_BOARDS] NULL, 1

	SELECT B.Id, B.Board, B.Description
	FROM [BoardMaster] B
	WHERE (@ACTIVE IS NULL OR B.[Active] = @ACTIVE) 
	AND (@BOARD_ID IS NULL OR B.Id = @BOARD_ID) 
	AND (@BOARD IS NULL OR B.Description = @BOARD) 
END




GO



USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_BOARD_LIST]    Script Date: 7/10/2018 10:49:26 AM ******/
DROP PROCEDURE [dbo].[SP_GET_DOCTOR_BOARD_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_DOCTOR_BOARD_LIST]    Script Date: 7/10/2018 10:49:26 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GET_DOCTOR_BOARD_LIST]
(	
	@DOCTOR_ID BIGINT,
	@DOCTOR_BOARD_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_GET_DOCTOR_BOARD_LIST] 3

SELECT DB.Id
	, DB.DoctorId
	, UD.FirstName + ', ' + UD.LastName AS 'DoctorName'
	, DB.BoardId
	, B.Board AS 'BoardName'
	, DB.OtherDescription
	, DB.AddedBy
	, DB.AddedDate
	, DB.ModifiedBy
	, DB.ModifiedDate	
	FROM [DoctorBoard] DB
	INNER JOIN UserDetail UD ON DB.DoctorId = UD.Id
	INNER JOIN BoardMaster B ON DB.BoardId = B.Id
	WHERE DB.DoctorId = @DOCTOR_ID
	AND (@DOCTOR_BOARD_ID IS NULL OR DB.Id = @DOCTOR_BOARD_ID)
	AND DB.Active = 1
	ORDER BY DB.AddedDate DESC
END










GO



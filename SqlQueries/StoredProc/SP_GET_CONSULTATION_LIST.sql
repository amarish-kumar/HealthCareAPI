USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_LIST]    Script Date: 2/11/2018 8:40:41 PM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_LIST]    Script Date: 2/11/2018 8:40:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_GET_CONSULTATION_LIST]
(	
	@USER_ID BIGINT,
	@USER_ROLE NVARCHAR(100) = 'Doctor'
)
AS

BEGIN

--EXEC [SP_GET_CONSULTATION_LIST] 3 
	SELECT C.Id AS 'ConsultationId' , C.[Description] AS 'ConsultationDescription'
	, C.PatientId, UP.FirstName + ' ' + UP.LastName AS 'PatientName'
	, C.DoctorId, UD.FirstName + ' ' + UD.LastName AS 'DoctorName'
	, C.ConsultationStatusId, CS.[Description] AS 'ConsultationStatus'
	, C.AddedDate AS [ConsultationCreateDate]
	FROM Consultation C
	INNER JOIN UserDetail UP ON UP.Id = C.PatientId
	INNER JOIN UserDetail UD ON UD.Id = C.DoctorId
	INNER JOIN ConsultationStatus CS ON CS.Id = C.ConsultationStatusId
	WHERE (@USER_ROLE = 'Doctor' AND C.DoctorId = @USER_ID)
	OR (@USER_ROLE <> 'Doctor' AND C.PatientId = @USER_ID)
	ORDER BY C.AddedDate DESC
END


GO



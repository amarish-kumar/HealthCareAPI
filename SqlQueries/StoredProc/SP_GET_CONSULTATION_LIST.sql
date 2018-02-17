USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_LIST]    Script Date: 2/17/2018 11:36:40 AM ******/
DROP PROCEDURE [dbo].[SP_GET_CONSULTATION_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONSULTATION_LIST]    Script Date: 2/17/2018 11:36:40 AM ******/
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
--EXEC [SP_GET_CONSULTATION_LIST] 4, 'Patient' 
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
	OR (@USER_ROLE = 'Patient' AND C.PatientId = @USER_ID)
	ORDER BY C.AddedDate DESC
END




GO



USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONVERSATION_LIST]    Script Date: 2/17/2018 5:53:09 PM ******/
DROP PROCEDURE [dbo].[SP_GET_CONVERSATION_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_CONVERSATION_LIST]    Script Date: 2/17/2018 5:53:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_GET_CONVERSATION_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@USER_ID BIGINT,
	@USER_ROLE NVARCHAR(100) = 'Doctor'
)
AS

BEGIN

--EXEC [SP_GET_CONVERSATION_LIST] 8, 4, 'Patient' 
--EXEC [SP_GET_CONVERSATION_LIST] 8, 3, 'Doctor' 
	SELECT C.Id AS 'ConsultationId' , C.[Description] AS 'ConsultationDescription'
	, P.Id as ProfileId, P.FirstName + ' ' + P.LastName AS 'ProfileName'
	, C.PatientId, UP.FirstName + ' ' + UP.LastName AS 'PatientName'
	, C.DoctorId, UD.FirstName + ' ' + UD.LastName AS 'DoctorName'
	, C.ConsultationStatusId, CS.[Description] AS 'ConsultationStatus'
	, C.AddedDate AS [ConsultationCreateDate]
	FROM Consultation C
	INNER JOIN UserDetail UP ON UP.Id = C.PatientId
	INNER JOIN UserDetail UD ON UD.Id = C.DoctorId
	INNER JOIN ConsultationStatus CS ON CS.Id = C.ConsultationStatusId
	INNER JOIN [Profile] P ON P.Id = C.ProfileId
	WHERE (
		(@USER_ROLE = 'Doctor' AND C.DoctorId = @USER_ID)
		OR	
		(@USER_ROLE <> 'Doctor' AND C.PatientId = @USER_ID)
	)
	AND C.Id = @CONSULTATION_ID
	

	SELECT CONV.Id AS ConversationId, CONV.Description AS ConversationDescription,
	CONV.ConsultationId AS ConsultationId, 
	CONV.PatientId, CASE P.Id WHEN NULL THEN NULL ELSE P.FirstName + ' ' +  P.LastName END AS PatientName, 
	CONV.DoctorId, CASE D.Id WHEN NULL THEN NULL ELSE D.FirstName + ' ' +  D.LastName END AS DoctorName,
	CONV.IsLocked, CONV.Active, CONV.AddedBy, CONV.AddedDate AS ConversationCreateDate,
	@USER_ID AS RequestorId, @USER_ROLE AS RequestorRole
	FROM [Conversation] CONV
	INNER JOIN Consultation C ON CONV.ConsultationId = C.Id
	LEFT OUTER JOIN UserDetail P ON P.Id = CONV.PatientId
	LEFT OUTER JOIN UserDetail D ON D.Id = CONV.DoctorId
	WHERE C.Id = @CONSULTATION_ID
	ORDER BY CONV.AddedDate
END


GO



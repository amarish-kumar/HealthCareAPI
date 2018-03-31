USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_PROFILES]    Script Date: 3/25/2018 10:53:19 AM ******/
DROP PROCEDURE [dbo].[SP_GET_PROFILES]
GO

/****** Object:  StoredProcedure [dbo].[SP_GET_PROFILES]    Script Date: 3/25/2018 10:53:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[SP_GET_PROFILES]
(
	@USER_ID INT,
	@PROFILE_ID INT = NULL
)
AS

BEGIN

--EXEC [SP_GET_PROFILES]

SELECT P.Id, P.UserId, U.FirstName AS 'UserFirstName', U.LastName AS 'UserLastName', P.FirstName, P.LastName, P.[DOB], R.[Description] AS Relationship, G.GenderName
	FROM [PROFILE] P
	INNER JOIN [UserDetail] U ON U.Id = P.UserId
	INNER JOIN [GenderMaster] G ON G.Id = P.GenderId
	INNER JOIN [RelationshipMaster] R ON R.ID = P.RelationshipId
	WHERE P.UserId = @USER_ID AND (@PROFILE_ID IS NULL OR P.Id = @PROFILE_ID)
END




GO



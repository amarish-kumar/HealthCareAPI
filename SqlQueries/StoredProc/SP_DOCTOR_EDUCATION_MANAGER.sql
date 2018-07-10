USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_EDUCATION_MANAGER]    Script Date: 7/10/2018 10:10:48 AM ******/
DROP PROCEDURE [dbo].[SP_DOCTOR_EDUCATION_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_EDUCATION_MANAGER]    Script Date: 7/10/2018 10:10:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DOCTOR_EDUCATION_MANAGER]
(
	@DOCTOR_EDUCATION_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE DOCTOR_AWARD RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @DoctorId AS BIGINT, @BeginingYear AS BIGINT, @EndingYear AS BIGINT
, @StateId AS BIGINT, @CountryId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @CollegeName as NVARCHAR(MAX), @City as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = DoctorEducationList.Columns.value('Id[1]', 'BIGINT')
	   , @DoctorId = DoctorEducationList.Columns.value('DoctorId[1]', 'BIGINT')
	   , @BeginingYear = DoctorEducationList.Columns.value('BeginingYear[1]', 'BIGINT')  
	   , @EndingYear = DoctorEducationList.Columns.value('EndingYear[1]', 'BIGINT') 
	   , @CollegeName = DoctorEducationList.Columns.value('CollegeName[1]', 'NVARCHAR(MAX)')
	   , @City = DoctorEducationList.Columns.value('City[1]', 'NVARCHAR(MAX)')
	   , @StateId = DoctorEducationList.Columns.value('StateId[1]', 'BIGINT')
	   , @CountryId = DoctorEducationList.Columns.value('CountryId[1]', 'BIGINT')
	   , @Active = DoctorEducationList.Columns.value('Active[1]', 'bit')
FROM   @DOCTOR_EDUCATION_XML.nodes('DoctorEducation') AS DoctorEducationList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN
	
	INSERT INTO [dbo].[DoctorEducation]
           ([DoctorId]
           ,[BeginingYear]
           ,[EndingYear]
           ,[CollegeName]
           ,[City]
           ,[StateId]
           ,[CountryId]
           ,[Active]
           ,[AddedBy]
		   ,[AddedDate])    
     VALUES          
           (@DoctorId
           ,@BeginingYear
		   ,@EndingYear
		   ,@CollegeName
		   ,@City
		   ,@StateId
		   ,@CountryId
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[DoctorEducation]
		   SET [BeginingYear] = ISNULL(@BeginingYear,[BeginingYear])
			  ,[EndingYear] = ISNULL(@EndingYear,[EndingYear])
			  ,[CollegeName] = ISNULL(@CollegeName,[CollegeName])
			  ,[City] = ISNULL(@City,[City])
			  ,[StateId] = ISNULL(@StateId,[StateId])
			  ,[CountryId] = ISNULL(@CountryId,[CountryId])
			  ,[ModifiedBy] = @USER_ID
			  ,[ModifiedDate] = GETUTCDATE()
		WHERE Id = @Id
		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'
	END
END

SELECT @Result AS Result, @ReturnMessage AS ReturnMessage
END


GO



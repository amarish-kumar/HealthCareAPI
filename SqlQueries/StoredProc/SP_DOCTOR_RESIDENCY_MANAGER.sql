USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_RESIDENCY_MANAGER]    Script Date: 7/10/2018 10:11:34 AM ******/
DROP PROCEDURE [dbo].[SP_DOCTOR_RESIDENCY_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_RESIDENCY_MANAGER]    Script Date: 7/10/2018 10:11:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DOCTOR_RESIDENCY_MANAGER]
(
	@DOCTOR_RESIDENCY_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE DOCTOR_AWARD RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @DoctorId AS BIGINT, @BeginingYear AS BIGINT, @EndingYear AS BIGINT
, @StateId AS BIGINT, @CountryId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @HospitalName as NVARCHAR(MAX), @City as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = DoctorResidencyList.Columns.value('Id[1]', 'BIGINT')
	   , @DoctorId = DoctorResidencyList.Columns.value('DoctorId[1]', 'BIGINT')
	   , @BeginingYear = DoctorResidencyList.Columns.value('BeginingYear[1]', 'BIGINT')  
	   , @EndingYear = DoctorResidencyList.Columns.value('EndingYear[1]', 'BIGINT') 
	   , @HospitalName = DoctorResidencyList.Columns.value('HospitalName[1]', 'NVARCHAR(MAX)')
	   , @City = DoctorResidencyList.Columns.value('City[1]', 'NVARCHAR(MAX)')
	   , @StateId = DoctorResidencyList.Columns.value('StateId[1]', 'BIGINT')
	   , @CountryId = DoctorResidencyList.Columns.value('CountryId[1]', 'BIGINT')
	   , @Active = DoctorResidencyList.Columns.value('Active[1]', 'bit')
FROM   @DOCTOR_RESIDENCY_XML.nodes('DoctorResidency') AS DoctorResidencyList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN
		
	INSERT INTO [dbo].[DoctorResidency]
           ([DoctorId]
           ,[BeginingYear]
           ,[EndingYear]
           ,[HospitalName]
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
		   ,@HospitalName
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

		UPDATE [dbo].[DoctorResidency]
		   SET [BeginingYear] = ISNULL(@BeginingYear,[BeginingYear])
			  ,[EndingYear] = ISNULL(@EndingYear,[EndingYear])
			  ,[HospitalName] = ISNULL(@HospitalName,[HospitalName])
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



USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_AWARD_MANAGER]    Script Date: 7/10/2018 10:10:11 AM ******/
DROP PROCEDURE [dbo].[SP_DOCTOR_AWARD_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_AWARD_MANAGER]    Script Date: 7/10/2018 10:10:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_DOCTOR_AWARD_MANAGER]
(
	@DOCTOR_AWARD_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE DOCTOR_AWARD RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @DoctorId AS BIGINT, @YearReceived AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @InstitutionName as NVARCHAR(MAX), @AwardName as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = DoctorAwardList.Columns.value('Id[1]', 'BIGINT')
	   , @DoctorId = DoctorAwardList.Columns.value('DoctorId[1]', 'BIGINT')
	   , @YearReceived = DoctorAwardList.Columns.value('YearReceived[1]', 'BIGINT')  
	   , @InstitutionName = DoctorAwardList.Columns.value('InstitutionName[1]', 'NVARCHAR(MAX)') 
	   , @AwardName = DoctorAwardList.Columns.value('AwardName[1]', 'NVARCHAR(MAX)')  
	   , @Active = DoctorAwardList.Columns.value('Active[1]', 'bit')
FROM   @DOCTOR_AWARD_XML.nodes('DoctorAwards') AS DoctorAwardList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN
	
	INSERT INTO [dbo].[DoctorAwards]
           ([DoctorId]
           ,[YearReceived]
           ,[InstitutionName]
           ,[AwardName]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     
     VALUES          
           (@DoctorId
           ,@YearReceived
		   ,@InstitutionName
		   ,@AwardName
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[DoctorAwards]
		   SET [YearReceived] = ISNULL(@YearReceived,[YearReceived])
			  ,[InstitutionName] = ISNULL(@InstitutionName,[InstitutionName])	
			  ,[AwardName] = ISNULL(@AwardName,[AwardName])
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



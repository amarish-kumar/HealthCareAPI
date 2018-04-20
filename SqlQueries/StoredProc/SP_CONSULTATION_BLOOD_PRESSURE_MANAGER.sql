USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_BLOOD_PRESSURE_MANAGER]    Script Date: 4/20/2018 10:35:16 PM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_BLOOD_PRESSURE_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_BLOOD_PRESSURE_MANAGER]    Script Date: 4/20/2018 10:35:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_CONSULTATION_BLOOD_PRESSURE_MANAGER]
(
	@CONSULTATION_BLOOD_PRESSURE_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE BLOOD PRESSURE RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT
DECLARE @Systolic AS INT, @Diastolic AS INT
DECLARE @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT
DECLARE @TimeStamp AS DATETIME

SELECT	 @Id = ConsultationBloodPressureList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationBloodPressureList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @Systolic = ConsultationBloodPressureList.Columns.value('Systolic[1]', 'INT')   
	   , @Diastolic = ConsultationBloodPressureList.Columns.value('Diastolic[1]', 'INT')
	   , @TimeStamp = ConsultationBloodPressureList.Columns.value('TimeStamp[1]', 'DATETIME')
	   , @Active = ConsultationBloodPressureList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_BLOOD_PRESSURE_XML.nodes('ConsultationBloodPressureReading') AS ConsultationBloodPressureList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN


	INSERT INTO [dbo].[ConsultationBloodPressureReading]
           ([ConsultationId]
           ,[Systolic]
           ,[Diastolic]
           ,[Timestamp]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ConsultationId
           ,@Systolic
           ,@Diastolic
		   ,ISNULL(@TimeStamp,GETUTCDATE()) 
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())     

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationBloodPressureReading]
		   SET [Systolic] = ISNULL(@Systolic,[Systolic])
			  ,[Diastolic] = ISNULL(@Diastolic, [Diastolic])
			  ,[TimeStamp] = ISNULL(@TimeStamp,GETUTCDATE()) 
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



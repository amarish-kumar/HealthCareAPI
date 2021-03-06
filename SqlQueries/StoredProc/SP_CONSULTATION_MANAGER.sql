USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_MANAGER]    Script Date: 4/22/2018 11:36:22 AM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_MANAGER]    Script Date: 4/22/2018 11:36:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[SP_CONSULTATION_MANAGER]
(
	@CONSULTATION_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --CONSULTATION FOR START/UPDATE CONSULTATION RECORD, REPLY FOR THE SUBSEQUENT ENTRIES
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED
)
AS

BEGIN

/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @PatientId AS BIGINT, @DoctorId AS BIGINT, @ConsultationStatusId AS BIGINT, @ProfileId AS BIGINT, @PackageId AS BIGINT
DECLARE @Description AS NVARCHAR(MAX), @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT, @IsLocked as BIT

SELECT	 @Id = ConsultationList.Columns.value('Id[1]', 'BIGINT')
	   , @Description = ConsultationList.Columns.value('Description[1]', 'nvarchar(max)')
	   , @PatientId = ConsultationList.Columns.value('PatientId[1]', 'BIGINT')
	   , @ProfileId = ConsultationList.Columns.value('ProfileId[1]', 'BIGINT')
	   , @PackageId = ConsultationList.Columns.value('PackageId[1]', 'BIGINT')
	   , @DoctorId = ConsultationList.Columns.value('DoctorId[1]', 'BIGINT')
	   , @ConsultationStatusId = ConsultationList.Columns.value('ConsultationStatusId[1]', 'BIGINT')
	   , @Active = ConsultationList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_XML.nodes('Consultation') AS ConsultationList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION = 'CONSULTATION'
BEGIN

	IF @Id IS NULL OR @Id =0 
	BEGIN

		INSERT INTO [dbo].[Consultation]
				   ([Description]
				   ,[PatientId]
				   ,[DoctorId]
				   ,[ConsultationStatusId]
				   ,[ProfileId]
				   ,[PackageId]
				   ,[IsLocked]
				   ,[Active]
				   ,[AddedBy]
				   ,[AddedDate])
			 VALUES
				   (@Description
				   ,@PatientId
				   ,@DoctorId
				   ,@ConsultationStatusId
				   ,@ProfileId
				   ,@PackageId
				   ,0
				   ,@Active
				   ,@USER_ID
				   ,GETUTCDATE())

			SET @Result = 1;
			SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN
		SELECT @IsLocked = IsLocked
		FROM [dbo].[Consultation]
		WHERE Id = @Id 
		IF ISNULL(@IsLocked, 1) <> 1
		BEGIN
			UPDATE [dbo].[Consultation]
				SET
				[Description] = @Description,
				[ConsultationStatusId] = @ConsultationStatusId,
				[PackageId] = @PackageId,
				[ModifiedBy] = @USER_ID,
				[ModifiedDate] = GETUTCDATE()
			WHERE Id = @Id
			SET @Result = 1;
			SET @ReturnMessage = 'Record updated successfully.'
		END

		ELSE
		BEGIN
			SET @Result = 0;
			SET @ReturnMessage = 'Record does not exist or locked.'
		END

	END
END

SELECT @Result AS Result, @ReturnMessage AS ReturnMessage
END



GO



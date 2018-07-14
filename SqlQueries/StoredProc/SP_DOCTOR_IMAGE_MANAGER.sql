USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_IMAGE_MANAGER]    Script Date: 7/14/2018 9:04:14 AM ******/
DROP PROCEDURE [dbo].[SP_DOCTOR_IMAGE_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_IMAGE_MANAGER]    Script Date: 7/14/2018 9:04:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DOCTOR_IMAGE_MANAGER]
(
	@DOCTOR_IMAGE_XML AS XML,
	@FILE_DATA AS VARBINARY(MAX) = NULL,
	@OPERATION AS NVARCHAR(100) = NULL, --CONVERSATION FOR START/UPDATE CONVERSATION RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @DoctorId AS BIGINT
DECLARE @FileName AS NVARCHAR(MAX), @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result AS BIT, @IsPrimary  AS BIT
DECLARE @ReportDate AS DATETIME

SELECT	 @Id = DoctorImageList.Columns.value('Id[1]', 'BIGINT')
	   , @DoctorId = DoctorImageList.Columns.value('DoctorId[1]', 'BIGINT')
	   , @IsPrimary = DoctorImageList.Columns.value('IsPrimary[1]', 'BIT')
	   , @FileName = DoctorImageList.Columns.value('FileName[1]', 'nvarchar(max)')	  
	   , @Active = DoctorImageList.Columns.value('Active[1]', 'bit')
FROM   @DOCTOR_IMAGE_XML.nodes('DoctorImages') AS DoctorImageList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id =0 
	BEGIN

	INSERT INTO [dbo].[DoctorImages]
           ([DoctorId]
           ,[IsPrimary]
           ,[FileName]
           ,[FileData]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@DoctorId
		   ,@IsPrimary
           ,@FileName
           ,@FILE_DATA
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())
	SET @Id = SCOPE_IDENTITY()
	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[DoctorImages]
		   SET [FileName] = ISNULL(@FileName,[FileName])
			  ,[FileData] = ISNULL(@FILE_DATA, [FileData])
			  ,[IsPrimary] = @IsPrimary
			  ,[ModifiedBy] = @USER_ID
			  ,[ModifiedDate] = GETUTCDATE()
		WHERE Id = @Id
		SET @Result = 1;
		SET @ReturnMessage = 'Record updated successfully.'
	END

	IF @IsPrimary = 1
	BEGIN
		UPDATE [dbo].[DoctorImages]
		   SET [IsPrimary] = 0
			  ,[ModifiedBy] = @USER_ID
			  ,[ModifiedDate] = GETUTCDATE()
		WHERE [DoctorId] = @DoctorId AND Id <> @Id
	END 
END

SELECT @Result AS Result, @ReturnMessage AS ReturnMessage
END


GO



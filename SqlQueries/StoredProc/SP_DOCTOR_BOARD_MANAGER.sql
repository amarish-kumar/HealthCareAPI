USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_BOARD_MANAGER]    Script Date: 7/10/2018 10:10:38 AM ******/
DROP PROCEDURE [dbo].[SP_DOCTOR_BOARD_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_DOCTOR_BOARD_MANAGER]    Script Date: 7/10/2018 10:10:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DOCTOR_BOARD_MANAGER]
(
	@DOCTOR_BOARD_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE DOCTOR_AWARD RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @DoctorId AS BIGINT, @BoardId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @OtherDescription as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = DoctorBoardList.Columns.value('Id[1]', 'BIGINT')
	   , @DoctorId = DoctorBoardList.Columns.value('DoctorId[1]', 'BIGINT')
	   , @BoardId = DoctorBoardList.Columns.value('BoardId[1]', 'BIGINT')  
	   , @OtherDescription = DoctorBoardList.Columns.value('OtherDescription[1]', 'NVARCHAR(MAX)') 
	   , @Active = DoctorBoardList.Columns.value('Active[1]', 'bit')
FROM   @DOCTOR_BOARD_XML.nodes('DoctorBoard') AS DoctorBoardList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[DoctorBoard]
           ([DoctorId]
           ,[BoardId]
           ,[OtherDescription]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])          
     VALUES          
           (@DoctorId
           ,@BoardId
		   ,@OtherDescription
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[DoctorBoard]
		   SET [BoardId] = ISNULL(@BoardId,[BoardId])
			  ,[OtherDescription] = ISNULL(@OtherDescription,[OtherDescription])
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



USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_OCCUPATION_MANAGER]    Script Date: 4/19/2018 11:51:07 PM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_OCCUPATION_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_OCCUPATION_MANAGER]    Script Date: 4/19/2018 11:51:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CONSULTATION_OCCUPATION_MANAGER]
(
	@CONSULTATION_OCCUPATION_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE ALLERGY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @OccupationId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = ConsultationOccupationList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationOccupationList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @OccupationId = ConsultationOccupationList.Columns.value('OccupationId[1]', 'BIGINT')  
	   , @Active = ConsultationOccupationList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_OCCUPATION_XML.nodes('ConsultationOccupation') AS ConsultationOccupationList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[ConsultationOccupation]
           ([ConsultationId]
           ,[OccupationId]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])     
     VALUES
           (@ConsultationId
           ,@OccupationId
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationOccupation]
		   SET [OccupationId] = ISNULL(@OccupationId,[OccupationId])
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



USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_PATIENT_TANDC_MANAGER]    Script Date: 1/20/2018 8:40:35 PM ******/
DROP PROCEDURE [dbo].[SP_PATIENT_TANDC_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_PATIENT_TANDC_MANAGER]    Script Date: 1/20/2018 8:40:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_PATIENT_TANDC_MANAGER]
(
	@PATIENT_T_ANC_MAPPING_ID BIGINT = NULL,
	@PATIENT_ID AS BIGINT,
	@T_AND_C_ID AS BIGINT,
	@USER_ID BIGINT
)
AS

BEGIN

IF @PATIENT_T_ANC_MAPPING_ID IS NOT NULL AND @PATIENT_T_ANC_MAPPING_ID > 0
		BEGIN
			/*THIS BLOCK IS FOR UPDATE */
			
			UPDATE [dbo].[PatientTAndCMapping]
			   SET [TAndCId] = ISNULL(@T_AND_C_ID, TAndCId)
				  ,[SignnedDate] = GETUTCDATE()
				  ,[ModifiedBy] = @USER_ID
				  ,[ModifiedDate] = GETUTCDATE()
			WHERE ID = @PATIENT_T_ANC_MAPPING_ID

		END
	ELSE
		BEGIN
		    /*THIS BLOCK IS FOR INSERT*/

			INSERT INTO [dbo].[PatientTAndCMapping]
			   ([UserId]
			   ,[TAndCId]
			   ,[SignnedDate]
			   ,[Active]
			   ,[AddedBy]
			   ,[AddedDate])
			VALUES
			   (@PATIENT_ID
			   ,@T_AND_C_ID
			   ,GETUTCDATE()
			   ,1
			   ,@USER_ID
			   ,GETUTCDATE())
			   
		END
END



GO



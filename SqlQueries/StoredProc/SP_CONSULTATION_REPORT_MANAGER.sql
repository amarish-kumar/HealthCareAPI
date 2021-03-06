USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_REPORT_MANAGER]    Script Date: 4/8/2018 10:17:58 PM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_REPORT_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_REPORT_MANAGER]    Script Date: 4/8/2018 10:17:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_CONSULTATION_REPORT_MANAGER]
(
	@CONSULTATION_REPORT_XML AS XML,
	@FILE_DATA AS VARBINARY(MAX) = NULL,
	@OPERATION AS NVARCHAR(100) = NULL, --CONVERSATION FOR START/UPDATE CONVERSATION RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @ConsultationId AS BIGINT, @CountryId AS BIGINT
DECLARE @FileName AS NVARCHAR(MAX),@Description AS NVARCHAR(MAX),@DoctorName AS NVARCHAR(MAX)
,@DoctorPhoneNumber AS NVARCHAR(MAX),@LabName AS NVARCHAR(MAX), @ReturnMessage as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT
DECLARE @ReportDate AS DATETIME
DECLARE @FileData AS NVARCHAR(MAX)

SELECT	 @Id = ConsultationReportList.Columns.value('Id[1]', 'BIGINT')
	   , @ConsultationId = ConsultationReportList.Columns.value('ConsultationId[1]', 'BIGINT')
	   , @CountryId = ConsultationReportList.Columns.value('CountryId[1]', 'BIGINT')
	   , @FileName = ConsultationReportList.Columns.value('FileName[1]', 'nvarchar(max)')
	   , @Description = ConsultationReportList.Columns.value('Description[1]', 'nvarchar(max)')
	   , @DoctorName = ConsultationReportList.Columns.value('DoctorName[1]', 'nvarchar(max)')
	   , @DoctorPhoneNumber = ConsultationReportList.Columns.value('DoctorPhoneNumber[1]', 'nvarchar(max)')
	   , @LabName = ConsultationReportList.Columns.value('LabName[1]', 'nvarchar(max)')
	   , @ReportDate = ConsultationReportList.Columns.value('ReportDate[1]', 'DATETIME')
	   , @Active = ConsultationReportList.Columns.value('Active[1]', 'bit')
FROM   @CONSULTATION_REPORT_XML.nodes('ConsultationReports') AS ConsultationReportList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION = 'ConsultationReport'
BEGIN

	IF @Id IS NULL OR @Id =0 
	BEGIN

	INSERT INTO [dbo].[ConsultationReports]
           ([ConsultationId]
           ,[FileName]
           ,[FileData]
           ,[Description]
           ,[DoctorName]
           ,[DoctorPhoneNumber]
           ,[CountryId]
           ,[ReportDate]
           ,[LabName]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@ConsultationId
           ,@FileName
           ,@FILE_DATA
           ,@Description
           ,@DoctorName
           ,@DoctorPhoneNumber
           ,@CountryId
           ,@ReportDate
           ,@LabName
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

			SET @Result = 1;
			SET @ReturnMessage = 'Record created successfully.'
	END

	ELSE
	BEGIN

		UPDATE [dbo].[ConsultationReports]
		   SET [FileName] = ISNULL(@FileName,[FileName])
			  ,[FileData] = ISNULL(@FILE_DATA, [FileData])
			  ,[Description] = @Description
			  ,[DoctorName] = @DoctorName
			  ,[DoctorPhoneNumber] = @DoctorPhoneNumber
			  ,[CountryId] = @CountryId
			  ,[ReportDate] = @ReportDate
			  ,[LabName] = @LabName
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



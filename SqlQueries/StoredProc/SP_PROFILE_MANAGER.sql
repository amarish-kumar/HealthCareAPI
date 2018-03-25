USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_PROFILE_MANAGER]    Script Date: 3/25/2018 9:17:48 AM ******/
DROP PROCEDURE [dbo].[SP_PROFILE_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_PROFILE_MANAGER]    Script Date: 3/25/2018 9:17:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_PROFILE_MANAGER]
(
	@PROFILE_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR CREATE/UPDATE, PASS EXCLUSIVE VALUES FOR USER DISABLING ETC
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED
)
AS

BEGIN

/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @GenderId AS BIGINT, @UserId AS BIGINT, @RelationshipId AS BIGINT
DECLARE @FirstName AS NVARCHAR(MAX), @LastName AS NVARCHAR(MAX)
,@DOB AS NVARCHAR(MAX), @IsDefault AS BIT, @Active AS BIT

SELECT @Id = ProfileList.Columns.value('Id[1]', 'BIGINT')
	   , @UserId = ProfileList.Columns.value('UserId[1]', 'BIGINT')
	   , @RelationshipId = ProfileList.Columns.value('RelationshipId[1]', 'BIGINT')
	   , @FirstName = ProfileList.Columns.value('FirstName[1]', 'nvarchar(max)')
	   , @LastName = ProfileList.Columns.value('LastName[1]', 'nvarchar(max)')
	   , @GenderId = ProfileList.Columns.value('GenderId[1]', 'BIGINT')	   
	   , @DOB = ProfileList.Columns.value('DOB[1]', 'nvarchar(max)')	   
	   , @Active = ProfileList.Columns.value('Active[1]', 'bit')
	   , @IsDefault = ProfileList.Columns.value('IsDefault[1]', 'bit')
FROM   @PROFILE_XML.nodes('Profile') AS ProfileList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/

IF @Id IS NOT NULL
	
	BEGIN		

	INSERT INTO [dbo].[Profile]
			   ([UserId]
			   ,[RelationshipId]
			   ,[FirstName]
			   ,[LastName]
			   ,[GenderId]
			   ,[DOB]
			   ,[Active]
			   ,[AddedBy]
			   ,[AddedDate]
			   ,[IsDefault])
		 VALUES
			   (@UserId
			   ,@RelationshipId
			   ,@FirstName
			   ,@LastName
			   ,@GenderId
			   ,@DOB
			   ,@Active
			   ,@USER_ID
			   ,GETUTCDATE()
			   ,@IsDefault)
	
		SET @Id = @@IDENTITY
	END

	ELSE
		BEGIN	

			UPDATE [dbo].[Profile]
			   SET [RelationshipId] = @RelationshipId
				  ,[FirstName] = @FirstName
				  ,[LastName] = @LastName
				  ,[GenderId] = @GenderId
				  ,[DOB] = @DOB
				  ,[Active] = @Active
				  ,[ModifiedBy] = @USER_ID
				  ,[ModifiedDate] = GETUTCDATE()
				  ,[IsDefault] = @IsDefault
			 WHERE Id = @Id


		END
	END

SELECT @Id AS Id


GO



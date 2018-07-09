USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_ADDRESS_MANAGER]    Script Date: 6/29/2018 9:59:32 AM ******/
DROP PROCEDURE [dbo].[SP_ADDRESS_MANAGER]
GO

/****** Object:  StoredProcedure [dbo].[SP_ADDRESS_MANAGER]    Script Date: 6/29/2018 9:59:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ADDRESS_MANAGER]
(
	@USER_ADDRESS_XML AS XML,
	@OPERATION AS NVARCHAR(100) = NULL, --NULL FOR INSERT/UPDATE ALLERGY RECORD
	@USER_ID BIGINT = 1 --SET THE DEFAULT VALUE TO 1 IF NOT PASSED,
)
AS

BEGIN


/*BLOCK TO READ THE VARIABLES*/

DECLARE @Id AS BIGINT, @UserId AS BIGINT, @AddressTypeId AS BIGINT, @CountryId AS BIGINT, @StateId AS BIGINT
DECLARE @ReturnMessage as NVARCHAR(MAX), @Address1 as NVARCHAR(MAX), @Address2 as NVARCHAR(MAX), @ZipCode as NVARCHAR(MAX)
, @City as NVARCHAR(MAX)
DECLARE @Active AS BIT, @Result as BIT

SELECT	 @Id = UserAddressMappingList.Columns.value('Id[1]', 'BIGINT')
	   , @UserId = UserAddressMappingList.Columns.value('UserId[1]', 'BIGINT')
	   , @AddressTypeId = UserAddressMappingList.Columns.value('AddressTypeId[1]', 'BIGINT')   
	   , @CountryId = UserAddressMappingList.Columns.value('CountryId[1]', 'BIGINT')
	   , @StateId = UserAddressMappingList.Columns.value('State[1]', 'BIGINT')
	   , @City = UserAddressMappingList.Columns.value('City[1]', 'NVARCHAR(MAX)')
	   , @Address1 = UserAddressMappingList.Columns.value('Address1[1]', 'NVARCHAR(MAX)')
	   , @Address2 = UserAddressMappingList.Columns.value('Address2[1]', 'NVARCHAR(MAX)')
	   , @ZipCode = UserAddressMappingList.Columns.value('ZipCode[1]', 'NVARCHAR(MAX)')
	   , @Active = UserAddressMappingList.Columns.value('Active[1]', 'bit')
FROM   @USER_ADDRESS_XML.nodes('UserAddress') AS UserAddressMappingList(Columns)

/*BLOCK TO READ THE VARIABLES ENDS HERE*/
IF @OPERATION IS NULL
BEGIN

	IF @Id IS NULL OR @Id = 0 
	BEGIN

	INSERT INTO [dbo].[UserAddressMapping]
           ([UserID]
           ,[AddressTypeID]
		   ,[Address1]
           ,[Address2]
           ,[City]
		   ,[State]
		   ,[ZipCode]
		   ,[CountryID]
           ,[Active]
           ,[AddedBy]
           ,[AddedDate])
     VALUES
           (@UserId
           ,@AddressTypeId
		   ,@Address1
           ,@Address2
		   ,@City
		   ,@StateId
		   ,@ZipCode
		   ,@CountryId
           ,@Active
		   ,@USER_ID
		   ,GETUTCDATE())

	SET @Result = 1;
	SET @ReturnMessage = 'Record created successfully.'

	END

	ELSE
	BEGIN

		UPDATE [dbo].[UserAddressMapping]
		   SET [AddressTypeID] = ISNULL(@AddressTypeId,[AddressTypeID])
			  ,[Address1] = ISNULL(@Address1,[Address1])
			  ,[Address2] = ISNULL(@Address2,[Address2])
			  ,[City] = ISNULL(@City, [City])
			  ,[State] = ISNULL(@StateId, [State])
			  ,[ZipCode] = ISNULL(@ZipCode, [ZipCode])
			  ,[CountryID] = ISNULL(@CountryId, [CountryID])
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



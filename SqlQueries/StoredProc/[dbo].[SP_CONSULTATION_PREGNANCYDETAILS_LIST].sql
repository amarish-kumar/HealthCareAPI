USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_PREGNANCYDETAILS_LIST]    Script Date: 4/29/2018 8:05:09 PM ******/
DROP PROCEDURE [dbo].[SP_CONSULTATION_PREGNANCYDETAILS_LIST]
GO

/****** Object:  StoredProcedure [dbo].[SP_CONSULTATION_PREGNANCYDETAILS_LIST]    Script Date: 4/29/2018 8:05:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_CONSULTATION_PREGNANCYDETAILS_LIST]
(	
	@CONSULTATION_ID BIGINT,
	@CONSULTATION_PREGNANCYDETAILS_ID BIGINT = NULL
)
AS

BEGIN

--EXEC [SP_CONSULTATION_PREGNANCYDETAILS_LIST] 10018

Declare @symptomIDList nvarchar(max), @symptomID bigint,
@symptomDesc nvarchar(max), @symptomDescList nvarchar(max);

set @symptomDescList = '';

select @symptomIDList=CPD.[MCSymptomID] FROM [ConsultationPregnancyDetails] CPD
	INNER JOIN Consultation C ON CPD.ConsultationId = C.Id
	WHERE C.Id = @CONSULTATION_ID

create table #TempDescID
(Description NVARCHAR(max))

insert into #TempDescID
select x.item from dbo.Split(@symptomIDList, ' ') x

DECLARE enq_cursor CURSOR FAST_FORWARD FOR
select Description from #TempDescID 

OPEN enq_cursor
FETCH NEXT FROM enq_cursor INTO @symptomID

WHILE @@FETCH_STATUS = 0
BEGIN
   
	print @symptomID

	select @symptomDesc=SymptomDescription from dbo.MenstrualSymptomsMaster where id = @symptomID

	print  @symptomDesc
	
	set @symptomDescList = @symptomDescList + @symptomDesc + ',';

	print  @symptomDescList

    FETCH NEXT FROM enq_cursor INTO @symptomID
END

CLOSE enq_cursor
DEALLOCATE enq_cursor



SELECT CPD.Id
	, CPD.ConsultationId
	, CPD.[CurrentlyPregnant]
	, CPD.[CurrentPregnancyMonths]
	, CPD.[CurrentPregnancyEDD]
	, CPD.[PregnantBefore]
	, CPD.[MenstrualCycles]
	, CPD.[NoMCReason]
	, CPD.[LastMCCycle]
	, CPD.[MCRegInterval]
	, CPD.[LenMCCycle]
	, CPD.[MCStartAge]
	, CPD.[MCFlow]
	, CPD.[MCProductType]
	, CPD.[MCProductPerDay]
	, CPD.[MCPain]
	, CPD.[MCPainSeverity]
	, CPD.[MCSymptomID]
	, (select reverse(stuff(reverse(@symptomDescList), 1, 1, ''))) as MCSymptomDesc
	, CPD.AddedBy
	, CPD.AddedDate
	, CPD.ModifiedBy
	, CPD.ModifiedDate	
	FROM [ConsultationPregnancyDetails] CPD
	INNER JOIN Consultation C ON CPD.ConsultationId = C.Id
	WHERE C.Id = @CONSULTATION_ID
	AND (@CONSULTATION_PREGNANCYDETAILS_ID IS NULL OR CPD.Id = @CONSULTATION_PREGNANCYDETAILS_ID)
	ORDER BY CPD.AddedDate DESC
END



drop table #TempDescID


GO



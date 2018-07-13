use [HealthCare]
go
sp_rename '[dbo].[ConsultationStatusesLog].doctorid','Userid','COLUMN'
go

ALTER TABLE [dbo].[ConsultationStatusesLog] ALTER COLUMN StatusChangeReasonId bigint NULL
go

SELECT table_name [Table Name], column_name [Column Name]
FROM information_schema.columns where data_type = 'BIGINT'
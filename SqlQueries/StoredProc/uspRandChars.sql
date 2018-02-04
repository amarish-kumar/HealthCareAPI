USE [HealthCare]
GO

/****** Object:  StoredProcedure [dbo].[uspRandChars]    Script Date: 1/20/2018 7:01:57 PM ******/
DROP PROCEDURE [dbo].[uspRandChars]
GO

/****** Object:  StoredProcedure [dbo].[uspRandChars]    Script Date: 1/20/2018 7:01:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[uspRandChars]
    @len int,
    @min tinyint = 48,
    @range tinyint = 74,
    @exclude varchar(50) = '0:;<=>?@O[]`^\/',
    @output varchar(50) output
as 
    declare @char char
    set @output = ''
 
    while @len > 0 begin
       select @char = char(round(rand() * @range + @min, 0))
       if charindex(@char, @exclude) = 0 begin
           set @output += @char
           set @len = @len - 1
       end
    end
;

GO



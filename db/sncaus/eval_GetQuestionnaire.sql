USE [sncaus]
GO

/****** Object:  StoredProcedure [dbo].[eval_GetQuestionnaire]    Script Date: 20-04-2021 7.20.51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Shailendar>
-- Create date: <16/04/2021>
-- Description:	<To get questionnaire>
-- =============================================
CREATE PROCEDURE [dbo].[eval_GetQuestionnaire]
	@clientCode INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 

    select distinct r.qnr_ref, r.qnr_desc as description ,
	cast( (case r.allow_webexpress when '1' then 1 else 0 end)as bit) as allow_webexpress,
	cast( (case r.allow_gapconnect when '1' then 1 else 0 end)as bit) as allow_gapconnect,
	cast( (case r.allow_kodo when '1' then 1 else 0 end)as bit) as allow_kodo,
	s.client_code,
	c.client_name
	from rpt_qtnnaire_header r (nolock) 
	join scenario_information s (nolock) on r.qnr_ref = s.questionnaire_ref
	join client_information c (nolock) on s.client_code = c.client_code
	where s.client_code= @clientCode
	order by qnr_ref desc
END


GO



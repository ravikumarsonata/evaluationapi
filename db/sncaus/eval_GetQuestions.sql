USE [sncaus]
GO

/****** Object:  StoredProcedure [dbo].[eval_GetQuestions]    Script Date: 21-04-2021 1.10.02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Shailendar>
-- Create date: <20/04/2021>
-- Description:	<To get questions>
-- =============================================
CREATE PROCEDURE [dbo].[eval_GetQuestions]
	@qnrRef int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	select *
		 from rpt_questionnaire (nolock)
		 where Qnr_Ref = @qnrRef

END

GO



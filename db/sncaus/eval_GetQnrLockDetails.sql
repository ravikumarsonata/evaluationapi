USE [sncaus]
GO

/****** Object:  StoredProcedure [dbo].[eval_GetQnrLockDetails]    Script Date: 21-04-2021 1.11.34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Shailendar>
-- Create date: <20/04/2021>
-- Description:	<To get Questionnaire lock details>
-- =============================================
CREATE PROCEDURE [dbo].[eval_GetQnrLockDetails]
	@qnr_ref as int  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 

    SELECT TOP 1 *
			FROM sncaus.dbo.MLToolLock_ChangeInfo NOLOCK
			WHERE qnrRef = @qnr_ref
			AND LockedDate IS NULL
			ORDER BY UnlockedDate DESC
END


GO



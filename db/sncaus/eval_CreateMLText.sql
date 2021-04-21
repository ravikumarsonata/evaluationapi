USE [i18n]
GO

/****** Object:  StoredProcedure [dbo].[eval_CreateMLText]    Script Date: 20-04-2021 1.37.15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Shailendar>
-- Create date: <20/04/2021>
-- Description:	<Create ML Text>
-- =============================================
CREATE PROCEDURE [dbo].[eval_CreateMLText]
	@mltext varchar(8000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	insert into i18n.dbo.ML_TextLib1033 (mltext)
		values (@mltext)

	select @@identity as textid 

END

GO



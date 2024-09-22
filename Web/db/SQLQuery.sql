USE [QLPhong]
GO
/****** Object:  Table [dbo].[phonghoc]    Script Date: 22/09/2024 9:14:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[phonghoc](
	[ma_phong] [int] NOT NULL,
	[ten_phong] [nvarchar](50) NULL,
	[trang_thai] [int] NULL,
 CONSTRAINT [PK_phonghoc] PRIMARY KEY CLUSTERED 
(
	[ma_phong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[phonghoc] ([ma_phong], [ten_phong], [trang_thai]) VALUES (1, N'Phòng 1 ', 1)
GO
INSERT [dbo].[phonghoc] ([ma_phong], [ten_phong], [trang_thai]) VALUES (2, N'Phòng 2', 2)
GO
INSERT [dbo].[phonghoc] ([ma_phong], [ten_phong], [trang_thai]) VALUES (3, N'Phòng 3', 3)
GO
INSERT [dbo].[phonghoc] ([ma_phong], [ten_phong], [trang_thai]) VALUES (4, N'Phòng 4', 3)
GO
INSERT [dbo].[phonghoc] ([ma_phong], [ten_phong], [trang_thai]) VALUES (5, N'Phòng 5', 1)
GO
INSERT [dbo].[phonghoc] ([ma_phong], [ten_phong], [trang_thai]) VALUES (6, N'Phòng 6', 2)
GO
/****** Object:  StoredProcedure [dbo].[SP_API]    Script Date: 22/09/2024 9:14:37 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Trần Quang Đức
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_API]
	@action varchar(50) = 'get_status'
AS
BEGIN

    IF (@action = 'get_status')
    BEGIN        
        SELECT 
            ma_phong,  
            trang_thai
        FROM phonghoc
        FOR JSON PATH;
    END
END
GO

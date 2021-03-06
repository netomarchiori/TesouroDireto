USE [master]
GO
/****** Object:  Database [WareHouseBD]    Script Date: 17/12/2014 03:41:32 ******/
CREATE DATABASE [WareHouseBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WareHouseBD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\WareHouseBD.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WareHouseBD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\WareHouseBD_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WareHouseBD] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WareHouseBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WareHouseBD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WareHouseBD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WareHouseBD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WareHouseBD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WareHouseBD] SET ARITHABORT OFF 
GO
ALTER DATABASE [WareHouseBD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WareHouseBD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WareHouseBD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WareHouseBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WareHouseBD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WareHouseBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WareHouseBD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WareHouseBD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WareHouseBD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WareHouseBD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WareHouseBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WareHouseBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WareHouseBD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WareHouseBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WareHouseBD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WareHouseBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WareHouseBD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WareHouseBD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WareHouseBD] SET  MULTI_USER 
GO
ALTER DATABASE [WareHouseBD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WareHouseBD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WareHouseBD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WareHouseBD] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [WareHouseBD] SET DELAYED_DURABILITY = DISABLED 
GO
USE [WareHouseBD]
GO
/****** Object:  Table [dbo].[tblAgentesCustodia]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAgentesCustodia](
	[idAgenteCustodia] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[InstituicaoFinan] [varchar](50) NOT NULL,
	[Integrado] [varchar](5) NOT NULL,
	[TaxaAdmin] [numeric](5, 2) NOT NULL,
	[TaxaDescricao] [varchar](80) NOT NULL,
	[Repasse] [varchar](10) NOT NULL,
	[AplicacaoProgramada] [varchar](50) NULL,
	[AtualizadoEm] [datetime] NULL,
 CONSTRAINT [PK_tblAgentesCustodia] PRIMARY KEY CLUSTERED 
(
	[idAgenteCustodia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblConfiguracao]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblConfiguracao](
	[idUsuarioConfig] [numeric](18, 0) NOT NULL,
	[AtualizacaoTitulos] [datetime] NOT NULL,
	[AtualizarTitulosAuto] [numeric](1, 0) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblTitulos]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTitulos](
	[idTitulo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[Vencimento] [date] NOT NULL,
	[TaxaCompra] [numeric](5, 2) NOT NULL CONSTRAINT [DF_tblTitulos_TaxaCompra]  DEFAULT ((0.00)),
	[ValorCompra] [numeric](18, 2) NOT NULL CONSTRAINT [DF_tblTitulos_ValorCompra]  DEFAULT ((0.00)),
	[TaxaVenda] [numeric](5, 2) NOT NULL CONSTRAINT [DF_tblTitulos_TaxaVenda]  DEFAULT ((0.00)),
	[ValorVenda] [numeric](18, 2) NOT NULL CONSTRAINT [DF_tblTitulos_ValorVenda]  DEFAULT ((0.00)),
	[idTituloTipo] [numeric](18, 0) NOT NULL,
	[AtualizadoEm] [datetime] NULL,
 CONSTRAINT [PK_tblTitulos] PRIMARY KEY CLUSTERED 
(
	[idTitulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTitulosAntigos]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTitulosAntigos](
	[idTitulo] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[Vencimento] [date] NOT NULL,
	[TaxaCompra] [numeric](5, 2) NOT NULL CONSTRAINT [DF_tblTitulosAntigos_TaxaCompra]  DEFAULT ((0.00)),
	[ValorCompra] [numeric](18, 2) NOT NULL CONSTRAINT [DF_tblTitulosAntigos_ValorCompra]  DEFAULT ((0.00)),
	[TaxaVenda] [numeric](5, 2) NOT NULL CONSTRAINT [DF_tblTitulosAntigos_TaxaVenda]  DEFAULT ((0.00)),
	[ValorVenda] [numeric](18, 2) NOT NULL CONSTRAINT [DF_tblTitulosAntigos_ValorVenda]  DEFAULT ((0.00)),
	[idTituloTipo] [numeric](18, 0) NOT NULL,
	[AtualizadoEm] [datetime] NOT NULL,
 CONSTRAINT [PK_tblTitulosAntigos] PRIMARY KEY CLUSTERED 
(
	[idTitulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTitulosUsuario]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTitulosUsuario](
	[idTitulosUsuario] [numeric](18, 0) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[Vencimento] [date] NOT NULL,
	[taxaCompra] [numeric](5, 2) NOT NULL,
	[ValorCompra] [numeric](18, 0) NOT NULL,
	[TaxaVenda] [numeric](5, 2) NULL,
	[ValorVenda] [numeric](18, 0) NULL,
	[DataCompra] [date] NOT NULL,
	[DataVenda] [date] NULL,
	[QuantCompra] [numeric](18, 1) NOT NULL,
	[QuantVenda] [numeric](18, 1) NULL,
	[idUsuario] [numeric](18, 0) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTituloTipo]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTituloTipo](
	[idTituloTipo] [numeric](18, 0) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tblTituloTipo] PRIMARY KEY CLUSTERED 
(
	[idTituloTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUsuario]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUsuario](
	[idUsuario] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Senha] [varchar](50) NOT NULL,
	[Nascimento] [date] NOT NULL,
	[idAgenteCustodia] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_tblUsuario] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tblTitulos]  WITH CHECK ADD  CONSTRAINT [FK_tblTitulos_tblTituloTipo] FOREIGN KEY([idTituloTipo])
REFERENCES [dbo].[tblTituloTipo] ([idTituloTipo])
GO
ALTER TABLE [dbo].[tblTitulos] CHECK CONSTRAINT [FK_tblTitulos_tblTituloTipo]
GO
ALTER TABLE [dbo].[tblTitulosUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tblTitulosUsuario_tblUsuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[tblUsuario] ([idUsuario])
GO
ALTER TABLE [dbo].[tblTitulosUsuario] CHECK CONSTRAINT [FK_tblTitulosUsuario_tblUsuario]
GO
ALTER TABLE [dbo].[tblUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tblUsuario_tblAgentesCustodia] FOREIGN KEY([idAgenteCustodia])
REFERENCES [dbo].[tblAgentesCustodia] ([idAgenteCustodia])
GO
ALTER TABLE [dbo].[tblUsuario] CHECK CONSTRAINT [FK_tblUsuario_tblAgentesCustodia]
GO
/****** Object:  StoredProcedure [dbo].[uspAgenteConsultar]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[uspAgenteConsultar]



as
begin
SELECT InstituicaoFinan,Integrado,AplicacaoProgramada,TaxaAdmin,TaxaDescricao,Repasse,AtualizadoEm FROM tblAgentesCustodia WHERE idAgenteCustodia>1;

end
GO
/****** Object:  StoredProcedure [dbo].[uspAgenteCustodia]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspAgenteCustodia]
@InstituicaoFinan varchar(50),
@Integrado varchar(50),
@TaxaAdimin numeric (5,2),
@TaxaDescricao varchar(80),
@Repasse varchar (10),
@AplicacaoProgramada varchar(50),
@AtualizadoEm datetime

AS
BEGIN

INSERT INTO tblAgentesCustodia VALUES (@InstituicaoFinan,@Integrado,@TaxaAdimin,@TaxaDescricao,@Repasse,@AplicacaoProgramada,@AtualizadoEm);

SELECT @@IDENTITY AS retorno;

END
GO
/****** Object:  StoredProcedure [dbo].[uspAgenteCustodiaDeletar]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspAgenteCustodiaDeletar]

AS
BEGIN

DELETE FROM tblAgentesCustodia WHERE idAgenteCustodia > 1


END
GO
/****** Object:  StoredProcedure [dbo].[uspTituloAtulizar]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspTituloAtulizar]
@idTitulo numeric (18,0),
@Descricao varchar(50),
@Vencimento date,
@TaxaCompra numeric(5,2),
@ValorCompra numeric(18,2),
@TaxaVenda numeric(5,2),
@ValorVenda numeric(18,2),
@idTituloTipo numeric(18,0)

AS
BEGIN

UPDATE  tblTitulos SET Descricao=@Descricao,Vencimento=@Vencimento,
						TaxaCompra=@TaxaCompra,ValorCompra=@ValorCompra,
						TaxaVenda=@TaxaVenda,ValorVenda=@ValorVenda,
						idTituloTipo=@idTituloTipo
WHERE idTitulo=@idTitulo;

END

GO
/****** Object:  StoredProcedure [dbo].[uspTituloInserir]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspTituloInserir]
@Descricao varchar(50),
@Vencimento date,
@TaxaCompra numeric(5,2),
@ValorCompra numeric(18,2),
@TaxaVenda numeric(5,2),
@ValorVenda numeric(18,2),
@idTituloTipo numeric(18,0),
@AtualizadoEm datetime

AS
BEGIN

INSERT INTO tblTitulosAntigos VALUES (@Descricao,@Vencimento,@TaxaCompra,@ValorCompra,@TaxaVenda,@ValorVenda,@idTituloTipo,@AtualizadoEm);

INSERT INTO tblTitulos VALUES (@Descricao,@Vencimento,@TaxaCompra,@ValorCompra,@TaxaVenda,@ValorVenda,@idTituloTipo,@AtualizadoEm);

SELECT @@IDENTITY AS RETORNO
END
GO
/****** Object:  StoredProcedure [dbo].[uspTitulosAntigosInserir]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspTitulosAntigosInserir]
@Descricao varchar(50),
@Vencimento date,
@TaxaCompra numeric(5,2),
@ValorCompra numeric(18,2),
@TaxaVenda numeric(5,2),
@ValorVenda numeric(18,2),
@idTituloTipo numeric(18,0),
@AtualizadoEm datetime

AS
BEGIN

INSERT INTO tblTitulosAntigos VALUES (@Descricao,@Vencimento,@TaxaCompra,@ValorCompra,@TaxaVenda,@ValorVenda,@idTituloTipo,@AtualizadoEm);

SELECT @@IDENTITY AS RETORNO

END
GO
/****** Object:  StoredProcedure [dbo].[uspTitulosConsultar]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspTitulosConsultar]

AS
BEGIN

SELECT TIT.Descricao,TIT.Vencimento,TIT.TaxaCompra,TIT.ValorCompra,TIT.TaxaVenda,TIT.ValorVenda,TIP.Descricao as Indexador,TIT.AtualizadoEm
FROM tblTitulos AS  TIT
INNER JOIN tblTituloTipo AS TIP
ON TIT.idTituloTipo = TIP.idTituloTipo

END
GO
/****** Object:  StoredProcedure [dbo].[uspTitulosDeletar]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspTitulosDeletar]

AS
BEGIN

DELETE FROM tblTitulos


END
GO
/****** Object:  StoredProcedure [dbo].[uspUsuarioConsultarSenha]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUsuarioConsultarSenha]
@Usuario varchar(50),
@Senha varchar(50)

AS
BEGIN
SELECT idUsuario,Nome,Email,Usuario,Senha,Nascimento,idAgenteCustodia FROM tblUsuario WHERE Usuario=@Usuario AND Senha = @Senha

END
GO
/****** Object:  StoredProcedure [dbo].[uspUsuarioExcluir]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUsuarioExcluir] 
@idUsuario int
AS
BEGIN
DELETE FROM tblUsuario WHERE idUsuario=@idUsuario;

END
GO
/****** Object:  StoredProcedure [dbo].[uspUsuarioInserirAtualizar]    Script Date: 17/12/2014 03:41:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspUsuarioInserirAtualizar] 
@Nome varchar(50),
@Email varchar(50),
@Usuario varchar(50),
@Senha varchar(50),
@Nascimento Date,
@idAgenteCustodia numeric (18,0),
@EmailCadastrado  Numeric(18,0) = 0 

AS
BEGIN

set @EmailCadastrado = (select count(*) from tblUsuario where Email = @Email);

if (@EmailCadastrado>=1)
	UPDATE tblUsuario SET NOME=@Nome,Usuario= @Usuario,Senha=@Senha, Nascimento=@Nascimento,idAgenteCustodia=@idAgenteCustodia where Email=@Email;
else
	INSERT INTO tblUsuario VALUES (@Nome,@Email,@Usuario,@Senha,@Nascimento,@idAgenteCustodia);

END
GO
USE [master]
GO
ALTER DATABASE [WareHouseBD] SET  READ_WRITE 
GO

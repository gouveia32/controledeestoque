-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           5.7.17-log - MySQL Community Server (GPL)
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Copiando estrutura do banco de dados para controledeestoque
CREATE DATABASE IF NOT EXISTS `controledeestoque` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `controledeestoque`;

-- Copiando estrutura para tabela controledeestoque.categoria
CREATE TABLE IF NOT EXISTS `categoria` (
  `cat_cod` int(11) NOT NULL AUTO_INCREMENT,
  `cat_nome` varchar(95) DEFAULT '0',
  PRIMARY KEY (`cat_cod`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela controledeestoque.categoria: 0 rows
/*!40000 ALTER TABLE `categoria` DISABLE KEYS */;
/*!40000 ALTER TABLE `categoria` ENABLE KEYS */;

-- Copiando estrutura para tabela controledeestoque.cliente
CREATE TABLE IF NOT EXISTS `cliente` (
  `cli_cod` int(11) NOT NULL AUTO_INCREMENT,
  `cli_nome` varchar(95) DEFAULT '',
  `cli_cpfcnpj` varchar(95) DEFAULT '',
  `cli_rgie` varchar(95) DEFAULT '',
  `cli_rsocial` varchar(95) DEFAULT '',
  `cli_tipo` varchar(20) DEFAULT '',
  `cli_cep` varchar(20) DEFAULT '',
  `cli_endereco` varchar(20) DEFAULT '',
  `cli_bairro` varchar(95) DEFAULT '',
  `cli_fone` varchar(95) DEFAULT '',
  `cli_cel` varchar(95) DEFAULT '',
  `cli_email` varchar(95) DEFAULT '',
  `cli_endnumero` varchar(95) DEFAULT '',
  `cli_cidade` varchar(95) DEFAULT '',
  `cli_estado` varchar(95) DEFAULT '',
  `cli_datanasc` date DEFAULT NULL,
  `cli_localtrabalho` varchar(95) DEFAULT '',
  `cli_fonetrabalho` varchar(95) DEFAULT '',
  PRIMARY KEY (`cli_cod`)
) ENGINE=MyISAM AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela controledeestoque.cliente: 1 rows
/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
INSERT INTO `cliente` (`cli_cod`, `cli_nome`, `cli_cpfcnpj`, `cli_rgie`, `cli_rsocial`, `cli_tipo`, `cli_cep`, `cli_endereco`, `cli_bairro`, `cli_fone`, `cli_cel`, `cli_email`, `cli_endnumero`, `cli_cidade`, `cli_estado`, `cli_datanasc`, `cli_localtrabalho`, `cli_fonetrabalho`) VALUES
	(6, 'JALECO & ACESSÓRIOS', '235.597.565-53', '  ,   ,   -', '', 'Fisíca', '     -', '', '', '(  )     -', '(  )      -', '', '', 'Aracaju', 'Se', '2001-01-01', '', '(  )     -');
/*!40000 ALTER TABLE `cliente` ENABLE KEYS */;

-- Copiando estrutura para tabela controledeestoque.compra
CREATE TABLE IF NOT EXISTS `compra` (
  `com_cod` int(11) NOT NULL AUTO_INCREMENT,
  `com_data` date DEFAULT NULL,
  `com_pagto_data` date DEFAULT NULL,
  `com_nfiscal` int(11) DEFAULT '0',
  `com_pagto_total` decimal(8,2) DEFAULT '0.00',
  `com_pagto_dinheiro` decimal(8,2) DEFAULT '0.00',
  `com_pagto_cartao` decimal(8,2) DEFAULT '0.00',
  `com_nparcela` int(11) DEFAULT '0',
  `com_status` int(11) DEFAULT '0',
  `for_cod` int(11) DEFAULT '0',
  `tpa_cod` int(11) DEFAULT '0',
  PRIMARY KEY (`com_cod`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela controledeestoque.compra: 0 rows
/*!40000 ALTER TABLE `compra` DISABLE KEYS */;
/*!40000 ALTER TABLE `compra` ENABLE KEYS */;

-- Copiando estrutura para tabela controledeestoque.tipopagamento
CREATE TABLE IF NOT EXISTS `tipopagamento` (
  `tpa_cod` int(11) NOT NULL AUTO_INCREMENT,
  `tpa_nome` varchar(95) DEFAULT '0',
  PRIMARY KEY (`tpa_cod`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela controledeestoque.tipopagamento: 5 rows
/*!40000 ALTER TABLE `tipopagamento` DISABLE KEYS */;
INSERT INTO `tipopagamento` (`tpa_cod`, `tpa_nome`) VALUES
	(6, 'CARTÃO'),
	(7, 'DINHEIRO'),
	(8, 'PROMISSÓRIA'),
	(9, 'BOLETO'),
	(10, 'CHEQUE');
/*!40000 ALTER TABLE `tipopagamento` ENABLE KEYS */;

-- Copiando estrutura para tabela controledeestoque.usuarios
CREATE TABLE IF NOT EXISTS `usuarios` (
  `usu_cod` int(11) NOT NULL AUTO_INCREMENT,
  `usu_nome` varchar(95) DEFAULT '0',
  `usu_senha` varchar(95) DEFAULT '0',
  `usu_email` varchar(95) DEFAULT '0',
  `usu_ativo` tinyint(4) NOT NULL DEFAULT '0',
  `usu_tipo` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`usu_cod`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Copiando dados para a tabela controledeestoque.usuarios: 5 rows
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` (`usu_cod`, `usu_nome`, `usu_senha`, `usu_email`, `usu_ativo`, `usu_tipo`) VALUES
	(1, 'root', 'toor', 'guizadumodas@hotmail.com', 1, 1),
	(2, 'Administrador', '123456', 'ADM@hotmail.com', 1, 2),
	(3, 'Gerente', 'gerente', 'gerente@hotmail.com', 1, 3),
	(4, 'Operador', 'operador', 'operador@hotmail.com', 1, 4),
	(5, 'teste', 'teste', 'teste@hotmail.com', 0, 4);
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

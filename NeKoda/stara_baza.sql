-- MySQL dump 10.13  Distrib 5.6.17, for Win64 (x86_64)
--
-- Host: slavnik.fri.uni-lj.si    Database: t8_2015
-- ------------------------------------------------------
-- Server version	5.5.41-0ubuntu0.14.10.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Drzava`
--

DROP TABLE IF EXISTS `Drzava`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Drzava` (
  `idDrzava` int(11) NOT NULL,
  `imeDrzave` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idDrzava`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Drzava`
--

LOCK TABLES `Drzava` WRITE;
/*!40000 ALTER TABLE `Drzava` DISABLE KEYS */;
INSERT INTO `Drzava` VALUES (4,'Afganistan'),(8,'Albanija'),(10,'Antarktika'),(12,'Alžirija'),(16,'Ameriška Samoa'),(20,'Andora'),(24,'Angola'),(28,'Antigva in Barbuda'),(31,'Azerbajdžan'),(32,'Argentina'),(36,'Avstralija'),(40,'Avstrija'),(44,'Bahami'),(48,'Bahrajn'),(50,'Bangladeš'),(51,'Armenija'),(52,'Barbados'),(56,'Belgija'),(60,'Bermudi'),(64,'Butan'),(68,'Bolivija'),(70,'Bosna in Hercegovina'),(72,'Bocvana'),(74,'Bouvetov otok'),(76,'Brazilija'),(84,'Belize'),(86,'Britansko ozemlje Indijskega oceana'),(90,'Salomonovi otoki'),(92,'Deviški otoki (britanski)'),(96,'Brunej'),(100,'Bolgarija'),(104,'Mjanmar'),(108,'Burundi'),(112,'Belorusija'),(116,'Kambodža'),(120,'Kamerun'),(124,'Kanada'),(132,'Zelenortski otoki'),(136,'Kajmanski otoki'),(140,'Srednjeafriška republika'),(144,'Šrilanka'),(148,'Čad'),(152,'Čile'),(156,'Kitajska'),(158,'Tajvan'),(162,'Božični otok'),(166,'Kokosovi (Keeling) otoki'),(170,'Kolumbija'),(174,'Komori'),(175,'Mayotte'),(178,'Kongo'),(180,'Kongo'),(184,'Cookovi otoki'),(188,'Kostarika'),(191,'Hrvaška'),(192,'Kuba'),(196,'Ciper'),(203,'Češka republika'),(204,'Benin'),(208,'Danska'),(212,'Dominika'),(214,'Dominikanska republika'),(218,'Ekvador'),(222,'Salvador'),(226,'Ekvatorialna Gvineja'),(231,'Etiopija'),(232,'Eritreja'),(233,'Estonija'),(234,'Ferski otoki'),(238,'Falklandski otoki (Malvini)'),(239,'Južna Georgia in otoki Južni Sandwich'),(242,'Fidži'),(246,'Finska'),(248,'Alandski otoki'),(250,'Francija'),(254,'Francoska Gvajana'),(258,'Francoska Polinezija'),(260,'Francosko južno ozemlje'),(262,'Džibuti'),(266,'Gabon'),(268,'Gruzija'),(270,'Gambija'),(275,'Palestinsko ozemlje'),(276,'Nemčija'),(288,'Gana'),(292,'Gibraltar'),(296,'Kiribati'),(300,'Grčija'),(304,'Grenlandija'),(308,'Grenada'),(312,'Guadeloupe'),(316,'Guam'),(320,'Gvatemala'),(324,'Gvineja'),(328,'Gvajana'),(332,'Haiti'),(334,'Heardov otok in McDonaldovi otoki'),(336,'Sveti sedež (Vatikanska mestna država)'),(340,'Honduras'),(344,'Hongkong'),(348,'Madžarska'),(352,'Islandija'),(356,'Indija'),(360,'Indonezija'),(364,'Iran (Islamska republika)'),(368,'Irak'),(372,'Irska'),(376,'Izrael'),(380,'Italija'),(384,'Slonokoščena obala'),(388,'Jamajka'),(392,'Japonska'),(398,'Kazahstan'),(400,'Jordanija'),(404,'Kenija'),(408,'Koreja'),(410,'Koreja'),(414,'Kuvajt'),(417,'Kirgizistan'),(418,'Laoška ljudska demokratična republika'),(422,'Libanon'),(426,'Lesoto'),(428,'Latvija'),(430,'Liberija'),(434,'Libijska arabska džamahirija'),(438,'Lihtenštajn'),(440,'Litva'),(442,'Luksemburg'),(446,'Macau'),(450,'Madagaskar'),(454,'Malavi'),(458,'Malezija'),(462,'Maldivi'),(466,'Mali'),(470,'Malta'),(474,'Martinik'),(478,'Mavretanija'),(480,'Mauritius'),(484,'Mehika'),(492,'Monako'),(496,'Mongolija'),(498,'Moldavija'),(499,'Črna Gora'),(500,'Montserrat'),(504,'Maroko'),(508,'Mozambik'),(512,'Oman'),(516,'Namibija'),(520,'Nauru'),(524,'Nepal'),(528,'Nizozemska'),(530,'Nizozemski Antili'),(533,'Aruba'),(540,'Nova Kaledonija'),(548,'Vanuatu'),(554,'Nova Zelandija'),(558,'Nikaragva'),(562,'Niger'),(566,'Nigerija'),(570,'Niue'),(574,'Norfolški otok'),(578,'Norveška'),(580,'Severni Marianski otoki'),(581,'Stranski zunanji otoki Združenih držav'),(583,'Mikronezija (Federativne države)'),(584,'Marshallovi otoki'),(585,'Palau'),(586,'Pakistan'),(591,'Panama'),(598,'Papua Nova Gvineja'),(600,'Paragvaj'),(604,'Peru'),(608,'Filipini'),(612,'Pitcairn'),(616,'Poljska'),(620,'Portugalska'),(624,'Gvineja Bissau'),(626,'Vzhodni Timor'),(630,'Portoriko'),(634,'Katar'),(638,'Reunion'),(642,'Romunija'),(643,'Ruska federacija'),(646,'Ruanda'),(654,'Saint Helena'),(659,'Saint Kitts in Nevis'),(660,'Angvila'),(662,'Saint Lucia'),(666,'Saint Pierre in Miquelon'),(670,'Saint Vincent in Grenadine'),(674,'San Marino'),(678,'Sao Tome in Principe'),(682,'Saudova Arabija'),(686,'Senegal'),(688,'Srbija'),(690,'Sejšeli'),(694,'Sierra Leone'),(702,'Singapur'),(703,'Slovaška'),(704,'Vietnam'),(705,'Slovenija'),(706,'Somalija'),(710,'Južna Afrika'),(716,'Zimbabve'),(724,'Španija'),(732,'Zahodna Sahara'),(736,'Sudan'),(740,'Surinam'),(744,'Svalbard in Jan Mayen'),(748,'Svazi'),(752,'Švedska'),(756,'Švica'),(760,'Sirska arabska republika'),(762,'Tadžikistan'),(764,'Tajska'),(768,'Togo'),(772,'Tokelau'),(776,'Tonga'),(780,'Trinidad in Tobago'),(784,'Združeni arabski emirati'),(788,'Tunizija'),(792,'Turčija'),(795,'Turkmenistan'),(796,'Otoki Turks in Caicos'),(798,'Tuvalu'),(800,'Uganda'),(804,'Ukrajina'),(807,'Makedonija'),(818,'Egipt'),(826,'Združeno kraljestvo'),(831,'Guernsey'),(832,'Jersey'),(833,'Otok Man'),(834,'Tanzanija'),(840,'Združene države'),(850,'Deviški otoki (ZDA)'),(854,'Burkina Faso'),(858,'Urugvaj'),(860,'Uzbekistan'),(862,'Venezuela'),(876,'Otoki Wallis in Futuna'),(882,'Samoa'),(887,'Jemen'),(894,'Zambija'),(999,'NERAZVRŠČENO');
/*!40000 ALTER TABLE `Drzava` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `IzpitniRok`
--

DROP TABLE IF EXISTS `IzpitniRok`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `IzpitniRok` (
  `IDIzpitniRok` int(11) NOT NULL AUTO_INCREMENT,
  `DatumIzpitnegaRoka` datetime DEFAULT NULL,
  `VrstaIzpitnegaRoka` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `Prostor` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `SteviloMest` int(11) DEFAULT NULL,
  `IzvedbaPredmeta_idIzvedbaPredmeta` int(11) DEFAULT NULL,
  `Veljaven` int(11) DEFAULT NULL,
  PRIMARY KEY (`IDIzpitniRok`),
  KEY `fk_IzvedbaPredmeta_idIzvedbaPredmeta_idx` (`IzvedbaPredmeta_idIzvedbaPredmeta`),
  CONSTRAINT `fk_IzvedbaPredmeta_idIzvedbaPredmeta` FOREIGN KEY (`IzvedbaPredmeta_idIzvedbaPredmeta`) REFERENCES `IzvedbaPredmeta` (`idIzvedbaPredmeta`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=110 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `IzpitniRok`
--

LOCK TABLES `IzpitniRok` WRITE;
/*!40000 ALTER TABLE `IzpitniRok` DISABLE KEYS */;
INSERT INTO `IzpitniRok` VALUES (3,'2025-10-29 00:00:00','redni','PA, PB, P1',0,2,NULL),(5,'2025-10-29 00:00:00','redni','PA, PB, P1',0,4,NULL),(6,'2025-10-29 00:00:00','redni','PA, PB, P1',0,5,NULL),(7,'2025-10-29 00:00:00','redni','PA, PB, P1',0,6,NULL),(8,'2025-10-29 00:00:00','redni','PA, PB, P1',0,7,NULL),(9,'2025-10-29 00:00:00','redni','PA, PB, P1',0,8,NULL),(10,'2025-10-29 00:00:00','redni','PA, PB, P1',0,9,NULL),(11,'2025-10-29 00:00:00','redni','PA, PB, P1',0,10,NULL),(12,'2025-10-29 00:00:00','redni','PA, PB, P1',0,11,NULL),(18,'2015-05-29 00:00:00','redni','asd',0,18,NULL),(19,'2015-05-29 00:00:00','redni','asd',0,18,NULL),(24,'2015-05-29 00:00:00','redni','sad',0,12,NULL),(25,'2015-05-29 00:00:00','izredni','asd',0,12,NULL),(29,'2015-06-11 23:30:00','redni','ASD',1,5,NULL),(30,'2015-05-30 00:00:00','izredni','asd',0,18,NULL),(34,'2015-06-03 00:00:00','izredni','asd',0,9,NULL),(44,'2015-06-26 00:00:00','izredni','P1',0,30,NULL),(49,'2015-03-16 00:00:00','izredni','ADJEEEEE',565,30,NULL),(53,'2015-06-18 00:00:00','izredni','Gal',0,28,NULL),(56,'2015-06-20 00:00:00','izredni','LUKA',0,47,NULL),(57,'2015-06-25 00:00:00','redni','asd',0,47,NULL),(58,'2015-06-19 00:00:00','izredni','Ga',0,25,NULL),(59,'2015-06-27 00:00:00','izredni','asd',0,59,NULL),(60,'2015-06-05 00:00:00','izredni','asd',0,31,1),(61,'2015-06-13 00:00:00','izredni','asd',0,35,0),(62,'2015-06-27 00:00:00','izredni','P1',0,35,0),(63,'2015-05-19 10:15:00','redni','P1',60,63,1),(64,'2015-06-12 00:00:00','izredni','asda',0,72,1),(65,'2015-06-13 00:00:00','izredni','asd',0,48,1),(66,'2015-06-13 00:00:00','izredni','asd',0,16,0),(67,'2015-06-19 00:00:00','izredni','sad',0,56,1),(68,'2015-06-12 01:00:00','izredni','asdfghj',0,35,1),(69,'2015-06-12 00:00:00','redni','sad',0,24,0),(70,'2015-06-06 10:00:00','redni','P1',60,31,1),(71,'2015-08-06 03:30:00','redni','Petelino ',0,35,0),(72,'2015-06-20 12:00:00','redni','P1',1,25,1),(73,'2015-06-18 22:00:00','redni','asd',0,35,0),(74,'2015-06-20 00:00:00','redni','asd',1,35,0),(75,'2015-06-19 02:00:00','izredni','sada',0,35,0),(76,'2015-08-07 12:00:00','izredni','P1',0,35,0),(77,'2015-06-26 12:00:00','izredni','MUHAHAHA',2,35,0),(78,'2015-09-18 00:00:00','izredni','asd',0,35,0),(79,'2014-07-23 00:00:00','redni','P2',3,35,1),(80,'2015-06-01 10:15:00','redni','P2',30,63,1),(81,'2013-07-17 12:00:00','redni','P2',30,1,1),(82,'2013-07-17 12:00:00','redni','P1',39,81,1),(83,'2015-07-09 00:00:00','izredni','',0,35,1),(84,'2015-09-11 00:00:00','izredni','',0,35,1),(85,'2015-08-14 00:00:00','izredni','',0,35,1),(86,'2014-06-05 00:00:00','redni','Luka Test',9,81,1),(87,'2014-06-06 00:00:00','izredni','Luka-Test 1',5,56,1),(88,'2015-07-30 00:00:00','izredni','Luka test 2',17,59,1),(89,'2014-06-09 00:00:00','redni','Luka Test',7,81,1),(90,'2015-06-08 00:00:00','redni','',0,18,1),(91,'2015-07-01 00:00:00','redni','',0,18,1),(92,'2014-09-10 00:00:00','izredni','MatejTest',0,82,1),(93,'2015-07-03 00:00:00','redni','',0,63,1),(94,'2015-06-06 00:00:00','redni','Zamujen Rok',4,60,1),(95,'2015-06-04 00:00:00','izredni','test PremaloCasa',10,78,1),(96,'2015-06-09 00:00:00','redni','test PremaloCasa',5,78,1),(97,'2015-06-05 11:30:02','mimo roka',NULL,NULL,18,0),(98,'2015-06-05 11:30:58','mimo roka',NULL,NULL,23,0),(99,'2015-06-05 11:34:20','mimo roka',NULL,NULL,81,0),(100,'2014-06-08 00:00:00','izredni','Test Prevec Na leto',1,61,1),(101,'2014-06-19 00:00:00','redni','Test Prevec Na leto',2,61,1),(102,'2014-06-27 00:00:00','redni','Test Prevec Na leto',3,61,1),(103,'2015-06-17 00:00:00','redni','Test Prevec Na Leto',4,61,1),(104,'2015-05-08 00:00:00','redni','test-nezakljucena',1,79,1),(105,'2015-06-22 00:00:00','izredni','test-nezakljucena',2,79,1),(106,'2015-06-17 00:00:00','izredni','Test limit 6',1,77,1),(107,'2015-06-25 00:00:00','izredni','Test limit 6 0',2,77,1),(108,'2015-06-16 00:00:00','izredni','p',2,64,0),(109,'2015-06-08 20:00:00','izredni','Zamujen ROk (odjava)',2,60,1);
/*!40000 ALTER TABLE `IzpitniRok` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `IzvedbaPredmeta`
--

DROP TABLE IF EXISTS `IzvedbaPredmeta`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `IzvedbaPredmeta` (
  `idIzvedbaPredmeta` int(11) NOT NULL AUTO_INCREMENT,
  `Profesor_idProfesor` int(11) NOT NULL,
  `Profesor_idProfesor1` int(11) DEFAULT NULL,
  `Profesor_idProfesor2` int(11) DEFAULT NULL,
  `StudijskoLeto_idStudijskoLeto` int(11) NOT NULL,
  `PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId` int(11) NOT NULL,
  PRIMARY KEY (`idIzvedbaPredmeta`),
  KEY `fk_IzvedbaPredmeta_Profesor1_idx` (`Profesor_idProfesor`),
  KEY `fk_IzvedbaPredmeta_Profesor2_idx` (`Profesor_idProfesor1`),
  KEY `fk_IzvedbaPredmeta_Profesor3_idx` (`Profesor_idProfesor2`),
  KEY `fk_IzvedbaPredmeta_ŠtudijskoLeto1_idx` (`StudijskoLeto_idStudijskoLeto`),
  KEY `fk_IzvedbaPredmeta_PredmetStudijskegaPrograma1_idx` (`PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId`),
  CONSTRAINT `fk_IzvedbaPredmeta_PredmetStudijskegaPrograma1` FOREIGN KEY (`PredmetStudijskegaPrograma_PredmetStudijskegaProgramaId`) REFERENCES `PredmetStudijskegaPrograma` (`IdPredmetStudijskegaPrograma`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_IzvedbaPredmeta_Profesor1` FOREIGN KEY (`Profesor_idProfesor`) REFERENCES `Profesor` (`idProfesor`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_IzvedbaPredmeta_Profesor2` FOREIGN KEY (`Profesor_idProfesor1`) REFERENCES `Profesor` (`idProfesor`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_IzvedbaPredmeta_Profesor3` FOREIGN KEY (`Profesor_idProfesor2`) REFERENCES `Profesor` (`idProfesor`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_IzvedbaPredmeta_ŠtudijskoLeto1` FOREIGN KEY (`StudijskoLeto_idStudijskoLeto`) REFERENCES `StudijskoLeto` (`idStudijskoLeto`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=83 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `IzvedbaPredmeta`
--

LOCK TABLES `IzvedbaPredmeta` WRITE;
/*!40000 ALTER TABLE `IzvedbaPredmeta` DISABLE KEYS */;
INSERT INTO `IzvedbaPredmeta` VALUES (1,5,NULL,NULL,1,46),(2,12,NULL,NULL,1,50),(3,13,NULL,NULL,1,51),(4,10,5,5,1,52),(5,12,27,NULL,1,53),(6,14,NULL,NULL,1,54),(7,11,NULL,NULL,1,55),(8,7,NULL,NULL,1,57),(9,6,NULL,NULL,1,58),(10,8,NULL,NULL,1,59),(11,9,NULL,NULL,1,60),(12,10,NULL,NULL,1,118),(14,12,27,NULL,11,102),(15,26,NULL,NULL,11,122),(16,24,NULL,NULL,11,123),(17,19,NULL,NULL,11,106),(18,15,NULL,NULL,11,96),(19,41,NULL,NULL,11,124),(20,14,NULL,NULL,11,98),(21,16,NULL,NULL,11,105),(23,7,NULL,NULL,11,101),(24,17,NULL,NULL,11,46),(25,10,NULL,NULL,11,126),(26,9,NULL,NULL,11,117),(27,15,NULL,NULL,11,127),(28,43,NULL,NULL,11,49),(29,25,NULL,NULL,11,50),(30,15,NULL,10,11,128),(31,22,23,NULL,11,52),(35,17,NULL,NULL,11,129),(36,18,NULL,NULL,11,133),(37,19,NULL,NULL,11,134),(38,20,40,NULL,11,131),(39,21,NULL,NULL,11,132),(40,22,NULL,NULL,11,130),(41,23,NULL,NULL,11,57),(42,24,NULL,NULL,11,58),(43,12,27,NULL,11,53),(44,25,NULL,NULL,11,54),(45,26,NULL,NULL,11,55),(47,27,NULL,NULL,11,66),(48,28,NULL,NULL,11,114),(49,45,NULL,NULL,11,60),(50,45,NULL,NULL,11,59),(55,29,NULL,NULL,11,115),(56,18,NULL,NULL,11,67),(57,20,NULL,NULL,11,68),(58,46,NULL,NULL,11,69),(59,30,NULL,NULL,11,135),(60,35,NULL,NULL,11,73),(61,39,NULL,NULL,11,74),(62,8,NULL,NULL,11,75),(63,40,41,NULL,11,76),(64,26,NULL,NULL,11,136),(65,13,NULL,NULL,11,78),(66,47,NULL,NULL,11,79),(67,19,NULL,NULL,11,80),(68,16,16,NULL,11,81),(69,25,NULL,NULL,11,82),(70,13,NULL,NULL,11,83),(71,30,NULL,NULL,11,84),(72,48,NULL,NULL,11,137),(73,41,NULL,NULL,11,121),(74,10,NULL,NULL,11,86),(75,34,NULL,NULL,11,87),(76,40,NULL,NULL,11,88),(77,37,NULL,NULL,11,89),(78,34,NULL,NULL,11,90),(79,31,NULL,NULL,11,91),(81,32,NULL,NULL,10,66),(82,33,NULL,NULL,10,96);
/*!40000 ALTER TABLE `IzvedbaPredmeta` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Klasius`
--

DROP TABLE IF EXISTS `Klasius`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Klasius` (
  `Id` int(11) NOT NULL,
  `Opis` varchar(100) COLLATE utf8_slovenian_ci NOT NULL,
  `stopnjaStudija_IdStopnjaStudija` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_stopnjaStudija_IdStopnjaStudija_idx` (`stopnjaStudija_IdStopnjaStudija`),
  CONSTRAINT `fk_stopnjaStudija_IdStopnjaStudija` FOREIGN KEY (`stopnjaStudija_IdStopnjaStudija`) REFERENCES `stopnjaStudija` (`idStopnjaStudija`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Klasius`
--

LOCK TABLES `Klasius` WRITE;
/*!40000 ALTER TABLE `Klasius` DISABLE KEYS */;
INSERT INTO `Klasius` VALUES (12001,'Osnovnošolska izobrazba',NULL),(13001,'Nižja poklicna izobrazba',NULL),(14001,'Srednja poklicna izobrazba',NULL),(15001,'Srednja strokovna izobrazba',NULL),(15002,'Srednja splošna izobrazba',NULL),(16101,'Višja strokovna izobrazba',NULL),(16102,'Višješolska izobrazba (predbolonjska)',NULL),(16201,'Specializacija po višješolski izobrazbi (predbolonjska)',NULL),(16202,'Visokošolska strokovna izobrazba (predbolonjska)',1),(16203,'Visokošolska strokovna izobrazba (prva bolonjska stopnja)',5),(16204,'Visokošolska univerzitetna izobrazba (prva bolonjska stopnja)',6),(17001,'Specializacija po visokošolski strokovni izobrazbi (predbolonjska)',NULL),(17002,'Visokošolska univerzitetna izobrazba (predbolonjska)',2),(17003,'Magistrska izobrazba (druga bolonjska stopnja)',7),(18101,'Specializacija po univerzitetni izobrazbi (predbolonjska)',NULL),(18102,'Magisterij znanosti (predbolonjska)',3),(18201,'Doktorat znanosti (predbolonjska)',4),(18202,'Doktorat znanosti (tretja bolonjska stopnja)',8);
/*!40000 ALTER TABLE `Klasius` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Letnik`
--

DROP TABLE IF EXISTS `Letnik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Letnik` (
  `idLetnik` int(11) NOT NULL AUTO_INCREMENT,
  `letnik` int(11) DEFAULT NULL,
  PRIMARY KEY (`idLetnik`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Letnik`
--

LOCK TABLES `Letnik` WRITE;
/*!40000 ALTER TABLE `Letnik` DISABLE KEYS */;
INSERT INTO `Letnik` VALUES (1,1),(2,2),(3,3),(4,4),(5,5),(6,6);
/*!40000 ALTER TABLE `Letnik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `NacinStudija`
--

DROP TABLE IF EXISTS `NacinStudija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `NacinStudija` (
  `idNacinStudija` int(11) NOT NULL,
  `opisNacina` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `engOpisNacina` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idNacinStudija`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `NacinStudija`
--

LOCK TABLES `NacinStudija` WRITE;
/*!40000 ALTER TABLE `NacinStudija` DISABLE KEYS */;
INSERT INTO `NacinStudija` VALUES (1,'redni','full-time'),(3,'izredni','part-time');
/*!40000 ALTER TABLE `NacinStudija` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Obcina`
--

DROP TABLE IF EXISTS `Obcina`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Obcina` (
  `idObcina` int(11) NOT NULL AUTO_INCREMENT,
  `imeObcine` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idObcina`)
) ENGINE=InnoDB AUTO_INCREMENT=214 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Obcina`
--

LOCK TABLES `Obcina` WRITE;
/*!40000 ALTER TABLE `Obcina` DISABLE KEYS */;
INSERT INTO `Obcina` VALUES (1,'Ajdovščina'),(2,'Beltinci'),(3,'Bled'),(4,'Bohinj'),(5,'Borovnica'),(6,'Bovec'),(7,'Brda'),(8,'Brezovica'),(9,'Brežice'),(10,'Tišina'),(11,'Celje'),(12,'Cerklje na Gorenjskem'),(13,'Cerknica'),(14,'Cerkno'),(15,'Črenšovci'),(16,'Črna na Koroškem'),(17,'Črnomelj'),(18,'Destrnik'),(19,'Divača'),(20,'Dobrepolje'),(21,'Dobrova - Polhov Gradec'),(22,'Dol pri Ljubljani'),(23,'Domžale'),(24,'Dornava'),(25,'Dravograd'),(26,'Duplek'),(27,'Gorenja vas - Poljane'),(28,'Gorišnica'),(29,'Gornja Radgona'),(30,'Gornji Grad'),(31,'Gornji Petrovci'),(32,'Grosuplje'),(33,'Šalovci'),(34,'Hrastnik'),(35,'Hrpelje - Kozina'),(36,'Idrija'),(37,'Ig'),(38,'Ilirska Bistrica'),(39,'Ivančna Gorica'),(40,'Izola'),(41,'Jesenice'),(42,'Juršinci'),(43,'Kamnik'),(44,'Kanal'),(45,'Kidričevo'),(46,'Kobarid'),(47,'Kobilje'),(48,'Kočevje'),(49,'Komen'),(50,'Koper'),(51,'Kozje'),(52,'Kranj'),(53,'Kranjska Gora'),(54,'Krško'),(55,'Kungota'),(56,'Kuzma'),(57,'Laško'),(58,'Lenart'),(59,'Lendava'),(60,'Litija'),(61,'Ljubljana'),(62,'Ljubno'),(63,'Ljutomer'),(64,'Logatec'),(65,'Loška dolina'),(66,'Loški Potok'),(67,'Luče'),(68,'Lukovica'),(69,'Majšperk'),(70,'Maribor'),(71,'Medvode'),(72,'Mengeš'),(73,'Metlika'),(74,'Mežica'),(75,'Miren - Kostanjevica'),(76,'Mislinja'),(77,'Moravče'),(78,'Moravske Toplice'),(79,'Mozirje'),(80,'Murska Sobota'),(81,'Muta'),(82,'Naklo'),(83,'Nazarje'),(84,'Nova Gorica'),(85,'Novo mesto'),(86,'Odranci'),(87,'Ormož'),(88,'Osilnica'),(89,'Pesnica'),(90,'Piran'),(91,'Pivka'),(92,'Podčetrtek'),(93,'Podvelka'),(94,'Postojna'),(95,'Preddvor'),(96,'Ptuj'),(97,'Puconci'),(98,'Rače - Fram'),(99,'Radeče'),(100,'Radenci'),(101,'Radlje ob Dravi'),(102,'Radovljica'),(103,'Ravne na Koroškem'),(104,'Ribnica'),(105,'Rogašovci'),(106,'Rogaška Slatina'),(107,'Rogatec'),(108,'Ruše'),(109,'Semič'),(110,'Sevnica'),(111,'Sežana'),(112,'Slovenj Gradec'),(113,'Slovenska Bistrica'),(114,'Slovenske Konjice'),(115,'Starše'),(116,'Sveti Jurij ob Ščavnici'),(117,'Šenčur'),(118,'Šentilj'),(119,'Šentjernej'),(120,'Šentjur pri Celju'),(121,'Škocjan'),(122,'Škofja Loka'),(123,'Škofljica'),(124,'Šmarje pri Jelšah'),(125,'Šmartno ob Paki'),(126,'Šoštanj'),(127,'Štore'),(128,'Tolmin'),(129,'Trbovlje'),(130,'Trebnje'),(131,'Tržič'),(132,'Turnišče'),(133,'Velenje'),(134,'Velike Lašče'),(135,'Videm'),(136,'Vipava'),(137,'Vitanje'),(138,'Vodice'),(139,'Vojnik'),(140,'Vrhnika'),(141,'Vuzenica'),(142,'Zagorje ob Savi'),(143,'Zavrč'),(144,'Zreče'),(146,'Železniki'),(147,'Žiri'),(148,'Benedikt'),(149,'Bistrica ob Sotli'),(150,'Bloke'),(151,'Braslovče'),(152,'Cankova'),(153,'Cerkvenjak'),(154,'Dobje'),(155,'Dobrna'),(156,'Dobrovnik'),(157,'Dolenjske Toplice'),(158,'Grad'),(159,'Hajdina'),(160,'Hoče - Slivnica'),(161,'Hodoš'),(162,'Horjul'),(163,'Jezersko'),(164,'Komenda'),(165,'Kostel'),(166,'Križevci'),(167,'Lovrenc na Pohorju'),(168,'Markovci'),(169,'Miklavž na Dravskem polju'),(170,'Mirna Peč'),(171,'Oplotnica'),(172,'Podlehnik'),(173,'Polzela'),(174,'Prebold'),(175,'Prevalje'),(176,'Razkrižje'),(177,'Ribnica na Pohorju'),(178,'Selnica ob Dravi'),(179,'Sodražica'),(180,'Solčava'),(181,'Sveta Ana'),(182,'Sveti Andraž v Slov. Goricah'),(183,'Šempeter - Vrtojba'),(184,'Tabor'),(185,'Trnovska vas'),(186,'Trzin'),(187,'Velika Polana'),(188,'Veržej'),(189,'Vransko'),(190,'Žalec'),(191,'Žetale'),(192,'Žirovnica'),(193,'Žužemberk'),(194,'Šmartno pri Litiji'),(195,'Apače'),(196,'Cirkulane'),(197,'Kostanjevica na Krki'),(198,'Makole'),(199,'Mokronog - Trebelno'),(200,'Poljčane'),(201,'Renče - Vogrsko'),(202,'Središče ob Dravi'),(203,'Straža'),(204,'Sv. Trojica v Slov. Goricah'),(205,'Sveti Tomaž'),(206,'Šmarješke Toplice'),(207,'Gorje'),(208,'Log - Dragomer'),(209,'Rečica ob Savinji'),(210,'Sveti Jurij v Slov. Goricah'),(211,'Šentrupert'),(212,'Mirna'),(213,'Ankaran');
/*!40000 ALTER TABLE `Obcina` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `OblikaStudija`
--

DROP TABLE IF EXISTS `OblikaStudija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `OblikaStudija` (
  `idOblikaStudija` int(11) NOT NULL,
  `opisOblike` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `engOpisOblike` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idOblikaStudija`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `OblikaStudija`
--

LOCK TABLES `OblikaStudija` WRITE;
/*!40000 ALTER TABLE `OblikaStudija` DISABLE KEYS */;
INSERT INTO `OblikaStudija` VALUES (1,'na lokaciji','on site'),(2,'na daljavo','distance learning'),(3,'e-študij','e-learning');
/*!40000 ALTER TABLE `OblikaStudija` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Posta`
--

DROP TABLE IF EXISTS `Posta`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Posta` (
  `idPosta` int(11) NOT NULL,
  `postnaStevilka` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idPosta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Posta`
--

LOCK TABLES `Posta` WRITE;
/*!40000 ALTER TABLE `Posta` DISABLE KEYS */;
INSERT INTO `Posta` VALUES (1000,'Ljubljana'),(1001,'Ljubljana'),(1002,'Ljubljana - poštni center'),(1004,'Ljubljana - carinska pošta'),(1210,'Ljubljana - Šentvid'),(1211,'Ljubljana - Šmartno'),(1215,'Medvode'),(1216,'Smlednik'),(1217,'Vodice'),(1218,'Komenda'),(1219,'Laze v Tuhinju'),(1221,'Motnik'),(1222,'Trojane'),(1223,'Blagovica'),(1225,'Lukovica'),(1230,'Domžale'),(1231,'Ljubljana - Črnuče'),(1233,'Dob'),(1234,'Mengeš'),(1235,'Radomlje'),(1236,'Trzin'),(1241,'Kamnik'),(1242,'Stahovica'),(1251,'Moravče'),(1252,'Vače'),(1260,'Ljubljana - Polje'),(1261,'Ljubljana - Dobrunje'),(1262,'Dol pri Ljubljani'),(1270,'Litija'),(1272,'Polšnik'),(1273,'Dole pri Litiji'),(1274,'Gabrovka'),(1275,'Šmartno pri Litiji'),(1276,'Primskovo'),(1281,'Kresnice'),(1282,'Sava'),(1290,'Grosuplje'),(1291,'Škofljica'),(1292,'Ig'),(1293,'Šmarje - Sap'),(1294,'Višnja Gora'),(1295,'Ivančna Gorica'),(1296,'Šentvid pri Stični'),(1301,'Krka'),(1303,'Zagradec'),(1310,'Ribnica'),(1311,'Turjak'),(1312,'Videm - Dobrepolje'),(1313,'Struge'),(1314,'Rob'),(1315,'Velike Lašče'),(1316,'Ortnek'),(1317,'Sodražica'),(1318,'Loški Potok'),(1319,'Draga'),(1330,'Kočevje'),(1331,'Dolenja vas'),(1332,'Stara Cerkev'),(1336,'Kostel'),(1337,'Osilnica'),(1338,'Kočevska Reka'),(1351,'Brezovica pri Ljubljani'),(1352,'Preserje'),(1353,'Borovnica'),(1354,'Horjul'),(1355,'Polhov Gradec'),(1356,'Dobrova'),(1357,'Notranje Gorice'),(1358,'Log pri Brezovici'),(1360,'Vrhnika'),(1370,'Logatec'),(1372,'Hotedršica'),(1373,'Rovte'),(1380,'Cerknica'),(1381,'Rakek'),(1382,'Begunje pri Cerknici'),(1384,'Grahovo'),(1385,'Nova vas'),(1386,'Stari trg pri Ložu'),(1410,'Zagorje ob Savi'),(1411,'Izlake'),(1412,'Kisovec'),(1413,'Čemšenik'),(1414,'Podkum'),(1420,'Trbovlje'),(1423,'Dobovec'),(1430,'Hrastnik'),(1431,'Dol pri Hrastniku'),(1432,'Zidani Most'),(1433,'Radeče'),(1434,'Loka pri Zidanem Mostu'),(1501,'Ljubljana'),(1502,'Ljubljana'),(1503,'Ljubljana'),(1504,'Ljubljana'),(1505,'Ljubljana'),(1506,'Ljubljana'),(1507,'Ljubljana'),(1509,'Ljubljana'),(1510,'Ljubljana'),(1511,'Ljubljana'),(1512,'Ljubljana'),(1513,'Ljubljana'),(1514,'Ljubljana'),(1516,'Ljubljana'),(1517,'Ljubljana'),(1518,'Ljubljana'),(1519,'Ljubljana'),(1520,'Ljubljana'),(1521,'Ljubljana'),(1522,'Ljubljana'),(1523,'Ljubljana'),(1524,'Ljubljana'),(1525,'Ljubljana'),(1526,'Ljubljana'),(1527,'Ljubljana'),(1528,'Ljubljana'),(1529,'Ljubljana'),(1532,'Ljubljana'),(1533,'Ljubljana'),(1534,'Ljubljana'),(1535,'Ljubljana'),(1536,'Ljubljana'),(1537,'Ljubljana'),(1538,'Ljubljana'),(1540,'Ljubljana'),(1542,'Ljubljana'),(1543,'Ljubljana'),(1544,'Ljubljana'),(1545,'Ljubljana'),(1546,'Ljubljana'),(1547,'Ljubljana'),(1550,'Ljubljana'),(1600,'Ljubljana'),(2000,'Maribor'),(2001,'Maribor'),(2002,'Maribor - poštni center'),(2201,'Zgornja Kungota'),(2204,'Miklavž na Dravskem polju'),(2205,'Starše'),(2206,'Marjeta na Dravskem polju'),(2208,'Pohorje'),(2211,'Pesnica pri Mariboru'),(2212,'Šentilj v Slovenskih goricah'),(2213,'Zgornja Velka'),(2214,'Sladki vrh'),(2215,'Ceršak'),(2221,'Jarenina'),(2222,'Jakobski Dol'),(2223,'Jurovski Dol'),(2229,'Malečnik'),(2230,'Lenart v Slovenskih goricah'),(2231,'Pernica'),(2232,'Voličina'),(2233,'Sveta Ana v Slovenskih goricah'),(2234,'Benedikt'),(2235,'Sveta Trojica v Slovenskih goricah'),(2236,'Cerkvenjak'),(2241,'Spodnji Duplek'),(2242,'Zgornja Korena'),(2250,'Ptuj'),(2252,'Dornava'),(2253,'Destrnik'),(2254,'Trnovska vas'),(2255,'Vitomarci'),(2256,'Juršinci'),(2257,'Polenšak'),(2258,'Sveti Tomaž'),(2259,'Ivanjkovci'),(2270,'Ormož'),(2272,'Gorišnica'),(2273,'Podgorci'),(2274,'Velika Nedelja'),(2275,'Miklavž pri Ormožu'),(2276,'Kog'),(2277,'Središče ob Dravi'),(2281,'Markovci'),(2282,'Cirkulane'),(2283,'Zavrč'),(2284,'Videm pri Ptuju'),(2285,'Zgornji Leskovec'),(2286,'Podlehnik'),(2287,'Žetale'),(2288,'Hajdina'),(2289,'Stoperce'),(2310,'Slovenska Bistrica'),(2311,'Hoče'),(2312,'Orehova vas'),(2313,'Fram'),(2314,'Zgornja Polskava'),(2315,'Šmartno na Pohorju'),(2316,'Zgornja Ložnica'),(2317,'Oplotnica'),(2318,'Laporje'),(2319,'Poljčane'),(2321,'Makole'),(2322,'Majšperk'),(2323,'Ptujska Gora'),(2324,'Lovrenc na Dravskem polju'),(2325,'Kidričevo'),(2326,'Cirkovce'),(2327,'Rače'),(2331,'Pragersko'),(2341,'Limbuš'),(2342,'Ruše'),(2343,'Fala'),(2344,'Lovrenc na Pohorju'),(2345,'Bistrica ob Dravi'),(2351,'Kamnica'),(2352,'Selnica ob Dravi'),(2353,'Sv. Duh na Ostrem Vrhu'),(2354,'Bresternica'),(2360,'Radlje ob Dravi'),(2361,'Ožbalt'),(2362,'Kapla'),(2363,'Podvelka'),(2364,'Ribnica na Pohorju'),(2365,'Vuhred'),(2366,'Muta'),(2367,'Vuzenica'),(2370,'Dravograd'),(2371,'Trbonje'),(2372,'Libeliče'),(2373,'Šentjanž pri Dravogradu'),(2380,'Slovenj Gradec'),(2381,'Podgorje pri Slovenj Gradcu'),(2382,'Mislinja'),(2383,'Šmartno pri Slovenj Gradcu'),(2390,'Ravne na Koroškem'),(2391,'Prevalje'),(2392,'Mežica'),(2393,'Črna na Koroškem'),(2394,'Kotlje'),(2500,'Maribor'),(2501,'Maribor'),(2502,'Maribor'),(2503,'Maribor'),(2504,'Maribor'),(2505,'Maribor'),(2506,'Maribor'),(2507,'Maribor'),(2508,'Maribor'),(2509,'Maribor'),(2600,'Maribor'),(2603,'Maribor'),(2607,'Maribor - center za digitalizacijo'),(2609,'Maribor'),(2610,'Maribor'),(3000,'Celje'),(3001,'Celje'),(3201,'Šmartno v Rožni dolini'),(3202,'Ljubečna'),(3203,'Nova Cerkev'),(3204,'Dobrna'),(3205,'Vitanje'),(3206,'Stranice'),(3210,'Slovenske Konjice'),(3211,'Škofja vas'),(3212,'Vojnik'),(3213,'Frankolovo'),(3214,'Zreče'),(3215,'Loče'),(3220,'Štore'),(3221,'Teharje'),(3222,'Dramlje'),(3223,'Loka pri Žusmu'),(3224,'Dobje pri Planini'),(3225,'Planina pri Sevnici'),(3230,'Šentjur'),(3231,'Grobelno'),(3232,'Ponikva'),(3233,'Kalobje'),(3240,'Šmarje pri Jelšah'),(3241,'Podplat'),(3250,'Rogaška Slatina'),(3252,'Rogatec'),(3253,'Pristava pri Mestinju'),(3254,'Podčetrtek'),(3255,'Buče'),(3256,'Bistrica ob Sotli'),(3257,'Podsreda'),(3260,'Kozje'),(3261,'Lesično'),(3262,'Prevorje'),(3263,'Gorica pri Slivnici'),(3264,'Sveti Štefan'),(3270,'Laško'),(3271,'Šentrupert'),(3272,'Rimske Toplice'),(3273,'Jurklošter'),(3301,'Petrovče'),(3302,'Griže'),(3303,'Gomilsko'),(3304,'Tabor'),(3305,'Vransko'),(3310,'Žalec'),(3311,'Šempeter v Savinjski dolini'),(3312,'Prebold'),(3313,'Polzela'),(3314,'Braslovče'),(3320,'Velenje'),(3322,'Velenje'),(3325,'Šoštanj'),(3326,'Topolšica'),(3327,'Šmartno ob Paki'),(3330,'Mozirje'),(3331,'Nazarje'),(3332,'Rečica ob Savinji'),(3333,'Ljubno ob Savinji'),(3334,'Luče'),(3335,'Solčava'),(3341,'Šmartno ob Dreti'),(3342,'Gornji Grad'),(3503,'Velenje'),(3504,'Velenje'),(3505,'Celje'),(3600,'Celje'),(4000,'Kranj'),(4001,'Kranj'),(4200,'Kranj'),(4201,'Zgornja Besnica'),(4202,'Naklo'),(4203,'Duplje'),(4204,'Golnik'),(4205,'Preddvor'),(4206,'Zgornje Jezersko'),(4207,'Cerklje na Gorenjskem'),(4208,'Šenčur'),(4209,'Žabnica'),(4210,'Brnik - aerodrom'),(4211,'Mavčiče'),(4212,'Visoko'),(4220,'Škofja Loka'),(4223,'Poljane nad Škofjo Loko'),(4224,'Gorenja vas'),(4225,'Sovodenj'),(4226,'Žiri'),(4227,'Selca'),(4228,'Železniki'),(4229,'Sorica'),(4240,'Radovljica'),(4243,'Brezje'),(4244,'Podnart'),(4245,'Kropa'),(4246,'Kamna Gorica'),(4247,'Zgornje Gorje'),(4248,'Lesce'),(4260,'Bled'),(4263,'Bohinjska Bela'),(4264,'Bohinjska Bistrica'),(4265,'Bohinjsko jezero'),(4267,'Srednja vas v Bohinju'),(4270,'Jesenice'),(4273,'Blejska Dobrava'),(4274,'Žirovnica'),(4275,'Begunje na Gorenjskem'),(4276,'Hrušica'),(4280,'Kranjska Gora'),(4281,'Mojstrana'),(4282,'Gozd Martuljek'),(4283,'Rateče - Planica'),(4290,'Tržič'),(4294,'Križe'),(4501,'Naklo'),(4502,'Kranj'),(4600,'Kranj'),(5000,'Nova Gorica'),(5001,'Nova Gorica'),(5210,'Deskle'),(5211,'Kojsko'),(5212,'Dobrovo v Brdih'),(5213,'Kanal'),(5214,'Kal nad Kanalom'),(5215,'Ročinj'),(5216,'Most na Soči'),(5220,'Tolmin'),(5222,'Kobarid'),(5223,'Breginj'),(5224,'Srpenica'),(5230,'Bovec'),(5231,'Log pod Mangartom'),(5232,'Soča'),(5242,'Grahovo ob Bači'),(5243,'Podbrdo'),(5250,'Solkan'),(5251,'Grgar'),(5252,'Trnovo pri Gorici'),(5253,'Čepovan'),(5261,'Šempas'),(5262,'Črniče'),(5263,'Dobravlje'),(5270,'Ajdovščina'),(5271,'Vipava'),(5272,'Podnanos'),(5273,'Col'),(5274,'Črni Vrh nad Idrijo'),(5275,'Godovič'),(5280,'Idrija'),(5281,'Spodnja Idrija'),(5282,'Cerkno'),(5283,'Slap ob Idrijci'),(5290,'Šempeter pri Gorici'),(5291,'Miren'),(5292,'Renče'),(5293,'Volčja Draga'),(5294,'Dornberk'),(5295,'Branik'),(5296,'Kostanjevica na Krasu'),(5297,'Prvačina'),(5600,'Nova Gorica'),(6000,'Koper - Capodistria'),(6001,'Koper - Capodistria'),(6200,'Koper - Capodistria'),(6210,'Sežana'),(6215,'Divača'),(6216,'Podgorje'),(6217,'Vremski Britof'),(6219,'Lokev'),(6221,'Dutovlje'),(6222,'Štanjel'),(6223,'Komen'),(6224,'Senožeče'),(6225,'Hruševje'),(6230,'Postojna'),(6232,'Planina'),(6240,'Kozina'),(6242,'Materija'),(6243,'Obrov'),(6244,'Podgrad'),(6250,'Ilirska Bistrica'),(6251,'Ilirska Bistrica - Trnovo'),(6253,'Knežak'),(6254,'Jelšane'),(6255,'Prem'),(6256,'Košana'),(6257,'Pivka'),(6258,'Prestranek'),(6271,'Dekani'),(6272,'Gračišče'),(6273,'Marezige'),(6274,'Šmarje'),(6275,'Črni Kal'),(6276,'Pobegi'),(6280,'Ankaran/Ancarano'),(6281,'Škofije'),(6310,'Izola/Isola'),(6320,'Portorož/Portorose'),(6330,'Piran/Pirano'),(6333,'Sečovlje/Sicciole'),(6501,'Koper - Capodistria'),(6502,'Koper - Capodistria'),(6503,'Koper - Capodistria'),(6504,'Koper - Capodistria'),(6600,'Koper - Capodistria'),(8000,'Novo mesto'),(8001,'Novo mesto'),(8210,'Trebnje'),(8211,'Dobrnič'),(8212,'Velika Loka'),(8213,'Veliki Gaber'),(8216,'Mirna Peč'),(8220,'Šmarješke Toplice'),(8222,'Otočec'),(8230,'Mokronog'),(8231,'Trebelno'),(8232,'Šentrupert'),(8233,'Mirna'),(8250,'Brežice'),(8251,'Čatež ob Savi'),(8253,'Artiče'),(8254,'Globoko'),(8255,'Pišece'),(8256,'Sromlje'),(8257,'Dobova'),(8258,'Kapele'),(8259,'Bizeljsko'),(8261,'Jesenice na Dolenjskem'),(8262,'Krška vas'),(8263,'Cerklje ob Krki'),(8270,'Krško'),(8272,'Zdole'),(8273,'Leskovec pri Krškem'),(8274,'Raka'),(8275,'Škocjan'),(8276,'Bučka'),(8280,'Brestanica'),(8281,'Senovo'),(8282,'Koprivnica'),(8283,'Blanca'),(8290,'Sevnica'),(8292,'Zabukovje'),(8293,'Studenec'),(8294,'Boštanj'),(8295,'Tržišče'),(8296,'Krmelj'),(8297,'Šentjanž'),(8310,'Šentjernej'),(8311,'Kostanjevica na Krki'),(8312,'Podbočje'),(8321,'Brusnice'),(8322,'Stopiče'),(8323,'Uršna sela'),(8330,'Metlika'),(8331,'Suhor'),(8332,'Gradac'),(8333,'Semič'),(8340,'Črnomelj'),(8341,'Adlešiči'),(8342,'Stari trg ob Kolpi'),(8343,'Dragatuš'),(8344,'Vinica'),(8350,'Dolenjske Toplice'),(8351,'Straža'),(8360,'Žužemberk'),(8361,'Dvor'),(8362,'Hinje'),(8501,'Novo mesto'),(8600,'Nova mesto'),(9000,'Murska Sobota'),(9001,'Murska Sobota'),(9201,'Puconci'),(9202,'Mačkovci'),(9203,'Petrovci'),(9204,'Šalovci'),(9205,'Hodoš/Hodos'),(9206,'Križevci'),(9207,'Prosenjakovci/Partosfalva'),(9208,'Fokovci'),(9220,'Lendava/Lendva'),(9221,'Martjanci'),(9222,'Bogojina'),(9223,'Dobrovnik/Dobronak'),(9224,'Turnišče'),(9225,'Velika Polana'),(9226,'Moravske Toplice'),(9227,'Kobilje'),(9231,'Beltinci'),(9232,'Črenšovci'),(9233,'Odranci'),(9240,'Ljutomer'),(9241,'Veržej'),(9242,'Križevci pri Ljutomeru'),(9243,'Mala Nedelja'),(9244,'Sveti Jurij ob Ščavnici'),(9245,'Spodnji Ivanjci'),(9246,'Razkrižje'),(9250,'Gornja Radgona'),(9251,'Tišina'),(9252,'Radenci'),(9253,'Apače'),(9261,'Cankova'),(9262,'Rogašovci'),(9263,'Kuzma'),(9264,'Grad'),(9265,'Bodonci'),(9501,'Murska Sobota'),(9502,'Radenci'),(9600,'Murska Sobota');
/*!40000 ALTER TABLE `Posta` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Predmet`
--

DROP TABLE IF EXISTS `Predmet`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Predmet` (
  `idPredmet` int(11) NOT NULL,
  `imePredmeta` varchar(255) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `kreditneTocke` int(9) DEFAULT NULL,
  PRIMARY KEY (`idPredmet`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Predmet`
--

LOCK TABLES `Predmet` WRITE;
/*!40000 ALTER TABLE `Predmet` DISABLE KEYS */;
INSERT INTO `Predmet` VALUES (1,'Teorija testiranja',6),(60000,'Diplomsko delo',6),(63202,'Osnove matematične analize',6),(63203,'Diskretne strukture',6),(63204,'Osnove digitalnih vezij',6),(63205,'Fizika',6),(63207,'Linearna algebra',6),(63208,'Osnove podatkovnih baz',6),(63209,'Računalniške komunikacije',6),(63210,'Komunikacija človek računalnik',6),(63212,'Arhitektura računalniških sistemov',6),(63213,'Verjetnost in statistika',6),(63215,'Osnove informacijskih sistemov',6),(63216,'Teorija informacij in sistemov',6),(63217,'Operacijski sistemi',6),(63218,'Organizacija računalniških sistemov',6),(63219,'Matematično modeliranje',6),(63220,'Principi programskih jezikov',6),(63221,'Računalniške tehnologije',6),(63222,'Angleški jezik nivo A',3),(63225,'Izbrana poglavja iz računalništva in informatike - Analiza omrežij',6),(63226,'Tehnologija upravljanja podatkov',6),(63241,'Računalništvo v praksi I',3),(63242,'Računalništvo v praksi II',3),(63246,'Komuniciranje in vodenje projektov',6),(63248,'Ekonomika in podjetništvo',6),(63249,'Elektronsko poslovanje',6),(63250,'Organizacija in management',6),(63251,'Poslovna inteligenca',6),(63252,'Razvoj informacijskih sistemov',6),(63253,'Planiranje in upravljanje informatike',6),(63254,'Postopki razvoja programske opreme',6),(63255,'Spletno programiranje',6),(63256,'Tehnologija programske opreme',6),(63257,'Modeliranje računalniških omrežij',6),(63258,'Komunikacijski protokoli',6),(63259,'Brezžična in mobilna omrežja',6),(63260,'Digitalno načrtovanje',6),(63261,'Porazdeljeni sistemi',6),(63262,'Zanesljivost in zmogljivost računalniških sistemov',6),(63263,'Računska zahtevnost in hevristično programiranje',6),(63264,'Sistemska programska oprema',6),(63265,'Prevajalniki',6),(63266,'Inteligentni sistemi',6),(63267,'Umetno zaznavanje',6),(63268,'Razvoj inteligentnih sistemov',6),(63269,'Računalniška grafika in tehnologija iger',6),(63270,'Multimedijski sistemi',6),(63271,'Osnove oblikovanja',6),(63277,'Programiranje 1',6),(63278,'Programiranje 2',6),(63279,'Algoritmi in podatkovne strukture 1',6),(63280,'Algoritmi in podatkovne strukture 2',6),(63701,'Uvod v računalništvo',6),(63702,'Programiranje 1',6),(63703,'Računalniška arhitektura',6),(63704,'Matematika',6),(63705,'Diskretne strukture',6),(63706,'Programiranje 2',6),(63707,'Podatkovne baze',6),(63708,'Računalniške komunikacije',6),(63709,'Operacijski sistemi',6),(63711,'Algoritmi in podatkovne strukture 1',6),(63712,'Elektronsko in mobilno poslovanje',6),(63713,'Podatkovne baze 2',6),(63714,'Informacijski sistemi',6),(63715,'Grafično oblikovanje',6),(63716,'Komunikacijski protokoli in omrežna varnost',6),(63717,'Organizacija računalnikov',6),(63718,'Digitalna vezja',6),(63719,'Računalniška grafika',6),(63720,'Umetna inteligenca',6),(63721,'Uporabniški vmesniki',6),(63722,'Prevajalnik in navidezni stroji',6),(63723,'Algoritmi in podatkovne strukture 2',6),(63724,'Testiranje in kakovost',6),(63725,'Razvoj informacijskih sistemov',6),(63726,'Produkcija multimedijskih gradiv',6),(63727,'Spletne tehnologije',6),(63728,'Vhodno izhodne naprave',6),(63729,'Načrtovanje digitalnih naprav',6),(63732,'Tehnologija programske opreme',6),(63733,'Strateško planiranje informatike',6),(63734,'Multimedijske tehnologije',6),(63735,'Vzporedni in porazdeljeni sistemi in algoritmi',6),(63736,'Sistemska programska oprema',6),(63737,'Procesna avtomatika',6),(63738,'Vgrajeni sistemi',6),(63739,'Robotika in računalniško zaznavanje',6),(63740,'Tehnologija iger in navidezna resničnost',6),(63741,'Odločitveni sistemi',6),(63742,'Numerične metode',6),(63744,'Digitalno procesiranje signalov',6),(63746,'Angleški jezik nivo B',3),(63747,'Angleški jezik nivo C',3),(63748,'Ekonomika in organizacija informatike',6),(63750,'Športna vzgoja',3),(63752,'Računalništvo v praksi I',3),(63753,'Računalništvo v praksi II',3),(63754,'Izvedbe algoritmov (Razvoj sistemov na čipu v jeziku Verilog)',6),(63755,'Projektni praktikum',6),(63765,'Podatkovno rudarjenje',6),(63769,'Racunalniške komunikacije',6),(63770,'Operacijski sistemi 1',6),(63771,'Tehnologija programske opreme',6);
/*!40000 ALTER TABLE `Predmet` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PredmetStudijskegaPrograma`
--

DROP TABLE IF EXISTS `PredmetStudijskegaPrograma`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PredmetStudijskegaPrograma` (
  `SestavniDelPred_idSestavniDelPred` int(11) NOT NULL,
  `Letnik_idLetnik` int(11) NOT NULL,
  `StudijskiProgram_idStudijskiProgram1` int(11) NOT NULL,
  `Predmet_idPredmet1` int(11) NOT NULL,
  `IdPredmetStudijskegaPrograma` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`IdPredmetStudijskegaPrograma`),
  KEY `fk_PredmetŠtudijskegaPrograma_Letnik1_idx` (`Letnik_idLetnik`),
  KEY `fk_PredmetŠtudijskegaPrograma_ŠtudijskiProgram1_idx` (`StudijskiProgram_idStudijskiProgram1`),
  KEY `fk_PredmetŠtudijskegaPrograma_Predmet1_idx` (`Predmet_idPredmet1`),
  KEY `fk_table2_SestavniDelPredmetnika1` (`SestavniDelPred_idSestavniDelPred`),
  CONSTRAINT `fk_PredmetŠtudijskegaPrograma_Letnik1` FOREIGN KEY (`Letnik_idLetnik`) REFERENCES `Letnik` (`idLetnik`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_PredmetŠtudijskegaPrograma_Predmet1` FOREIGN KEY (`Predmet_idPredmet1`) REFERENCES `Predmet` (`idPredmet`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_PredmetŠtudijskegaPrograma_ŠtudijskiProgram1` FOREIGN KEY (`StudijskiProgram_idStudijskiProgram1`) REFERENCES `StudijskiProgram` (`idStudijskiProgram`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_table2_SestavniDelPredmetnika1` FOREIGN KEY (`SestavniDelPred_idSestavniDelPred`) REFERENCES `SestavniDelPred` (`idSestavniDelPred`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=138 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PredmetStudijskegaPrograma`
--

LOCK TABLES `PredmetStudijskegaPrograma` WRITE;
/*!40000 ALTER TABLE `PredmetStudijskegaPrograma` DISABLE KEYS */;
INSERT INTO `PredmetStudijskegaPrograma` VALUES (3,2,8,63212,46),(3,2,8,63711,48),(3,2,8,63215,49),(3,2,8,63216,50),(3,2,8,63709,51),(3,2,8,63218,52),(1,2,8,63219,53),(1,2,8,63220,54),(1,2,8,63221,55),(2,2,8,63746,57),(2,2,8,63747,58),(2,3,8,63747,59),(2,3,8,63746,60),(2,2,8,63752,62),(2,3,8,63752,63),(2,2,8,63753,64),(2,3,8,63753,65),(3,3,8,63246,66),(4,3,8,63249,67),(4,3,8,63251,68),(4,3,8,63250,69),(5,3,8,63725,70),(5,3,8,63226,73),(5,3,8,63253,74),(6,3,8,63254,75),(6,3,8,63255,76),(6,3,8,63771,77),(7,3,8,63257,78),(7,3,8,63258,79),(7,3,8,63259,80),(8,3,8,63260,81),(8,3,8,63261,82),(8,3,8,63262,83),(9,3,8,63263,84),(9,3,8,63736,85),(10,3,8,63266,86),(10,3,8,63267,87),(10,3,8,63268,88),(11,3,8,63269,89),(11,3,8,63270,90),(11,3,8,63271,91),(3,1,8,63205,96),(3,1,8,63705,97),(3,1,8,63207,98),(3,1,8,63708,100),(3,1,8,63210,101),(3,1,8,63202,102),(3,1,8,63702,103),(3,1,8,63706,104),(3,1,8,63208,105),(3,1,8,63204,106),(3,2,8,63723,107),(3,1,9,1,108),(3,1,9,63702,110),(3,3,8,63248,114),(3,3,8,60000,115),(3,2,8,63213,117),(2,3,8,63750,118),(9,3,8,63265,121),(3,1,8,63277,122),(3,1,8,63203,123),(3,1,8,63278,124),(3,2,8,63279,126),(3,2,8,63280,127),(3,2,8,63217,128),(2,2,8,63222,129),(2,3,8,63222,130),(2,2,8,63242,131),(2,3,8,63242,132),(2,2,8,63241,133),(2,3,8,63241,134),(5,3,8,63252,135),(6,3,8,63256,136),(9,3,8,63264,137);
/*!40000 ALTER TABLE `PredmetStudijskegaPrograma` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Prijava`
--

DROP TABLE IF EXISTS `Prijava`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Prijava` (
  `idPrijava` int(11) NOT NULL AUTO_INCREMENT,
  `imeStudenta` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `priimekStudenta` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `gesloStudent` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `mailStudent` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idPrijava`)
) ENGINE=InnoDB AUTO_INCREMENT=111 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Prijava`
--

LOCK TABLES `Prijava` WRITE;
/*!40000 ALTER TABLE `Prijava` DISABLE KEYS */;
INSERT INTO `Prijava` VALUES (100,'Gal Neki','Kos','15galy@gmail.com','15galy@gmail.com'),(101,'Ana-Marija','Mitic','marija.mitic@email.com','marija.mitic@email.com'),(102,'Valerija','Podlesnik','valerija.podlesnik@gmail.com','valerija.podlesnik@gmail.com'),(103,'Karmen','Zidar-Kos','karmen.kos@outlook.com','karmen.kos@outlook.com'),(104,'Izidor Jan','Pececnik','izidor.pececnik@pececnik-net.si','izidor.pececnik@pececnik-net.si'),(105,'Steve','Jobc','sceve.obs@macinsosh.com','sceve.obs@macinsosh.com'),(106,'Borut','Pahor','izidor.pececnik@pececnia-net.si','izidor.pececnik@pececnia-net.si'),(107,'Steve','Jobc','sceve.obs@macintosh.com','sceve.obs@macintosh.com'),(108,'Borut','Pahor','borut.pahor@gmail.com','borut.pahor@gmail.com'),(109,'Janez','Janša','janez.jansa@outlook.com','janez.jansa@outlook.com'),(110,'Miro','Cerar','miro.cerar@outlook.com','miro.cerar@outlook.com');
/*!40000 ALTER TABLE `Prijava` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PrijavaNaIzpit`
--

DROP TABLE IF EXISTS `PrijavaNaIzpit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PrijavaNaIzpit` (
  `idPrijavaNaizpit` int(11) NOT NULL AUTO_INCREMENT,
  `DatumPrijave` datetime DEFAULT NULL,
  `DatumSpremembe` datetime DEFAULT NULL,
  `IzpitniRok_IdIzpitniRok` int(11) DEFAULT NULL,
  `VpisaniPredmet_IdVpisanipredmet` int(11) DEFAULT NULL,
  `StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit` int(11) DEFAULT NULL,
  `OcenaIzpita` int(11) DEFAULT NULL,
  `StTock` int(11) DEFAULT NULL,
  PRIMARY KEY (`idPrijavaNaizpit`),
  KEY `fk_IzpitniRok_IdIzpitniRok_idx` (`IzpitniRok_IdIzpitniRok`),
  KEY `fk_VpisaniPredmet_IdVpisanipredmet_idx` (`VpisaniPredmet_IdVpisanipredmet`),
  KEY `fk_StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit_idx` (`StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit`),
  CONSTRAINT `fk_IzpitniRok_IdIzpitniRok` FOREIGN KEY (`IzpitniRok_IdIzpitniRok`) REFERENCES `IzpitniRok` (`IDIzpitniRok`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit` FOREIGN KEY (`StatusPrijaveNaIzpit_IdStatusPrijaveNaIzpit`) REFERENCES `StatusPrijave` (`idStatusPrijave`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_VpisaniPredmet_IdVpisanipredmet` FOREIGN KEY (`VpisaniPredmet_IdVpisanipredmet`) REFERENCES `VpisaniPredmet` (`idVpisaniPredmet`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=87 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PrijavaNaIzpit`
--

LOCK TABLES `PrijavaNaIzpit` WRITE;
/*!40000 ALTER TABLE `PrijavaNaIzpit` DISABLE KEYS */;
INSERT INTO `PrijavaNaIzpit` VALUES (1,NULL,NULL,49,565,1,5,NULL),(2,NULL,'2015-06-02 16:59:03',79,559,8,5,NULL),(3,NULL,'2015-06-02 16:59:04',79,607,8,5,NULL),(4,NULL,NULL,63,625,2,5,60),(7,NULL,NULL,63,656,2,5,100),(9,NULL,NULL,80,625,10,1,12),(38,NULL,NULL,81,473,2,5,NULL),(44,NULL,'2015-06-05 12:09:43',65,640,1,0,NULL),(52,NULL,NULL,87,642,1,8,NULL),(53,NULL,'2015-06-05 12:57:49',90,909,7,5,40),(55,NULL,NULL,93,925,1,7,60),(56,NULL,NULL,65,650,2,7,NULL),(57,NULL,NULL,95,647,1,5,NULL),(59,'2015-06-05 11:22:13','2015-06-05 11:22:13',90,486,3,10,NULL),(60,'2015-06-05 11:30:02','2015-06-05 11:30:02',97,486,3,9,NULL),(61,'2015-06-05 11:30:58','2015-06-05 11:30:58',98,488,3,10,NULL),(62,'2015-06-05 11:34:20','2015-06-05 11:34:20',99,123,3,10,NULL),(63,'2015-06-05 11:35:09','2015-06-05 11:35:09',82,123,3,6,NULL),(66,'2015-06-05 11:38:21','2015-06-05 11:38:21',86,123,3,9,NULL),(71,NULL,NULL,101,645,1,5,NULL),(72,NULL,NULL,102,645,1,5,NULL),(73,NULL,NULL,100,645,1,5,NULL),(74,NULL,'2015-06-07 21:27:15',59,643,7,NULL,NULL),(75,NULL,NULL,104,648,1,NULL,NULL),(76,NULL,NULL,106,646,1,5,NULL),(77,NULL,NULL,106,646,1,5,NULL),(78,NULL,NULL,106,646,1,5,NULL),(79,NULL,NULL,106,646,1,5,NULL),(80,NULL,NULL,106,646,1,5,NULL),(81,NULL,NULL,106,646,1,5,NULL),(82,NULL,'2015-06-05 14:41:36',66,916,1,NULL,NULL),(83,NULL,'2015-06-05 14:55:17',56,639,7,NULL,NULL),(84,NULL,NULL,18,909,1,NULL,NULL),(85,NULL,'2015-06-07 20:30:42',67,642,7,NULL,NULL),(86,NULL,'2015-06-07 20:29:19',109,644,1,NULL,NULL);
/*!40000 ALTER TABLE `PrijavaNaIzpit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Profesor`
--

DROP TABLE IF EXISTS `Profesor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Profesor` (
  `idProfesor` int(11) NOT NULL AUTO_INCREMENT,
  `imeProfesorja` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `priimekProfesorja` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idProfesor`)
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Profesor`
--

LOCK TABLES `Profesor` WRITE;
/*!40000 ALTER TABLE `Profesor` DISABLE KEYS */;
INSERT INTO `Profesor` VALUES (3,'',''),(4,'',''),(5,'Marko','Bajec'),(6,'Ivan','Bratko'),(7,'Franc','Jager'),(8,'Matjaž','Branko Jurič'),(9,'Aleksandar','Jurišić'),(10,'Igor','Kononenko'),(11,'Aleš','Leonardis'),(12,'Neža','Mramor Kosta'),(13,'Miha','Mraz'),(14,'Bojan','Orel'),(15,'Borut','Robič'),(16,'Franc','Solina'),(17,'Branko','Šter'),(18,'Denis','Trček'),(19,'Nikolaj','Zimic'),(20,'Blaž','Zupan'),(21,'Zoran','Bosnić'),(22,'Patricio','Bulić'),(23,'Janez','Demšar'),(24,'Gašper','Fijavž'),(25,'Uroš','Lotrič'),(26,'Viljan','Mahnič'),(27,'Polona','Oblak'),(28,'Peter','Peer'),(29,'Fabio','Ricciato'),(30,'Marko','Robnik-Šikonja'),(31,'Narvika','Bovcon'),(32,'Andrej','Brodnik'),(33,'Tomaž','Curk'),(34,'Matej','Kristan'),(35,'Matjaž','Kukar'),(36,'Iztok','Lebar Bajec'),(37,'Matija','Marolt'),(38,'Veljko','Pejović'),(39,'Rok','Rupnik'),(40,'Danijel','Skočaj'),(41,'Boštjan','Slivnik'),(42,'Mira','Trebar'),(43,'Dejan','Lavbič'),(44,'Rok','Žitko'),(45,'Marina','Štros Bračko'),(46,'Tomaž','Hovelja'),(47,'Mojca','Ciglarič '),(48,'Tomaž','Dobravec ');
/*!40000 ALTER TABLE `Profesor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SestavniDelPred`
--

DROP TABLE IF EXISTS `SestavniDelPred`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `SestavniDelPred` (
  `idSestavniDelPred` int(11) NOT NULL AUTO_INCREMENT,
  `opisSestavnegaDela` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `jeModul` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`idSestavniDelPred`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SestavniDelPred`
--

LOCK TABLES `SestavniDelPred` WRITE;
/*!40000 ALTER TABLE `SestavniDelPred` DISABLE KEYS */;
INSERT INTO `SestavniDelPred` VALUES (1,'Strokovni izbirni predmet',0),(2,'Prosti izbirni predmet',0),(3,'Obvezni predmet',0),(4,'Informacijski sistemi',1),(5,'Obvladovanje informatike',1),(6,'Razvoj programske opreme',1),(7,'Računalniška omrežja',1),(8,'Računalniški sistemi',1),(9,'Algoritmi in sistemski programi',1),(10,'Umetna inteligenca',1),(11,'Medijske tehnologije',1);
/*!40000 ALTER TABLE `SestavniDelPred` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Sklep`
--

DROP TABLE IF EXISTS `Sklep`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Sklep` (
  `idSklep` int(11) NOT NULL AUTO_INCREMENT,
  `VsebinaSklepa` varchar(9999) COLLATE utf8_slovenian_ci NOT NULL,
  `Student_IdStudenta` int(11) NOT NULL,
  `Student_idStudent` int(11) NOT NULL,
  `Organ` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `DatumSprejetjaSklepa` date DEFAULT NULL,
  `DatumVeljaveSklepa` date DEFAULT NULL,
  PRIMARY KEY (`idSklep`),
  KEY `fk_Sklep_StudentId_idx` (`Student_IdStudenta`),
  CONSTRAINT `fk_Sklep_StudentId` FOREIGN KEY (`Student_IdStudenta`) REFERENCES `Student` (`idStudent`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Sklep`
--

LOCK TABLES `Sklep` WRITE;
/*!40000 ALTER TABLE `Sklep` DISABLE KEYS */;
INSERT INTO `Sklep` VALUES (1,'Svoboda Narodu! a',1,1,'SS','2000-11-11','2000-11-11'),(2,'Konec Lakote',1,1,'SCS','2000-11-11','2000-11-11'),(3,'Dol z Zupanom',1,1,'Svet','2000-11-11','2000-11-11'),(4,'Pravice delavcem',2,2,'SS','2000-11-11','2000-11-11'),(5,'If you live long enough, you\'ll make mistakes. But if you learn from them, you\'ll be a better person. It\'s how you handle adversity, not how it affects you. The main thing is never quit, never quit, never quit.',3,3,'SS','2000-11-11',NULL),(6,'abc',4,0,'abc','2000-11-11',NULL),(7,'def',4,0,'def','2000-11-11',NULL),(8,'Novi sklep',2,0,'Novi sklep','2000-11-11',NULL),(9,'test',1,0,'test','2000-11-11',NULL),(10,'abc',4,0,'abc','2000-11-11','2000-11-11'),(11,'zADNJI TEST',4,0,'zADNJI TEST','2000-11-11','2000-11-11'),(12,'abs',1,0,'abs','2000-11-02','2001-11-02'),(13,'asasfg',1,0,'asasfg','2001-11-02','2001-11-02'),(14,'123',5,0,'SS','2014-11-02','2015-04-30'),(15,'dsaf',1,0,'dsaf','2000-11-11','2000-11-11'),(16,'asdf',1,0,'asdf','2000-11-11','2000-11-11');
/*!40000 ALTER TABLE `Sklep` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `StatusPrijave`
--

DROP TABLE IF EXISTS `StatusPrijave`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `StatusPrijave` (
  `idStatusPrijave` int(11) NOT NULL,
  `opisStatusaPrijave` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idStatusPrijave`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `StatusPrijave`
--

LOCK TABLES `StatusPrijave` WRITE;
/*!40000 ALTER TABLE `StatusPrijave` DISABLE KEYS */;
INSERT INTO `StatusPrijave` VALUES (1,'prijava dodana STUDENT'),(2,'prijava dodana REFERAT'),(3,'prijava dodana UCITELJ'),(4,'prijava spremenjena STUDENT'),(5,'prijava spremenjena REFERAT'),(6,'prijava spremenjena UCITELJ'),(7,'prijava preklicana STUDENT'),(8,'prijava preklicana REFERAT'),(9,'prijava preklicana UCITELJ'),(10,'prijava vrnjena UCITELJ');
/*!40000 ALTER TABLE `StatusPrijave` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Student`
--

DROP TABLE IF EXISTS `Student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Student` (
  `idStudent` int(11) NOT NULL AUTO_INCREMENT,
  `Drzava_idDrzava` int(11) DEFAULT NULL,
  `Posta_idPosta` int(11) DEFAULT NULL,
  `Drzava_idDrzavaRojstva` int(11) DEFAULT NULL,
  `Prijava_idPrijava` int(11) DEFAULT NULL,
  `imeStudenta` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `priimekStudenta` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `gesloStudenta` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `mailStudenta` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `vpisnaStudenta` int(11) DEFAULT NULL,
  `Vloge_idVloge` int(11) DEFAULT NULL,
  `Obcina_idObcina` int(11) DEFAULT NULL,
  `Naslov` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `NaslovZacasni` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `Drzava_idDrzavaZacasna` int(11) DEFAULT NULL,
  `Posta_idPostaZacasna` int(11) DEFAULT NULL,
  `Telefon` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `Obcina_idObcinaZacasna` int(11) DEFAULT NULL,
  `VrocanjeStalnoBivalisce` tinyint(4) DEFAULT NULL,
  `EMSO` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `Drzava_idDrzavaDrzavljanstvo` int(11) DEFAULT NULL,
  `Spol` char(1) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `DavcnaStevilka` bigint(20) DEFAULT NULL,
  `DatumRojstva` date DEFAULT NULL,
  `KrajRojstva` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `Obcina_idObcinaRojstva` int(11) DEFAULT NULL,
  `Klasius_idKlasius` int(11) DEFAULT NULL,
  PRIMARY KEY (`idStudent`),
  KEY `fk_Student_Vloga_idx` (`Vloge_idVloge`),
  KEY `fk_Student_idPrijava_idx` (`Prijava_idPrijava`),
  KEY `fk_Obcina_idObcina_idx` (`Obcina_idObcina`),
  KEY `fk_Posta_idPosta_idx` (`Posta_idPosta`),
  KEY `fk_Drzava_idDrzava_idx` (`Drzava_idDrzava`),
  KEY `fk_Drzava_idDrzavaRojstva_idx` (`Drzava_idDrzavaRojstva`),
  KEY `fk_Drzava_idDrzavaZacasna_idx` (`Drzava_idDrzavaZacasna`),
  KEY `fk_Posta_idPostaZacasna_idx` (`Posta_idPostaZacasna`),
  KEY `fk_Obcina_idObcinaZacasna_idx` (`Obcina_idObcinaZacasna`),
  KEY `fk_Drzava_idDrzavaDrzavljanstvo_idx` (`Drzava_idDrzavaDrzavljanstvo`),
  KEY `fk_Obcina_idObcina_idx1` (`Obcina_idObcinaRojstva`),
  KEY `fk_Klasius_idKlasius_idx` (`Klasius_idKlasius`),
  CONSTRAINT `fk_Drzava_idDrzava` FOREIGN KEY (`Drzava_idDrzava`) REFERENCES `Drzava` (`idDrzava`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Drzava_idDrzavaDrzavljanstvo` FOREIGN KEY (`Drzava_idDrzavaDrzavljanstvo`) REFERENCES `Drzava` (`idDrzava`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Drzava_idDrzavaRojstva` FOREIGN KEY (`Drzava_idDrzavaRojstva`) REFERENCES `Drzava` (`idDrzava`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Drzava_idDrzavaZacasna` FOREIGN KEY (`Drzava_idDrzavaZacasna`) REFERENCES `Drzava` (`idDrzava`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Klasius_idKlasius` FOREIGN KEY (`Klasius_idKlasius`) REFERENCES `Klasius` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Obcina_idObcina` FOREIGN KEY (`Obcina_idObcina`) REFERENCES `Obcina` (`idObcina`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Obcina_idObcinaRojstva` FOREIGN KEY (`Obcina_idObcinaRojstva`) REFERENCES `Obcina` (`idObcina`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Obcina_idObcinaZacasna` FOREIGN KEY (`Obcina_idObcinaZacasna`) REFERENCES `Obcina` (`idObcina`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Posta_idPosta` FOREIGN KEY (`Posta_idPosta`) REFERENCES `Posta` (`idPosta`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Posta_idPostaZacasna` FOREIGN KEY (`Posta_idPostaZacasna`) REFERENCES `Posta` (`idPosta`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Prijava_idPrijava` FOREIGN KEY (`Prijava_idPrijava`) REFERENCES `Prijava` (`idPrijava`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Vloga_idVloga` FOREIGN KEY (`Vloge_idVloge`) REFERENCES `Vloge` (`idVloge`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=63121025 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Student`
--

LOCK TABLES `Student` WRITE;
/*!40000 ALTER TABLE `Student` DISABLE KEYS */;
INSERT INTO `Student` VALUES (1,705,1000,705,NULL,'test','test','test','test@test.com',1,1,1,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(2,705,1000,705,NULL,'Ana','Mitic','referent','referent@referent.com',2,2,1,'Zacret',NULL,705,NULL,'0404040404',1,1,'0401994500109',NULL,'m',4040404,'1994-01-04','Celje',11,NULL),(3,705,1000,705,NULL,'ucitelj','ucitelj','ucitelj','ucitelj@ucitelj.com',3,3,1,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(4,705,1000,705,NULL,'Luka','Locniskar','5l&a:arn','luka.locniskar@gmail.com',0,1,1,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(5,705,1000,705,NULL,'Gal','Kos',')l+@G|wi','15galy@gmail.com',63125444,1,1,'Ponikva pri žalcu 8','Zalog pri Vodicah 12',40,1217,'222-222-222',43,1,'0210993500101',184,'m',123123123,'2015-05-19','Celje',190,16204),(6,705,1000,705,NULL,'Ana','Mitic','marija.mitic@email.com','marija.mitic@email.com',63150000,1,1,'Zacret',NULL,705,NULL,'0404040404',1,1,'0401994500109',705,'m',4040404,'1994-01-04','Celje',11,16204),(7,NULL,NULL,NULL,NULL,'Valerija','Podlesnik','valerija.podlesnik@gmail.com','valerija.podlesnik@gmail.com',63125488,1,1,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(8,NULL,NULL,NULL,NULL,'Karmen','Zidar-Kos','karmen.kos@outlook.com','karmen.kos@outlook.com',63125497,1,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(9,NULL,NULL,NULL,NULL,'Izidor Jan','Pececnik','izidor.pececnik@pececnik-net.si','izidor.pececnik@pececnik-net.si',63215548,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(10,705,NULL,705,NULL,'Steve','Jobs','steve.jobs@macintosh.com','steve.jobs@macintosh.com',63298745,1,1,NULL,NULL,705,NULL,NULL,1,1,'0401994500109',705,'m',1561565,'1994-01-04',NULL,1,17003),(11,NULL,NULL,NULL,NULL,'Zoran','Bosnić','test123','zoran.bosnic@gmail.com',11,3,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(63121021,NULL,NULL,NULL,NULL,'Blaž','Artač','test123','gal.kos.fri@gmail.com',63120181,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(63121022,NULL,NULL,NULL,NULL,'Janez','Novak','test123','janez.novak@gmail.com',63120180,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(63121023,705,NULL,705,NULL,'Test','Zeton','test123','test.zeton@gmail.com',63120111,1,1,NULL,NULL,705,NULL,NULL,1,1,'0401994500109',705,'m',1561565,'1994-01-04',NULL,1,17003),(63121024,NULL,NULL,NULL,NULL,'Daniel','Skovik','test123','daniel.skovik@gmail.com',63120001,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,15002);
/*!40000 ALTER TABLE `Student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `StudijskiProgram`
--

DROP TABLE IF EXISTS `StudijskiProgram`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `StudijskiProgram` (
  `idStudijskiProgram` int(11) NOT NULL AUTO_INCREMENT,
  `naziv` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `sifraEVS` int(7) DEFAULT NULL,
  `sifraKratka` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `stSemestrov` int(3) DEFAULT NULL,
  `StudijskiProgram_idStopnjaStudija` int(9) DEFAULT NULL,
  PRIMARY KEY (`idStudijskiProgram`),
  KEY `fk_StudijskiProgram_stopnjaStudija_idx` (`StudijskiProgram_idStopnjaStudija`),
  CONSTRAINT `fk_StudijskiProgram_stopnjaStudija` FOREIGN KEY (`StudijskiProgram_idStopnjaStudija`) REFERENCES `stopnjaStudija` (`idStopnjaStudija`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `StudijskiProgram`
--

LOCK TABLES `StudijskiProgram` WRITE;
/*!40000 ALTER TABLE `StudijskiProgram` DISABLE KEYS */;
INSERT INTO `StudijskiProgram` VALUES (1,'Humanistika in družb.-DR. III',0,'XU',6,8),(2,'INF. SISTEMI IN ODLOČANJE - DR',1000479,'LE',4,4),(3,'INFORMAC. SISTEMI IN ODLOČANJE',1000480,'L3',4,3),(4,'Kognitivna znanost MAG II. st.',1000472,'X5',4,7),(5,'Multimedija UN 1. st.',1001001,'MM',6,6),(6,'PEDAGOŠKO RAČ. IN INF. MAG-II. st.',1000977,'7002801',4,7),(7,'Predmetnik za tuje študente na izmenjavi',NULL,'Izmenjave',NULL,NULL),(8,'RAČUNAL. IN INFORMATIKA UN',1000475,'L2',9,2),(9,'RAČUNAL. IN MATEMATIKA UN',1000425,'P7',8,2),(10,'RAĆUNAL. IN INFORMATIKA VS',1000477,'HB',6,1),(11,'RAČUNAL. IN MATEMA. UN-I. ST.',1000407,'VV',6,6),(12,'RAČUNALN. IN INFORM. MAG II. ST',1000471,'L1',4,7),(13,'RAČUNALN. IN INFORM. UN-I. ST',1000468,'VT',6,6),(14,'RAČUNALN. IN INFORM. VS-I.ST',1000470,'VU',6,5),(15,'RAČUNALNIŠ. IN INF. DR-III ST.',1000474,'X6',6,8),(16,'RAČUNALNIŠTVO IN INF. - DR',1000478,'7E',4,4),(17,'RAČUNALNIŠTVO IN INF. - MAG',1000482,'71',4,3),(18,'RAČUNALNIŠTVO IN INF. - VIS',NULL,'02',8,1),(19,'RAČUNALNIŠTVO IN INF. - VŠ',NULL,'03',4,2),(20,'Računalništvo in matematika MAG II. st.',1000934,'KP00',4,7),(21,'Upravna informatikaq UN 1. st.',1000469,'Z2',6,6);
/*!40000 ALTER TABLE `StudijskiProgram` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `StudijskoLeto`
--

DROP TABLE IF EXISTS `StudijskoLeto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `StudijskoLeto` (
  `idStudijskoLeto` int(11) NOT NULL AUTO_INCREMENT,
  `studijskoLeto` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idStudijskoLeto`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `StudijskoLeto`
--

LOCK TABLES `StudijskoLeto` WRITE;
/*!40000 ALTER TABLE `StudijskoLeto` DISABLE KEYS */;
INSERT INTO `StudijskoLeto` VALUES (1,'2005/2006'),(2,'2006/2007'),(3,'2007/2008'),(4,'2008/2009'),(5,'2009/2010'),(6,'2010/2011'),(7,'2011/2012'),(8,'2012/2013'),(9,'2013/2014'),(10,'2014/2015'),(11,'2015/2016'),(12,'2016/2017'),(13,'2017/2018'),(14,'2018/2019'),(15,'2019/2020');
/*!40000 ALTER TABLE `StudijskoLeto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Vloge`
--

DROP TABLE IF EXISTS `Vloge`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Vloge` (
  `idVloge` int(11) NOT NULL,
  `opisVloge` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idVloge`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Vloge`
--

LOCK TABLES `Vloge` WRITE;
/*!40000 ALTER TABLE `Vloge` DISABLE KEYS */;
INSERT INTO `Vloge` VALUES (1,'Student'),(2,'Referent'),(3,'Ucitelj');
/*!40000 ALTER TABLE `Vloge` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Vpis`
--

DROP TABLE IF EXISTS `Vpis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Vpis` (
  `idVpis` int(11) NOT NULL AUTO_INCREMENT,
  `VrstaVpisa_idVrstaVpisa` int(11) NOT NULL,
  `OblikaStudija_idOblikaStudija` int(11) NOT NULL,
  `Letnik_idLetnik` int(11) NOT NULL,
  `StudijskiProgram_idStudijskiProgram` int(11) NOT NULL,
  `NacinStudija_idNacinStudija` int(11) NOT NULL,
  `Student_idStudent1` int(11) NOT NULL,
  `Potrjen` int(11) NOT NULL DEFAULT '0',
  `StudijskoLeto` varchar(45) COLLATE utf8_slovenian_ci NOT NULL,
  PRIMARY KEY (`idVpis`),
  KEY `fk_Vpis_VrstaVpisa1_idx` (`VrstaVpisa_idVrstaVpisa`),
  KEY `fk_Vpis_OblikaŠtudija1_idx` (`OblikaStudija_idOblikaStudija`),
  KEY `fk_Vpis_Letnik1_idx` (`Letnik_idLetnik`),
  KEY `fk_Vpis_ŠtudijskiProgram1_idx` (`StudijskiProgram_idStudijskiProgram`),
  KEY `fk_Vpis_NačinŠtudija1_idx` (`NacinStudija_idNacinStudija`),
  KEY `fk_Vpis_Študent1_idx` (`Student_idStudent1`),
  CONSTRAINT `fk_Vpis_Letnik1` FOREIGN KEY (`Letnik_idLetnik`) REFERENCES `Letnik` (`idLetnik`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Vpis_NačinŠtudija1` FOREIGN KEY (`NacinStudija_idNacinStudija`) REFERENCES `NacinStudija` (`idNacinStudija`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Vpis_OblikaŠtudija1` FOREIGN KEY (`OblikaStudija_idOblikaStudija`) REFERENCES `OblikaStudija` (`idOblikaStudija`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Vpis_VrstaVpisa1` FOREIGN KEY (`VrstaVpisa_idVrstaVpisa`) REFERENCES `VrstaVpisa` (`idVrstaVpisa`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Vpis_Študent1` FOREIGN KEY (`Student_idStudent1`) REFERENCES `Student` (`idStudent`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Vpis_ŠtudijskiProgram1` FOREIGN KEY (`StudijskiProgram_idStudijskiProgram`) REFERENCES `StudijskiProgram` (`idStudijskiProgram`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=87 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Vpis`
--

LOCK TABLES `Vpis` WRITE;
/*!40000 ALTER TABLE `Vpis` DISABLE KEYS */;
INSERT INTO `Vpis` VALUES (1,1,1,1,8,1,5,1,'2012/2013'),(2,1,1,2,8,1,5,1,'2013/2014'),(3,1,1,3,8,1,5,1,'2014/2015'),(4,1,1,3,8,1,1,1,'2014/2015'),(6,1,1,2,3,1,2,0,'2015/2016'),(7,1,1,3,8,1,3,1,'2014/2015'),(12,1,1,1,3,1,5,0,'2015/2016'),(17,1,1,1,8,1,63121021,1,'2014/2015'),(18,1,1,2,8,1,63121021,0,'2014/2015'),(19,1,1,3,8,1,63121021,1,'2014/2015'),(28,1,1,1,8,1,63121022,0,'2014/2015'),(29,1,1,2,8,1,63121022,0,'2014/2015'),(30,1,1,2,8,1,63121022,0,'2014/2015'),(74,1,1,1,8,1,6,0,'2012/2013'),(75,1,1,2,8,1,6,0,'2014/2015'),(80,1,1,1,8,1,63121023,0,'2015/2016'),(81,1,1,1,9,1,6,0,'2013/2014'),(82,1,1,1,8,1,10,0,'2015/2016'),(83,1,1,1,8,1,10,0,'2014/2015'),(84,1,1,3,8,1,6,0,'2015/2016'),(85,1,1,2,8,1,63121023,0,'2015/2016'),(86,1,1,2,3,1,63121023,0,'2015/2016');
/*!40000 ALTER TABLE `Vpis` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `VpisaniPredmet`
--

DROP TABLE IF EXISTS `VpisaniPredmet`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `VpisaniPredmet` (
  `Vpis_idVpis` int(11) NOT NULL,
  `idVpisaniPredmet` int(11) NOT NULL AUTO_INCREMENT,
  `ocena` int(11) DEFAULT NULL,
  `IzvedbaPredmeta_idIzvedbaPredmeta` int(11) DEFAULT NULL,
  PRIMARY KEY (`idVpisaniPredmet`),
  KEY `fk_Vpis1_idVpis_idx` (`Vpis_idVpis`),
  KEY `fk_IzvedbaPredmeta_idIzvedbaPredmeta_idx` (`IzvedbaPredmeta_idIzvedbaPredmeta`),
  CONSTRAINT `fk_sadsadasdsd` FOREIGN KEY (`IzvedbaPredmeta_idIzvedbaPredmeta`) REFERENCES `IzvedbaPredmeta` (`idIzvedbaPredmeta`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Vpis1_idVpis` FOREIGN KEY (`Vpis_idVpis`) REFERENCES `Vpis` (`idVpis`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=930 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `VpisaniPredmet`
--

LOCK TABLES `VpisaniPredmet` WRITE;
/*!40000 ALTER TABLE `VpisaniPredmet` DISABLE KEYS */;
INSERT INTO `VpisaniPredmet` VALUES (2,123,5,81),(2,473,10,1),(17,486,9,18),(17,487,9,20),(17,488,9,23),(17,489,9,14),(17,490,9,21),(17,491,8,17),(17,492,8,15),(17,493,8,16),(17,494,8,19),(18,555,NULL,44),(18,557,NULL,41),(18,558,NULL,24),(18,559,NULL,28),(18,560,NULL,29),(18,561,NULL,31),(18,562,NULL,26),(18,563,NULL,25),(18,564,NULL,27),(18,565,NULL,30),(28,577,9,18),(28,578,9,20),(28,579,9,23),(28,580,9,14),(28,581,9,21),(28,582,9,17),(28,583,9,15),(28,584,9,16),(28,585,9,19),(29,597,9,44),(29,598,9,41),(29,599,9,36),(29,600,9,24),(29,601,9,28),(29,602,5,29),(29,603,9,31),(29,604,9,26),(29,605,9,25),(29,606,9,27),(29,607,NULL,30),(30,621,NULL,48),(30,622,NULL,49),(30,623,NULL,55),(30,624,NULL,62),(30,625,8,63),(30,626,NULL,64),(30,627,NULL,65),(30,628,NULL,66),(30,629,NULL,67),(3,639,NULL,47),(3,640,NULL,48),(3,641,NULL,55),(3,642,NULL,56),(3,643,NULL,59),(3,644,NULL,60),(3,645,NULL,61),(3,646,NULL,77),(3,647,NULL,78),(3,648,NULL,79),(19,649,NULL,47),(19,650,NULL,48),(19,651,NULL,55),(19,652,NULL,66),(19,653,NULL,71),(19,654,NULL,72),(19,655,NULL,65),(19,656,NULL,67),(19,657,NULL,63),(19,658,NULL,73),(7,659,NULL,47),(7,660,NULL,48),(7,661,NULL,55),(7,662,NULL,62),(7,663,9,63),(7,664,NULL,64),(7,665,NULL,65),(7,666,NULL,77),(7,667,NULL,78),(7,668,NULL,79),(1,669,NULL,18),(1,670,NULL,20),(1,671,NULL,23),(1,672,NULL,14),(1,673,NULL,21),(1,674,NULL,17),(1,675,NULL,15),(1,676,NULL,16),(1,677,NULL,19),(17,714,NULL,18),(17,715,NULL,20),(17,716,NULL,23),(17,717,NULL,14),(17,718,NULL,21),(17,719,NULL,17),(17,720,NULL,15),(17,721,NULL,16),(17,722,NULL,19),(74,848,9,18),(74,849,9,20),(74,850,9,23),(74,851,10,14),(74,852,10,21),(74,853,10,17),(74,854,10,15),(74,855,10,16),(74,856,10,19),(75,857,NULL,43),(75,858,NULL,41),(75,859,NULL,42),(75,860,NULL,24),(75,861,NULL,28),(75,862,NULL,29),(75,863,NULL,31),(75,864,NULL,26),(75,865,NULL,25),(75,866,NULL,27),(75,867,NULL,30),(77,878,NULL,67),(77,879,NULL,68),(77,880,NULL,55),(77,881,NULL,48),(77,882,NULL,56),(77,883,NULL,74),(77,884,NULL,47),(77,885,NULL,66),(77,886,NULL,37),(77,887,NULL,39),(77,888,NULL,75),(77,889,NULL,68),(77,890,NULL,55),(77,891,NULL,48),(77,892,NULL,56),(77,893,NULL,74),(77,894,NULL,47),(77,895,NULL,66),(77,896,NULL,65),(77,897,NULL,73),(77,898,NULL,37),(77,899,NULL,39),(80,900,NULL,18),(80,901,10,20),(80,902,NULL,23),(80,903,NULL,14),(80,904,NULL,21),(80,905,NULL,17),(80,906,NULL,15),(80,907,NULL,16),(80,908,NULL,19),(82,909,NULL,18),(82,910,NULL,20),(82,911,NULL,23),(82,912,NULL,14),(82,913,NULL,21),(82,914,NULL,17),(82,915,NULL,15),(82,916,NULL,16),(82,917,NULL,19),(83,918,NULL,18),(84,919,NULL,47),(84,920,NULL,48),(84,921,NULL,55),(84,922,NULL,56),(84,923,NULL,57),(84,924,NULL,58),(84,925,NULL,63),(84,926,NULL,71),(84,927,NULL,72),(84,928,NULL,73),(30,929,NULL,29);
/*!40000 ALTER TABLE `VpisaniPredmet` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `VrstaVpisa`
--

DROP TABLE IF EXISTS `VrstaVpisa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `VrstaVpisa` (
  `idVrstaVpisa` int(11) NOT NULL AUTO_INCREMENT,
  `opisVpisa` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idVrstaVpisa`)
) ENGINE=InnoDB AUTO_INCREMENT=99 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `VrstaVpisa`
--

LOCK TABLES `VrstaVpisa` WRITE;
/*!40000 ALTER TABLE `VrstaVpisa` DISABLE KEYS */;
INSERT INTO `VrstaVpisa` VALUES (1,'Prvi vpis v letnik/dodatno leto'),(2,'Ponavljanje letnika'),(3,'Nadaljevanje letnika'),(4,'Podaljšanje statusa študenta'),(5,'Vpis po merilih za prehode v višji letnik'),(6,'Vpis v semester skupnega št. programa'),(7,'Vpis po merilih za prehode v isti letnik'),(98,'Vpis za zaključek');
/*!40000 ALTER TABLE `VrstaVpisa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Zeton`
--

DROP TABLE IF EXISTS `Zeton`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Zeton` (
  `idZeton` int(11) NOT NULL AUTO_INCREMENT,
  `Izkoriscen` bit(1) DEFAULT NULL,
  `Student_idStudent` int(11) DEFAULT NULL,
  `Vpis_idVpis` int(11) DEFAULT NULL,
  `studijskiProgram` int(11) DEFAULT NULL,
  `letnik` int(11) DEFAULT NULL,
  `vrstaVpisa` int(11) DEFAULT NULL,
  `nacinStudija` int(11) DEFAULT NULL,
  `oblikaStudija` int(11) DEFAULT NULL,
  `prostaIzbira` int(11) DEFAULT NULL,
  PRIMARY KEY (`idZeton`),
  KEY `fk_Student_idStudent_idx` (`Student_idStudent`),
  KEY `fk_Vpis_idVpis_idx` (`Vpis_idVpis`),
  CONSTRAINT `fk_Student_idStudent` FOREIGN KEY (`Student_idStudent`) REFERENCES `Student` (`idStudent`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Vpis_idVpis` FOREIGN KEY (`Vpis_idVpis`) REFERENCES `Vpis` (`idVpis`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=92 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Zeton`
--

LOCK TABLES `Zeton` WRITE;
/*!40000 ALTER TABLE `Zeton` DISABLE KEYS */;
INSERT INTO `Zeton` VALUES (14,'',6,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(15,'',6,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(74,'',6,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(75,'',6,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(76,'',6,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(77,'',6,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(81,'',6,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(82,'',10,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(83,'',6,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(84,'',63121023,80,8,1,2,1,3,1),(85,'',63121023,86,3,2,1,1,1,1),(89,'',63121023,NULL,1,1,1,1,1,0),(91,'',1,NULL,1,1,1,1,1,0);
/*!40000 ALTER TABLE `Zeton` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stopnjaStudija`
--

DROP TABLE IF EXISTS `stopnjaStudija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stopnjaStudija` (
  `idStopnjaStudija` int(9) NOT NULL AUTO_INCREMENT,
  `kratica` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `stopnja` varchar(95) COLLATE utf8_slovenian_ci DEFAULT NULL,
  `naziv` varchar(45) COLLATE utf8_slovenian_ci DEFAULT NULL,
  PRIMARY KEY (`idStopnjaStudija`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8 COLLATE=utf8_slovenian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stopnjaStudija`
--

LOCK TABLES `stopnjaStudija` WRITE;
/*!40000 ALTER TABLE `stopnjaStudija` DISABLE KEYS */;
INSERT INTO `stopnjaStudija` VALUES (1,'B','predbolonjski','visokošolski strokovni'),(2,'C','predbolonjski','univerzitetni'),(3,'F','predbolonjski','magistrski'),(4,'G','predbolonjski','doktorski'),(5,'J','prva stopnja','visokošolski strokovni'),(6,'K','prva stopnja','univerzitetni'),(7,'L','druga stopnja','magistrski'),(8,'M','tretja stopnja','doktorski'),(9,'71','','visokošolski (univerzitetni) študij'),(10,'61','','višješolski študij');
/*!40000 ALTER TABLE `stopnjaStudija` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-06-09 15:14:55

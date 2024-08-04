-- MySQL dump 10.13  Distrib 9.0.1, for Linux (x86_64)
--
-- Host: localhost    Database: Phonefolio
-- ------------------------------------------------------
-- Server version	9.0.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Categories`
--

DROP TABLE IF EXISTS `Categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Categories` (
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RowVersion` timestamp(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  `Policy` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Categories`
--

LOCK TABLES `Categories` WRITE;
/*!40000 ALTER TABLE `Categories` DISABLE KEYS */;
INSERT INTO `Categories` VALUES ('Other','2024-08-03 14:36:45.752882',2),('Private','2024-08-03 14:36:57.238897',1),('Work','2024-08-01 10:10:11.889645',0);
/*!40000 ALTER TABLE `Categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Contacts`
--

DROP TABLE IF EXISTS `Contacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Contacts` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Surname` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SubcategoryName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PhoneNumber` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `BirthDate` datetime(6) NOT NULL,
  `Username` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RowVersion` timestamp(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Contacts_Email` (`Email`),
  KEY `IX_Contacts_SubcategoryName` (`SubcategoryName`),
  KEY `IX_Contacts_Username` (`Username`),
  CONSTRAINT `FK_Contacts_Subcategories_SubcategoryName` FOREIGN KEY (`SubcategoryName`) REFERENCES `Subcategories` (`Name`) ON DELETE CASCADE,
  CONSTRAINT `FK_Contacts_Users_Username` FOREIGN KEY (`Username`) REFERENCES `Users` (`Username`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Contacts`
--

LOCK TABLES `Contacts` WRITE;
/*!40000 ALTER TABLE `Contacts` DISABLE KEYS */;
INSERT INTO `Contacts` VALUES ('0a5d1e9a-0e3b-4d28-917b-a9b69d7e5e68','William','Davis','william.davis@example.com','P@ssw0rd6#','Boss','+567 890-123-456','1988-03-18 14:20:00.000000','admin','2024-08-04 14:08:47.974032'),('10a5e6f9-7b4c-4a1e-9d2b-8f9e3d6b5c7a','Amelia','White','amelia.white@example.com','Pa$$w0rd!','Colege','+012 345-678-901','1994-07-14 15:30:00.000000','admin','2024-08-04 14:09:36.463416'),('1b88a923-20b0-4a3e-bd80-1f27c9375f7c','Olivia','Wilson','olivia.wilson@example.com','Pa$$w0rd7!','Client','+678 901-234-567','1992-11-23 16:00:00.000000','admin','2024-08-04 14:09:36.463416'),('21b7e4f8-1c5d-4e3c-9a7b-d2f3a9e8b6c7','Mason','Harris','mason.harris@example.com','P@ss1234$','Client','+123 456-789-012','1986-03-12 11:45:00.000000','admin','2024-08-04 14:09:36.463416'),('2b5c7a4e-9d8b-4f3e-9b1e-7a3b8e2d8f5d','Emily','Williams','emily.williams@example.com','Edu!4Me$','Teacher','+345 678-901-234','1990-04-22 12:00:00.000000','admin','2024-08-04 14:17:16.364499'),('32c8d9e5-7d6e-4b9a-91e2-6f3a4b2c8d9f','Harper','Martin','harper.martin@example.com','P@ssw0rd!','Boss','+234 567-890-123','1992-09-07 14:20:00.000000','admin','2024-08-04 14:09:36.463416'),('3b8c6a5d-9e0f-4a1b-b6d9-7e2d8f6c0a3b','Sophia','Jones','sophia.jones@example.com','Sch00l!2$','Teacher','+567 890-123-456','1993-03-25 16:45:00.000000','admin','2024-08-04 14:17:16.364499'),('3e7cf16c-d035-4e7c-84c4-8a5f434edb5e','James','Lopez','james.lopez@example.com','P@ssword8$','Boss','+789 012-345-678','1986-09-12 15:30:00.000000','admin','2024-08-04 14:09:36.463416'),('43d9e6f2-1c5b-4a3e-9b6d-7e0f2a8b9c3d','Elijah','Thompson','elijah.thompson@example.com','SecreT!9$','Colege','+345 678-901-234','1990-04-25 12:00:00.000000','admin','2024-08-04 14:09:36.463416'),('4c8c1e56-0e02-4d9f-9e5a-0d67e6a8a28c','Isabella','Martinez','isabella.martinez@example.com','1A!b2@c3$','Colege','+890 123-456-789','1994-06-05 13:20:00.000000','admin','2024-08-04 14:09:36.463416'),('54e8d9a3-7b2c-4a5e-8d3a-9f6e7b8c0d2e','Evelyn','Garcia','evelyn.garcia@example.com','AdmiN@7$','Client','+456 789-012-345','1987-10-22 16:30:00.000000','admin','2024-08-04 14:09:36.463416'),('5a72d903-58d7-4934-b23b-f36b4c95d540','Ethan','Harris','ethan.harris@example.com','W0rld!@#9','Client','+901 234-567-890','1989-05-14 12:45:00.000000','admin','2024-08-04 14:09:36.463416'),('65f9d2e4-8b7c-4a6e-9f2d-1a3e5b6c7d8e','Jackson','Lopez','jackson.lopez@example.com','F!ndM3$','Boss','+567 890-123-456','1995-11-11 09:15:00.000000','admin','2024-08-04 14:09:36.463416'),('6a4e2b5c-8f9d-4d0e-b1a6-c7e9d8f6a1b2','Michaele','Brown','michaele.brown@example.com','Teach3r@!','Teacher','+456 789-012-345','1982-12-11 14:30:00.000000','admin','2024-08-04 14:17:16.364499'),('6d8f0a58-005d-453f-9f4d-6b07e9e3e0b2','Mia','Jackson','mia.jackson@example.com','Str0ng@!5','Boss','+012 345-678-901','1991-07-19 10:30:00.000000','admin','2024-08-04 14:09:36.463416'),('76a0e9b2-1c4d-4e5f-8b6e-9f3a7d8e0c2d','Avery','Scott','avery.scott@example.com','Qwerty@3$','Colege','+678 901-234-567','1988-12-30 13:00:00.000000','admin','2024-08-04 14:09:36.463416'),('7c8d0a4b-f6f0-46c1-9e1f-05a17845d93a','Jacob','Lee','jacob.lee@example.com','Passw0rd#9','Colege','+123 456-789-012','1984-12-11 17:00:00.000000','admin','2024-08-04 14:09:36.463416'),('7d5a4d08-2f23-4f36-b9a6-b3d275fa6a00','Alice','Johnson','alice.johnson@example.com','P@ssw0rd1!','Teacher','+123 456-789-012','1985-06-15 08:30:00.000000','admin','2024-08-04 14:17:16.364499'),('87b1e4c9-7d2e-4b3e-9f1a-6e5c7d8b0f2d','Lily','Adams','lily.adams@example.com','C0mpl!ex$','Client','+789 012-345-678','1991-05-08 11:30:00.000000','admin','2024-08-04 14:09:36.463416'),('8e7c1f8b-d0f8-4a72-8c2d-1e1f9a6e7c9d','Sophia','Garcia','sophia.garcia@example.com','Qwerty@2$','Client','+234 567-890-123','1993-10-10 18:15:00.000000','admin','2024-08-04 14:09:36.463416'),('98c2d6e3-8b4d-4a9e-9f1a-7e3b8d5c0f7d','Lucas','Nelson','lucas.nelson@example.com','P@ssw0rd!','Boss','+890 123-456-789','1989-04-22 16:15:00.000000','admin','2024-08-04 14:09:36.463416'),('9f0b2d8d-04f0-462b-a5b1-6e4c09f8b1c4','Liam','Miller','liam.miller@example.com','G00d!@3$','Boss','+345 678-901-234','1987-01-26 11:00:00.000000','admin','2024-08-04 14:09:36.463416'),('a72c1b82-5b8e-4d19-8b3d-f74e89e5e251','Jane','Smith','jane.smith@example.com','Secre@t1!','Boss','+234 567-890-123','1990-04-22 08:30:00.000000','admin','2024-08-04 14:08:47.974032'),('a92d7e3b-f5b6-485f-80bb-5d1e8f1a9b3e','Ava','Davis','ava.davis@example.com','Secure#4$','Colege','+456 789-012-345','1990-03-22 14:30:00.000000','admin','2024-08-04 14:09:36.463416'),('a9d3e5f4-9c7e-4b2d-9f3a-6e0f2c7b8d9e','Megan','Baker','megan.baker@example.com','Str0ng#4$','Colege','+901 234-567-890','1990-02-11 10:00:00.000000','admin','2024-08-04 14:09:36.463416'),('b0c7d2a1-abe6-4a3e-9f6b-5a32c5f2a42d','Noah','Rodriguez','noah.rodriguez@example.com','P@ssw0rd!','Client','+567 890-123-456','1992-05-07 09:00:00.000000','admin','2024-08-04 14:09:36.463416'),('b0e4d6f5-1c8b-4a3e-9d2b-5e7c8d9f0a1b','Owen','Mitchell','owen.mitchell@example.com','AdM1n$@#','Client','+012 345-678-901','1986-08-19 12:30:00.000000','admin','2024-08-04 14:09:36.463416'),('c1d8e1f2-2e3b-41c1-9142-0f8a36e7d6b3','Emma','Martinez','emma.martinez@example.com','C0mpl!ex$','Boss','+678 901-234-567','1988-08-15 16:30:00.000000','admin','2024-08-04 14:09:36.463416'),('c1f5e7d8-9b6a-4c2e-9d3a-6e8b9f0c7d8e','Zoe','Perez','zoe.perez@example.com','P@ssw0rd1$','Boss','+123 456-789-012','1992-06-13 15:00:00.000000','admin','2024-08-04 14:09:36.463416'),('c83e3c6e-3c84-4e31-a4a7-5d65b3e7e5a6','Michael','Brown','michael.brown@example.com','Admi@n2#34','Colege','+345 678-901-234','1982-07-11 09:45:00.000000','admin','2024-08-04 14:08:47.974032'),('d2e6f7c9-1a8b-4d3e-9b0a-5e7c9f1d2e3f','Henry','Young','henry.young@example.com','Secure!2$','Colege','+234 567-890-123','1993-11-30 10:45:00.000000','admin','2024-08-04 14:09:36.463416'),('d2f1c3b8-bf7d-4b7d-92ea-8d3e6f7c6b34','Benjamin','Anderson','benjamin.anderson@example.com','F@st!9#0','Colege','+789 012-345-678','1985-04-18 12:00:00.000000','admin','2024-08-04 14:09:36.463416'),('d61fd379-e2c3-48d5-b6d3-2fcef9b57379','John','Doe','john.doe@example.com','Passw@rd1!','Client','+123 456-789-012','1985-10-15 12:00:00.000000','admin','2024-08-04 14:08:47.974032'),('e3d7f2a9-5c8b-469b-b0e1-3d2e6e5d8f6e','Charlotte','Thomas','charlotte.thomas@example.com','1234!@#$','Client','+890 123-456-789','1991-11-28 10:45:00.000000','admin','2024-08-04 14:09:36.463416'),('e9a1b07d-6c9b-4c2f-bb70-6e9c6f98a8c0','Robert','Smith','robert.smith@example.com','T3ach!ng$','Teacher','+234 567-890-123','1979-09-30 10:15:00.000000','admin','2024-08-04 14:17:16.364499'),('f4e8d1b2-9f3c-4b3e-8f2d-7e5c9b3a0c2d','Alexander','Taylor','alexander.taylor@example.com','Secur3!@#','Boss','+901 234-567-890','1989-02-20 13:15:00.000000','admin','2024-08-04 14:09:36.463416'),('f94d45e8-7c16-4f14-b8d6-8d65c8e5e9b7','Emily','Clark','emily.clark@example.com','Secur!ty5$','Client','+456 789-012-345','1995-12-30 11:00:00.000000','admin','2024-08-04 14:08:47.974032');
/*!40000 ALTER TABLE `Contacts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Subcategories`
--

DROP TABLE IF EXISTS `Subcategories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Subcategories` (
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CategoryName` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RowVersion` timestamp(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  PRIMARY KEY (`Name`),
  KEY `IX_Subcategories_CategoryName` (`CategoryName`),
  CONSTRAINT `FK_Subcategories_Categories_CategoryName` FOREIGN KEY (`CategoryName`) REFERENCES `Categories` (`Name`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Subcategories`
--

LOCK TABLES `Subcategories` WRITE;
/*!40000 ALTER TABLE `Subcategories` DISABLE KEYS */;
INSERT INTO `Subcategories` VALUES ('','Private','2024-08-04 13:55:48.506256'),('Boss','Work','2024-08-01 10:11:04.860915'),('Client','Work','2024-08-01 10:11:14.001158'),('Colege','Work','2024-08-01 10:11:18.770694'),('Teacher','Other','2024-08-04 13:55:48.506256');
/*!40000 ALTER TABLE `Subcategories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
  `Username` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RowVersion` timestamp(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
  PRIMARY KEY (`Username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES ('admin','AQAAAAIAAYagAAAAEEB60qQAM96BVguVm1VHPOvWlv5S8bYGNls1gPCpBacmG+L2RnX0Oo+QTouANjmJTw==','2024-08-04 13:57:26.981449');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20240729164929_InitialMigration','8.0.7'),('20240731224929_InitialNewDatabase','8.0.7'),('20240801095058_InitialMigration','8.0.7'),('20240801100821_UpdateCategory','8.0.7'),('20240803133527_DatabaseV3','8.0.7');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-08-04 14:27:11

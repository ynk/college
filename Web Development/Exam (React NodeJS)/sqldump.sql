-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: localhost    Database: web3
-- ------------------------------------------------------
-- Server version	8.0.22

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order` (
  `id` int NOT NULL AUTO_INCREMENT,
  `created` varchar(45) NOT NULL,
  `firstName` varchar(45) NOT NULL,
  `lastName` varchar(45) NOT NULL,
  `street` varchar(45) NOT NULL,
  `number` varchar(45) NOT NULL,
  `postalCode` varchar(45) NOT NULL,
  `city` varchar(45) NOT NULL,
  `telephone` varchar(45) NOT NULL,
  `email` varchar(45) NOT NULL,
  `totalPrice` double DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=208 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
INSERT INTO `order` VALUES (188,'2021-01-03T13:49:57.209Z','ynk','Test','Test','212a','EZ1238','Rift','+3241234566789','test@test.com',NULL),(189,'2021-01-03T13:53:12.421Z','ynk','Test','Test','212a','EZ1238','Rift','+3241234566789','test@test.com',NULL),(190,'2021-01-03T22:57:50.049Z','dave','daw','dwad','awdawd','awd','awdawd','awdawd','awdawd@dadawd.be',3950),(191,'2021-01-04T13:38:08.268Z','Dave','CHapal','Summoners Rift','487','1337Q','Rift','+2128448','dave@example.com',3950),(192,'2021-01-05T13:55:18.553Z','Dave','Test','1452','1455','14515','1651','65156','4141@846.be',1300),(193,'2021-01-05T13:55:27.455Z','Dave','Test','1452','1455','14515','1651','65156','4141@846.be',1300),(194,'2021-01-05T13:56:42.275Z','Dave','Test','1452','1455','14515','1651','65156','4141@846.be',1300),(195,'2021-01-05T13:57:15.942Z','Dave','Test','1452','1455','14515','1651','65156','4141@846.be',1300),(196,'2021-01-05T14:11:41.041Z','Dave','Test','1452','1455','14515','1651','65156','4141@846.be',1300),(197,'2021-01-05T14:12:05.451Z','Dave','Test','1452','1455','14515','1651','65156','4141@846.be',1300),(198,'2021-01-05T14:18:47.051Z','Dave','test','test','44','421','rift','54','dave@expalme.com',1300),(199,'2021-01-05T14:19:30.113Z','Dave','test','test','test','test','test','test','test@test.be',1300),(200,'2021-01-05T14:47:11.971Z','Dave','Stevens','Rift','487','1337q','rift','123516','dave@example.com',1300),(201,'2021-01-05T14:47:42.712Z','dadad','dawdaw','dawdw','15648','adwaw','dawd','125484','dave@example.com',1300),(202,'2021-01-05T16:52:27.310Z','Dave','Stevens','Rift','487','1337AQ','Rift','+123456','dave@example.com',1750),(203,'2021-01-05T18:48:15.349Z','Dave','Stevens','Rift','487','1337SQ','Rift','12345','dave@exmaple.be',8450),(204,'2021-01-05T18:50:45.456Z','Dave','Stevens','Rift','487','1337S','Rift','+123','dave@example.be',28150),(205,'2021-01-05T18:52:08.057Z','Dave','Stevens','Rift','487','1337SQ','Rift','+165','dave@example.be',13600),(206,'2021-01-05T18:52:34.479Z','Dave','Stevens','Rift','487','1337SQ','Rift','+165','dave@example.be',13600),(207,'2021-01-05T21:37:50.035Z','Dave','Test','1262','48','15616','rift','+1561','dave@example.be',3100);
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderline`
--

DROP TABLE IF EXISTS `orderline`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderline` (
  `orderId` int DEFAULT NULL,
  `productId` int DEFAULT NULL,
  `qty` int DEFAULT NULL,
  `price` double DEFAULT NULL,
  KEY `fk_order_idx` (`orderId`),
  KEY `fk_product_idx` (`productId`),
  CONSTRAINT `fk_order` FOREIGN KEY (`orderId`) REFERENCES `order` (`id`),
  CONSTRAINT `fk_product` FOREIGN KEY (`productId`) REFERENCES `product` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderline`
--

LOCK TABLES `orderline` WRITE;
/*!40000 ALTER TABLE `orderline` DISABLE KEYS */;
INSERT INTO `orderline` VALUES (188,12,1,3100),(188,7,1,1200),(189,12,1,3100),(189,7,1,1200),(190,1,1,400),(190,2,1,450),(190,12,1,3100),(191,1,1,400),(191,2,1,450),(191,12,1,3100),(192,1,1,400),(192,2,2,450),(193,2,2,450),(193,1,1,400),(194,1,1,400),(194,2,2,450),(195,1,1,400),(195,2,2,450),(196,2,2,450),(196,1,1,400),(197,2,2,450),(197,1,1,400),(198,2,2,450),(198,1,1,400),(199,2,2,450),(199,1,1,400),(200,1,1,400),(200,2,2,450),(201,2,2,450),(201,1,1,400),(202,2,3,450),(202,1,1,400),(203,2,5,450),(203,12,1,3100),(203,11,1,3100),(204,1,1,400),(204,12,2,3100),(204,15,2,3500),(204,9,2,2500),(204,11,1,3100),(204,2,1,450),(204,14,1,3500),(204,8,1,2500),(205,1,1,400),(205,15,2,3500),(205,12,2,3100),(206,1,1,400),(206,12,2,3100),(206,15,2,3500),(207,12,1,3100);
/*!40000 ALTER TABLE `orderline` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `price` double NOT NULL,
  `description` varchar(1000) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'Spellthief\'s Edge',400,'Earn 500 gold from this item to transform it into Frostfang, gaining Active - Warding.\n'),(2,'Doran\'s Ring',450,'Attacks deal an additional 5 physical damage to minions.'),(3,'Dark Seal',450,'Gain 2 stacks for a champion kill or 1 stacks for an assist (up to 10 stacks total). Lose 4 stacks on death.'),(4,'Oblivion Orb',650,'Cursed: Dealing magic damage applies 40% Grievous Wounds to enemy champions for 2 seconds.'),(5,'Hextech Alternator',800,'Damaging a champion deals an additional 50 - 125 magic damage (40s ).'),(6,'Lost Chapter',1000,'Upon leveling up, restores 20% max Mana over 3 seconds.'),(7,'Mejai\'s Soulstealer',1200,'Gain 4 stacks for a champion kill or 2 stacks for an assist (up to 25 stacks total). Lose 10 stacks on death.'),(8,'Void Staff',2500,'It does dmg, but more.'),(9,'Banshee\'s Veil',2500,'Grants a Spell Shield that blocks the next enemy Ability (40s ).'),(10,'Lich Bane',3000,'After using Ability, your next Attack is enhanced with an additional 150% base +60% magic damage (2.5 ).'),(11,'Rylai\'s Crystal Scepter',3100,'Damaging Abilities Slow enemies by 30% for 1 second.'),(12,'Nashor\'s Tooth',3100,'Attacks apply 15 +25% magic damage On-Hit'),(13,'Liandry\'s Anguish',3250,'Grants all other Legendary items 5 Ability Haste.'),(14,'Everfrost',3500,'Grants all other Legendary items 15 Ability Power.'),(15,'Luden\'s Tempest',3500,'Grants all other Legendary items 5 Magic Penetration.');
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-01-05 22:42:10

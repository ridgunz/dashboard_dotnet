-- phpMyAdmin SQL Dump
-- version 4.9.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Nov 16, 2023 at 05:53 AM
-- Server version: 10.4.10-MariaDB
-- PHP Version: 7.1.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `technical_dotnet`
--

-- --------------------------------------------------------

--
-- Table structure for table `Categories`
--

CREATE TABLE `Categories` (
  `Id` int(11) NOT NULL,
  `CategoryName` varchar(50) DEFAULT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `UpdatedAt` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `Categories`
--

INSERT INTO `Categories` (`Id`, `CategoryName`, `CreatedAt`, `UpdatedAt`) VALUES
(1, 'Smartphones', '2023-11-14 17:00:00', '2023-11-13 17:00:00'),
(3, 'Books', '2023-11-16 03:14:23', '2023-11-16 03:14:23');

-- --------------------------------------------------------

--
-- Table structure for table `Products`
--

CREATE TABLE `Products` (
  `Id` int(11) NOT NULL,
  `CategoryId` int(11) DEFAULT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `Price` decimal(18,0) DEFAULT NULL,
  `Image` varchar(250) DEFAULT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `UpdatedAt` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `Products`
--

INSERT INTO `Products` (`Id`, `CategoryId`, `Name`, `Price`, `Image`, `Description`, `CreatedAt`, `UpdatedAt`) VALUES
(8, 3, 'Books Harry Potter', '150000', '/images/fffd6f7d-705e-4f67-9cce-6867e6a69627_hp.jpeg', 'Deadly Hallows', '2023-11-16 03:37:57', '2023-11-16 03:37:57'),
(9, 1, 'Iphone 18', '150000', '/images/a5994a2d-0a4d-4b32-9471-68ddf529336f_iPhone-15-plus.jpg', 'New Iphone', '2023-11-16 03:38:23', '2023-11-16 03:38:23'),
(10, 3, 'Iphone 18', '150000', '/images/1dde4ff3-d52d-4642-a70c-f2b37425df6b_hp.jpeg', 'New Iphone', '2023-11-16 04:11:38', '2023-11-16 04:11:38'),
(11, 1, 'Buku Bekas', '1000000', '/images/9409b64c-2262-48f6-b769-2875d3f206a4_iPhone-15-plus.jpg', 'Buku Lama', '2023-11-16 04:26:40', '2023-11-16 04:26:40');

-- --------------------------------------------------------

--
-- Table structure for table `Users`
--

CREATE TABLE `Users` (
  `Id` int(11) NOT NULL,
  `Username` varchar(100) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Password` varchar(250) DEFAULT NULL,
  `Role` varchar(50) DEFAULT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT current_timestamp(),
  `UpdatedAt` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `Users`
--

INSERT INTO `Users` (`Id`, `Username`, `Email`, `Password`, `Role`, `CreatedAt`, `UpdatedAt`) VALUES
(4, 'ridgunz', 'ridwanmhmmd18@gmail.com', '$2b$10$R3f4ISezOyqgKKQaLOCdOOmAJLlRY7nx.ZH.tPbuda7o/vjA2Yhua', 'admin', '2023-11-14 14:29:15', '2023-11-14 14:29:15'),
(5, 'iwan', 'rid6unz@gmail.com', '$2b$10$R3f4ISezOyqgKKQaLOCdOOmAJLlRY7nx.ZH.tPbuda7o/vjA2Yhua', 'member', '2023-11-14 14:29:15', '2023-11-14 14:29:15');

-- --------------------------------------------------------

--
-- Table structure for table `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20231114120915_InitialCreate', '7.0.13');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Categories`
--
ALTER TABLE `Categories`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `Products`
--
ALTER TABLE `Products`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Products_CategoryId` (`CategoryId`);

--
-- Indexes for table `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Categories`
--
ALTER TABLE `Categories`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `Products`
--
ALTER TABLE `Products`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `Users`
--
ALTER TABLE `Users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `Products`
--
ALTER TABLE `Products`
  ADD CONSTRAINT `FK_Products_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`Id`) ON DELETE SET NULL;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

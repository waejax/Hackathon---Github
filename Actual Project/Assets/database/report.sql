-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 28, 2025 at 05:45 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.1.25

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `hackathon`
--

-- --------------------------------------------------------

--
-- Table structure for table `report`
--

CREATE TABLE `report` (
  `id` int(11) NOT NULL,
  `subject` text NOT NULL,
  `incident` text NOT NULL,
  `info` text NOT NULL,
  `corruption` text NOT NULL,
  `people` text NOT NULL,
  `peopleAddress` text NOT NULL,
  `position` text NOT NULL,
  `peopleNo` int(11) DEFAULT NULL,
  `peopleIc` int(11) DEFAULT NULL,
  `evidence` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `report`
--

INSERT INTO `report` (`id`, `subject`, `incident`, `info`, `corruption`, `people`, `peopleAddress`, `position`, `peopleNo`, `peopleIc`, `evidence`) VALUES
(2, 'accepting bribe for vendor permit', 'BSB, 15 August, 1.23pm', 'witness', 'bribery', 'ali bin abu', 'kampong rimba', 'student treasurer', 8888888, 1111111, ''),
(3, 'MIsuse of gov property', 'kampong kiulap, 23 august 2025, 8:34am', 'witness. saw them use gov car to go on trips', 'misuse', 'abu bin ali', 'kampong tungku, bandar seri begawan', 'driver', 7318232, 12312312, ''),
(4, 'giving money to cover up crime', 'kg tungku, 27/7/2025', 'witness', 'bribery', 'abu bin abu', 'kg tungku', 'village head', 0, 0, ''),
(5, 'bribe for faster building approval', 'On 12 August 2025 at around 10:30am, at the Land Department in Bandar Seri Begawan, a contractor handed an envelope to an officer in exchange for expediting the building permit process. The envelope appeared to contain cash.', 'Anonymous tip-off through internal staff', 'bribery', 'ali bin ali', 'kg kiulap', 'senior officer', 0, 0, 'uploads/1756300345_68af04398eea6_slip.png,uploads/1756300345_68af04398f6d1_home.png');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `report`
--
ALTER TABLE `report`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `report`
--
ALTER TABLE `report`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

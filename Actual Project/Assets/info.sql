-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 19, 2025 at 03:06 PM
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
-- Table structure for table `info`
--

CREATE TABLE `info` (
  `id` int(11) NOT NULL,
  `line` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `info`
--

INSERT INTO `info` (`id`, `line`) VALUES
(1, 'Report can be done anonymously and reporter are protected by the law'),
(2, 'Reports can be done by writing a letter, calling, email, or reporting from this app'),
(3, 'Act of corruption is to be reported to Anti-Corruption Bureau'),
(4, 'Receiving/soliciting bribes are an example of corruption'),
(5, 'Submitting false documents/claims/accounts can also be considered as corruption'),
(6, 'Corruption is when there is possession of unexplained property'),
(7, 'Misconduct in public office is another example of corruption'),
(8, 'Corruption is when someone is giving/promising/offering bribes'),
(9, 'Corruption is the act of giving or receiving gratification in return for a favour related to his/her official duty'),
(10, 'Anti-Corruption Bureau (ACB) handles all problem related to corruption'),
(11, 'ACB investigates corruption offences and aims to prevent corruption in both public and private sectors');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `info`
--
ALTER TABLE `info`
  ADD PRIMARY KEY (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

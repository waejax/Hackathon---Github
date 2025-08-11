-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Aug 11, 2025 at 11:51 AM
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
(1, 'Act of corruption is to be reported to Anti-Corruption Bureau (ACB). You can hand in your report by coming to ACB office, by writing a letter to ACB, by calling, through email, or you can report from this app. Report can be done anonymously and reporter are protected by the law. So if you see someone doing corruption, do what is right and report it to ACB.'),
(2, 'Submitting a false claim is punishable by law. If you are unsure, you can contact Anti-Corruption Bureau (ACB) on what is considered as corruption, or explore this app further. We aim to spread awareness on corruption and your feedback is greatly appreciated'),
(3, 'Corruption is the act of giving or receiving gratification in return for a favour related to his/her official duty. Corruption include receiving/soliciting bribes, giving/promising/offering bribes, submitting false documents/claims/accounts, possession of unexplained property, and misconduct in public office'),
(4, 'Anti-Corruption Bureau (ACB) handles all problem related to corruption. ACB investigates corruption offences and aims to prevent corruption in both public and private sectors');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `info`
--
ALTER TABLE `info`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `info`
--
ALTER TABLE `info`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

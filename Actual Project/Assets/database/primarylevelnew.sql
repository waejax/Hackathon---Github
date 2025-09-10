-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 10, 2025 at 01:52 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

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
-- Table structure for table `primarylevel`
--

CREATE TABLE `primarylevel` (
  `id` int(11) NOT NULL,
  `line` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `primarylevel`
--

INSERT INTO `primarylevel` (`id`, `line`) VALUES
(1, 'Treat others the way you want to be treated, with kindness, fairness, and respect.'),
(2, 'Be kind to everyone you meet, because the kindness you give often comes back to you in surprising ways.'),
(3, 'Work hard and never give up, even if something is difficult, because effort always makes you stronger.'),
(4, 'Sharing your toys, food, or time makes others happy, and it shows you care about their feelings.'),
(5, 'Forgive others when they make mistakes, because everyone needs another chance to do better.'),
(6, 'Always tell the truth, even if it feels scary, because honesty helps people trust you.'),
(7, 'You have encountered the first shard!'),
(8, 'Collect all four shards to help budiman find his keris!'),
(9, 'One more piece returns to me, the blade grows stronger!\r\n'),
(10, 'Continue on your journey. There is 2 more!'),
(11, 'We are almost done. This shard shines with your honesty!'),
(12, 'At last, the blade can be whole again.'),
(13, 'Every trial taught me humility. This final piece isn’t just metal; it’s a lesson');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `primarylevel`
--
ALTER TABLE `primarylevel`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `primarylevel`
--
ALTER TABLE `primarylevel`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

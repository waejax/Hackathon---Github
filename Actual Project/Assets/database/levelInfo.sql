-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 06, 2025 at 05:04 AM
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
  `line` text NOT NULL,
  `level` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `info`
--

INSERT INTO `info` (`id`, `line`, `level`) VALUES
(1, 'Always tell the truth, even if you made a mistake.', 'primary'),
(2, 'Lying make people lose trust in you.', 'primary'),
(3, 'Telling lies can lead to more trouble later.', 'primary'),
(4, 'Coming to school on time shows your discipline.', 'primary'),
(5, 'When you take responsibility, people learn to trust you.', 'primary'),
(6, 'Have patient and be calm when things don\'t go well.', 'secondary'),
(7, 'Learning from mistakes helps you become smarter.', 'secondary'),
(8, 'Be fair, everyone should be treated the same', 'secondary'),
(9, 'It\'s better to try again than to cheat or ask for special treatment', 'secondary'),
(10, 'Respect your parents and teachers.', 'secondary'),
(11, 'Don\'t ask for a pass you didn\'t earn.', 'secondary'),
(12, 'Respect the rules, even if it feels hard or unfair', 'secondary'),
(13, 'Using someone else\'s address is unfair to others who follow the rules.', 'uni'),
(14, 'Trust is earned by doing what is right, not by taking easy ways out', 'uni'),
(15, 'Being truthful helps you build your own success honestly.', 'uni'),
(16, 'It\'s better to be creative and find honest solutions to your problems', 'uni'),
(17, 'Taking shortcuts can make you feel guilty and stressed', 'uni'),
(18, 'Accepting the job without trying might stop you from learning important skills.', 'work'),
(19, 'Choosing to work honestly helps you build real confidence and experience', 'work'),
(20, 'It\'s fair to let others have a chance by going through the normal process.', 'work'),
(21, 'Taking a job without proving yourself may make others think you didn?t earn it', 'work'),
(22, 'Sometimes saying no shows that you respect hard work and fairness', 'work'),
(23, 'Accepting favors can create unfair situations for others', 'work');

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

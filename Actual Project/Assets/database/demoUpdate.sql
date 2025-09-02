-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 02, 2025 at 01:46 PM
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
-- Table structure for table `demo`
--

CREATE TABLE `demo` (
  `id` int(11) NOT NULL,
  `line` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `demo`
--

INSERT INTO `demo` (`id`, `line`) VALUES
(1, 'Hello, Welcome to Awang Budiman: Leap towards Goodness!'),
(2, 'I am Awang Budiman, I will be your \'budi\' throughout this game'),
(3, 'Use the left and right arrow to move, and space bar to jump'),
(4, 'Now you know how to move along the screen!'),
(5, 'The icon above the player is an info icon'),
(6, 'Info icon will help you during the actual game'),
(7, 'There is a pause button on the top right.'),
(8, 'Click on the \'instruction\' tab to see how the game work'),
(9, 'Hit more icon to learn interesting info!'),
(10, 'You just encounter an evidence'),
(11, 'Collect as many evidence as you can'),
(12, 'This will help you on the upcoming game'),
(13, 'The number on the top left will show you the number of evidence you have collected'),
(14, 'You are required to collect a minimum of 2 evidence before you can move to the next stage'),
(15, 'You are now ready to start playing!'),
(16, 'Welcome to the first level.'),
(17, 'Here in this level, we learn good values.'),
(18, 'Hit the info icon to learn more about it!');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `demo`
--
ALTER TABLE `demo`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `demo`
--
ALTER TABLE `demo`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

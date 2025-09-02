-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 02, 2025 at 04:59 AM
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
(1, 'Hello, Welcome to (name)!'),
(2, 'I am Awang Budiman'),
(3, 'I will be your \'budi\' throughout this game'),
(4, 'Press the right arrow key to move to the right'),
(5, 'and press the left arrow key to move to the left'),
(6, 'Press on the space bar to jump'),
(7, 'Now you know how to move along the screen!'),
(8, 'You just encounter an info icon'),
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

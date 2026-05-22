-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 22, 2026 at 05:05 PM
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
-- Database: `cruz_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbllogin_credentials`
--

CREATE TABLE `tbllogin_credentials` (
  `LoginID` int(11) NOT NULL,
  `user_username` varchar(50) NOT NULL,
  `user_password` varchar(50) NOT NULL,
  `UserID` int(11) NOT NULL,
  `is_active` int(11) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbllogin_credentials`
--

INSERT INTO `tbllogin_credentials` (`LoginID`, `user_username`, `user_password`, `UserID`, `is_active`) VALUES
(1, 'username1', 'admin', 1, 1),
(2, 'username2', 'admin2', 2, 1),
(3, 'kiel', 'cruz12345678', 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `tbluser_information`
--

CREATE TABLE `tbluser_information` (
  `userID` int(11) NOT NULL,
  `firstname` varchar(50) NOT NULL,
  `middlename` varchar(50) NOT NULL,
  `lastname` varchar(50) NOT NULL,
  `emailAddress` varchar(255) NOT NULL,
  `homeAddress` varchar(255) NOT NULL,
  `birthDate` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbluser_information`
--

INSERT INTO `tbluser_information` (`userID`, `firstname`, `middlename`, `lastname`, `emailAddress`, `homeAddress`, `birthDate`) VALUES
(1, 'dasdw', 'sdasd', 'asdawd', 'sdad', 'dasdaw', '2026-05-22');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbllogin_credentials`
--
ALTER TABLE `tbllogin_credentials`
  ADD PRIMARY KEY (`LoginID`);

--
-- Indexes for table `tbluser_information`
--
ALTER TABLE `tbluser_information`
  ADD PRIMARY KEY (`userID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbllogin_credentials`
--
ALTER TABLE `tbllogin_credentials`
  MODIFY `LoginID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `tbluser_information`
--
ALTER TABLE `tbluser_information`
  MODIFY `userID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

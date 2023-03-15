-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 09, 2023 at 05:58 PM
-- Server version: 10.4.21-MariaDB
-- PHP Version: 7.4.25

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `maturski rad`
--

-- --------------------------------------------------------

--
-- Table structure for table `aktivnost`
--

CREATE TABLE `aktivnost` (
  `id` int(11) NOT NULL,
  `Ucenik` varchar(25) NOT NULL,
  `Aktivnost` varchar(3) NOT NULL,
  `Predmet` varchar(255) NOT NULL,
  `Datum` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `aktivnost`
--

INSERT INTO `aktivnost` (`id`, `Ucenik`, `Aktivnost`, `Predmet`, `Datum`) VALUES
(1, 'adjurekovic', '+', 'Matematika', '01-Mar-23'),
(2, 'maleksic', '+', 'Matematika', '02-Mar-23'),
(3, 'pmaodus', '-', 'Matematika', '05-Mar-23'),
(4, 'pmaodus', '-', 'Matematika', '06-Mar-23'),
(5, 'maleksic', '+', 'Matematika', '09-Mar-23'),
(6, 'maleksic', '-', 'Matematika', '09-Mar-23');

-- --------------------------------------------------------

--
-- Table structure for table `korisnik`
--

CREATE TABLE `korisnik` (
  `id` int(255) NOT NULL,
  `Username` varchar(20) NOT NULL,
  `Password` varchar(20) NOT NULL,
  `Role` varchar(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `korisnik`
--

INSERT INTO `korisnik` (`id`, `Username`, `Password`, `Role`) VALUES
(1, 'adjurekovic', 'djurekovica', 'user'),
(2, 'pmaodus', 'maodusp', 'user'),
(3, 'maleksic', 'aleksicm', 'user'),
(4, 'akovacevic', 'kovacevica', 'admin'),
(5, 'msavic', 'savicm', 'admin'),
(6, 'skrulj', 'kruljs', 'admin'),
(7, 'jlazarevic', 'lazarevicj', 'admin'),
(8, 'aradisic', 'radisica', 'admin'),
(9, 'tdjuric', 'djurict', 'admin'),
(10, 'dlukic', 'lukicd', 'admin'),
(11, 'mradulovic', 'radulovicm', 'admin'),
(12, 'amedan', 'medana', 'admin'),
(13, 'kdimitrijevic', 'dimitrijevick', 'admin'),
(14, 'djtovjanin', 'tovjanindj', 'admin'),
(15, 'jstojsic', 'stojsicj', 'admin'),
(16, 'abulatovic', 'bulatovica', 'user'),
(17, 'rpalkovljevic', 'palkovljevicr', 'user'),
(18, 'norosic', 'orosicn', 'user'),
(19, 'mdjekic', 'djekicm', 'user'),
(20, 'akljajic', 'kljajica', 'user');

-- --------------------------------------------------------

--
-- Table structure for table `ocene`
--

CREATE TABLE `ocene` (
  `id` int(11) NOT NULL,
  `Ocena` int(1) NOT NULL,
  `Ucenik` varchar(20) NOT NULL,
  `Predmet` varchar(50) NOT NULL,
  `Datum` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `ocene`
--

INSERT INTO `ocene` (`id`, `Ocena`, `Ucenik`, `Predmet`, `Datum`) VALUES
(1, 1, 'maleksic', 'Matematika', '05-Mar-23'),
(2, 1, 'maleksic', 'Filozofija', '05-Mar-23'),
(3, 3, 'maleksic', 'Fizičko', '04-Mar-23'),
(4, 2, 'maleksic', 'Fizičko', '01-Mar-23'),
(5, 5, 'maleksic', 'Fizičko', '06-Mar-23');

-- --------------------------------------------------------

--
-- Table structure for table `predmeti`
--

CREATE TABLE `predmeti` (
  `id` int(11) NOT NULL,
  `Naziv` varchar(50) NOT NULL,
  `Predavac` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `predmeti`
--

INSERT INTO `predmeti` (`id`, `Naziv`, `Predavac`) VALUES
(1, 'Biologija', 7),
(2, 'Engleski jezik', 8),
(3, 'Filozofija', 9),
(4, 'Fizičko', 10),
(5, 'Fizika', 11),
(6, 'Matematika', 12),
(7, 'Modeli i baze podataka', 6),
(8, 'Napredne tehnike', 5),
(9, 'Nemački jezik', 13),
(10, 'Primena računara', 14),
(11, 'Srpski jezik i književnost', 15),
(12, 'Programiranje i programski jezici', 4);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `aktivnost`
--
ALTER TABLE `aktivnost`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `korisnik`
--
ALTER TABLE `korisnik`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `ocene`
--
ALTER TABLE `ocene`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `predmeti`
--
ALTER TABLE `predmeti`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `aktivnost`
--
ALTER TABLE `aktivnost`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `korisnik`
--
ALTER TABLE `korisnik`
  MODIFY `id` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `ocene`
--
ALTER TABLE `ocene`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `predmeti`
--
ALTER TABLE `predmeti`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

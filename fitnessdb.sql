-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2020. Máj 17. 13:50
-- Kiszolgáló verziója: 10.4.11-MariaDB
-- PHP verzió: 7.2.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `fitnessdb`
--

-- --------------------------------------------------------

CREATE TABLE `logins` (
  `azonosito` varchar(20) NOT NULL,
  `FirstName` varchar(20) NOT NULL,
  `LastName` varchar(20) NOT NULL,
  `Datum` datetime DEFAULT NULL,
  `TicketID` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;



--
-- Tábla szerkezet ehhez a táblához `tickets`
--





CREATE TABLE `tickets` (
  `id` int(10) NOT NULL,
  `valid_from` date DEFAULT NULL,
  `valid_until` date DEFAULT NULL,
  `nr_of_entries` int(20) DEFAULT NULL,
  `nr_of_entries_day` int(20) DEFAULT NULL,
  `hour_from` int(30) DEFAULT NULL,
  `hour_until` int(30) DEFAULT NULL,
  `weekend` tinyint(1) NOT NULL,
  `azonosito` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- A tábla adatainak kiíratása `tickets`
--

INSERT INTO `tickets` (`id`, `valid_from`, `valid_until`, `nr_of_entries`, `nr_of_entries_day`, `hour_from`, `hour_until`, `weekend`, `azonosito`) VALUES
(19, '2020-05-10', '2020-05-30', 14, 2, 8, 21, 0, '513339016'),
(20, '2020-05-10', '2020-07-10', 48, 3, 8, 22, 0, '513339016'),
(22, '2020-05-20', '2020-12-20', 150, 3, 8, 22, 0, '510109830'),
(23, '2020-05-16', '2020-05-30', 10, 1, 0, 24, 0, '510109830'),
(24, '2020-01-01', '2020-12-01', 500, 5, 8, 22, 0, '987654321');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `FirstName` varchar(20) NOT NULL,
  `LastName` varchar(20) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `PhoneNumber` varchar(10) NOT NULL,
  `birthday` varchar(20) NOT NULL,
  `admin` int(1) NOT NULL,
  `azonosito` varchar(128) NOT NULL,
  `active` tinyint(4) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`FirstName`, `LastName`, `Email`, `PhoneNumber`, `birthday`, `admin`, `azonosito`, `active`) VALUES
('admin', 'admin', 'admin@valami.com', '0766666666', '1980. 03. 22', 1, '123456789', 1),
('Saska ', 'Elod', 'saka\"valam.ro', '0454545455', '1998. 05. 29.', 0, '231480678', 1),
('Ebed', 'Elek', 'ebed.elek@yahoo.com', '0715456987', '1998. 06. 07.', 0, '510109830', 1),
('Szocs Z', 'Zoltan', 'szocs@zoltan.ro', '0751284484', '1998. 05. 29', 0, '513339016', 1),
('Szocs', 'Zoltan', 'szocs.zoltan@yahoo.com', '0751284484', '1998. 05. 29.', 0, '987654321', 1);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `tickets`
--
ALTER TABLE `tickets`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`azonosito`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `tickets`
--
ALTER TABLE `tickets`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

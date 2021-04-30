using NUnit.Framework;
using DWMH.Core;
using DWMH.Core.Repos;
using DWMH.Core.Exceptions;
using TestsDAL.TestDoubles;
using System.Collections.Generic;
using System;
using System.IO;
using DWMH.DAL;
using DWMH.Core.Loggers;

namespace TestsDAL
{
   public class FileRepoTests
    {
        [Test]
        public void ReadReservationsShouldReturnAllReservationsForHost()
        {
            IReservationRepository reservationRepo = new ReservationFileRepository("test", new NullLogger());
            IHostRepository hostRepository = new HostFileRepository("test\\hosts.csv", new NullLogger());
            IGuestRepository guestRepository = new GuestFileRepository("test\\guests.csv", new NullLogger());
            Host host = hostRepository.ReadByEmail("test1@gmail.com");
            Guest guest = guestRepository.ReadByEmail("tt@gmail.com");

            Reservation copy = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = host,
                Guest = guest
            };
            copy.SetTotal();
            copy.ID = 1;

            List<Reservation> reservations = reservationRepo.ReadByHost(host);

            Assert.AreEqual(1, reservations.Count);
            Assert.AreEqual(410, reservations[0].Total);
            Assert.AreEqual(copy.StartDate, reservations[0].StartDate);
            Assert.AreEqual(copy.EndDate, reservations[0].EndDate);
            Assert.AreEqual(copy.ID, reservations[0].ID);
        }

        [Test]
        public void ReadReservationShouldReturnEmptyListForNoOrEmptyFile()
        {
            IReservationRepository reservationRepo = new ReservationFileRepository("test", new NullLogger());
            IHostRepository hostRepository = new HostFileRepository("test\\hosts.csv", new NullLogger());
            IGuestRepository guestRepository = new GuestFileRepository("test\\guests.csv", new NullLogger());
            Host host = new Host
            {
                Email = "one@gmail.com",
                ID = "2",
                City = "Chicago",
                State = "IL",
                LastName = "Fretty",
                WeekendRate = 100,
                StandardRate = 40
            };

            Guest guest = guestRepository.ReadByEmail("tt@gmail.com");

            List<Reservation> reservations = reservationRepo.ReadByHost(host);

            Assert.AreEqual(0,reservations.Count);
        }
        [Test]
        public void CreateThenRemoveFileEntry()
        {
            IReservationRepository reservationRepo = new ReservationFileRepository("test", new NullLogger());
            IHostRepository hostRepository = new HostFileRepository("test\\hosts.csv", new NullLogger());
            IGuestRepository guestRepository = new GuestFileRepository("test\\guests.csv", new NullLogger());

            Host host = hostRepository.ReadByEmail("test1@gmail.com");
            Guest guest = guestRepository.ReadByEmail("tt@gmail.com");

            Reservation toAdd = new Reservation
            {
                StartDate = new DateTime(2022, 2, 1),
                EndDate = new DateTime(2022, 2, 8),
                Host = host,
                Guest = guest
            };
            toAdd.SetTotal();

            var result = reservationRepo.Create(toAdd);

            Assert.AreEqual(2, reservationRepo.ReadByHost(host).Count);
            Assert.AreEqual(2, result.ID);
            Assert.AreEqual(toAdd.StartDate, reservationRepo.ReadByHost(host)[1].StartDate);
            Assert.AreEqual(toAdd.EndDate, reservationRepo.ReadByHost(host)[1].EndDate);
            Assert.AreEqual(toAdd.Guest.ID, reservationRepo.ReadByHost(host)[1].Guest.ID);

            result = reservationRepo.Delete(toAdd);

            Assert.AreEqual(1, reservationRepo.ReadByHost(host).Count);
            Assert.AreEqual(2, result.ID);
        }

        [Test]
        public void DeleteShouldNotRemoveNonMatchedID()
        {
            IReservationRepository reservationRepo = new ReservationFileRepository("test", new NullLogger());
            IHostRepository hostRepository = new HostFileRepository("test\\hosts.csv", new NullLogger());
            IGuestRepository guestRepository = new GuestFileRepository("test\\guests.csv", new NullLogger());

            Host host = hostRepository.ReadByEmail("test1@gmail.com");
            Guest guest = guestRepository.ReadByEmail("tt@gmail.com");

            Reservation toDelete = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = host,
                Guest = guest,
                ID=2
            };
            toDelete.SetTotal();

            Reservation result = reservationRepo.Delete(toDelete);

            Assert.AreEqual(1, reservationRepo.ReadByHost(host).Count);
            Assert.IsNull(result);
        }

        [Test]
        public void UpdateShouldNotChangeNonMatchedID()
        {
            IReservationRepository reservationRepo = new ReservationFileRepository("test", new NullLogger());
            IHostRepository hostRepository = new HostFileRepository("test\\hosts.csv", new NullLogger());
            IGuestRepository guestRepository = new GuestFileRepository("test\\guests.csv", new NullLogger());

            Host host = hostRepository.ReadByEmail("test1@gmail.com");
            Guest guest = guestRepository.ReadByEmail("tt@gmail.com");
            Reservation original = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = host,
                Guest = guest,
                ID = 1
            };
            original.SetTotal();

            Reservation toChange = new Reservation
            {
                StartDate = new DateTime(2022, 2, 1),
                EndDate = new DateTime(2022, 2, 8),
                Host = host,
                Guest = guest,
                ID = 2
            };
            toChange.SetTotal();

            Reservation result = reservationRepo.Update(2, toChange);

            Assert.AreEqual(1, reservationRepo.ReadByHost(host).Count);
            Assert.AreEqual(original.EndDate, reservationRepo.ReadByHost(host)[0].EndDate);
            Assert.AreEqual(original.StartDate, reservationRepo.ReadByHost(host)[0].StartDate);

        }

        [Test]
        public void UpdateShouldSucceedTwice()
        {
            IReservationRepository reservationRepo = new ReservationFileRepository("test", new NullLogger());
            IHostRepository hostRepository = new HostFileRepository("test\\hosts.csv", new NullLogger());
            IGuestRepository guestRepository = new GuestFileRepository("test\\guests.csv", new NullLogger());

            Host host = hostRepository.ReadByEmail("test1@gmail.com");
            Guest guest = guestRepository.ReadByEmail("tt@gmail.com");
            Reservation original = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = host,
                Guest = guest,
                ID = 1
            };
            original.SetTotal();

            Reservation toChange = new Reservation
            {
                StartDate = new DateTime(2022, 2, 1),
                EndDate = new DateTime(2022, 2, 8),
                Host = host,
                Guest = guest,
                ID = 1
            };
            toChange.SetTotal();

            Reservation result = reservationRepo.Update(1, toChange);

            Assert.AreEqual(1, reservationRepo.ReadByHost(host).Count);
            Assert.AreEqual(toChange.EndDate, reservationRepo.ReadByHost(host)[0].EndDate);
            Assert.AreEqual(toChange.StartDate, reservationRepo.ReadByHost(host)[0].StartDate);

            result = reservationRepo.Update(1, original);

            Assert.AreEqual(1, reservationRepo.ReadByHost(host).Count);
            Assert.AreEqual(original.EndDate, reservationRepo.ReadByHost(host)[0].EndDate);
            Assert.AreEqual(original.StartDate, reservationRepo.ReadByHost(host)[0].StartDate);
        }
    }
}

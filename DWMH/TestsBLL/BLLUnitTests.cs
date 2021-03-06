using NUnit.Framework;
using DWMH.Core;
using DWMH.Core.Repos;
using TestsDAL.TestDoubles;
using DWMH.BLL;
using System.Collections.Generic;
using System;


namespace TestsBLL
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnSuccessAndList()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = new Host
            {
                LastName = "Testy1",
                ID = "abc-123",
                Email = "test1@gmail.com",
                City = "Chicago",
                State = "IL",
            };
            host.SetRates(50M, 80M);

            Result<List<Reservation>> result = service.ViewByHost(host);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Value.Count);
            Assert.AreEqual(410M, result.Value[0].Total);
            Assert.AreEqual(host, result.Value[0].Host);
        }

        [Test]
        public void ShouldBeEmptyWithNotFoundMessage()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = new Host
            {
                LastName = "Testy1",
                ID = "abc-123",
                Email = "oops",
                City = "Chicago",
                State = "IL",
            };
            host.SetRates(50M, 80M);

            Result<List<Reservation>> result = service.ViewByHost(host);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("no reservations found for host", result.Messages[0]);
            Assert.AreEqual(0, result.Value.Count);
        }

        [Test]
        public void ShouldFindGuestWithEmail()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Guest guest = new Guest
            {
                LastName = "Testington",
                FirstName = "Tesla",
                Email = "tt@gmail.com",
                ID = 1
            };
            var result = service.FindGuestByEmail("tt@gmail.com");

            Assert.IsTrue(result.Success);
            Assert.AreEqual(guest, result.Value);
        }

        [Test]
        public void ShouldNotFindGuestWithNoMatchingEmail()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            var result = service.FindGuestByEmail("t@gmail.com");

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.Messages[0].Contains("guest"));
        }

        //both dates before existing start
        [TestCase("2021,06,06", "2021,06,09")]
        //both dates after existing end
        [TestCase("2022,02,02", "2022,02,06")]

        public void ShouldCreateValidReservation(string start, string end)
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];
            Guest guest = guestRepo.ReadAll()[0];
            Reservation toAdd = new Reservation
            {
                StartDate = DateTime.Parse(start),
                EndDate = DateTime.Parse(end),
                Guest = guest,
                Host = host
            };

            Result<Reservation> result = service.Create(toAdd);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, result.Value.ID);
            Assert.AreEqual(host, result.Value.Host);
            Assert.AreEqual(guest, result.Value.Guest);
            Assert.AreEqual(DateTime.Parse(start), result.Value.StartDate);
            Assert.AreEqual(DateTime.Parse(end), result.Value.EndDate);

        }

        [Test]
        public static void CreateShouldNotAllowNullReservation()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];
            Guest guest = guestRepo.ReadAll()[0];
            Reservation toAdd = null;

            Result<Reservation> result = service.Create(toAdd);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Messages[0].Contains("reservation"));
            Assert.IsNull(result.Value);

        }

        //start date must be future
        [TestCase("2020,02,20", "2022,02,06")]
        //start date must be before end date
        [TestCase("2021,06,09", "2021,06,05")]
        //startExisting <= endNew && endNew <= endExisting
        [TestCase("2021,12,30", "2022,01,05")]
        //startExisting <= startNew && startNew<= endExisting
        [TestCase("2022,1,3", "2022,01,12")]
        //startNew >= startExisting && endNew <= endExisting
        [TestCase("2022, 01, 03", "2022, 01, 08")]
        // startNew <= startExisting && endNew >= endExisting
        [TestCase("2021,12,30", "2022, 01, 11")]
        public void CreateShouldNotAllowInvalidReservationDates(string start, string end)
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];
            Guest guest = guestRepo.ReadAll()[0];
            Reservation toAdd = new Reservation
            {
                StartDate = DateTime.Parse(start),
                EndDate = DateTime.Parse(end),
                Guest = guest,
                Host = host
            };

            Result<Reservation> result = service.Create(toAdd);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.Messages[0].Contains("date"));
        }

        [Test]
        public void CreateShouldNotAllowNonListedGuest()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];
            Guest guest = new Guest
            {
                Email = "friend@friends.com",
                FirstName = "Super",
                LastName = "Duper",
                ID = 3
            };
            Reservation toAdd = new Reservation
            {
                StartDate = DateTime.Parse("2022,02,02"),
                EndDate = DateTime.Parse("2022,02,06"),
                Guest = guest,
                Host = host
            };

            Result<Reservation> result = service.Create(toAdd);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Messages[0].Contains("guest"));
            Assert.IsNull(result.Value);
        }

        [Test]
        public void CreateShouldNotAllowNonListedHost()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = new Host
            {
                Email = "friend@friends.com",
                LastName = "Duper",
                ID = "abc-123",
                City = "Chicago",
                State = "IL",
                StandardRate = 50M,
                WeekendRate = 100M
            };

            Guest guest = guestRepo.ReadAll()[0];

            Reservation toAdd = new Reservation
            {
                StartDate = DateTime.Parse("2022,02,02"),
                EndDate = DateTime.Parse("2022,02,06"),
                Guest = guest,
                Host = host
            };

            Result<Reservation> result = service.Create(toAdd);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Messages[0].Contains("host"));
            Assert.IsNull(result.Value);
        }
        [Test]
        public void CreateShouldNotAllowNullGuest()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];
            Guest guest = null;

            Reservation toAdd = new Reservation
            {
                StartDate = DateTime.Parse("2022,02,02"),
                EndDate = DateTime.Parse("2022,02,06"),
                Guest = guest,
                Host = host
            };

            Result<Reservation> result = service.Create(toAdd);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Messages[0].Contains("guest"));
            Assert.IsNull(result.Value);
        }

        [Test]
        public void AddShouldNotAllowNullHost()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = null;

            Guest guest = guestRepo.ReadAll()[0];

            Reservation toAdd = new Reservation
            {
                StartDate = DateTime.Parse("2022,02,02"),
                EndDate = DateTime.Parse("2022,02,06"),
                Guest = guest,
                Host = host
            };

            Result<Reservation> result = service.Create(toAdd);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Messages[0].Contains("host"));
            Assert.IsNull(result.Value);
        }

        [Test]
        public void UpdateShouldSucceed()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];
            Guest guest = guestRepo.ReadAll()[0];
            Reservation copy = new Reservation
            {
                StartDate = new DateTime(2021, 12, 28),
                EndDate = new DateTime(2021, 12, 29),
                Guest = guest,
                Host = host
            };
            copy.SetTotal();
            copy.ID = 1;

            Reservation toUpdate = new Reservation
            {
                StartDate = new DateTime(2021, 12, 28),
                EndDate = new DateTime(2021, 12, 29),
                Host = host,
                Guest = guest,
                ID = 1
            };

            var result = service.Update(1, toUpdate);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(copy, result.Value);
            Assert.AreEqual(copy, service.ViewByHost(host).Value[0]);
            Assert.AreEqual(1, service.ViewByHost(host).Value.Count);
        }
        [Test]
        public void UpdateShouldNotAllowNullHost()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = null;

            Reservation toChange = new Reservation
            {
                StartDate = new DateTime(2022, 02, 03),
                EndDate = new DateTime(2022, 02, 06),
                Host = host,
                Guest = guestRepo.ReadAll()[0],
                ID = 1
            };


            Reservation copy = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = hostRepo.ReadAll()[0],
                Guest = guestRepo.ReadAll()[0]
            };
            copy.SetTotal();
            copy.ID = 1;

            Result<Reservation> result = service.Update(1, toChange);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Messages[0].Contains("host"));
            Assert.IsNull(result.Value);
            Assert.AreEqual(copy, service.ViewByHost(hostRepo.ReadAll()[0]).Value[0]);
            Assert.AreEqual(1, service.ViewByHost(hostRepo.ReadAll()[0]).Value.Count);
        }

        [Test]
        public void UpdateShouldNotAllowNullGuest()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];
            Guest guest = null;

            Reservation toChange = new Reservation
            {
                StartDate = new DateTime(2022, 02, 03),
                EndDate = new DateTime(2022, 02, 06),
                Host = host,
                Guest = guest,
                ID = 1
            };


            Reservation copy = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = host,
                Guest = guestRepo.ReadAll()[0]
            };
            copy.SetTotal();
            copy.ID = 1;

            Result<Reservation> result = service.Update(1, toChange);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Messages[0].Contains("guest"));
            Assert.IsNull(result.Value);
            Assert.AreEqual(copy, service.ViewByHost(hostRepo.ReadAll()[0]).Value[0]);
            Assert.AreEqual(1, service.ViewByHost(hostRepo.ReadAll()[0]).Value.Count);
        }

        //start date must be future
        [TestCase("2020,02,20", "2022,02,06")]
        //start date must be before end date
        [TestCase("2021,06,09", "2021,06,05")]
        //startExisting <= endNew && endNew <= endExisting
        [TestCase("2021,12,30", "2022,01,05")]
        //startExisting <= startNew && startNew<= endExisting
        [TestCase("2022,1,3", "2022,01,12")]
        //startNew >= startExisting && endNew <= endExisting
        [TestCase("2022, 01, 03", "2022, 01, 08")]
        // startNew <= startExisting && endNew >= endExisting
        [TestCase("2021,12,30", "2022, 01, 11")]
        public void UpdateShouldNotAllowInvalidDates(string newStart, string newEnd)
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];
            Guest guest = guestRepo.ReadAll()[0];
            Reservation toAdd = new Reservation
            {
                StartDate = new DateTime(2021, 12, 28),
                EndDate = new DateTime(2021, 12, 29),
                Guest = guest,
                Host = host
            };
            toAdd.SetTotal();

            Reservation toUpdate = new Reservation
            {
                StartDate = DateTime.Parse(newStart),
                EndDate = DateTime.Parse(newEnd),
                Host = host,
                Guest = guest,
                ID = 2
            };

            var adddingResult = service.Create(toAdd);

            var result = service.Update(2, toUpdate);

            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Messages[0].Contains("date"));
            Assert.IsNull(result.Value);
            Assert.AreEqual(adddingResult.Value, service.ViewByHost(hostRepo.ReadAll()[0]).Value[0]);
            Assert.AreEqual(2, service.ViewByHost(hostRepo.ReadAll()[0]).Value.Count);
        }

        [TestCase(0)]
        [TestCase(2)]
        public void UpdateShouldNotUpdateInvalidID(int id)
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];
            Guest guest = guestRepo.ReadAll()[0];
            Reservation toUpdate = new Reservation
            {
                StartDate = new DateTime(2021, 12, 28),
                EndDate = new DateTime(2021, 12, 29),
                Guest = guest,
                Host = host,
                ID = id
            };
            toUpdate.SetTotal();

            Reservation copy = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = hostRepo.ReadAll()[0],
                Guest = guestRepo.ReadAll()[0]
            };
            copy.SetTotal();
            copy.ID = 1;


            var result = service.Update(id, toUpdate);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.Messages[0].Contains("ID"));
            Assert.AreEqual(copy, service.ViewByHost(hostRepo.ReadAll()[0]).Value[0]);
            Assert.AreEqual(1, service.ViewByHost(hostRepo.ReadAll()[0]).Value.Count);

        }

        [Test]
        public void UpdateShouldAllowOverlapDatesOnResBeingUpdated()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];
            Guest guest = guestRepo.ReadAll()[0];

            Reservation toUpdate = new Reservation
            {
                StartDate = new DateTime(2022, 1, 2),
                EndDate = new DateTime(2022, 1, 9),
                Host = hostRepo.ReadAll()[0],
                Guest = guestRepo.ReadAll()[0]
            };
            toUpdate.SetTotal();
            toUpdate.ID = 1;

            var result = service.Update(1, toUpdate);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(toUpdate, result.Value);
            Assert.AreEqual(1, resRepo.ReadByHost(host).Count);
        }

        [Test]
        public void DeleteShouldDelete()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];

            Reservation toDelete = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = host,
                ID = 1
            };

            Reservation copy = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = hostRepo.ReadAll()[0],
                Guest = guestRepo.ReadAll()[0]
            };
            copy.SetTotal();
            copy.ID = 1;

            var result = service.Delete(toDelete);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(0,service.ViewByHost(host).Value.Count);
            Assert.AreEqual(copy, result.Value);
        }

        [Test]
        public void DeleteShouldNotRunForNotFoundID()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];

            Reservation toDelete = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = host,
                ID = 2
            };

            Reservation copy = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = hostRepo.ReadAll()[0],
                Guest = guestRepo.ReadAll()[0]
            };
            copy.SetTotal();
            copy.ID = 1;

            var result = service.Delete(toDelete);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.Messages[0].Contains("ID"));
            Assert.AreEqual(1, service.ViewByHost(host).Value.Count);
            Assert.AreEqual(copy, service.ViewByHost(host).Value[0]);
        }
        [Test]
        public void DeleteShouldNotRunWithNullHost()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = null;

            Reservation toDelete = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = host,
                ID = 1
            };

            Reservation copy = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = hostRepo.ReadAll()[0],
                Guest = guestRepo.ReadAll()[0]
            };
            copy.SetTotal();
            copy.ID = 1;

            var result = service.Delete(toDelete);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.Messages[0].Contains("Host"));

        }

        [Test]
        public void DeleteShouldNotWorkForPast()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            ReservationService service = new ReservationService(resRepo, guestRepo, hostRepo);

            Host host = hostRepo.ReadAll()[0];

            Reservation toDelete = new Reservation
            {
                StartDate = new DateTime(2020, 1, 1),
                EndDate = new DateTime(2020, 1, 8),
                Host = host,
                ID = 1

            };

            Reservation copy = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = hostRepo.ReadAll()[0],
                Guest = guestRepo.ReadAll()[0]
            };
            copy.SetTotal();
            copy.ID = 1;

            var result = service.Delete(toDelete);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Value);
            Assert.IsTrue(result.Messages[0].Contains("past"));
            Assert.AreEqual(1, service.ViewByHost(host).Value.Count);
            Assert.AreEqual(copy, service.ViewByHost(host).Value[0]);
        }
   }         
}
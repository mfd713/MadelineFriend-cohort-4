using NUnit.Framework;
using DWMH.Core;
using DWMH.Core.Repos;
using TestsDAL.TestDoubles;
using System.Collections.Generic;
using System;

namespace TestsDAL
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnListsOfDTOs()
        {
            //arrange
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            Host host = new Host
            {
                LastName = "Testy1",
                ID = "abc-123",
                Email = "test1@gmail.com",
                City = "Chicago",
                State = "IL",
            };
            host.SetRates(50M, 80M);

            Guest guest = new Guest {

                LastName = "Testington",
                FirstName = "Tesla",
                Email = "tt@gmail.com",
                ID = 1
            };

            Reservation reservation = new Reservation
            {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 1, 8),
                Host = host,
                Guest = guest
            };
            reservation.SetTotal();
            reservation.ID = 1;

            //act
            List<Host> hostResult = hostRepo.ReadAll();
            List<Guest> guestResult = guestRepo.ReadAll();
            List<Reservation> resResult = resRepo.ReadByHost(hostResult[0]);

            //assert
            //host list has length 1, host in it == the expected one
            Assert.AreEqual(1, hostResult.Count);
            Assert.AreEqual(host, hostResult[0]);
            //guest list has length 1, guest in it == expected one
            Assert.AreEqual(1, guestResult.Count);
            Assert.AreEqual(guest, guestResult[0]);
            //reservation list has length 1, reservation in it == expected one
            Assert.AreEqual(1, resResult.Count);
            Assert.AreEqual(410M, resResult[0].Total);
            Assert.AreEqual(reservation, resResult[0]);
        }

        [Test]
        public void ShouldReturnNothingWhenNoReservationsForHost()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            Host host = new Host
            {
                Email = "none@no.com"
            };

            List<Reservation> resResult = resRepo.ReadByHost(host);

            Assert.AreEqual(0, resResult.Count);
        }

        [Test]
        public void ShouldAddReservationToList()
        {
            IReservationRepository resRepo = new ReservationRepoDouble();
            IHostRepository hostRepo = new HostRepoDouble();
            IGuestRepository guestRepo = new GuestRepoDouble();

            Reservation toAdd = new Reservation{
                StartDate = new DateTime(2022, 02, 02), 
                EndDate = new DateTime(2022, 02, 05),
                Host = hostRepo.ReadAll()[0], 
                Guest = guestRepo.ReadAll()[0] };
            

            Reservation result = resRepo.Create(toAdd);
            

            Assert.AreEqual(2, resRepo.ReadByHost(hostRepo.ReadAll()[0]).Count);
            Assert.AreEqual(result, resRepo.ReadByHost(hostRepo.ReadAll()[0])[1]);
        }
    }
}
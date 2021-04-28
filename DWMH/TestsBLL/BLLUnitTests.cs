using NUnit.Framework;
using DWMH.Core;
using DWMH.Core.Repos;
using TestsDAL.TestDoubles;
using DWMH.BLL;
using System.Collections.Generic;

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
            Assert.AreEqual(0,result.Value.Count);
        }
    }
}
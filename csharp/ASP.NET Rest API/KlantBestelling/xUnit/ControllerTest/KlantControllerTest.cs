using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BusinessLayer.Exceptions;
using BusinessLayer.Manager;
using BusinessLayer.Model;
using BusinessLayer.Repositories;
using DataLayer;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using RestAPI.Controller;
using RestAPI.JsonResponse;
using RestAPI.Model;
using Xunit;

namespace XUnitKlantBestellingService.ControllerTest
{
    public class KlantControllerTest
    {
        private readonly Mock<IUnitOfWork> mockRepo;
        private readonly KlantController klantController;


        public KlantControllerTest()
        {
            mockRepo = new Mock<IUnitOfWork>();
            klantController = new KlantController(mockRepo.Object);
        }


        [Fact]
        public void GET_UnknownID_ReturnsNotFound()
        {
            mockRepo.Setup(repo => repo.KlantRepository.ZoekKlantMetId(8)).Throws(new KlantException("Klant bestaat niet"));
            var result = klantController.GetKlant(8);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public void GET_CorectID_ReturnsKlant()
        {
            mockRepo.Setup(repo => repo.KlantRepository.ZoekKlantMetId(1)).Returns(new Klant(1, "Homer Simpson", "Evergreen Terrace 742"));
            var result = klantController.GetKlant(1).Result as OkObjectResult;
            Assert.IsType<KlantJSON>(result.Value);
        }


    }
}
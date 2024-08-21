using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Exceptions;
using BusinessLayer.Manager;
using BusinessLayer.Model;
using BusinessLayer.Repositories;
using DataLayer;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestAPI.Controller;
using RestAPI.JsonResponse;
using RestAPI.Model;
using Xunit;
using Xunit.Sdk;

namespace XUnitKlantBestellingService.ControllerTest
{
    public class BestelControllerTest
    {
        private readonly Mock<IUnitOfWork> mockRepo;
        private readonly BestelController bestelController;
        

        public BestelControllerTest()
        {
            mockRepo = new Mock<IUnitOfWork>();
            bestelController = new BestelController(mockRepo.Object);
        }



    }

}

using AutoMapper;
using Jokenpo.Dto;
using Jokenpo.Mapper;
using Jokenpo.Models;
using Jokenpo.Repositories.Interface;
using Jokenpo.Services;
using Jokenpo.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JokenpoTest.Services
{
    class PlayerServiceTest
    {
        private Mock<IRepositoryPlayer> _repositoryPlayer;
        private IPlayerService _servicePlayer;

        [SetUp]
        public void SetUo()
        {
            _repositoryPlayer = new Mock<IRepositoryPlayer>();
            var mapConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperDto()));
            _servicePlayer = new PlayerService(_repositoryPlayer.Object, mapConfig.CreateMapper());
        }

        public void AddPlayerTest()
        {
            var player = new PlayerDto
            {
                Name = "Test",
            };
           
        }
    }
}

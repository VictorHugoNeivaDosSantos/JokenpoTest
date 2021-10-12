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

        [Test]
        public void AddPlayerTest()
        {
            var playerDto = new PlayerDto
            {
                Name = "Test",
                Email = "teste"
            };

            Assert.DoesNotThrow(() => _servicePlayer.AddPlayer(playerDto));
        }

        [Test]
        public void AddPlayerTest_NotEmail()
        {
            var playerDto = new PlayerDto
            {
                Name = "Test",
 
            };
          

            Assert.Throws<Exception>(() => _servicePlayer.AddPlayer(playerDto));
        }

        [Test]
        public void GetPlayerByIdTest()
        {
            var player = new Player
            {
                Id = Guid.Parse("50d72022-4ae8-4ee0-96ff-7470fc4719bf"),
                Email = "teste",
                Name = "teste",
                Status = Jokenpo.Enuns.StatusPlayer.Ativo
            };

            var list = new List<Player>();
            _repositoryPlayer.Setup(s => s.GetPlayerById(player.Id)).Returns(player);
           
            Assert.AreEqual("teste", _servicePlayer.GetPlayerById(player.Id).Name);
        }


    }
}

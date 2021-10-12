using AutoMapper;
using Jokenpo.Context;
using Jokenpo.Mapper;
using Jokenpo.Models;
using Jokenpo.Repositories;
using Jokenpo.Repositories.Interface;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace JokenpoTest
{
    public class RepositoryPlayerTest
    {
        private Mock<IJokenpoContext> _context;
        private IRepositoryPlayer _repository;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<IJokenpoContext>();
            var mapConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperDto()));
            _repository = new RepositoryPlayer(_context.Object, mapConfig.CreateMapper());
        }

        [Test]
        public void AddPlayerTest_IsSucess()
        {
            var player = new Player
            {
                Name = "teste",
                Id = Guid.NewGuid(),
                Email = "teste@teste.com",
                Status = Jokenpo.Enuns.StatusPlayer.Ativo
            };

            var list = new List<Player>();
            _context.Setup(s => s.PlayersList()).Returns(list);

            Assert.DoesNotThrow(() => _repository.AddPlayer(player));
        }

        [Test]
        public void GetPlayerByIdTest_IsSucess()
        {
            var player = new Player
            {
                Name = "teste",
                Id = Guid.NewGuid(),
                Email = "teste@teste.com",
                Status = Jokenpo.Enuns.StatusPlayer.Ativo
            };

            var list = new List<Player>();
            _context.Setup(s => s.PlayersList()).Returns(list);
            var idPlayer = _repository.AddPlayer(player);

            Assert.NotNull(_repository.GetPlayerById(idPlayer));
        }
    }
}
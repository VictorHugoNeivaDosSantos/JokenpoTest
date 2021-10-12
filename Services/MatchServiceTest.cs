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
    class MatchServiceTest
    {
        private Mock<IPlayerService> _playerService;
        private Mock<IRepositoryMatch> _repositoryMatch;
        private IMatchService _serviceMatch;

        [SetUp]
        public void SetUp()
        {
            _repositoryMatch = new Mock<IRepositoryMatch>();
            _playerService = new Mock<IPlayerService>();
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperDto()));
            _serviceMatch = new MatchService(_repositoryMatch.Object, mapperConfig.CreateMapper(), _playerService.Object);
        }

        [Test]
        public void AddMoveInMatch_IsSucessFull()
        {
            var playerDto = new PlayerDto
            {
                Email = "teste",
                Name = "teste"
            };
            
            var move = new MoveDto
            {
                JogadorId = playerDto.PlayerId,
                PlayPay = Jokenpo.Enuns.GameParts.Pedra
            };

            var player = new Player
            {
                Status = Jokenpo.Enuns.StatusPlayer.Ativo
            };

            _playerService.Setup(s => s.AddPlayer(playerDto))
                .Returns(playerDto.PlayerId);
            _playerService.Setup(s => s.GetPlayerById(playerDto.PlayerId))
                .Returns(player);

            Assert.DoesNotThrow(() => _serviceMatch.AddMoveInMatch(move));
        }

        [Test]
        public void AddMoveInMatch_NotPlayer()
        {
            var expected = new Exception("Jogador não encontrado.");
            var playerDto = new PlayerDto
            {
                Email = "teste",
                Name = "teste"
            };

            var move = new MoveDto
            {
                JogadorId = Guid.NewGuid(),
                PlayPay = Jokenpo.Enuns.GameParts.Pedra
            };

            var player = new Player
            {
                Status = Jokenpo.Enuns.StatusPlayer.Ativo
            };

            _playerService.Setup(s => s.GetPlayerById(playerDto.PlayerId))
                 .Returns(player);

           Assert.Throws<Exception>(() => _serviceMatch.AddMoveInMatch(move));
        }

        [Test]
        public void AddMoveInMatch_PlayerDeactivated()
        {
            var playerDto = new PlayerDto
            {
                Email = "teste",
                Name = "teste"
            };

            var move = new MoveDto
            {
                JogadorId = playerDto.PlayerId,
                PlayPay = Jokenpo.Enuns.GameParts.Pedra
            };

            var player = new Player
            {
                Status = Jokenpo.Enuns.StatusPlayer.Desativo
            };

            _playerService.Setup(s => s.AddPlayer(playerDto))
                .Returns(playerDto.PlayerId);
            _playerService.Setup(s => s.GetPlayerById(playerDto.PlayerId))
                .Returns(player);

            Assert.Throws<Exception>(() => _serviceMatch.AddMoveInMatch(move));
        }
    }
    
}

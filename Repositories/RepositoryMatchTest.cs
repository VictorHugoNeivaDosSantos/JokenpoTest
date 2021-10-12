using Jokenpo.Context;
using Jokenpo.Repositories;
using Jokenpo.Repositories.Interface;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JokenpoTest.Repositories
{
    class RepositoryMatchTest
    {
        private Mock<IJokenpoContext> _context;
        private IRepositoryMatch _repository;

        [SetUp]
        public void SetUp()
        {
            _context = new Mock<IJokenpoContext>();
            _repository = new RepositoryMatch(_context.Object);
        }

        [Test]
        public void AddMatchTest_IsSucess()
        {
            var match = new Jokenpo.Models.Match
            {
                Id = Guid.NewGuid(),
                Moves = new List<Jokenpo.Models.Move>(),
                Status = Jokenpo.Enuns.StatusMatch.Aberta
            };
            _context.Setup(s => s.MatchList()).Returns(new List<Jokenpo.Models.Match>());

            var idMatch = _repository.AddMatch(match);

            Assert.NotNull(_repository.GetMatchById(idMatch));
        }

        [Test]
        public void GetOpenMatchTest()
        {

            var matchOpen = new Jokenpo.Models.Match
            {
                Id = Guid.NewGuid(),
                Moves = new List<Jokenpo.Models.Move>(),
                Status = Jokenpo.Enuns.StatusMatch.Aberta
            };
            var matchClosed = new Jokenpo.Models.Match
            {
                Id = Guid.NewGuid(),
                Moves = new List<Jokenpo.Models.Move>(),
                Status = Jokenpo.Enuns.StatusMatch.Fechada
            };

            _context.Setup(s => s.MatchList()).Returns(new List<Jokenpo.Models.Match>());
            var expected = _repository.AddMatch(matchOpen);        
            _repository.AddMatch(matchClosed);

            Assert.AreEqual(expected, _repository.GetOpenMatch().Id);
        }

        [Test]
        public void GetMatchByIdTest_IsSucess()
        {
            var match = new Jokenpo.Models.Match
            {
                Id = Guid.NewGuid(),
                Moves = new List<Jokenpo.Models.Move>(),
                Status = Jokenpo.Enuns.StatusMatch.Fechada
            };
            _context.Setup(s => s.MatchList()).Returns(new List<Jokenpo.Models.Match>());
            var idMatch = _repository.AddMatch(match);

            Assert.NotNull(_repository.GetMatchById(idMatch));
        }
    }
}

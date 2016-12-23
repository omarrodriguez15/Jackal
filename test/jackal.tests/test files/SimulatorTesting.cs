using System;
using Xunit;
using Jackal;
namespace Tests
{
    public class SimulatorTesting
    {
        private int _numOfDecks = 4, _numOfPlayers = 2, _baseBet = 10, _betAmt = 10, _numOfRounds = 10;

        public SimulatorTesting()
        {
            CustomLogger.ConsoleOut = false;
        }

        [Fact]
        public void SimulatorShouldInitializeProperly() 
        {
            BlackJackSim blackJack = new BlackJackSim(_numOfDecks, _numOfPlayers, _baseBet, _betAmt);
            blackJack.SimulateRounds(_numOfRounds);
            Assert.NotNull(blackJack.mainDeck);
        }

        [Fact]
        public void CardDeckShouldBeFull() 
        {
            BlackJackSim blackJack = new BlackJackSim(_numOfDecks, _numOfPlayers);
            Assert.Equal(blackJack.mainDeck.cards.Count, _numOfDecks * 52);
        }
    }
}

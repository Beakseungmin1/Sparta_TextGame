using System.Collections.Generic;

namespace BlackJack
{
    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }


    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }

        public Card(Suit s, Rank r)
        {
            Suit = s;
            Rank = r;
        }

        public int GetValue()
        {
            if ((int)Rank <= 10)
            {
                return (int)Rank;
            }
            else if ((int)Rank <= 13)
            {
                return 10;
            }
            else
            {
                return 11;
            }
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }

    // 덱을 표현하는 클래스
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(s, r));
                }
            }

            Shuffle();
        }

        public void Shuffle()
        {
            Random rand = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int j = rand.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public Card DrawCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }

    // 패를 표현하는 클래스
    public class Hand
    {
        private List<Card> cards;

        public Hand()
        {
            cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int GetTotalValue()
        {
            int total = 0;
            int aceCount = 0;

            foreach (Card card in cards)
            {
                if (card.Rank == Rank.Ace)
                {
                    aceCount++;
                }
                total += card.GetValue();
            }

            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }
    }

    // 플레이어를 표현하는 클래스
    public class Player
    {
        public Hand Hand { get; private set; }

        public Player()
        {
            Hand = new Hand();
        }

        public Card DrawCardFromDeck(Deck deck)
        {
            Card drawnCard = deck.DrawCard();
            Hand.AddCard(drawnCard);
            return drawnCard;
        }
    }

    // 여기부터는 학습자가 작성
    // 딜러 클래스를 작성하고, 딜러의 행동 로직을 구현하세요.
    public class Dealer : Player
    {
        public Hand Hand { get; private set; }

        public Dealer()
        {
            Hand = new Hand();
        }

        public Card DrawCardFromDeck(Deck deck)
        {
            Card drawnCard = deck.DrawCard();
            Hand.AddCard(drawnCard);
            return drawnCard;
        }
    }

    // 블랙잭 게임을 구현하세요. 
    public class Blackjack
    {
        
        // 코드를 여기에 작성하세요
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Card> playerCards = new List<Card>();
            List<Card> dealerCards = new List<Card>();

            Deck deck = new Deck();
            Player player = new Player();
            Dealer dealer = new Dealer();
            bool turn = true;
            bool gameEnd = false;
            int choice;

            Card playerCard = player.DrawCardFromDeck(deck);
            playerCards.Add(playerCard);
            playerCard = player.DrawCardFromDeck(deck);
            playerCards.Add(playerCard);
            Card dealerCard = dealer.DrawCardFromDeck(deck);
            dealerCards.Add(dealerCard);
            dealerCard = dealer.DrawCardFromDeck(deck);
            dealerCards.Add(dealerCard);


            Console.SetCursorPosition(5, 0);
            Console.WriteLine("딜러의 패");
            Console.Write(dealerCards[0].ToString() + ",");
            Console.Write("?");


            Console.SetCursorPosition(5, 5);
            Console.WriteLine("당신의 패");
            for (int i = 0; i < playerCards.Count; i++)
            {
                Console.Write(playerCards[i] + ",");
            }
            Console.WriteLine(" 총합 :  " + player.Hand.GetTotalValue());

            if (!gameEnd)
            {
                if (turn)
                {
                    Console.WriteLine("카드를 새로 받으시겠습니까? (1.예 2.아니요)");
                    choice = int.Parse(Console.ReadLine());
                    while (turn)
                    {
                        if (choice == 1)
                        {
                            playerCard = player.DrawCardFromDeck(deck);
                            playerCards.Add(playerCard);
                            if (player.Hand.GetTotalValue() > 21)
                            {
                                turn = false;
                                gameEnd = true;
                            }
                            Console.WriteLine("플레이어의 카드");
                            for (int i = 0; i < playerCards.Count; i++)
                            {
                                Console.Write(playerCards[i] + ",");
                            }
                            Console.WriteLine(" 총합 :  " + player.Hand.GetTotalValue());
                            Console.WriteLine();
                            Console.WriteLine("카드를 새로 받으시겠습니까? (1.예 2.아니요)");
                            choice = int.Parse(Console.ReadLine());

                        }
                        else if (choice == 2)
                        {
                            Console.WriteLine("딜러에게 턴을 넘깁니다");
                            turn = false;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 숫자를 입력하셨습니다");
                            Console.WriteLine("카드를 새로 받으시겠습니까? (1.예 2.아니요)");
                            choice = int.Parse(Console.ReadLine());
                        }
                    }

                }
                if (!turn)
                {
                    Console.WriteLine("딜러의 턴입니다");
                    while (dealer.Hand.GetTotalValue() <= 17)
                    {
                        dealerCard = dealer.DrawCardFromDeck(deck);
                        dealerCards.Add(dealerCard);
                    }
                    gameEnd = true;
                }
            }
            if (gameEnd)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("딜러의 카드");
                for (int i = 0; i < dealerCards.Count; i++)
                {
                    Console.Write(dealerCards[i]+ ",");
                }
                Console.WriteLine(" 총합 :  " + dealer.Hand.GetTotalValue());

                Console.SetCursorPosition(0, 5);
                Console.WriteLine("플레이어의 카드");
                for (int i = 0; i < playerCards.Count; i++)
                {
                    Console.Write(playerCards[i] + ",");
                }
                Console.WriteLine(" 총합 :  " + player.Hand.GetTotalValue());

                if (dealer.Hand.GetTotalValue() <= player.Hand.GetTotalValue() || dealer.Hand.GetTotalValue() > 21)
                {
                    Console.WriteLine("플레이어 승리");
                }
                if (dealer.Hand.GetTotalValue() > player.Hand.GetTotalValue() || player.Hand.GetTotalValue() > 21)
                {
                    Console.WriteLine("딜러 승리");
                }
            }
            // 블랙잭 게임을 실행하세요
        }
    }
}

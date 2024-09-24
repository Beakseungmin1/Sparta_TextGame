using System.Text;
using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace TEXT_RPG
{
    internal class Program
    {
        public enum ItemType
        {
            Equipment,
            Consumable
        }

        interface ICharacter
        {
            string Name { get; set; }
            int Health { get; set; }

            int Attack { get; set; }

            int Defense { get; set; }
            bool IsDead { get; set; }

            public int TakeDamage(int damage);
        }

        interface IItem
        {
            ItemType Type { get; set; }
            string Name { get; set; }
            int Price { get; set; }
            string ValueInformation { get; set; }
            string Information { get; set; }
            bool BuyHistory { get; set; }

            public void Use(Warrior warrior);

        }

        class Warrior : ICharacter
        {
            public int Level { get; set; }
            public int Gold { get; set; }
            string Job { get; set; }
            public string Name { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }
            public int Defense { get; set; }
            public bool IsDead { get; set; }


            public int ExtraAttack { get; set; }

            public int ExtraDefense { get; set; }



            public Inventory Inventory { get; private set; }

            public Warrior(int lev, string name, string job, int attack, int defense, int health, int gold)
            {
                Level = lev;
                Name = name;
                Job = job;
                Attack = attack;
                Defense = defense;
                Health = health;
                Gold = gold;
                Inventory = new Inventory(this);
            }
            public int TakeDamage(int damage)
            {
                int takeDamage = damage - Defense;
                Health -= takeDamage;
                return Health;
            }

            public void ShowStats()
            {
                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine("LV. {0}", Level < 10 ? "0" + Level : Level);
                Console.WriteLine("{0} ( {1} )", Name, Job);
                if (ExtraAttack != null && ExtraAttack > 0)
                {
                    Console.WriteLine("공격력 : {0} (+{1})", Attack, ExtraAttack);
                }
                else
                {
                    Console.WriteLine("공격력 : {0}", Attack);
                }
                if (ExtraDefense != null && ExtraDefense > 0)
                {
                    Console.WriteLine("방어력 : {0} (+{1})", Defense, ExtraDefense);
                }
                else
                {
                    Console.WriteLine("방어력 : {0}", Defense);
                }
                Console.WriteLine("체력 : {0}", Health);
                Console.WriteLine("Gold : {0} G", Gold);
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.Write(">>");

                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                        ShowStats();
                        break;
                }
            }

            public void DIE()
            {
                Console.WriteLine("캐릭터가 사망하였습니다");
            }

        }

        class Inventory
        {
            List<IItem> Items;
            private Warrior Setwarrior;


            public Inventory(Warrior warrior)
            {
                Items = new List<IItem>();
                Setwarrior = warrior;
            }

            public void AddItem(IItem item)
            {
                Items.Add(item);
                Console.WriteLine("{0}을(를) 획득하였습니다", item.Name);
            }

            public void ShowInventory()
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.Write(">>");

                int choice = int.Parse(Console.ReadLine());
                if (choice == 0)
                {
                    Console.Clear();
                    return;
                }
                else if (choice == 1)
                {
                    Console.Clear();
                    ShowEquipManagement();
                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다. 올바른 선택지를 골라주세요");
                }

                void ShowEquipManagement()
                {
                    int n = 1;
                    Console.Clear();
                    Console.WriteLine("인벤토리 - 장착 관리");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");
                    foreach (var item in Items)
                    {
                        string equippedMarker = "";

                        if (item is EquipItem equipItem && equipItem.EquipNow)
                        {
                            equippedMarker = "[E]";
                        }
                        Console.WriteLine("- {0} {1}{2}  | {3} | {4}", n, equippedMarker, item.Name, item.ValueInformation, item.Information);
                        n++;
                    }
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("장착을 원하시는 아이템의 숫자를 입력하세요");
                    Console.Write(">>");

                    choice = int.Parse(Console.ReadLine());
                    if (choice == 0)
                    {
                        ShowInventory();
                    }
                    else if (choice > 0 && choice <= Items.Count)
                    {
                        var selectedItem = Items[choice - 1];

                        selectedItem.Use(Setwarrior);
                        ShowEquipManagement();
                    }
                }
            }
        }


        class Monster : ICharacter
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }

            public int Defense { get; set; }
            public bool IsDead { get; set; }

            public int TakeDamage(int damage)
            {
                int takeDamage = damage - Defense;
                Health -= takeDamage;
                return Health;
            }
        }

        class Goblin : Monster
        {
            public Goblin()
            {
                Name = "Goblin";
                Health = 30;
                Attack = 5;
                Defense = 1;
            }
        }

        class Dragon : Monster
        {
            public Dragon()
            {
                Name = "Dragon";
                Health = 100;
                Attack = 20;
                Defense = 5;
            }
        }

        class Potion : IItem
        {
            public ItemType Type { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public string ValueInformation { get; set; }
            public string Information { get; set; }
            public bool BuyHistory { get; set; }

            public virtual void Use(Warrior warrior)
            {

            }
        }

        class HealthPotion : Potion
        {
            public int Healvalue { get; }

            public HealthPotion()
            {
                Type = ItemType.Consumable;
                Name = "체력 포션";
                Price = 300;
                ValueInformation = "현재체력 +30";
                Information = "먹으면 체력을 회복시켜준다";
                Healvalue = 30;
            }

            public override void Use(Warrior warrior)
            {
                warrior.Health += Healvalue;
            }
        }

        class StrengthPotion : Potion
        {
            public int StrUpValue { get; }

            public StrengthPotion()
            {
                Type = ItemType.Consumable;
                Name = "힘 포션";
                Price = 300;
                ValueInformation = "공격력 +3";
                Information = "먹으면 힘을 증가시켜준다";
                StrUpValue = 3;
            }

            public override void Use(Warrior warrior)
            {
                warrior.Attack += StrUpValue;
            }
        }

        class EquipItem : IItem
        {
            public ItemType Type { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public int Value { get; set; }
            public string ValueInformation { get; set; }
            public string Information { get; set; }

            private bool equipNow = false;
            public bool BuyHistory { get; set; }

            public bool EquipNow
            {
                get { return equipNow; }
                set { equipNow = value; }
            }

            public virtual void Use(Warrior warrior)
            {
                if (!EquipNow)
                {
                    EquipNow = true;
                }
                else
                {
                    EquipNow = false;
                }
            }
        }

        class ApprenticeArmor : EquipItem
        {
            public ApprenticeArmor()
            {
                Type = ItemType.Equipment;
                Name = "수련자 갑옷";
                Price = 1000;
                Value = 5;
                ValueInformation = "방어력 +5";
                Information = "수련에 도움을 주는 갑옷입니다.";
                BuyHistory = false;
            }

            public override void Use(Warrior warrior)
            {
                if (!EquipNow)
                {
                    EquipNow = true;
                    warrior.ExtraDefense += Value;
                }
                else
                {
                    EquipNow = false;
                    warrior.ExtraDefense -= Value;
                }
            }
        }
        class IronArmor : EquipItem
        {
            public IronArmor()
            {
                Type = ItemType.Equipment;
                Name = "무쇠갑옷";
                Price = 2000;
                Value = 9;
                ValueInformation = "방어력 +9";
                Information = "무쇠로 만들어져 튼튼한 갑옷입니다.";
                BuyHistory = false;
            }

            public override void Use(Warrior warrior)
            {
                if (!EquipNow)
                {
                    EquipNow = true;
                    warrior.ExtraDefense += Value;
                }
                else
                {
                    EquipNow = false;
                    warrior.ExtraDefense -= Value;
                }
            }
        }
        class SpartanArmor : EquipItem
        {
            public SpartanArmor()
            {
                Type = ItemType.Equipment;
                Name = "스파르타의 갑옷";
                Price = 3500;
                Value = 15;
                ValueInformation = "방어력 +15";
                Information = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.";
                BuyHistory = false;
            }

            public override void Use(Warrior warrior)
            {
                if (!EquipNow)
                {
                    EquipNow = true;
                    warrior.ExtraDefense += Value;
                }
                else
                {
                    EquipNow = false;
                    warrior.ExtraDefense -= Value;
                }
            }
        }
        class OldSword : EquipItem
        {
            public OldSword()
            {
                Type = ItemType.Equipment;
                Name = "낡은 검";
                Price = 600;
                Value = 2;
                ValueInformation = "공격력 +2";
                Information = "쉽게 볼 수 있는 낡은 검 입니다.";
                BuyHistory = false;
            }

            public override void Use(Warrior warrior)
            {
                if (!EquipNow)
                {
                    EquipNow = true;
                    warrior.ExtraAttack += Value;
                }
                else
                {
                    EquipNow = false;
                    warrior.ExtraAttack -= Value;
                }
            }
        }
        class BronzeAxe : EquipItem
        {
            public BronzeAxe()
            {
                Type = ItemType.Equipment;
                Name = "청동 도끼";
                Price = 1500;
                Value = 5;
                ValueInformation = "공격력 +5";
                Information = "어디선가 사용됐던거 같은 도끼입니다.";
                BuyHistory = false;
            }

            public override void Use(Warrior warrior)
            {
                if (!EquipNow)
                {
                    EquipNow = true;
                    warrior.ExtraAttack += Value;
                }
                else
                {
                    EquipNow = false;
                    warrior.ExtraAttack -= Value;
                }
            }
        }
        class SpartanSpear : EquipItem
        {
            public SpartanSpear()
            {
                Type = ItemType.Equipment;
                Name = "스파르타의 창";
                Price = 2200;
                Value = 7;
                ValueInformation = "공격력 +7";
                Information = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
                BuyHistory = false;
            }

            public override void Use(Warrior warrior)
            {
                if (!EquipNow)
                {
                    EquipNow = true;
                    warrior.ExtraAttack += Value;
                }
                else
                {
                    EquipNow = false;
                    warrior.ExtraAttack -= Value;
                }
            }
        }


        class Shop
        {
            List<IItem> SellingItems;
            private Warrior Setwarrior;


            public Shop(Warrior warrior)
            {
                SellingItems = new List<IItem>();
                Setwarrior = warrior;
            }

            public void AddItem(IItem item)
            {
                SellingItems.Add(item);
            }

            public void BuyItem(IItem item)
            {
                Setwarrior.Inventory.AddItem(item);
            }

            public void ShowShopList()
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine();
                Console.WriteLine("{0} G", Setwarrior.Gold);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                foreach (var item in SellingItems)
                {
                    if (item.BuyHistory)
                    {
                        Console.WriteLine("- {0}  | {1} | {2}\t | 구매완료", item.Name, item.ValueInformation, item.Information);
                    }
                    else
                    {
                        Console.WriteLine("- {0}  | {1} | {2}\t | {3} G", item.Name, item.ValueInformation, item.Information, item.Price);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.Write(">>");

                int choice = int.Parse(Console.ReadLine());
                if (choice == 0)
                {
                    Console.Clear();
                    return;
                }
                else if (choice == 1)
                {
                    int n = 1;
                    Console.Clear();
                    Console.WriteLine("상점");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine();
                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine();
                    Console.WriteLine("{0} G", Setwarrior.Gold);
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");
                    foreach (var item in SellingItems)
                    {
                        if (item.BuyHistory)
                        {
                            Console.WriteLine("- {0} {1}  | {2} | {3}\t | 구매완료", n, item.Name, item.ValueInformation, item.Information);
                        }
                        else
                        {
                            Console.WriteLine("- {0} {1}  | {2} | {3}\t | {4} G", n, item.Name, item.ValueInformation, item.Information, item.Price);
                        }
                        n++;
                    }
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    Console.WriteLine("구매를 원하시는 상품의 숫자를 입력하세요");
                    Console.Write(">>");

                    while (true)
                    {
                        choice = int.Parse(Console.ReadLine());
                        if (choice == 0)
                        {
                            Console.Clear();
                            ShowShopList();
                            return;
                        }
                        else if (choice > 0 && choice <= SellingItems.Count)
                        {
                            var selectedItem = SellingItems[choice - 1];

                            if (selectedItem.Price <= Setwarrior.Gold)
                            {
                                Console.WriteLine("구매를 완료하였습니다.");
                                BuyItem(selectedItem);
                                Setwarrior.Gold -= selectedItem.Price;
                            }
                            else
                            {
                                Console.WriteLine("Gold가 부족합니다");
                            }
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                }
            }

        }

        class GameManager
        {
            Shop Shop;
            Warrior Warrior;

            public GameManager(Warrior warrior, Shop shop)
            {
                Warrior = warrior;
                Shop = shop;
            }


            public void EnterGame()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                    Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                    Console.WriteLine();

                    Console.WriteLine("1. 상태 보기");
                    Console.WriteLine("2. 인벤토리");
                    Console.WriteLine("3. 상점");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");

                    int input = int.Parse(Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            Warrior.ShowStats();
                            break;
                        case 2:
                            Warrior.Inventory.ShowInventory();
                            break;
                        case 3:
                            Shop.ShowShopList();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                            break;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Warrior player = new Warrior(1, "Chad", "전사", 10, 5, 100, 1500);
            Shop shop = new Shop(player);
            GameManager gameManager = new GameManager(player, shop);

            ApprenticeArmor apprenticeArmor = new ApprenticeArmor();
            IronArmor ironArmor = new IronArmor();
            SpartanArmor spartan = new SpartanArmor();
            OldSword oldSword = new OldSword();
            BronzeAxe bronzeAxe = new BronzeAxe();
            SpartanSpear spartanSpear = new SpartanSpear();


            shop.AddItem(apprenticeArmor);
            shop.AddItem(ironArmor);
            shop.AddItem(spartan);
            shop.AddItem(oldSword);
            shop.AddItem(bronzeAxe);
            shop.AddItem(spartanSpear);

            gameManager.EnterGame();


        }
    }
}

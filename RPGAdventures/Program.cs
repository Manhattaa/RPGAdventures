using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to The Vale!");
        Console.Write("Choose your Hero ");
        Console.WriteLine("\n1. Margery the Magical ");
        Console.WriteLine("\n2. Arwen, Elven Archery expert ");
        Console.WriteLine("\n3. Ser Podrick Payne, A squire from Westeros ");
        int HeroChoice = int.Parse(Console.ReadLine());

        Hero protagonist;

        switch (HeroChoice)
        {
            case 1:
                protagonist = new Margery();
                break;
            case 2:
                protagonist = new Arwen();
                break;
            case 3:
                protagonist = new Podrick();
                break;
            default:
                Console.WriteLine("Invalid choice. Selecting Margery by default.");
                protagonist = new Margery();
                break;
        }

        Console.WriteLine($"You have chosen {protagonist.Name}!");

        // Initialize gold pieces
        int goldPieces = 100;

        // Add a boolean variable to track if the player is blessed
        bool isFavoured = false;
        int favouredRounds = 0;

        while (true)
        {
            Monsters foe = foegen.GenRandomMonster(protagonist.Level);
            Console.WriteLine($"\nA wild {foe.Name} appears!");

            while (protagonist.IsAlive && foe.IsAlive)
            {
                Console.WriteLine("\nChoose your action:");
                Console.WriteLine("1 - Attack");
                Console.WriteLine("2 - Retreat");

                // Display the chapel option in the menu
                Console.WriteLine("3 - Visit the Local Chapel (Pray for Blessing)");

                // Display the Inn option in the menu
                Console.WriteLine("4 - Visit the Inn");

                int action = int.Parse(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        protagonist.Attack(foe);
                        if (foe.IsAlive)
                        {
                            foe.Attack(protagonist);
                        }
                        break;
                    case 2:
                        Console.WriteLine("You managed to escape!");
                        break;
                    case 3:
                        // Visit the chapel to pray and get blessed
                        if (!isFavoured)
                        {
                            // Add logic to determine the success of the prayer
                            int praySuccess = new Random().Next(1, 101); // 1 to 100

                            if (praySuccess <= 30) // 30% chance of success
                            {
                                Console.WriteLine($"{protagonist.Name} has been blessed by the gods!");
                                protagonist.PotionEffect = 1.3; // 30% damage boost
                                isFavoured = true;
                                favouredRounds = 5; // Blessing lasts for 5 rounds
                            }
                            else
                            {
                                Console.WriteLine($"{protagonist.Name}'s prayer went unanswered.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{protagonist.Name} is already blessed. You can't pray again.");
                        }
                        break;
                    case 4:
                        // Visit the Inn
                        Console.WriteLine("You enter the Inn...");
                        Console.WriteLine("What would you like to do in the Inn?");
                        Console.WriteLine("1 - Buy a beer ");
                        Console.WriteLine("2 - Listen to the town's musician 'Aymaynem'");
                        Console.WriteLine("3 - Rent a bed (80 Gold Pieces)");
                        //Console.WriteLine("Hang out with the Town Drunk"); //update
                        Console.WriteLine("4 - Leave the Inn (return to the main menu)");

                        int InnChoice = int.Parse(Console.ReadLine());

                        switch (InnChoice)
                        {
                            case 1:
                                // Buy a beer
                                if (goldPieces >= 49)
                                {
                                    Console.WriteLine("You know the Barkeep, so you get an Erkan's Discount!");
                                    Console.WriteLine("You bought a beer for 49 Gold Pieces.");
                                    goldPieces -= 49;
                                }
                                else
                                {
                                    Console.WriteLine("You don't have enough Gold Pieces to buy a beer.");
                                }
                                break;
                            case 2:
                                // Display a random Eminem lyric as an old folk's tale
                                string eminemLyrics = GetRandomeminemLyrics();
                                Console.WriteLine($"Old Folk's Tale: {eminemLyrics}");
                                break;
                            case 3:
                                // Rent a bed
                                if (goldPieces >= 80)
                                {
                                    Console.WriteLine("You rented a bed for the night for 80 Gold Pieces.");
                                    goldPieces -= 80;
                                    Console.WriteLine("You feel well-rested!");
                                    protagonist.Reset(); // Reset Hero stats
                                }
                                else
                                {
                                    Console.WriteLine("You don't have enough Gold Pieces to rent a bed.");
                                }
                                break;
                            case 4:
                                // Leave the Inn (return to the main menu)
                                Console.WriteLine("You leave the Inn and return to the main menu.");
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please choose a valid option.");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid action. Please choose a valid option.");
                        break;
                }

                // Check if the player is blessed and decrement the rounds if necessary
                if (isFavoured)
                {
                    favouredRounds--;

                    // Remove the blessing if the rounds are over
                    if (favouredRounds <= 0)
                    {
                        Console.WriteLine($"{protagonist.Name} is no longer blessed!");
                        protagonist.PotionEffect = 1.0; // Reset the damage boost
                        isFavoured = false;
                    }
                }

                // Display remaining HP
                Console.WriteLine($"{protagonist.Name}'s HP: {protagonist.CurrentHealth}/{protagonist.MaxHealth}");
                Console.WriteLine($"{foe.Name}'s HP: {foe.CurrentHealth}/{foe.MaxHealth}");
            }

            if (!protagonist.IsAlive)
            {
                Console.WriteLine("You have been defeated!");
                Console.WriteLine("You blackout and wake up in a Inn.");
                protagonist.Reset(); // Reset Hero stats
                continue;
            }

            if (!foe.IsAlive)
            {
                Console.WriteLine($"You defeated {foe.Name}!");

                // Automatically heal to full health
                protagonist.CurrentHealth = protagonist.MaxHealth;

                protagonist.GetExperience(foe.Exp);

                // Lootdrop gen
                Lootbox loot = Lootbox.GenLoot(protagonist);
                Console.WriteLine($"You found {loot.Name}!");

                Console.WriteLine($"You gained {foe.Exp} EXP.");
                Console.WriteLine($"Current EXP: {protagonist.Exp}");

                // MAX LEVEL NO LIFE
                if (protagonist.LevelUp() && protagonist.Level == 100)
                {
                    Console.WriteLine("Congratulations! You've reached the maximum level, go touch grass!!");
                    break;
                }

                // dopamine kick
                if (protagonist.LevelUp())
                {
                    Console.WriteLine($"Congratulations! You've Advanced to level {protagonist.Level}!");
                }
            }
        }
    }

    static string[] eminemLyricss = //eminem lyrics
    {
        "His palms are sweaty, knees weak, arms are heavy",
        "Mom's spaghetti",
        "I'm friends with a Monsters that's under my bed",
        "Lose yourself in the music, the moment, you own it",
        "You better lose yourself in the music, the moment",
        "The soul's escaping through this hole that is gaping",
        "You own it, you better never let it go",
        "You only get one shot, do not miss your chance to blow",
        "This opportunity comes once in a lifetime, yo"
    };

    static string GetRandomeminemLyrics() //eminem lyrics
    {
        Random random = new Random();
        int index = random.Next(eminemLyricss.Length);
        return eminemLyricss[index];
    }
}
//class Bag FIX DOES NOT WORK
//{
//    private List<string> items;
//    private int capacity;

//    public Bag(int capacity)
//    {
//        this.capacity = capacity;
//        items = new List<string>();
//    }

//    public bool IsFull()
//    {
//        return items.Count >= capacity;
//    }

//    public bool AddItem(string item)
//    {
//        if (IsFull())
//        {
//            Console.WriteLine("Your Bag is full. You cannot pick up more items.");
//            return false;
//        }

//        items.Add(item);
//        Console.WriteLine($"You picked up {item} and placed it in your Bag.");
//        return true;
//    }
//}



class foegen
{
    private static readonly Random random = new Random();

    public static Monsters GenRandomMonster(int playerLvl)
    {
        // Define Monsters names and their associated difficulty levels
        string[] MonstersNames = { "Imp", "Leprechaun", "Ent", "Wight", "Wyvern", "The Night King" };
        string[] MonstersDifficulty = { "Baby", "Baby", "Moderate", "Moderate", "Tough", "Toughest" };

        // Determine the difficulty threshold based on player level
        int diffThresh = playerLvl / 10; // Adjust as needed

        // Select a random Monsters name within the specified difficulty threshold
        int randomIndex = random.Next(0, MonstersNames.Length);
        string selMonsterName = MonstersNames[randomIndex];
        string selectedDiff = MonstersDifficulty[randomIndex];

        // GOAL = "Blood Dragon" instead of just Dragon to signify stronger foe
        string MonstersFullName = selectedDiff + " " + selMonsterName;

        // Adjust Monsters stats based on difficulty (You can fine-tune these values)
        int maxHealth = random.Next(playerLvl * 8, playerLvl * 12); // maxhealth
        int Exp = maxHealth / 2;
        int attackPower = random.Next(playerLvl * 3, playerLvl * 6); // ***testing balancing

        return new Monsters
        {
            Name = MonstersFullName,
            MaxHealth = maxHealth,
            CurrentHealth = maxHealth,
            Exp = Exp,
            AttackPower = attackPower
        };
    }
}
class Hero
{
    public string Name { get; protected set; }
    public int Level { get; protected set; }
    public long Exp { get; protected set; }
    public int MaxHealth { get; protected set; }
    protected internal int CurrentHealth { get; set; }
    public bool IsAlive => CurrentHealth > 0;

    public int WeaponTier { get; protected set; }

    protected internal double PotionEffect { get; set; }

    public int BaseDamage { get; protected set; }


    protected Random random;

    // gold prop
    public int Gold { get; set; }

    public Hero()
    {
        random = new Random();
        Gold = 100; // Start with 100 gold pieces
    }

    public virtual void Attack(Monsters foe)
    {
        int minDamage = WeaponTier * 3;  // Minimum damage
        int maxDamage = (WeaponTier + 1) * 5;  // Maximum damage

        // Calculate damage within the specified range
        int damage = random.Next(minDamage, maxDamage + 1);

        foe.CurrentHealth -= damage;
        Console.WriteLine($"{Name} attacked {foe.Name} for {damage} damage!");
        GetExperience(damage * 50);
    }

    public void GetExperience(long experience)
    {
        Exp += experience;
    }

    public bool LevelUp()
    {
        if (Level < 100 && Exp >= ExpReqForLevel(Level + 1))
        {
            Level++;
            Console.WriteLine($"Congratulations! {Name} leveled up to level {Level}!");
            return true;
        }
        return false;
    }

    public virtual void GiveLoot(Lootbox loot)
    {
        //Loot effects
    }

    public void Reset()
    {
        CurrentHealth = MaxHealth;
        WeaponTier = 1;
        PotionEffect = 1.0;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Console.WriteLine($"{Name} took {damage} damage!");
    }

    public static long ExpReqForLevel(int level)
    {
        if (level >= 100)
        {
            return 100_000_000;
        }
        return (long)Math.Pow(level, 3) * 1_000;
    }
}
class Margery : Hero
{
    public Margery()
    {
        Name = "Margery";
        Level = 1;
        Exp = 0;
        MaxHealth = 10;
        CurrentHealth = MaxHealth;
        WeaponTier = 1;
        BaseDamage = 5;
    }

    public override void Attack(Monsters foe)
    {
        int damage = random.Next(WeaponTier * BaseDamage * 2, (WeaponTier + 1) * BaseDamage * 2);
        foe.CurrentHealth -= damage;
        Console.WriteLine($"{Name} attacked {foe.Name} for {damage} damage!");
        GetExperience(damage * 50);
    }
    public virtual void GiveLoot(Lootbox loot)
    {
        if (loot.Name == "Aether" && WeaponTier < 5)
        {
            Console.WriteLine("You found an Aether! Your magic power has been upgraded!");
            WeaponTier++;
        }
        else if (loot.Name == "Draft of Magic")
        {
            Console.WriteLine("You found a Draft of Magic! Your magic skill is temporarily boosted!");
            PotionEffect = 1.14; // 14% boost
        }
    }
}

class Arwen : Hero
{
    public Arwen()
    {
        Name = "Arwen";
        Level = 1;
        Exp = 0;
        MaxHealth = 10;
        CurrentHealth = MaxHealth;
        WeaponTier = 1;
        BaseDamage = 5;
    }

    public override void Attack(Monsters foe)
    {
        int damage = random.Next(WeaponTier * BaseDamage * 2, (WeaponTier + 1) * BaseDamage * 2);
        foe.CurrentHealth -= damage;
        Console.WriteLine($"{Name} attacked {foe.Name} for {damage} damage!");
        GetExperience(damage * 50);
    }
    public override void GiveLoot(Lootbox loot)
    {
        if (loot.Name == "Aether" && WeaponTier < 5)
        {
            Console.WriteLine("You found an Aether! Your archery power has been upgraded!");
            WeaponTier++;
        }
        else if (loot.Name == "Draft of the Bow Master")
        {
            Console.WriteLine("You found an Draft of the Bow Master! Your archery skill is temporarily boosted!");
            PotionEffect = 1.14;
        }
    }
}

class Podrick : Hero
{
    public Podrick()
    {
        Name = "Podrick";
        Level = 1;
        Exp = 0;
        MaxHealth = 10;
        CurrentHealth = MaxHealth;
        WeaponTier = 1;
        BaseDamage = 5;
    }

    public override void Attack(Monsters foe)
    {
        int damage = random.Next(WeaponTier * BaseDamage * 2, (WeaponTier + 1) * BaseDamage * 2);
        foe.CurrentHealth -= damage;
        Console.WriteLine($"{Name} attacked {foe.Name} for {damage} damage!");
        GetExperience(damage * 50);
    }

    public override void GiveLoot(Lootbox loot)
    {
        if (loot.Name == "Aether" && WeaponTier < 5)
        {
            Console.WriteLine("You found an Aether! Your swordsmanship has been upgraded!");
            WeaponTier++;
        }
        else if (loot.Name == "Draft of the Sword")
        {
            Console.WriteLine("You found a Draft of the Sword! Your swordsmanship skill is temporarily boosted!");
            PotionEffect = 1.14;
        }
    }
}
class Monsters
{
    private readonly Random random = new Random();

    public string Name { get; set; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int AttackPower { get; set; }
    public int Exp { get; set; }
    public bool IsAlive => CurrentHealth > 0;

    public void Attack(Hero Hero)
    {
        int damage = random.Next(AttackPower / 2, AttackPower);
        Hero.TakeDamage(damage);
        Console.WriteLine($"{Name} attacked {Hero.Name} for {damage} damage!");
    }
}
class Lootbox
{
    public string Name { get; set; }

    public static Lootbox GenLoot(Hero protagonist)
    {
        Random random = new Random();
        int lootType = random.Next(1, 21); // 1 in 20 chance for Aether (upgrade item)

        if (lootType == 20)
        {
            return new Lootbox { Name = "Aether" };
        }

        switch (protagonist)
        {
            case Margery _:
                return new Lootbox { Name = "Draft of Magic" };
            case Arwen _:
                return new Lootbox { Name = "Draft of the Bow Master" };
            case Podrick _:
                return new Lootbox { Name = "Draft of the Sword" };
            default:
                return new Lootbox { Name = "Bones" };
        }
    }
}
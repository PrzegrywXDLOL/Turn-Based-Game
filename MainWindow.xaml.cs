using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Turn_Based_Game
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        Player player1 = new Player(), player2 = new Player();
        int[,] characterStats = { {5, 5, 3, 2, 3},
                                  {7, 4, 2, 3, 2},
                                  {4, 3, 5, 1, 5}};
        BitmapImage[] Sprites = {new BitmapImage(new Uri("pack://application:,,,/Assets/Thief.png")),
                              new BitmapImage(new Uri("pack://application:,,,/Assets/Warrior.png")),
                              new BitmapImage(new Uri("pack://application:,,,/Assets/Wizard.png"))};
        BitmapImage[] Buffs = {new BitmapImage(new Uri("pack://application:,,,/Assets/AttackIcon.png")),
                              new BitmapImage(new Uri("pack://application:,,,/Assets/BlockIcon.png")),
                              new BitmapImage(new Uri("pack://application:,,,/Assets/HealIcon.png")),
                              new BitmapImage(new Uri("pack://application:,,,/Assets/Decline.png"))};
        int TurnCounter = 1;
        bool gameover = false;
        bool p1OK = false, p2OK = false;
        int player1Hero, player2Hero;
        int p1HP, p2HP;
        string p1choice, p2choice;
        private bool _p1Turn = true;
        
        public bool p1Turn
        {
            get { return _p1Turn; }
            set
            {
                if (_p1Turn != value)
                {
                    _p1Turn = value;
                    OnPropertyChanged(nameof(p1Turn));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindow()
        {
            do
            {
                PlayerCreate player1create = new PlayerCreate();
                player1create.ShowDialog();

                if (player1create.playerchoice < 3)
                {
                    player1.SetStats(player1create.playername, characterStats[player1create.playerchoice, 0], characterStats[player1create.playerchoice, 1],
                        characterStats[player1create.playerchoice, 2], characterStats[player1create.playerchoice, 3], characterStats[player1create.playerchoice, 4]);
                    player1Hero = player1create.playerchoice;
                    p1HP = player1.hp;
                    p1OK = true;
                }
                else
                    MessageBox.Show("Player 1 didn't choose character!");

            } while(!p1OK);

            do
            {
                PlayerCreate player2create = new PlayerCreate();
                player2create.ShowDialog();

                if (player2create.playerchoice < 3)
                {
                    player2.SetStats(player2create.playername, characterStats[player2create.playerchoice, 0], characterStats[player2create.playerchoice, 1],
                        characterStats[player2create.playerchoice, 2], characterStats[player2create.playerchoice, 3], characterStats[player2create.playerchoice, 4]);
                    player2Hero = player2create.playerchoice;
                    p2HP = player2.hp;
                    p2OK = true;
                }
                else
                    MessageBox.Show("Player 2 didn't choose character!");

            } while (!p2OK);

            int p1Heals = player1.healsAvailable;
            int p2Heals = player2.healsAvailable;
            int p1Blocks = player1.blocksAvailable;
            int p2Blocks = player2.blocksAvailable;

            InitializeComponent();

            DataContext = this;

            Player1Name.Text = player1.name;
            Player2Name.Text = player2.name;
            Player1HP.Value = player1.hp;
            Player2HP.Value = player2.hp;
            Player1HP.Maximum = player1.hp;
            Player2HP.Maximum = player2.hp;
            pl1Sprite.Source = Sprites[player1Hero];
            pl2Sprite.Source = Sprites[player2Hero];

            roundInfo.Text = "------------------Turn " + (TurnCounter++) + "-------------------";
        }

        private void TurnManager()
        {
            if (player1.hp <= 0)
            {
                roundInfo.Text += "\n" + player1.name + " has died!";
                roundInfo.Text += "\n" + player2.name + " wins!";
                gameover = true;
                AttackBtn.IsEnabled = false;
                BlockBtn.IsEnabled = false;
                HealBtn.IsEnabled = false;
            }
            else if (player2.hp <= 0)
            {
                roundInfo.Text += "\n" + player2.name + " has died!";
                roundInfo.Text += "\n" + player1.name + " wins!";
                gameover = true;
                AttackBtn.IsEnabled = false;
                BlockBtn.IsEnabled = false;
                HealBtn.IsEnabled = false;
            }
            else if (player1.hp <= 0 && player2.hp <= 0)
            {
                roundInfo.Text += "\nBoth players have died!";
                roundInfo.Text += "\nIt's a draw!";
                gameover = true;
                AttackBtn.IsEnabled = false;
                BlockBtn.IsEnabled = false;
                HealBtn.IsEnabled = false;
            }
            else
            {
                p1Turn = !p1Turn;
                roundInfo.Text += "\n------------------Turn " + (TurnCounter++) + "-------------------";
            }
        }
        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            if(p1Turn)
            {
                animation(0);
                roundInfo.Text += "\n" + player1.name + " will attack " + player2.name + " for " + player1.damage;
                p1choice = "Attack";
                p1Turn = !p1Turn;
            }
            else
            {
                animation(0);
                roundInfo.Text += "\n" + player2.name + " will attack " + player1.name + " for " + player2.damage;
                p2choice = "Attack";
                MoveCheck();
            }
        }

        private void Block_Click(object sender, RoutedEventArgs e)
        {
            if (p1Turn)
            {
                if (player1.blocksAvailable > 0)
                {
                    animation(1);
                    roundInfo.Text += "\n" + player1.name + " will block " + "next attack";
                    player1.block = true;
                    player1.blocksAvailable--;
                    roundInfo.Text += "\n" + player1.blocksAvailable + " blocks left ";
                    p1choice = "Block";
                    p1Turn = !p1Turn;
                }
                else
                {
                    animation(3);
                    roundInfo.Text += "\n" + player1.name + " has no blocks left!";
                }
            }
            else
            {
                if (player2.blocksAvailable > 0)
                {
                    animation(1);
                    roundInfo.Text += "\n" + player2.name + " will block " + "next attack";
                    player2.block = true;
                    player2.blocksAvailable--;
                    roundInfo.Text += "\n" + player2.blocksAvailable + " blocks left ";
                    p2choice = "Block";
                    MoveCheck();
                }
                else
                {
                    animation(3);
                    roundInfo.Text += "\n" + player2.name + " has no blocks left!";
                }
            }
        }

        private void Heal_Click(object sender, RoutedEventArgs e)
        {
            if(p1Turn)
            {
                if (player1.healsAvailable > 0)
                {
                    animation(2);
                    roundInfo.Text += "\n" + player1.name + " will heal for " + player1.heal;
                    player1.healsAvailable--;
                    roundInfo.Text += "\n" + player1.healsAvailable + " heals left ";
                    p1choice = "Heal";
                    p1Turn = !p1Turn;
                }
                else
                {
                    animation(3);
                    roundInfo.Text += "\n" + player1.name + " has no heals left!";
                }
            }
            else
            {
                if (player1.healsAvailable > 0)
                {
                    animation(2);
                    roundInfo.Text += "\n" + player2.name + " will heal for " + player2.heal;
                    player2.healsAvailable--;
                    roundInfo.Text += "\n" + player2.healsAvailable + " heals left ";
                    p2choice = "Heal";
                    MoveCheck();
                }
                else
                {
                    animation(3);
                    roundInfo.Text += "\n" + player2.name + " has no heals left!";
                }
            }
            
        }

        private async void animation(int buffId)
        {
            double op = 0.0;
            if (p1Turn)
            {
                pl1Buff.Source = Buffs[buffId];
                pl1Buff.Opacity = op;
                while (op < 1.0)
                {
                    op += 0.25;
                    pl1Buff.Opacity = op;
                    await Task.Delay(100);
                }
                await Task.Delay(500);
                while (op > 0.0)
                {
                    op -= 0.25;
                    pl1Buff.Opacity = op;
                    await Task.Delay(100);
                }
                pl1Buff.Source = null;
            }
            else
            {
                pl2Buff.Source = Buffs[buffId];
                pl2Buff.Opacity = op;
                while (op < 1.0)
                {
                    op += 0.25;
                    pl2Buff.Opacity = op;
                    await Task.Delay(100);
                }
                await Task.Delay(500);
                while (op > 0.0)
                {
                    op -= 0.25;
                    pl2Buff.Opacity = op;
                    await Task.Delay(100);
                }
                pl2Buff.Source = null;
            }
        }

        private void roundInfo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Scroll.ScrollToBottom();
        }

        private void MoveCheck()
        {
            if (p1choice == "Attack" && p2choice == "Block")
            {
                roundInfo.Text += "\n" + player2.name + " blocked the attack!";
                player2.block = false;
                TurnManager();
            }
            else if (p1choice == "Block" && p2choice == "Attack")
            {
                roundInfo.Text += "\n" + player1.name + " blocked the attack!";
                player1.block = false;
                TurnManager();
            }
            else if (p1choice == "Attack" && p2choice == "Heal")
            {
                player2.hp += player2.heal;
                if (player2.hp > Player2HP.Maximum)
                    Player2HP.Maximum = player2.hp;
                roundInfo.Text += "\n" + player2.name + " healed for " + player2.heal;
                roundInfo.Text += "\n" + player1.name + " attacked " + player2.name + " for " + player1.damage;
                player2.hp -= player1.damage;
                Player2HP.Value = player2.hp;
                TurnManager();
            }
            else if (p1choice == "Heal" && p2choice == "Attack")
            {
                player1.hp += player1.heal;
                if (player1.hp > Player1HP.Maximum)
                    Player1HP.Maximum = player1.hp;
                roundInfo.Text += "\n" + player1.name + " healed for " + player1.heal;
                roundInfo.Text += "\n" + player2.name + " attacked " + player1.name + " for " + player2.damage;
                player1.hp -= player2.damage;
                Player1HP.Value = player1.hp;
                TurnManager();
            }
            else if(p1choice == "Heal" && p2choice == "Heal")
            {
                player1.hp += player1.heal;
                if (player1.hp > Player1HP.Maximum)
                    Player1HP.Maximum = player1.hp;
                Player1HP.Value = player1.hp;
                roundInfo.Text += "\n" + player1.name + " healed for " + player1.heal;
                player2.hp += player2.heal;
                if (player2.hp > Player2HP.Maximum)
                    Player2HP.Maximum = player2.hp;
                Player2HP.Value = player2.hp;
                roundInfo.Text += "\n" + player2.name + " healed for " + player2.heal;
                TurnManager();
            }
            else if (p1choice == "Block" && p2choice == "Block")
            {
                roundInfo.Text += "\nBoth players blocked the next attack!";
                player1.block = false;
                player2.block = false;
                TurnManager();
            }
            else if (p1choice == "Block" && p2choice == "Heal")
            {
                player2.hp += player2.heal;
                if (player2.hp > Player2HP.Maximum)
                    Player2HP.Maximum = player2.hp;
                Player2HP.Value = player2.hp;
                roundInfo.Text += "\n" + player2.name + " healed for " + player2.heal;
                roundInfo.Text += "\n" + player1.name + " blocked nothing!";
                TurnManager();
            }
            else if (p1choice == "Heal" && p2choice == "Block")
            {
                player1.hp += player1.heal;
                if (player1.hp > Player1HP.Maximum)
                    Player1HP.Maximum = player1.hp;
                Player1HP.Value = player1.hp;
                roundInfo.Text += "\n" + player1.name + " healed for " + player1.heal;
                roundInfo.Text += "\n" + player2.name + " blocked nothing!";
                TurnManager();
            }
            else
            {
                roundInfo.Text += "\n" + player1.name + " attacked " + player2.name + " for " + player1.damage;
                roundInfo.Text += "\n" + player2.name + " attacked " + player1.name + " for " + player2.damage;
                player1.hp -= player2.damage;
                Player1HP.Value = player1.hp;
                player2.hp -= player1.damage;
                Player2HP.Value = player2.hp;
                TurnManager();
            }
        }
    }
}

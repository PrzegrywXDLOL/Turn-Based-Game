using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Turn_Based_Game
{
    /// <summary>
    /// Logika interakcji dla klasy PlayerCreate.xaml
    /// </summary>
    public partial class PlayerCreate : Window
    {
        public int playerchoice;
        public string playername;
        public PlayerCreate()
        {
            playerchoice = 3;
            playername = "NoNameSelected";

            InitializeComponent();
        }

        private void Mage(object sender, RoutedEventArgs e)
        {
            playerchoice = 2;
            if(PlayerName.Text != "")
            {
                playername = PlayerName.Text;
            }  
            this.Close();
        }

        private void Knight(object sender, RoutedEventArgs e)
        {
            playerchoice = 1;
            if (PlayerName.Text != "")
            {
                playername = PlayerName.Text;
            }
            this.Close();
        }

        private void Assasin(object sender, RoutedEventArgs e)
        {
            playerchoice = 0;
            if (PlayerName.Text != "")
            {
                playername = PlayerName.Text;
            }
            this.Close();
        }
    }
}

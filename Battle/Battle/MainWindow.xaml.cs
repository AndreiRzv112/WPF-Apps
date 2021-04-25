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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        bool isActive = false;
        bool isEnded = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hit_click(object sender, RoutedEventArgs e)
        {
            if ((isEnded == true) || (isActive == true)) return;
            isActive = true;
            int boss_hp = Convert.ToInt32(boss_health.Text);
            int user_hp = Convert.ToInt32(user_health.Text);
            int user_damage = rand.Next(1, 16);
            int mortal = rand.Next(8, 20);

            int random_attack = rand.Next(1, 6);
            string log_msg;
            int attack_dmg;
            if (random_attack == 1)
            {
                attack_dmg = user_damage + mortal;
                log_msg = "Mortal Hit for ";
            }
            else
            {
                attack_dmg = user_damage;
                log_msg = "Hit for ";
            }
            if (attack_dmg >= boss_hp)
            {
                isEnded = true;
                boss_health.Text = "0";
                battle_logs.Text = "You WON!";
                return;
            }
            else
            {
                this.Title = log_msg + Convert.ToString(attack_dmg);
                boss_health.Text = Convert.ToString(boss_hp - attack_dmg);
            }
            BossActivity();
        }

        private void Heal_Click(object sender, RoutedEventArgs e)
        {
            if ((isEnded == true) || (isActive == true)) return;
            isActive = true;
            int user_heal = rand.Next(4, 16);
            int user_hp = Convert.ToInt32(user_health.Text);
            user_health.Text = Convert.ToString(user_hp + user_heal);
            this.Title = "Healed for " + user_heal;
            BossActivity();
        }

        private void BossActivity()
        {
            int boss_hp = Convert.ToInt32(boss_health.Text);
            int user_hp = Convert.ToInt32(user_health.Text);
            int battle_round = Convert.ToInt32(round.Text);
            int boss_damage = rand.Next(6, 12);
            int boss_heal = rand.Next(2, 12);

            int choice = rand.Next(1, 6);
            if (!(choice <= 5) && (boss_hp <= 55))
            {
                boss_health.Text = Convert.ToString(boss_hp + boss_heal);
                battle_logs.Text = "Boss Healed for " + Convert.ToString(boss_heal) + "HP";
            }
            else
            {
                if (boss_damage >= user_hp)
                {
                    isEnded = true;
                    user_health.Text = "0";
                    battle_logs.Text = "You LOST!";
                    return;
                }
                else
                {
                    battle_logs.Text = "Boss hit you for " + Convert.ToInt32(boss_damage);
                    user_health.Text = Convert.ToString(user_hp - boss_damage);
                }
            }
            round.Text = Convert.ToString(battle_round + 1);
            isActive = false;
        }
    }
}

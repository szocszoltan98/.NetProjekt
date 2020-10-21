﻿using FitnessProject.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace FitnessProject
{
    public partial class TicketData : Window
    {
        MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessDb");
        public List<Ticket> tickets = new List<Ticket>();
        public static int selectedTicket;
        

        public TicketData()
        {
            InitializeComponent();

            conn.Open();
            MySqlCommand comm = new MySqlCommand("select * from tickets where " +
                            "((valid_from is null and valid_until is null and nr_of_entries > 0) " +
                            "or " +
                            "(valid_from is not null and valid_until is not null and " +
                            "DATE(valid_from) <= DATE(NOW()) and DATE(valid_until) >= DATE(NOW()))) and azonosito like " + MainWindow.azonosito, conn);
            MySqlDataReader reader = comm.ExecuteReader();

           

            
            if (reader.HasRows)
            {
                txtTicketMessage.Visibility = Visibility.Collapsed;
                comboBoxTickets.Visibility = Visibility.Visible;
                comboLabelTicket.Visibility = Visibility.Visible;
                optionsTextBlockTicket.Visibility = Visibility.Visible;

                while (reader.Read())
                {
                   
                    Ticket ticket = new Ticket();
                    ticket.id = (int)reader["id"];
                    ticket.valid_from = reader["valid_from"].ToString();
                    ticket.valid_until = reader["valid_until"].ToString();
                    ticket.nr_of_entries = (int)reader["nr_of_entries"];
                    ticket.nr_of_entries_day = (int)reader["nr_of_entries_day"];
                    ticket.hour_from = (int)reader["hour_from"];
                    ticket.hour_until = (int)reader["hour_until"];
                    ticket.weekend = (bool)reader["weekend"];
                    ticket.barcode = reader["azonosito"].ToString();
                    tickets.Add(ticket);

                }
                tickets.TrimExcess();
                
                for (int i = 0; i < tickets.Capacity; i++)
                {
                    if(tickets[i].valid_until != "")
                    {
                        optionsTextBlockTicket.Text = optionsTextBlockTicket.Text + (i + 1)+"." + " Érvényes még: " +  tickets[i].valid_until;
                    }
                    else
                    {
                        optionsTextBlockTicket.Text = optionsTextBlockTicket.Text + (i + 1) + " Fenmaradt belépések: " + tickets[i].nr_of_entries;
                    }
                    if (tickets[i].weekend)
                    {
                        optionsTextBlockTicket.Text = optionsTextBlockTicket.Text + " (hétvégi) ";
                    }
                    optionsTextBlockTicket.Text = optionsTextBlockTicket.Text + ", napi belépések száma: " + tickets[i].nr_of_entries_day;
                    if(tickets[i].hour_from != 0 || tickets[i].hour_until != 24)
                    {
                        optionsTextBlockTicket.Text = optionsTextBlockTicket.Text + ", a kovetzeő időhatáron belül: " + tickets[i].hour_from + " - " + tickets[i].hour_until;
                    }
                    optionsTextBlockTicket.Text = optionsTextBlockTicket.Text+ "\n";
                    comboBoxTickets.Items.Add(i + 1) ;
                }
                conn.Close();
            }
            else
            {
                txtTicketMessage.Visibility = Visibility.Visible;
                comboBoxTickets.Visibility = Visibility.Collapsed;
                comboLabelTicket.Visibility = Visibility.Collapsed;
            }
            

        }

        private void selectedListener(object sender, SelectionChangedEventArgs e)
        {
            selectedTicket = tickets[(int)comboBoxTickets.SelectedItem - 1].id;
            Access access = new Access();
            access.Show();
            this.Close();
        }

        private void BtnTicketBuy_Click(object sender, RoutedEventArgs e)
        {
            BuyTicket buyTicket = new BuyTicket();
            buyTicket.Show();
            this.Close();
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            UserData userData = new UserData();
            userData.Show();
            this.Close();
        }
   
        
        
    }
}

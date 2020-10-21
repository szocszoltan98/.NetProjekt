using FitnessProject.Data;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Text;
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
    public partial class BuyTicket : Window
    {
        public Ticket ticket;
        public BuyTicket()
        {
            InitializeComponent();           
        }

        private void BackToTicket_Click(object sender, RoutedEventArgs e)
        {
            TicketData ticketData = new TicketData();
            ticketData.Show();
            this.Close();
        }

        private void ButtonBuy_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessdb");
            connection.Open();        
            MySqlCommand comm = new MySqlCommand("select azonosito from users where azonosito like " + MainWindow.azonosito, connection);
            MySqlDataReader reader = comm.ExecuteReader();
            reader.Read();
            string azonosito = reader["azonosito"].ToString();

            connection.Close();

            connection.Open();
            MySqlCommand comm2 = connection.CreateCommand();
            comm2.CommandText = "INSERT INTO tickets(valid_from, valid_until, nr_of_entries, nr_of_entries_day, hour_from, hour_until, weekend, azonosito) " +
                                 "VALUES(@valid_from, @valid_until,@nr_of_entries,@nr_of_entries_day,@hour_from,@hour_until,@weekend,@azonosito   )";
            comm2.Parameters.AddWithValue("@valid_from", validFromBuy.Text);
            comm2.Parameters.AddWithValue("@valid_until", validUntilBuy.Text);
            comm2.Parameters.AddWithValue("@nr_of_entries", nrOfEntriesBuy.Text);
            comm2.Parameters.AddWithValue("@nr_of_entries_day", nrOfEntriesDayBuy.Text);
            comm2.Parameters.AddWithValue("@hour_from", hourFromBuy.Text);
            comm2.Parameters.AddWithValue("@hour_until", hourUntilBuy.Text);
            comm2.Parameters.AddWithValue("@weekend", weekendBuy.IsChecked);
            comm2.Parameters.AddWithValue("@azonosito", azonosito);
          
            comm2.ExecuteNonQuery();

            connection.Close();
        


            TicketData ticket = new TicketData();
            ticket.Show();
            this.Close();
        }
    }
}

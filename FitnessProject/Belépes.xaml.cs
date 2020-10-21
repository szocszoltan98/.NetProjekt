using FitnessProject.Data;
using MySql.Data.MySqlClient;
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

namespace FitnessProject
{
    public partial class Access : Window
    {
        private MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessdb");
        private MySqlCommand command;


        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public void ExecuteQuery(string query)
        {
            try
            {
                OpenConnection();
                command = new MySqlCommand(query, connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    // Query executed
                }
                else
                {
                    // Query not executed
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public Access()
        {
            InitializeComponent();
            connection.Open();
            MySqlCommand comm = new MySqlCommand("select LastName,FirstName,azonosito from users where azonosito like " + MainWindow.azonosito, connection);
            MySqlDataReader reader = comm.ExecuteReader();
            reader.Read();


            
            string name1 = reader["FirstName"].ToString();
            string name2 = reader["LastName"].ToString();
            string azonosito = reader["azonosito"].ToString();
            DateTime ido = DateTime.Now;
            nameAccess.Text = nameAccess.Text + name1 + "!";
            connection.Close();

         
            
           

            connection.Open();
            MySqlCommand update = new MySqlCommand("UPDATE tickets SET nr_of_entries = nr_of_entries - 1 WHERE id = " + TicketData.selectedTicket, connection);
            update.ExecuteNonQuery();
            connection.Close();

            connection.Open();
            MySqlCommand command = new MySqlCommand("select * from tickets where id=" + TicketData.selectedTicket, connection);
            MySqlDataReader read = command.ExecuteReader();
            if (read.HasRows)
            {
                read.Read();
                Ticket ticket = new Ticket();
                ticket.valid_from = read["valid_from"].ToString();
                ticket.valid_until = read["valid_until"].ToString();
                ticketDetailsAccess.Text = ticketDetailsAccess.Text + '\n';
                if(ticket.valid_from != "")
                {
                    ticketDetailsAccess.Text = ticketDetailsAccess.Text + "Érvenyes: " + ticket.valid_from + " - " + ticket.valid_until + '\n';
                }
                ticket.nr_of_entries = (int)read["nr_of_entries"];
                ticketDetailsAccess.Text = ticketDetailsAccess.Text + "Belépések: " + ticket.nr_of_entries + '\n';
                ticket.nr_of_entries_day = (int)read["nr_of_entries_day"];
                ticketDetailsAccess.Text = ticketDetailsAccess.Text  + "Belépések/Nap: " + ticket.nr_of_entries_day + '\n';
                ticket.hour_from = (int)read["hour_from"];
                ticket.hour_until = (int)read["hour_until"];
                ticketDetailsAccess.Text = ticketDetailsAccess.Text + "A kovetkező órákban: " + ticket.hour_from + " - " + ticket.hour_until + '\n';
                ticket.weekend = (bool)read["weekend"];
                if (ticket.weekend)
                {
                    ticketDetailsAccess.Text = ticketDetailsAccess.Text + "Hétvégén is" + '\n';
                }
            }
            connection.Close();


            connection.Open();



            MySqlCommand comm2 = connection.CreateCommand();
            comm2.CommandText = "INSERT INTO logins(azonosito,Firstname,LastName,Datum,TicketID) VALUES(@azonosito, @FirstName,@LastName,@Datum,@TicketID)";
            comm2.Parameters.AddWithValue("@azonosito", azonosito);
            comm2.Parameters.AddWithValue("@FirstName", name1);
            comm2.Parameters.AddWithValue("@Lastname", name2);
            comm2.Parameters.AddWithValue("@Datum", ido);
            comm2.Parameters.AddWithValue("@TicketID", TicketData.selectedTicket);

            comm2.ExecuteNonQuery();

            connection.Close();
            
        }
        private void backToTickets(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TicketData ticket = new TicketData();
            ticket.Show();
            this.Close();
        }
    }
}

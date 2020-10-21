﻿using FitnessProject.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FitnessProject
{
    public partial class AdminData : Window
    {
        public AdminData()
        {
            InitializeComponent();
            RunQuery("select * from users");
        }

        private void RunQuery(string query)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=fitnessdb";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(query, mySqlConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "LoadData");
            dataGridInfo.DataContext = dataSet;

            try
            {
                mySqlConnection.Open();
                MySqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {

                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("query error " + e.Message);
            }
        }

        private void onSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboBoxSelect.SelectedIndex)
            {
                case 0:
                    btnDeleteRow.Visibility = Visibility.Collapsed;
                    comboBoxSelectListOptions.Visibility = Visibility.Collapsed;
                    RunQuery("select * from users");
                    break;
                case 1:
                    RunQuery("select * from tickets");
                    btnDeleteRow.Visibility = Visibility.Collapsed;
                    comboBoxSelectListOptions.Visibility = Visibility.Visible;
                    break;
                case 2:
                    btnDeleteRow.Visibility = Visibility.Collapsed;
                    comboBoxSelectListOptions.Visibility = Visibility.Collapsed;
                    RunQuery("select * from logins");
                    break;
            }
        }

        private void ExportToExcel()
        {
            dataGridInfo.SelectAllCells();
            dataGridInfo.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dataGridInfo);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);
            dataGridInfo.UnselectAllCells();
            System.IO.StreamWriter file = null;
            
            switch (comboBoxSelect.SelectedIndex)
            {
                case 0:
                    file = new System.IO.StreamWriter("Users.xls");
                    break;
                case 1:
                    file = new System.IO.StreamWriter("Tickets.xls");
                    break;
                case 2:
                    file = new System.IO.StreamWriter("Logins.xls");
                    break;
            }

            if (file != null)
            {
                file.WriteLine(result.Replace(',', ' '));
                file.Close();
            }
        }

        private void btnSaveToTxt(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }

        private void btnSearchUser(object sender, RoutedEventArgs e)
        {
            RunQuery("select * from users where concat(FirstName, LastName, Email, PhoneNumber, birthday, admin, azonosito)" +
                " like '%" + txtBoxSearchUser.Text + "%'");
        }

        private void onSelectionChangedList1(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxSelectListOptions.Visibility == Visibility.Visible)
            {
                switch(comboBoxSelectListOptions.SelectedIndex)
                {
                    case 0:
                        RunQuery("select * from tickets where " +
                            "(valid_from is null and valid_until is null and nr_of_entries > 0) " +
                            "or " +
                            "(valid_from is not null and valid_until is not null and " +
                            "DATE(valid_from) <= DATE(NOW()) and DATE(valid_until) >= DATE(NOW()))");
                        break;
                    case 1:
                        RunQuery("select * from tickets where " +
                            "(valid_from is null and valid_until is null and nr_of_entries = 0) " +
                            "or " +
                            "(valid_from is not null and valid_until is not null and " +
                            "(DATE(valid_from) > DATE(NOW()) or DATE(valid_until) < DATE(NOW())))");
                        break;
                }
            }
        }

        private void btnDeleteSelectedRow(object sender, RoutedEventArgs e)
        {
            DataRowView row;
            switch (comboBoxSelect.SelectedIndex)
            {
                case 0:
                    row = (DataRowView)dataGridInfo.SelectedItems[0];
                    RunQuery("delete from users where azonosito like " + row["azonosito"].ToString());
                    MessageBox.Show("Sikeresen törltük a sort!", "Törlés", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 1:
                    row = (DataRowView)dataGridInfo.SelectedItems[0];
                    RunQuery("delete from tickets where id = " + (int)row["id"]);
                    MessageBox.Show("Sikeresen törltük a sort!", "Törlés", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 2:
                    row = (DataRowView)dataGridInfo.SelectedItems[0];
                    RunQuery("delete from logins where azonosito like " + row["azonosito"].ToString());
                    MessageBox.Show("Sikeresen törltük a sort!", "Törlés", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }

        private void dataGridRowSelected(object sender, RoutedEventArgs e)
        {
            btnDeleteRow.Visibility = Visibility.Visible;
        }

        private void btnClickLogOut(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Kijelentkezel?", "Kijelentkezés", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }
    }
}

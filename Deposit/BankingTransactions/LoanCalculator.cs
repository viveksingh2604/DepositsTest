using System;
using System.Data.SQLite;

namespace BankingTransactions
{
    public class LoanCalculator
    {
        public double CalculateMaturityAmount(double principalamount, DateTime startdate, DateTime enddate, double interestrate, int year, int count)
        {
            double maturityamount = 0;
            maturityamount = MaturityAmount(principalamount, interestrate, year);
            DepositProcess(principalamount, startdate, enddate, interestrate, year, maturityamount, count);
            return maturityamount;
        }
        public double CalculateMaturityAmount()
        {
            double maturityamount = 0;
            SQLiteConnection conn;
            conn = CreateConnection();
            SellProcess();
            maturityamount = GetMaturityAmount(conn);
            return maturityamount;
        }
        public double CalculateTotalMaturityAmount()
        {
            SQLiteConnection conn = null;

            conn = CreateConnection();
            return (GetTotalMaturityAmount(conn));

        }
        private double MaturityAmount(double principalamount, double interestrate, int year)
        {
            return principalamount * Math.Pow((interestrate), year);
        }
        protected void DepositProcess(double principalamount, DateTime startdate, DateTime enddate, double interestrate, int year, double maturityamount, int count)
        {
            SQLiteConnection conn;
            conn = CreateConnection();
            int id;
            double totalmaturityamount = 0;
            if (!isTableExists(conn))
            {
                CreateTable(conn);
            }
            if (count == 1)
            {
                DeleteAllRows(conn);
            }
            id = GetMaxID(conn);
            totalmaturityamount = GetTotalMaturityAmount(conn);
            totalmaturityamount = totalmaturityamount + maturityamount;
            InsertRow(++id, principalamount, startdate, enddate, interestrate, year, maturityamount, totalmaturityamount, conn);

        }
        protected void SellProcess()
        {
            SQLiteConnection conn;
            conn = CreateConnection();
            DeletetRow(conn);
        }
        private SQLiteConnection CreateConnection()
        {
            SQLiteConnection conn;
            conn = new SQLiteConnection("Data Source=database.db;Version = 3;New=true;Compress=True;");
            return conn;
        }
        private bool isTableExists(SQLiteConnection conn)
        {
            var sql = "SELECT name FROM sqlite_master WHERE type='table' AND name='tblDeposits';";
            bool tblexists = false;
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    tblexists = true;
                }
                reader.Close();
                command.Dispose();
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return tblexists;

        }
        private void CreateTable(SQLiteConnection conn)
        {
            try
            {
                SQLiteCommand sqlite_cmd;
                string Createsql = "CREATE TABLE tblDeposits(ID INT PRIMARY KEY, PrincipalAmount DOUBLE, StartDate DATE, EndDate DATE, InterestRate DOUBLE, Term INT, MaturityAmount DOUBLE, TotalMaturityAmount DOUBLE)";
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = Createsql;
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.Dispose();
            }
            catch (Exception e)
            { }
            finally
            {
                conn.Close();
            }
        }
        private void DeleteAllRows(SQLiteConnection conn)
        {
            try
            {
                SQLiteCommand sqlite_cmd;
                string Createsql = "DELETE FROM tblDeposits";
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = Createsql;
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.Dispose();
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

        }
        private double GetMaturityAmount(SQLiteConnection conn)
        {
            double totalmaturityamount = 0;
            try
            {
                string selectMaxId = "Select MaturityAmount From tblDeposits where ID=(select MAX(ID) from tblDeposits)";
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                SQLiteCommand selectCmd = new SQLiteCommand(selectMaxId, conn);
                SQLiteDataReader dataReader = selectCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    totalmaturityamount = (double)(dataReader.GetValue(0));
                }
                dataReader.Close();
                selectCmd.Dispose();

            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return totalmaturityamount;
        }
        private double GetTotalMaturityAmount(SQLiteConnection conn)
        {
            double totalmaturityamount = 0;
            try
            {
                string selectMaxId = "Select TotalMaturityAmount From tblDeposits where ID=(select MAX(ID) from tblDeposits)";
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                SQLiteCommand selectCmd = new SQLiteCommand(selectMaxId, conn);
                SQLiteDataReader dataReader = selectCmd.ExecuteReader();

                while (dataReader.Read())
                {
                    totalmaturityamount = (double)(dataReader.GetValue(0));
                }
                dataReader.Close();
                selectCmd.Dispose();

            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return totalmaturityamount;
        }

        private int GetMaxID(SQLiteConnection conn)
        {
            int maxID = 0;
            try
            {
                string selectMaxId = "Select Max(ID) From tblDeposits";
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                SQLiteCommand selectMaxCmd = new SQLiteCommand(selectMaxId, conn);
                SQLiteDataReader dataReader = selectMaxCmd.ExecuteReader();



                while (dataReader.Read())
                {
                    if (dataReader.GetValue(0).ToString() != "")
                        maxID = Convert.ToInt16(dataReader.GetValue(0));
                }
                dataReader.Close();
                selectMaxCmd.Dispose();
            }
            catch (Exception e)
            {

            }
            finally
            {

                conn.Close();
            }
            return maxID;
        }
        private void InsertRow(int Id, double principalamount, DateTime startdate, DateTime enddate, double interestrate, int term, double maturityamount, double totalmaturityamount, SQLiteConnection conn)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                using (var cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "INSERT INTO tblDeposits (ID, PrincipalAmount, StartDate, EndDate, InterestRate, Term, MaturityAmount, TotalMaturityAmount) VALUES (@ID, @PrincipalAmount, @StartDate, @EndDate, @InterestRate, @Term, @MaturityAmount, @TotalMaturityAmount)";
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@PrincipalAmount", principalamount);
                    cmd.Parameters.AddWithValue("@StartDate", startdate);
                    cmd.Parameters.AddWithValue("@EndDate", enddate);
                    cmd.Parameters.AddWithValue("@InterestRate", interestrate);
                    cmd.Parameters.AddWithValue("@Term", term);
                    cmd.Parameters.AddWithValue("@MaturityAmount", maturityamount);
                    cmd.Parameters.AddWithValue("@TotalMaturityAmount", totalmaturityamount);
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

        }
        private void DeletetRow(SQLiteConnection conn)
        {
            try
            {
                SQLiteCommand sqlite_cmd;
                string Createsql = "DELETE FROM tblDeposits where ID =(select MAX(ID) from tblDeposits)";
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = Createsql;
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd.Dispose();
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
        }
    }
}

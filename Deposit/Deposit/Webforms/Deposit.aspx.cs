using System;
using BankingTransactions;

namespace Deposit.Webforms
{
    public partial class Deposit : System.Web.UI.Page
    {
        public const double BUYLIMIT = 120000000;
        public const double SELLLIMIT = 50000000;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Timer1.Enabled = false;

        }
        protected void OnSelectedIndexChanged(object sender, EventArgs e)

        {
            if (ddlOptions.SelectedIndex == 1)
            {

                if (Convert.ToDouble(txttotalMaturityAmount.Text) < BUYLIMIT)
                {
                    Global.count++;
                    ProcessBuy();
                    Timer1.Enabled = true;
                }
                else
                {
                    Timer1.Enabled = false;
                }

            }
            if (ddlOptions.SelectedIndex == 2)
            {
                if (Convert.ToDouble(txttotalMaturityAmount.Text) > SELLLIMIT)
                {
                    Global.count++;
                    ProcessSell();
                    Timer1.Enabled = true;
                }
                else
                {
                    Timer1.Enabled = false;
                }
            }
            if (ddlOptions.SelectedIndex == 0)
            {
                Timer1.Enabled = false;
            }

        }
        private void ProcessBuy()
        {
            LoanCalculator CalculateBuy = new LoanCalculator();
            txtMaturityAmount.Text = CalculateBuy.CalculateMaturityAmount(Convert.ToDouble(txtPrincipal.Text.Trim()), Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToDouble(txtInterestRate.Text.Trim()), Convert.ToInt16(txtTerm.Text.Trim()), Global.count).ToString();
            txttotalMaturityAmount.Text = CalculateBuy.CalculateTotalMaturityAmount().ToString();
        }
        private void ProcessSell()
        {
            LoanCalculator CalculateSell = new LoanCalculator();
            txtMaturityAmount.Text = CalculateSell.CalculateMaturityAmount().ToString();
            txttotalMaturityAmount.Text = CalculateSell.CalculateTotalMaturityAmount().ToString();

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            OnSelectedIndexChanged(sender, e);
        }
    }
}
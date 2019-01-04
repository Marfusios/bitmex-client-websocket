using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bitmex.Client.Websocket.Sample.WinForms.Views
{
    public partial class Form1 : Form, IStatsView
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Action OnInit { get; set; }
        public Action OnStart { get; set; }
        public Action OnStop { get; set; }

        public bool IsTestNet => cbTestNet.Checked;

        public string Pair
        {
            get => tbPair.Text;
            set => SetTextOnGuiThread(tbPair, value);
        }

        public string Bid
        {
            get => tbBid.Text;
            set => SetTextOnGuiThread(tbBid, value);
        }
        public string Ask         
        {
            get => tbAsk.Text;
            set => SetTextOnGuiThread(tbAsk, value);
        }

        public string BidAmount 
        {
            get => tbBidAmount.Text;
            set => SetTextOnGuiThread(tbBidAmount, value);
        }
        public string AskAmount 
        {
            get => tbAskAmount.Text;
            set => SetTextOnGuiThread(tbAskAmount, value);
        }

        void IStatsView.Trades1Min(string value, Side side)
        {
            Trades1Min = value;
            tb1MinTrades.ForeColor = GetForeColorFor(side);
        }

        void IStatsView.Trades5Min(string value, Side side)
        {
            Trades5Min = value;
            tb5MinTrades.ForeColor = GetForeColorFor(side);
        }

        void IStatsView.Trades15Min(string value, Side side)
        {
            Trades15Min = value;
            tb15MinTrades.ForeColor = GetForeColorFor(side);
        }

        void IStatsView.Trades1Hour(string value, Side side)
        {
            Trades1Hour = value;
            tb1HoursTrades.ForeColor = GetForeColorFor(side);
        }

        void IStatsView.Trades24Hours(string value, Side side)
        {
            Trades24Hours = value;
            tb24HoursTrades.ForeColor = GetForeColorFor(side);
        }

        public void Status(string value, StatusType type)
        {
            SetTextOnGuiThread(tbStatus, value);
            tbStatus.ForeColor = GetForeColorFor(type);
        }

        public string Ping
        {
            get => tbPing.Text;
            set => SetTextOnGuiThread(tbPing, value);
        }

        public string Trades1Min
        {
            get => tb1MinTrades.Text;
            set => SetTextOnGuiThread(tb1MinTrades, value);
        }
        public string Trades5Min 
        {
            get => tb5MinTrades.Text;
            set => SetTextOnGuiThread(tb5MinTrades, value);
        }
        public string Trades15Min
        {
            get => tb15MinTrades.Text;
            set => SetTextOnGuiThread(tb15MinTrades, value);
        }

        public string Trades1Hour
        {
            get => tb1HoursTrades.Text;
            set => SetTextOnGuiThread(tb1HoursTrades, value);
        }

        public string Trades24Hours
        {
            get => tb24HoursTrades.Text;
            set => SetTextOnGuiThread(tb24HoursTrades, value);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OnInit?.Invoke();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            OnStart?.Invoke();
            btnStart.Visible = false;
            btnStop.Visible = true;
            tbPair.Visible = false;
            tbSelectedPair.Text = $"{tbPair.Text}" + (IsTestNet ? $" (TestNet)" : string.Empty);
            cbTestNet.Visible = false;
            btnStop.Focus();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            OnStop?.Invoke();
            btnStart.Visible = true;
            btnStop.Visible = false;
            tbPair.Visible = true;
            tbSelectedPair.Text = string.Empty;
            cbTestNet.Visible = true;
            btnStart.Focus();
        }

        private Color GetForeColorFor(Side side)
        {
            if (side == Side.Buy)
                return Color.GreenYellow;
            return Color.LightCoral;
        }

        private Color GetForeColorFor(StatusType type)
        {
            switch (type)
            {
                case StatusType.Error:
                    return Color.IndianRed;
                case StatusType.Warning:
                    return Color.DarkOrange;
                default:
                    return Color.DarkSeaGreen;
            }
        }

        private void SetTextOnGuiThread(TextBox tb, string value)
        {
            if(tb.Text == value)
                return;

            if (!InvokeRequired)
            {
                tb.Text = value;
                return;
            }

            this.Invoke(new Action(() =>
            {
                tb.Text = value;
            }));
        }
    }
}

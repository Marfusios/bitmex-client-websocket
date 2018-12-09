using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bitmex.Client.Websocket.Sample.WinForms
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

        private void Form1_Load(object sender, EventArgs e)
        {
            OnInit?.Invoke();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            OnStart?.Invoke();
            btnStart.Visible = false;
            btnStop.Visible = true;
            btnStop.Focus();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            OnStop?.Invoke();
            btnStart.Visible = true;
            btnStop.Visible = false;
            btnStart.Focus();
        }

        private Color GetForeColorFor(Side side)
        {
            if (side == Side.Buy)
                return Color.GreenYellow;
            return Color.LightCoral;
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

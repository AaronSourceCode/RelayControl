using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RelayControl
{
    public partial class frmRelayControl : Form
    {
        List<bool> lstRelays;
        Timer tmrRefresh;

        string requestAddress = "http://192.168.0.75/30000/";

        public frmRelayControl()
        {
            InitializeComponent();
            lstRelays = new List<bool>(new bool[] { false, false, false, false, 
                                                    false, false, false, false, 
                                                    false, false, false, false, 
                                                    false, false, false, false});
            tmrRefresh = new Timer();
            tmrRefresh.Interval = 5000;
            tmrRefresh.Tick += OnRefreshTimerTick;
            tmrRefresh.Enabled = true;

            checkRelayStates();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sCommand = "45";
            sendCommand(sCommand);
            checkRelayStates();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sCommand = "44";
            sendCommand(sCommand);
            checkRelayStates();
        }

        private void OnRefreshTimerTick(object sender, EventArgs e)
        {
            checkRelayStates();
        }

        private string sendCommand(string sCommand)
        {
            tmrRefresh.Enabled = false;
            string sResponse = String.Empty;
            string sURL = requestAddress + sCommand;
            System.Net.WebRequest wrGETURL = System.Net.WebRequest.Create(sURL);

            System.IO.Stream objStream = wrGETURL.GetResponse().GetResponseStream();
            
            System.IO.StreamReader objReader = new System.IO.StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine != null)
            {
                i++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    System.Diagnostics.Debug.WriteLine("{0}:{1}", i, sLine);
                    sResponse += sLine;
                }
            }
            System.Threading.Thread.Sleep(100);
            tmrRefresh.Enabled = true;
            return sResponse;
        }

        private void checkRelayStates()
        {
            parseResponse(sendCommand(""));
            parseResponse(sendCommand("43"));
            parseResponse(sendCommand("42"));
            parseResponse(sendCommand("43"));
        }

        private void parseResponse(string sResponse)
        {
            Regex urlRx = new Regex(@"(?<url>(http:|https:[/][/]|www.)([a-z]|[A-Z]|[0-9]|[/.]|[~])*)", RegexOptions.IgnoreCase);
            MatchCollection matches = urlRx.Matches(sResponse);
            foreach (Match match in matches)
            {
                    int RelayNum = Int32.Parse(match.Value.Substring(match.Value.LastIndexOf('/') + 1));
                    if (RelayNum < 32 && RelayNum >= 0)
                    {
                        updateRelayInfo(RelayNum);
                    }
            }
            updateRelayLabels();
        }

        private void updateRelayInfo(int iRelayValue)
        {
            int iRelayNum;
            bool blnIsOn;

            iRelayNum = (iRelayValue + 2) / 2;
            blnIsOn = iRelayValue % 2 == 0;

            lstRelays[iRelayNum-1] = blnIsOn;
        }

        private void updateRelayLabels()
        {
            updateRelayLabel(lblRelayStatus1, 0, lstRelays[0]);
            updateRelayLabel(lblRelayStatus2, 1, lstRelays[1]);
            updateRelayLabel(lblRelayStatus3, 2, lstRelays[2]);
            updateRelayLabel(lblRelayStatus4, 3, lstRelays[3]);
            updateRelayLabel(lblRelayStatus5, 4, lstRelays[4]);
            updateRelayLabel(lblRelayStatus6, 5, lstRelays[5]);
            updateRelayLabel(lblRelayStatus7, 6, lstRelays[6]);
            updateRelayLabel(lblRelayStatus8, 7, lstRelays[7]);
            updateRelayLabel(lblRelayStatus9, 8, lstRelays[8]);
            updateRelayLabel(lblRelayStatus10, 9, lstRelays[9]);
            updateRelayLabel(lblRelayStatus11, 10, lstRelays[10]);
            updateRelayLabel(lblRelayStatus12, 11, lstRelays[11]);
            updateRelayLabel(lblRelayStatus13, 12, lstRelays[12]);
            updateRelayLabel(lblRelayStatus14, 13, lstRelays[13]);
            updateRelayLabel(lblRelayStatus15, 14, lstRelays[14]);
            updateRelayLabel(lblRelayStatus16, 15, lstRelays[15]);
        }

        private void updateRelayLabel(Label label, int iRelay, bool bStatus)
        {
            if (bStatus == true)
            {
                label.ForeColor = Color.Green;
                label.Text = "On";
            }
            else
            {
                label.ForeColor = Color.Red;
                label.Text = "Off";
            }
        }

        private void toggleRelay(int iRelayNum)
        {
            int iCommand = iRelayNum * 2 - 2;
            if (lstRelays[iRelayNum-1] == false)
            {
                iCommand += 1;
            }
            parseResponse(sendCommand(iCommand.ToString().PadLeft(2, '0')));
        }

        private void btnOnOff1_Click(object sender, EventArgs e)
        {
            toggleRelay(1);
        }

        private void btnOnOff2_Click(object sender, EventArgs e)
        {
            toggleRelay(2);
        }

        private void btnOnOff3_Click(object sender, EventArgs e)
        {
            toggleRelay(3);
        }

        private void btnOnOff4_Click(object sender, EventArgs e)
        {
            toggleRelay(4);
        }

        private void btnOnOff5_Click(object sender, EventArgs e)
        {
            toggleRelay(5);
        }

        private void btnOnOff6_Click(object sender, EventArgs e)
        {
            toggleRelay(6);
        }

        private void btnOnOff7_Click(object sender, EventArgs e)
        {
            toggleRelay(7);
        }

        private void btnOnOff8_Click(object sender, EventArgs e)
        {
            toggleRelay(8);
        }

        private void btnOnOff9_Click(object sender, EventArgs e)
        {
            toggleRelay(9);
        }

        private void btnOnOff10_Click(object sender, EventArgs e)
        {
            toggleRelay(10);
        }

        private void btnOnOff11_Click(object sender, EventArgs e)
        {
            toggleRelay(11);
        }

        private void btnOnOff12_Click(object sender, EventArgs e)
        {
            toggleRelay(12);
        }

        private void btnOnOff13_Click(object sender, EventArgs e)
        {
            toggleRelay(13);
        }

        private void btnOnOff14_Click(object sender, EventArgs e)
        {
            toggleRelay(14);
        }

        private void btnOnOff15_Click(object sender, EventArgs e)
        {
            toggleRelay(15);
        }

        private void btnOnOff16_Click(object sender, EventArgs e)
        {
            toggleRelay(16);
        }
    }
}

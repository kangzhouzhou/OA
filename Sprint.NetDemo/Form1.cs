using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sprint.NetDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            IApplicationContext ctx = ContextRegistry.GetContext();
            IUserInfoService userService = (UserInfoSerivce)ctx.GetObject("UserInfoService");
            sw.Stop();
            MessageBox.Show(userService.ShowMsg() +"   " + sw.Elapsed);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            IUserInfoService userService = new UserInfoSerivce();
            sw.Stop();
            MessageBox.Show(userService.ShowMsg() + "   " + sw.Elapsed);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1.Forms
{
    public partial class tipForms : Form
    {
        public tipForms(Point point, int width, string messageText)
        {
            InitializeComponent();
            this.Location = new Point(point.X, point.Y);//设置出现位置
            //lblTip 显示提示信息的Lable
            lblTip.Text = messageText;
            this.Load += TipFrom_Load;
            this.FormClosing += TipFrom_FormClosing;
            timer1.Tick += timer1_Tick;

        }

        private void TipFrom_Load(object sender, EventArgs e)
        {
            lblTip.Location = new Point((this.Width - lblTip.Width) / 2, (this.Height - lblTip.Height) / 2);//提示信息居中显示
            timer1.Interval = 500 * 2;//显示时间
            timer1.Start();
            AnimateWindow(this.Handle, 500, AW_VER_POSITIVE);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TipFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnimateWindow(this.Handle, 500, AW_SLIDE | AW_HIDE | AW_VER_NEGATIVE);
        }
        public const Int32 AW_HOR_POSITIVE = 0x00000001; // 从左到右打开窗口
        public const Int32 AW_HOR_NEGATIVE = 0x00000002; // 从右到左打开窗口
        public const Int32 AW_VER_POSITIVE = 0x00000004; // 从上到下打开窗口
        public const Int32 AW_VER_NEGATIVE = 0x00000008; // 从下到上打开窗口
        public const Int32 AW_CENTER = 0x00000010; //若使用了AW_HIDE标志，则使窗口向内重叠；若未使用AW_HIDE标志，则使窗口向外扩展。
        public const Int32 AW_HIDE = 0x00010000; //隐藏窗口，缺省则显示窗口。
        public const Int32 AW_ACTIVATE = 0x00020000; //激活窗口。在使用了AW_HIDE标志后不要使用这个标志。
        public const Int32 AW_SLIDE = 0x00040000; //使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略。
        public const Int32 AW_BLEND = 0x00080000; //使用淡出效果。只有当hWnd为顶层窗口的时候才可以使用此标志。
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool AnimateWindow(
              IntPtr hwnd, // handle to window 
              int dwTime, // duration of animation 
              int dwFlags // animation type 
            );
    }
}

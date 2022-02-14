using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Application = FlaUI.Core.Application;
using FlaUI.Core.Definitions;
using FlaUI.Core.WindowsAPI;
using System.IO;

namespace WeChatAutomationDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        const string appDefaultPath = @"C:\Program Files (x86)\Tencent\WeChat\WeChat.exe";

        /// <summary>
        /// 获取微信的主程序对象
        /// </summary>
        /// <returns></returns>
        private Application GetWeChatApplication()
        {
            //程序是否在运行
            var targetId = GetProcessId("WeChat");
            if (targetId == 0)
            {
                if (TextBox_AppPath.Text != "" && File.Exists(TextBox_AppPath.Text))
                {
                    WriteLog("启动微信。");
                    return Application.Launch(TextBox_AppPath.Text);
                }
            }
            else
            {
               return Application.Attach(targetId);
            }
            return null;
        }

        /// <summary>
        /// 获取微信的主窗口
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private Window GetWeChatMainWindow(ref Application app)
        {
            AutomationElement desktop = new UIA3Automation().GetDesktop();
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
            /**
            * 微信，如果隐藏在托盘区域的话（非最小化状态），那么试图获取主窗口会一直卡住，直到手动点击微信
            * 图标将其窗口显示出来，程序才能正常运行。
            * 可以通过查找桌面的子元素，看看是否有微信的窗口来判断其是否隐藏在托盘区
            * 
            */
            //查找「通知V形」按钮并单击
            var btn_NotifyMore = desktop.FindFirstDescendant(cf.ByAutomationId("1502")).AsButton();
            btn_NotifyMore.Click();

            AutomationElement btn_WeChat_TrayButton = null;
            AutomationElement Panel_NotifyOverFlowArea = null;
            Retry.WhileNull(() =>
            {
                Panel_NotifyOverFlowArea = desktop.FindFirstDescendant(cf.ByName("通知溢出")).AsWindow();
                return Panel_NotifyOverFlowArea;
            }, TimeSpan.FromSeconds(5), null, true, false, "没有找到「通知溢出」的按钮。");
            //查找并点击托盘区域微信的图标
            btn_WeChat_TrayButton = Panel_NotifyOverFlowArea.FindFirstDescendant(cf.ByName("微信")).AsButton();
            //如果微信有新消息，直接点击按钮不会调出主窗口，需要点击“忽略全部”
            var rect = btn_WeChat_TrayButton.BoundingRectangle;
            WriteLog(rect.Width + "," + rect.Height + "," + rect.X + "," + rect.Y);
            Mouse.MoveTo(rect.X, rect.Y); //移动到微信按钮上
            //查找是否有“忽略全部”的按钮
            var ignoreButton = desktop.FindFirstDescendant(cf.ByName("微信")).AsWindow().FindFirstDescendant(cf.ByName("忽略全部")).AsButton();
            ignoreButton.DrawHighlight();
            if(ignoreButton != null)
            {
                WriteLog("忽略新消息");
                rect = ignoreButton.BoundingRectangle;
                //这里不能直接点击按钮
                Mouse.MoveTo(new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)));
            }
            Thread.Sleep(3000);
            btn_WeChat_TrayButton.Click();
            Thread.Sleep(300);
            btn_NotifyMore.Click();
            //微信主窗口
            return app.GetMainWindow(new UIA3Automation());
        }

        /// <summary>
        /// 搜索指定的对象并激活其聊天窗口
        /// </summary>
        /// <param name="wechatWindow"></param>
        /// <param name="target"></param>
        /// <returns>返回指定搜索对象的聊天输入框</returns>
        private AutomationElement SearchTarget(in Window wechatWindow, string target)
        {
            //获取并单击「聊天」按钮，如果直接将其 AsButton() 单击的话，会出现找不到点击位置的异常
            //所以这里获取按钮的矩形，然后点击其中间
            var Btn_Chat = GetWeChatControl(wechatWindow,"聊天", ControlType.Button);
            if (Btn_Chat == null)
            {
                WriteLog("没有获取到微信「聊天」按钮。");
                return null;
            }
            var rect = Btn_Chat.BoundingRectangle;
            Mouse.Click(new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)));
            Thread.Sleep(500);

            var edit_SearchChatObject = GetWeChatControl(in wechatWindow,"搜索", ControlType.Edit);
            if (edit_SearchChatObject == null)
            {
                WriteLog("没有找到指定的控件：搜索框。");
                return null;
            }
            WriteLog("搜索指定内容：" + target);
            rect = edit_SearchChatObject.BoundingRectangle;
            Mouse.Click(new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)));
            edit_SearchChatObject.AsTextBox().Enter(target);
            Thread.Sleep(1000);
            Keyboard.Press(VirtualKeyShort.ENTER);

            //查找发送消息的文本框
            var edit_MessageInput = GetWeChatControl(in wechatWindow, "输入", ControlType.Edit);
            if (edit_MessageInput == null)
            {
                WriteLog("没有找到消息输入的控件。");
                return null;
            }
            return edit_MessageInput;
        }

        private void SendMessage(in Window wechatWindow, in AutomationElement chatInputBux, string message)
        {
            var rect = chatInputBux.BoundingRectangle;
            Mouse.Click(new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)));
            Thread.Sleep(500);

            //查找「发送」按钮，并单击发送消息
            var btn_SendMessage = GetWeChatControl(wechatWindow,"发送(S)", ControlType.Button);
            if (btn_SendMessage == null)
            {
                btn_SendMessage = GetWeChatControl(wechatWindow, "sendBtn", ControlType.Button);
            }
            if (btn_SendMessage == null)
            {
                WriteLog("没有找到「发送消息」的按钮。");
                return;
            }
            rect = btn_SendMessage.BoundingRectangle;
            //这里如果是中文输入法的话，输入英文可能出现问题，这里暂时使用粘贴方式解决
            Clipboard.SetText(message);
            Keyboard.Pressing(VirtualKeyShort.CONTROL);
            Keyboard.Press(VirtualKeyShort.KEY_V);
            Keyboard.Release(VirtualKeyShort.CONTROL);
            Mouse.Click(new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)));
            WriteLog("发送成功。");
            Thread.Sleep(500);
        }

        private int GetProcessId(string processName)
        {
            Process[] target = Process.GetProcessesByName(processName);
            if(target != null && target.Length > 0)
            {
                return target[0].Id;
            }
            return 0;
        }

        private void WriteLog(string log)
        {
            TextBox_Log.AppendText(string.Format("{0}  {1}{2}", DateTime.Now.ToString("HH:mm:ss"), log, Environment.NewLine));
        }

        /// <summary>
        /// 获取指定窗口是否可见（存在）
        /// </summary>
        /// <param name="windowName">窗口名称</param>
        /// <returns></returns>
        private bool GetWindowVisibleStatus(string windowName)
        {
            var desktop = new UIA3Automation().GetDesktop();
            if (desktop == null)
            {
                desktop = new UIA3Automation().GetDesktop();
            }
            var element = desktop.FindFirstDescendant(cf => cf.ByName(windowName));
            return !(element == null);
        }

        /// <summary>
        /// 通过名称获取微信的按钮
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        private AutomationElement GetWeChatControl(in Window wechatWindow, string controlName, ControlType controlType)
        {
            if (wechatWindow == null)
            {
                return null;
            }
            wechatWindow.Focus();
            var elements = wechatWindow.FindAllDescendants();
            foreach (var element in elements)
            {
                if (element.Name == controlName && element.ControlType == controlType)
                {
                    return element;
                }

            }
            return null;
        }

        private void Btn_FindFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "微信主程序(wechat.exe)|*.exe";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TextBox_AppPath.Text = openFileDialog.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(File.Exists(appDefaultPath))
            {
                TextBox_AppPath.Text = appDefaultPath;
            }
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker1.Enabled = CheckBox_ScheduleTask.Checked;
        }

        private void CheckBox_ScheduleTask_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = CheckBox_ScheduleTask.Checked;
            if(CheckBox_ScheduleTask.Checked)
            {
                Button_SendMessage.Text = "开始任务";
            }
            else
            {
                Button_SendMessage.Text = "发送消息";
            }
        }

        private void StartProgress()
        {
            timer1.Enabled = false;
            Button_SendMessage.Text = "发送";
            var app = GetWeChatApplication();
            if (app == null)
            {
                WriteLog("没有获取到微信的主程序。");
                return;
            }
            var wechatMainWindow = GetWeChatMainWindow(ref app);
            wechatMainWindow.Focus();
            if (wechatMainWindow == null)
            {
                WriteLog("没有获取到微信的主窗口");
                return;
            }
            var chatBox = SearchTarget(wechatMainWindow, TextBox_SendTarget.Text);
            if (chatBox == null)
            {
                WriteLog("没有获取到消息的输入框");
                return;
            }
            SendMessage(wechatMainWindow, chatBox, TextBox_Message.Text);
        }

        private void Button_SendMessage_Click(object sender, EventArgs e)
        {
            if(timer1.Enabled)
            {
                timer1.Enabled = false;
                WriteLog("停止计划任务。");
                Button_SendMessage.Text = "发送";
            }
            if (CheckBox_ScheduleTask.Checked)
            {
                timer1.Enabled = true;
                WriteLog("开始执行定时任务。");
                Button_SendMessage.Text = "执行中";
            }
            else
            {
                StartProgress();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value.Date == DateTime.Now.Date &&
                dateTimePicker1.Value.Hour == DateTime.Now.Hour &&
                dateTimePicker1.Value.Minute == DateTime.Now.Minute &&
                dateTimePicker1.Value.Second == DateTime.Now.Second)
            {
                WriteLog($"到达指定时间：{dateTimePicker1.Value}，开始执行定时任务。");
                StartProgress();
            }
        }
    }
}

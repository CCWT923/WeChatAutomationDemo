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

namespace WeChatAutomationDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Application WeChatApp = null;
        Window Wnd_WeChat = null;
        AutomationElement Desktop;
        private void button1_Click(object sender, EventArgs e)
        {
            if(WeChatApp == null)
            {
                WriteLog("正在尝试连接到微信主程序。");
                var targetId = GetProcessId("WeChat");
                if(targetId == 0)
                {
                    WriteLog("没有找到微信的进程，请确定微信是否启动！");
                    return;
                }
                WeChatApp = Application.Attach(targetId);
                WriteLog("连接微信成功。");
                /**
                 * 微信，如果隐藏在托盘区域的话（非最小化状态），那么试图获取主窗口会一直卡住，直到手动点击微信
                 * 图标将其窗口显示出来，程序才能正常运行。
                 * 可以通过查找桌面的子元素，看看是否有微信的窗口来判断其是否隐藏在托盘区
                 * 
                 */
                Desktop = new UIA3Automation().GetDesktop();
            }
            WriteLog("正在激活微信。");
            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
            //查找「通知V形」按钮并单击
            var btn_NotifyMore = Desktop.FindFirstDescendant(cf.ByAutomationId("1502")).AsButton();
            btn_NotifyMore.Click();
            
            AutomationElement btn_WeChat_TrayButton = null;
            AutomationElement Panel_NotifyOverFlowArea = null;
            Retry.WhileNull(() =>
            {
                Panel_NotifyOverFlowArea = Desktop.FindFirstDescendant(cf.ByName("通知溢出")).AsWindow();
                return Panel_NotifyOverFlowArea;
            }, TimeSpan.FromSeconds(5), null, true, false, "没有找到「通知溢出」的按钮。");
            //查找并点击托盘区域微信的图标
            //微信按钮：{DDC66FF1-5768-0E74-ABB5-9025FBC4CFDA}
            btn_WeChat_TrayButton = Panel_NotifyOverFlowArea.FindFirstDescendant(cf.ByAutomationId("{DDC66FF1-5768-0E74-ABB5-9025FBC4CFDA}")).AsButton();
            btn_WeChat_TrayButton.Click();
            Thread.Sleep(300);
            btn_NotifyMore.Click();
            //微信主窗口
            Wnd_WeChat = WeChatApp.GetMainWindow(new UIA3Automation());
            if(GetWindowVisibleStatus("微信"))
            {
                WriteLog("微信窗口已激活。");
            }
            Wnd_WeChat.Focus();
            //获取并单击「聊天」按钮，如果直接将其 AsButton() 单击的话，会出现找不到点击位置的异常
            //所以这里获取按钮的矩形，然后点击其中间
            WriteLog("激活「聊天」界面。");
            var Btn_Chat = GetWeChatControl("聊天", ControlType.Button);
            if(Btn_Chat == null)
            {
                WriteLog("没有获取到微信「聊天」按钮。");
                return;
            }
            var rect = Btn_Chat.BoundingRectangle;
            Mouse.Click(new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)));
            Thread.Sleep(500);
            string targetChatObject = "文件传输助手";
            WriteLog("搜索指定内容：" + targetChatObject);
            var edit_SearchChatObject = GetWeChatControl("搜索", ControlType.Edit);
            if(edit_SearchChatObject == null)
            {
                WriteLog("没有找到指定的控件：搜索框。");
                return;
            }
            rect = edit_SearchChatObject.BoundingRectangle;
            Mouse.Click(new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)));
            edit_SearchChatObject.AsTextBox().Enter(targetChatObject);
            Thread.Sleep(1000);
            Keyboard.Press(VirtualKeyShort.ENTER);
            //查找发送消息的文本框
            WriteLog("查找消息发送编辑框，并输入文字。");
            var edit_MessageInput = GetWeChatControl("输入", ControlType.Edit);
            if(edit_MessageInput == null)
            {
                WriteLog("没有找到消息输入的控件。");
                return;
            }
            string messageToSend = "";//string.Format("{1}    {2}",DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.FFF"),"UI Automation for WeChat(By:WuTao).");
            rect = edit_MessageInput.BoundingRectangle;
            Mouse.Click(new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)));
            Thread.Sleep(500);

            //查找「发送」按钮，并单击发送消息
            var btn_SendMessage = GetWeChatControl("发送(S)", ControlType.Button);
            if (btn_SendMessage == null)
            {
                WriteLog("没有找到「发送消息」的按钮。");
                return;
            }
            rect = btn_SendMessage.BoundingRectangle;
            //这里如果是中文输入法的话，输入英文可能出现问题，这里暂时使用粘贴方式解决
            //edit_MessageInput.AsTextBox().Enter(messageToSend);
            for (int i = 0; i < 10; i++)
            {
                messageToSend = string.Format("{0}   {1}    {2}",i+1, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.FFF"), "UI Automation for WeChat(By:WuTao).");
                Clipboard.SetText(messageToSend);
                Keyboard.Pressing(VirtualKeyShort.CONTROL);
                Keyboard.Press(VirtualKeyShort.KEY_V);
                Keyboard.Release(VirtualKeyShort.CONTROL);
                Mouse.Click(new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)));
                Thread.Sleep(500);
            }

            WriteLog("消息发送成功，关闭微信窗口");
            Wnd_WeChat.Close();
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
            textBox1.AppendText(string.Format("{0}\t{1}{2}", DateTime.Now.ToString("HH:mm:ss"), log, Environment.NewLine));
        }

        /// <summary>
        /// 获取指定窗口是否可见（存在）
        /// </summary>
        /// <param name="windowName">窗口名称</param>
        /// <returns></returns>
        private bool GetWindowVisibleStatus(string windowName)
        {
            if(Desktop == null)
            {
                Desktop = new UIA3Automation().GetDesktop();
            }
            var element = Desktop.FindFirstDescendant(cf => cf.ByName(windowName));
            return !(element == null);
        }

        private void Btn_WeChatButtonTest_Click(object sender, EventArgs e)
        {
            if(Wnd_WeChat == null)
            {
                WriteLog("请先激活微信。");
                return;
            }
            Wnd_WeChat.Focus();
            var elements = Wnd_WeChat.FindAllDescendants();
            foreach (var element in elements)
            {
                if(element.ControlType == FlaUI.Core.Definitions.ControlType.Button)
                {
                    //element.DrawHighlight();
                    WriteLog("微信按钮：" + element.Name);
                    Thread.Sleep(100);
                }
            }
        }

        /// <summary>
        /// 通过名称获取微信的按钮
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        private AutomationElement GetWeChatControl(string controlName, ControlType controlType)
        {
            if (Wnd_WeChat == null)
            {
                return null;
            }
            Wnd_WeChat.Focus();
            var elements = Wnd_WeChat.FindAllDescendants();
            foreach (var element in elements)
            {
                if (element.Name == controlName && element.ControlType == controlType)
                {
                    return element;
                }

            }
            return null;
        }
    }
}

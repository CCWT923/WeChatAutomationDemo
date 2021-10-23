
namespace WeChatAutomationDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_ActiveWeChat = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Btn_WeChatButtonTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_ActiveWeChat
            // 
            this.Btn_ActiveWeChat.Location = new System.Drawing.Point(12, 12);
            this.Btn_ActiveWeChat.Name = "Btn_ActiveWeChat";
            this.Btn_ActiveWeChat.Size = new System.Drawing.Size(137, 56);
            this.Btn_ActiveWeChat.TabIndex = 1;
            this.Btn_ActiveWeChat.Text = "发送微信消息";
            this.Btn_ActiveWeChat.UseVisualStyleBackColor = true;
            this.Btn_ActiveWeChat.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(197, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(591, 426);
            this.textBox1.TabIndex = 2;
            // 
            // Btn_WeChatButtonTest
            // 
            this.Btn_WeChatButtonTest.Location = new System.Drawing.Point(12, 103);
            this.Btn_WeChatButtonTest.Name = "Btn_WeChatButtonTest";
            this.Btn_WeChatButtonTest.Size = new System.Drawing.Size(137, 56);
            this.Btn_WeChatButtonTest.TabIndex = 1;
            this.Btn_WeChatButtonTest.Text = "测试微信按钮";
            this.Btn_WeChatButtonTest.UseVisualStyleBackColor = true;
            this.Btn_WeChatButtonTest.Click += new System.EventHandler(this.Btn_WeChatButtonTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Btn_WeChatButtonTest);
            this.Controls.Add(this.Btn_ActiveWeChat);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Btn_ActiveWeChat;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Btn_WeChatButtonTest;
    }
}


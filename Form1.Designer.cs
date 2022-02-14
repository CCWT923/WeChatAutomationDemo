
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
            this.components = new System.ComponentModel.Container();
            this.TextBox_Log = new System.Windows.Forms.TextBox();
            this.TextBox_AppPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_FindFile = new System.Windows.Forms.Button();
            this.TextBox_SendTarget = new System.Windows.Forms.TextBox();
            this.TextBox_Message = new System.Windows.Forms.TextBox();
            this.Button_SendMessage = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.CheckBox_ScheduleTask = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox_Log
            // 
            this.TextBox_Log.Location = new System.Drawing.Point(12, 258);
            this.TextBox_Log.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_Log.Multiline = true;
            this.TextBox_Log.Name = "TextBox_Log";
            this.TextBox_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Log.Size = new System.Drawing.Size(571, 94);
            this.TextBox_Log.TabIndex = 2;
            // 
            // TextBox_AppPath
            // 
            this.TextBox_AppPath.Location = new System.Drawing.Point(82, 12);
            this.TextBox_AppPath.Name = "TextBox_AppPath";
            this.TextBox_AppPath.Size = new System.Drawing.Size(384, 21);
            this.TextBox_AppPath.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "微信位置：";
            // 
            // Btn_FindFile
            // 
            this.Btn_FindFile.Location = new System.Drawing.Point(486, 10);
            this.Btn_FindFile.Name = "Btn_FindFile";
            this.Btn_FindFile.Size = new System.Drawing.Size(75, 23);
            this.Btn_FindFile.TabIndex = 5;
            this.Btn_FindFile.Text = "浏览";
            this.Btn_FindFile.UseVisualStyleBackColor = true;
            this.Btn_FindFile.Click += new System.EventHandler(this.Btn_FindFile_Click);
            // 
            // TextBox_SendTarget
            // 
            this.TextBox_SendTarget.Location = new System.Drawing.Point(79, 31);
            this.TextBox_SendTarget.Name = "TextBox_SendTarget";
            this.TextBox_SendTarget.Size = new System.Drawing.Size(383, 21);
            this.TextBox_SendTarget.TabIndex = 6;
            this.TextBox_SendTarget.Text = "文件传输助手";
            // 
            // TextBox_Message
            // 
            this.TextBox_Message.Location = new System.Drawing.Point(79, 68);
            this.TextBox_Message.Multiline = true;
            this.TextBox_Message.Name = "TextBox_Message";
            this.TextBox_Message.Size = new System.Drawing.Size(384, 98);
            this.TextBox_Message.TabIndex = 6;
            this.TextBox_Message.Text = "测试内容";
            // 
            // Button_SendMessage
            // 
            this.Button_SendMessage.Location = new System.Drawing.Point(474, 24);
            this.Button_SendMessage.Margin = new System.Windows.Forms.Padding(2);
            this.Button_SendMessage.Name = "Button_SendMessage";
            this.Button_SendMessage.Size = new System.Drawing.Size(97, 32);
            this.Button_SendMessage.TabIndex = 1;
            this.Button_SendMessage.Text = "发送消息";
            this.Button_SendMessage.UseVisualStyleBackColor = true;
            this.Button_SendMessage.Click += new System.EventHandler(this.Button_SendMessage_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "发送目标：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "消息内容：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.CheckBox_ScheduleTask);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TextBox_Message);
            this.groupBox1.Controls.Add(this.Button_SendMessage);
            this.groupBox1.Controls.Add(this.TextBox_SendTarget);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(576, 214);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(157, 181);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(172, 21);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // CheckBox_ScheduleTask
            // 
            this.CheckBox_ScheduleTask.AutoSize = true;
            this.CheckBox_ScheduleTask.Location = new System.Drawing.Point(79, 181);
            this.CheckBox_ScheduleTask.Name = "CheckBox_ScheduleTask";
            this.CheckBox_ScheduleTask.Size = new System.Drawing.Size(72, 16);
            this.CheckBox_ScheduleTask.TabIndex = 7;
            this.CheckBox_ScheduleTask.Text = "定时任务";
            this.CheckBox_ScheduleTask.UseVisualStyleBackColor = true;
            this.CheckBox_ScheduleTask.CheckedChanged += new System.EventHandler(this.CheckBox_ScheduleTask_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Btn_FindFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBox_AppPath);
            this.Controls.Add(this.TextBox_Log);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBox_Log;
        private System.Windows.Forms.TextBox TextBox_AppPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_FindFile;
        private System.Windows.Forms.TextBox TextBox_SendTarget;
        private System.Windows.Forms.TextBox TextBox_Message;
        private System.Windows.Forms.Button Button_SendMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox CheckBox_ScheduleTask;
        private System.Windows.Forms.Timer timer1;
    }
}


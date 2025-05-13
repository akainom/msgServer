namespace ClientApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            UserNameTextBox = new TextBox();
            label2 = new Label();
            EnteredMessageTextBox = new TextBox();
            label3 = new Label();
            DestinationSourceTextBox = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            RSAKeyLabel = new Label();
            DHKeyLabel = new Label();
            AESKeyLabel = new Label();
            SendToServerButton = new Button();
            dataGridView1 = new DataGridView();
            label7 = new Label();
            UpdateStatusButton = new Button();
            EncryptedMessageLabel = new Label();
            label9 = new Label();
            RegisterButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 11);
            label1.Name = "label1";
            label1.Size = new Size(114, 20);
            label1.TabIndex = 0;
            label1.Text = "Пользователь :";
            // 
            // UserNameTextBox
            // 
            UserNameTextBox.Location = new Point(176, 11);
            UserNameTextBox.Margin = new Padding(3, 4, 3, 4);
            UserNameTextBox.Name = "UserNameTextBox";
            UserNameTextBox.Size = new Size(100, 27);
            UserNameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 55);
            label2.Name = "label2";
            label2.Size = new Size(98, 20);
            label2.TabIndex = 2;
            label2.Text = "Сообщение :";
            // 
            // EnteredMessageTextBox
            // 
            EnteredMessageTextBox.Location = new Point(176, 55);
            EnteredMessageTextBox.Margin = new Padding(3, 4, 3, 4);
            EnteredMessageTextBox.Name = "EnteredMessageTextBox";
            EnteredMessageTextBox.Size = new Size(100, 27);
            EnteredMessageTextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 95);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 4;
            label3.Text = "Кому :";
            // 
            // DestinationSourceTextBox
            // 
            DestinationSourceTextBox.Location = new Point(176, 95);
            DestinationSourceTextBox.Margin = new Padding(3, 4, 3, 4);
            DestinationSourceTextBox.Name = "DestinationSourceTextBox";
            DestinationSourceTextBox.Size = new Size(100, 27);
            DestinationSourceTextBox.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(456, 102);
            label4.Name = "label4";
            label4.Size = new Size(79, 20);
            label4.TabIndex = 10;
            label4.Text = "AES-ключ:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(456, 62);
            label5.Name = "label5";
            label5.Size = new Size(75, 20);
            label5.TabIndex = 8;
            label5.Text = "DH-ключ:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(456, 19);
            label6.Name = "label6";
            label6.Size = new Size(84, 20);
            label6.TabIndex = 6;
            label6.Text = "RSA-ключ :";
            // 
            // RSAKeyLabel
            // 
            RSAKeyLabel.AutoSize = true;
            RSAKeyLabel.Location = new Point(540, 19);
            RSAKeyLabel.Name = "RSAKeyLabel";
            RSAKeyLabel.Size = new Size(0, 20);
            RSAKeyLabel.TabIndex = 11;
            // 
            // DHKeyLabel
            // 
            DHKeyLabel.AutoSize = true;
            DHKeyLabel.Location = new Point(540, 62);
            DHKeyLabel.Name = "DHKeyLabel";
            DHKeyLabel.Size = new Size(0, 20);
            DHKeyLabel.TabIndex = 12;
            // 
            // AESKeyLabel
            // 
            AESKeyLabel.AutoSize = true;
            AESKeyLabel.Location = new Point(540, 102);
            AESKeyLabel.Name = "AESKeyLabel";
            AESKeyLabel.Size = new Size(0, 20);
            AESKeyLabel.TabIndex = 13;
            // 
            // SendToServerButton
            // 
            SendToServerButton.Location = new Point(223, 180);
            SendToServerButton.Margin = new Padding(3, 4, 3, 4);
            SendToServerButton.Name = "SendToServerButton";
            SendToServerButton.Size = new Size(176, 29);
            SendToServerButton.TabIndex = 14;
            SendToServerButton.Text = "Отправить на сервер";
            SendToServerButton.UseVisualStyleBackColor = true;
            SendToServerButton.Click += SendToServerButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(-2, 274);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(798, 285);
            dataGridView1.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(22, 250);
            label7.Name = "label7";
            label7.Size = new Size(170, 20);
            label7.TabIndex = 16;
            label7.Text = "Входящие сообщения :";
            // 
            // UpdateStatusButton
            // 
            UpdateStatusButton.Location = new Point(285, 241);
            UpdateStatusButton.Margin = new Padding(3, 4, 3, 4);
            UpdateStatusButton.Name = "UpdateStatusButton";
            UpdateStatusButton.Size = new Size(89, 29);
            UpdateStatusButton.TabIndex = 17;
            UpdateStatusButton.Text = "Обновить";
            UpdateStatusButton.UseVisualStyleBackColor = true;
            UpdateStatusButton.Click += UpdateStatusButton_Click;
            // 
            // EncryptedMessageLabel
            // 
            EncryptedMessageLabel.AutoSize = true;
            EncryptedMessageLabel.Location = new Point(173, 126);
            EncryptedMessageLabel.Name = "EncryptedMessageLabel";
            EncryptedMessageLabel.Size = new Size(0, 20);
            EncryptedMessageLabel.TabIndex = 19;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(22, 126);
            label9.Name = "label9";
            label9.Size = new Size(146, 20);
            label9.TabIndex = 18;
            label9.Text = "Шифрованный вид:";
            label9.Click += label9_Click;
            // 
            // RegisterButton
            // 
            RegisterButton.Font = new Font("Microsoft Sans Serif", 7.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            RegisterButton.Location = new Point(285, 6);
            RegisterButton.Margin = new Padding(3, 4, 3, 4);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new Size(149, 29);
            RegisterButton.TabIndex = 20;
            RegisterButton.Text = "Зарегистрировать";
            RegisterButton.UseVisualStyleBackColor = true;
            RegisterButton.Click += RegisterButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 562);
            Controls.Add(RegisterButton);
            Controls.Add(EncryptedMessageLabel);
            Controls.Add(label9);
            Controls.Add(UpdateStatusButton);
            Controls.Add(label7);
            Controls.Add(dataGridView1);
            Controls.Add(SendToServerButton);
            Controls.Add(AESKeyLabel);
            Controls.Add(DHKeyLabel);
            Controls.Add(RSAKeyLabel);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(DestinationSourceTextBox);
            Controls.Add(label3);
            Controls.Add(EnteredMessageTextBox);
            Controls.Add(label2);
            Controls.Add(UserNameTextBox);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EnteredMessageTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DestinationSourceTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label RSAKeyLabel;
        private System.Windows.Forms.Label DHKeyLabel;
        private System.Windows.Forms.Label AESKeyLabel;
        private System.Windows.Forms.Button SendToServerButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button UpdateStatusButton;
        private System.Windows.Forms.Label EncryptedMessageLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button RegisterButton;
    }
}


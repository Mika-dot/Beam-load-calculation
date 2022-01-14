
namespace EngineeringProjectV3
{
    public partial class BeamCalculation
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BeamCalculation));
            this.LateraForce = new System.Windows.Forms.Button();
            this.BendingMoment = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.LateralForces2 = new System.Windows.Forms.TabPage();
            this.ShearForcePlot = new ZedGraph.ZedGraphControl();
            this.BendingMoment2 = new System.Windows.Forms.TabPage();
            this.BendingMomentPlot = new ZedGraph.ZedGraphControl();
            this.DistributedForceNM = new System.Windows.Forms.Label();
            this.StrengthOneN = new System.Windows.Forms.Label();
            this.StrengthTwoN = new System.Windows.Forms.Label();
            this.LengthM = new System.Windows.Forms.Label();
            this.Distance = new System.Windows.Forms.Label();
            this.StrengthOneM = new System.Windows.Forms.Label();
            this.StrengthTwoM = new System.Windows.Forms.Label();
            this.StartDistributedLoadM = new System.Windows.Forms.Label();
            this.EndOfDistributedLoadM = new System.Windows.Forms.Label();
            this.MeaningDistributedForceNM = new System.Windows.Forms.NumericUpDown();
            this.MeaningStrengthOneN = new System.Windows.Forms.NumericUpDown();
            this.MeaningStrengthTwoN = new System.Windows.Forms.NumericUpDown();
            this.MeaningLengthM = new System.Windows.Forms.NumericUpDown();
            this.MeaningStrengthOneM = new System.Windows.Forms.NumericUpDown();
            this.MeaningStrengthTwoM = new System.Windows.Forms.NumericUpDown();
            this.MeaningStartDistributedLoadM = new System.Windows.Forms.NumericUpDown();
            this.MeaningEndOfDistributedLoadM = new System.Windows.Forms.NumericUpDown();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.YB = new System.Windows.Forms.TextBox();
            this.YA = new System.Windows.Forms.TextBox();
            this.YbText = new System.Windows.Forms.Label();
            this.YaText = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.LateralForces2.SuspendLayout();
            this.BendingMoment2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningDistributedForceNM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningStrengthOneN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningStrengthTwoN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningLengthM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningStrengthOneM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningStrengthTwoM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningStartDistributedLoadM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningEndOfDistributedLoadM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // LateraForce
            // 
            this.LateraForce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LateraForce.Location = new System.Drawing.Point(10, 12);
            this.LateraForce.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LateraForce.Name = "LateraForce";
            this.LateraForce.Size = new System.Drawing.Size(262, 32);
            this.LateraForce.TabIndex = 0;
            this.LateraForce.Text = "Эпюра поперечной  силы - Q";
            this.LateraForce.UseVisualStyleBackColor = true;
            this.LateraForce.Click += new System.EventHandler(this.LateraForce_Click);
            // 
            // BendingMoment
            // 
            this.BendingMoment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BendingMoment.Location = new System.Drawing.Point(10, 50);
            this.BendingMoment.Name = "BendingMoment";
            this.BendingMoment.Size = new System.Drawing.Size(262, 32);
            this.BendingMoment.TabIndex = 1;
            this.BendingMoment.Text = "Эпюра изгибающего момента - М";
            this.BendingMoment.UseVisualStyleBackColor = true;
            this.BendingMoment.Click += new System.EventHandler(this.BendingMoment_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.LateralForces2);
            this.tabControl1.Controls.Add(this.BendingMoment2);
            this.tabControl1.Location = new System.Drawing.Point(278, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(569, 603);
            this.tabControl1.TabIndex = 2;
            // 
            // LateralForces2
            // 
            this.LateralForces2.Controls.Add(this.ShearForcePlot);
            this.LateralForces2.Location = new System.Drawing.Point(4, 29);
            this.LateralForces2.Name = "LateralForces2";
            this.LateralForces2.Padding = new System.Windows.Forms.Padding(3);
            this.LateralForces2.Size = new System.Drawing.Size(561, 570);
            this.LateralForces2.TabIndex = 0;
            this.LateralForces2.Text = "Lateral Forces2";
            this.LateralForces2.UseVisualStyleBackColor = true;
            // 
            // ShearForcePlot
            // 
            this.ShearForcePlot.Location = new System.Drawing.Point(-4, -4);
            this.ShearForcePlot.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ShearForcePlot.Name = "ShearForcePlot";
            this.ShearForcePlot.ScrollGrace = 0D;
            this.ShearForcePlot.ScrollMaxX = 0D;
            this.ShearForcePlot.ScrollMaxY = 0D;
            this.ShearForcePlot.ScrollMaxY2 = 0D;
            this.ShearForcePlot.ScrollMinX = 0D;
            this.ShearForcePlot.ScrollMinY = 0D;
            this.ShearForcePlot.ScrollMinY2 = 0D;
            this.ShearForcePlot.Size = new System.Drawing.Size(569, 578);
            this.ShearForcePlot.TabIndex = 0;
            this.ShearForcePlot.UseExtendedPrintDialog = true;
            // 
            // BendingMoment2
            // 
            this.BendingMoment2.Controls.Add(this.BendingMomentPlot);
            this.BendingMoment2.Location = new System.Drawing.Point(4, 29);
            this.BendingMoment2.Name = "BendingMoment2";
            this.BendingMoment2.Padding = new System.Windows.Forms.Padding(3);
            this.BendingMoment2.Size = new System.Drawing.Size(561, 570);
            this.BendingMoment2.TabIndex = 1;
            this.BendingMoment2.Text = "BendingMoment2";
            this.BendingMoment2.UseVisualStyleBackColor = true;
            // 
            // BendingMomentPlot
            // 
            this.BendingMomentPlot.Location = new System.Drawing.Point(-8, -4);
            this.BendingMomentPlot.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.BendingMomentPlot.Name = "BendingMomentPlot";
            this.BendingMomentPlot.ScrollGrace = 0D;
            this.BendingMomentPlot.ScrollMaxX = 0D;
            this.BendingMomentPlot.ScrollMaxY = 0D;
            this.BendingMomentPlot.ScrollMaxY2 = 0D;
            this.BendingMomentPlot.ScrollMinX = 0D;
            this.BendingMomentPlot.ScrollMinY = 0D;
            this.BendingMomentPlot.ScrollMinY2 = 0D;
            this.BendingMomentPlot.Size = new System.Drawing.Size(573, 578);
            this.BendingMomentPlot.TabIndex = 0;
            this.BendingMomentPlot.UseExtendedPrintDialog = true;
            // 
            // DistributedForceNM
            // 
            this.DistributedForceNM.AutoSize = true;
            this.DistributedForceNM.Location = new System.Drawing.Point(12, 98);
            this.DistributedForceNM.Name = "DistributedForceNM";
            this.DistributedForceNM.Size = new System.Drawing.Size(143, 20);
            this.DistributedForceNM.TabIndex = 3;
            this.DistributedForceNM.Text = "Distributed force (N*M)";
            // 
            // StrengthOneN
            // 
            this.StrengthOneN.AutoSize = true;
            this.StrengthOneN.Location = new System.Drawing.Point(10, 130);
            this.StrengthOneN.Name = "StrengthOneN";
            this.StrengthOneN.Size = new System.Drawing.Size(106, 20);
            this.StrengthOneN.TabIndex = 4;
            this.StrengthOneN.Text = "Strength one (N)";
            // 
            // StrengthTwoN
            // 
            this.StrengthTwoN.AutoSize = true;
            this.StrengthTwoN.Location = new System.Drawing.Point(10, 164);
            this.StrengthTwoN.Name = "StrengthTwoN";
            this.StrengthTwoN.Size = new System.Drawing.Size(103, 20);
            this.StrengthTwoN.TabIndex = 7;
            this.StrengthTwoN.Text = "Strength two (N)";
            // 
            // LengthM
            // 
            this.LengthM.AutoSize = true;
            this.LengthM.Location = new System.Drawing.Point(10, 197);
            this.LengthM.Name = "LengthM";
            this.LengthM.Size = new System.Drawing.Size(72, 20);
            this.LengthM.TabIndex = 8;
            this.LengthM.Text = "Length (M)";
            // 
            // Distance
            // 
            this.Distance.AutoSize = true;
            this.Distance.Location = new System.Drawing.Point(95, 231);
            this.Distance.Name = "Distance";
            this.Distance.Size = new System.Drawing.Size(60, 20);
            this.Distance.TabIndex = 9;
            this.Distance.Text = "Distance";
            // 
            // StrengthOneM
            // 
            this.StrengthOneM.AutoSize = true;
            this.StrengthOneM.Location = new System.Drawing.Point(10, 269);
            this.StrengthOneM.Name = "StrengthOneM";
            this.StrengthOneM.Size = new System.Drawing.Size(108, 20);
            this.StrengthOneM.TabIndex = 10;
            this.StrengthOneM.Text = "Strength one (M)";
            // 
            // StrengthTwoM
            // 
            this.StrengthTwoM.AutoSize = true;
            this.StrengthTwoM.Location = new System.Drawing.Point(12, 301);
            this.StrengthTwoM.Name = "StrengthTwoM";
            this.StrengthTwoM.Size = new System.Drawing.Size(105, 20);
            this.StrengthTwoM.TabIndex = 11;
            this.StrengthTwoM.Text = "Strength two (M)";
            // 
            // StartDistributedLoadM
            // 
            this.StartDistributedLoadM.AutoSize = true;
            this.StartDistributedLoadM.Location = new System.Drawing.Point(12, 333);
            this.StartDistributedLoadM.Name = "StartDistributedLoadM";
            this.StartDistributedLoadM.Size = new System.Drawing.Size(154, 20);
            this.StartDistributedLoadM.TabIndex = 12;
            this.StartDistributedLoadM.Text = "Start distributed load (M)";
            // 
            // EndOfDistributedLoadM
            // 
            this.EndOfDistributedLoadM.AutoSize = true;
            this.EndOfDistributedLoadM.Location = new System.Drawing.Point(12, 365);
            this.EndOfDistributedLoadM.Name = "EndOfDistributedLoadM";
            this.EndOfDistributedLoadM.Size = new System.Drawing.Size(167, 20);
            this.EndOfDistributedLoadM.TabIndex = 13;
            this.EndOfDistributedLoadM.Text = "End of distributed load (M)";
            // 
            // MeaningDistributedForceNM
            // 
            this.MeaningDistributedForceNM.DecimalPlaces = 3;
            this.MeaningDistributedForceNM.Location = new System.Drawing.Point(192, 98);
            this.MeaningDistributedForceNM.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.MeaningDistributedForceNM.Name = "MeaningDistributedForceNM";
            this.MeaningDistributedForceNM.Size = new System.Drawing.Size(80, 26);
            this.MeaningDistributedForceNM.TabIndex = 14;
            this.MeaningDistributedForceNM.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // MeaningStrengthOneN
            // 
            this.MeaningStrengthOneN.DecimalPlaces = 3;
            this.MeaningStrengthOneN.Location = new System.Drawing.Point(192, 128);
            this.MeaningStrengthOneN.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.MeaningStrengthOneN.Name = "MeaningStrengthOneN";
            this.MeaningStrengthOneN.Size = new System.Drawing.Size(80, 26);
            this.MeaningStrengthOneN.TabIndex = 15;
            this.MeaningStrengthOneN.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // MeaningStrengthTwoN
            // 
            this.MeaningStrengthTwoN.DecimalPlaces = 3;
            this.MeaningStrengthTwoN.Location = new System.Drawing.Point(192, 160);
            this.MeaningStrengthTwoN.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.MeaningStrengthTwoN.Name = "MeaningStrengthTwoN";
            this.MeaningStrengthTwoN.Size = new System.Drawing.Size(80, 26);
            this.MeaningStrengthTwoN.TabIndex = 16;
            this.MeaningStrengthTwoN.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // MeaningLengthM
            // 
            this.MeaningLengthM.DecimalPlaces = 3;
            this.MeaningLengthM.Location = new System.Drawing.Point(192, 192);
            this.MeaningLengthM.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.MeaningLengthM.Name = "MeaningLengthM";
            this.MeaningLengthM.Size = new System.Drawing.Size(80, 26);
            this.MeaningLengthM.TabIndex = 17;
            this.MeaningLengthM.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // MeaningStrengthOneM
            // 
            this.MeaningStrengthOneM.DecimalPlaces = 3;
            this.MeaningStrengthOneM.Location = new System.Drawing.Point(192, 269);
            this.MeaningStrengthOneM.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.MeaningStrengthOneM.Name = "MeaningStrengthOneM";
            this.MeaningStrengthOneM.Size = new System.Drawing.Size(80, 26);
            this.MeaningStrengthOneM.TabIndex = 18;
            this.MeaningStrengthOneM.Value = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            // 
            // MeaningStrengthTwoM
            // 
            this.MeaningStrengthTwoM.DecimalPlaces = 3;
            this.MeaningStrengthTwoM.Location = new System.Drawing.Point(192, 301);
            this.MeaningStrengthTwoM.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.MeaningStrengthTwoM.Name = "MeaningStrengthTwoM";
            this.MeaningStrengthTwoM.Size = new System.Drawing.Size(80, 26);
            this.MeaningStrengthTwoM.TabIndex = 19;
            this.MeaningStrengthTwoM.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            // 
            // MeaningStartDistributedLoadM
            // 
            this.MeaningStartDistributedLoadM.DecimalPlaces = 3;
            this.MeaningStartDistributedLoadM.Location = new System.Drawing.Point(192, 333);
            this.MeaningStartDistributedLoadM.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.MeaningStartDistributedLoadM.Name = "MeaningStartDistributedLoadM";
            this.MeaningStartDistributedLoadM.Size = new System.Drawing.Size(80, 26);
            this.MeaningStartDistributedLoadM.TabIndex = 20;
            this.MeaningStartDistributedLoadM.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // MeaningEndOfDistributedLoadM
            // 
            this.MeaningEndOfDistributedLoadM.DecimalPlaces = 3;
            this.MeaningEndOfDistributedLoadM.Location = new System.Drawing.Point(192, 365);
            this.MeaningEndOfDistributedLoadM.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.MeaningEndOfDistributedLoadM.Name = "MeaningEndOfDistributedLoadM";
            this.MeaningEndOfDistributedLoadM.Size = new System.Drawing.Size(80, 26);
            this.MeaningEndOfDistributedLoadM.TabIndex = 21;
            this.MeaningEndOfDistributedLoadM.Value = new decimal(new int[] {
            6,
            0,
            0,
            65536});
            // 
            // Picture
            // 
            this.Picture.Image = ((System.Drawing.Image)(resources.GetObject("Picture.Image")));
            this.Picture.Location = new System.Drawing.Point(12, 432);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(258, 183);
            this.Picture.TabIndex = 22;
            this.Picture.TabStop = false;
            this.Picture.Click += new System.EventHandler(this.Picture_Click);
            // 
            // YB
            // 
            this.YB.Location = new System.Drawing.Point(192, 397);
            this.YB.Name = "YB";
            this.YB.ReadOnly = true;
            this.YB.Size = new System.Drawing.Size(78, 26);
            this.YB.TabIndex = 23;
            // 
            // YA
            // 
            this.YA.Location = new System.Drawing.Point(56, 397);
            this.YA.Name = "YA";
            this.YA.ReadOnly = true;
            this.YA.Size = new System.Drawing.Size(78, 26);
            this.YA.TabIndex = 24;
            // 
            // YbText
            // 
            this.YbText.AutoSize = true;
            this.YbText.Location = new System.Drawing.Point(153, 397);
            this.YbText.Name = "YbText";
            this.YbText.Size = new System.Drawing.Size(26, 20);
            this.YbText.TabIndex = 25;
            this.YbText.Text = "Yb";
            // 
            // YaText
            // 
            this.YaText.AutoSize = true;
            this.YaText.Location = new System.Drawing.Point(12, 397);
            this.YaText.Name = "YaText";
            this.YaText.Size = new System.Drawing.Size(25, 20);
            this.YaText.TabIndex = 26;
            this.YaText.Text = "Ya";
            // 
            // BeamCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(856, 627);
            this.Controls.Add(this.YaText);
            this.Controls.Add(this.YbText);
            this.Controls.Add(this.YA);
            this.Controls.Add(this.YB);
            this.Controls.Add(this.Picture);
            this.Controls.Add(this.MeaningEndOfDistributedLoadM);
            this.Controls.Add(this.MeaningStartDistributedLoadM);
            this.Controls.Add(this.MeaningStrengthTwoM);
            this.Controls.Add(this.MeaningStrengthOneM);
            this.Controls.Add(this.MeaningLengthM);
            this.Controls.Add(this.MeaningStrengthTwoN);
            this.Controls.Add(this.MeaningStrengthOneN);
            this.Controls.Add(this.MeaningDistributedForceNM);
            this.Controls.Add(this.EndOfDistributedLoadM);
            this.Controls.Add(this.StartDistributedLoadM);
            this.Controls.Add(this.StrengthTwoM);
            this.Controls.Add(this.StrengthOneM);
            this.Controls.Add(this.Distance);
            this.Controls.Add(this.LengthM);
            this.Controls.Add(this.StrengthTwoN);
            this.Controls.Add(this.StrengthOneN);
            this.Controls.Add(this.DistributedForceNM);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.BendingMoment);
            this.Controls.Add(this.LateraForce);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BeamCalculation";
            this.Text = "Beam calculation";
            this.tabControl1.ResumeLayout(false);
            this.LateralForces2.ResumeLayout(false);
            this.BendingMoment2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MeaningDistributedForceNM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningStrengthOneN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningStrengthTwoN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningLengthM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningStrengthOneM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningStrengthTwoM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningStartDistributedLoadM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MeaningEndOfDistributedLoadM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LateraForce;
        private System.Windows.Forms.Button BendingMoment;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage LateralForces2;
        private System.Windows.Forms.TabPage BendingMoment2;
        public ZedGraph.ZedGraphControl ShearForcePlot;
        public ZedGraph.ZedGraphControl BendingMomentPlot;
        private System.Windows.Forms.Label DistributedForceNM;
        private System.Windows.Forms.Label StrengthOneN;
        private System.Windows.Forms.Label StrengthTwoN;
        private System.Windows.Forms.Label LengthM;
        private System.Windows.Forms.Label Distance;
        private System.Windows.Forms.Label StrengthOneM;
        private System.Windows.Forms.Label StrengthTwoM;
        private System.Windows.Forms.Label StartDistributedLoadM;
        private System.Windows.Forms.Label EndOfDistributedLoadM;
        private System.Windows.Forms.NumericUpDown MeaningDistributedForceNM;
        private System.Windows.Forms.NumericUpDown MeaningStrengthOneN;
        private System.Windows.Forms.NumericUpDown MeaningStrengthTwoN;
        private System.Windows.Forms.NumericUpDown MeaningLengthM;
        private System.Windows.Forms.NumericUpDown MeaningStrengthOneM;
        private System.Windows.Forms.NumericUpDown MeaningStrengthTwoM;
        private System.Windows.Forms.NumericUpDown MeaningStartDistributedLoadM;
        private System.Windows.Forms.NumericUpDown MeaningEndOfDistributedLoadM;
        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.TextBox YB;
        private System.Windows.Forms.TextBox YA;
        private System.Windows.Forms.Label YbText;
        private System.Windows.Forms.Label YaText;
    }
}


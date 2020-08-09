using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Rectangle bitmapRectangle;
        Size originalWindowSize;
        int originalPanel1Width, originalPanel1Height;
        string blockChosen;
        List<Block> listOfBlock;
        Graphics graphics;
        StringBuilder saveText;
        string fileContent;
        Form2 form2;
        string language;
        Block blockGrabbed = null;
        int prev_x = -1, prev_y = -1;
        bool ifStartBlockExists = false;
        public Form1()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl");
            InitializeComponent();
            hScrollBar1.Visible = true;
            vScrollBar1.Visible = true;
            vScrollBar1.Enabled = true;
            bitmapRectangle = new Rectangle(pictureBox.Location.X, pictureBox.Location.Y, pictureBox.Width, pictureBox.Height);
            originalWindowSize = Size;
            originalPanel1Width = splitContainer.Panel1.Width;
            originalPanel1Height = splitContainer.Panel1.Height;
            blockChosen = "start";
            listOfBlock = new List<Block>();
            textBoxMarked.Enabled = false;
            graphics = pictureBox.CreateGraphics();
            saveText = new StringBuilder();
            language = "Polish";
            form2 = new Form2();
            buttonBlockStart.ChangeColorWhenClickedAndMouseOff();
        }
        private void showScrollers()
        {
            int xDiff = Width - originalWindowSize.Width;
            int yDiff = Height - originalWindowSize.Height;
            if (xDiff < bitmapRectangle.Width - originalPanel1Width)
                hScrollBar1.Visible = true;
            else
                hScrollBar1.Visible = false;
            if (yDiff < bitmapRectangle.Height - originalPanel1Height)
                vScrollBar1.Visible = true;
            else
                vScrollBar1.Visible = false;
            if (pictureBox.Width < originalPanel1Width)
                hScrollBar1.Visible = false;
            if (pictureBox.Height < originalPanel1Height)
                vScrollBar1.Visible = false;
        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                pictureBox.Refresh();
                foreach (Block block in listOfBlock)
                {
                    if (block.IfActive)
                    {
                        if (prev_x != -1)
                            block.MoveAndDraw(e.X - prev_x, e.Y - prev_y, graphics);
                        prev_x = e.X;
                        prev_y = e.Y;
                    }
                    else
                        block.Draw(graphics);
                }
            }
            else
                prev_x = prev_y = -1;
            if (e.Button == MouseButtons.Left && blockChosen == "bind")
            {
                if (blockGrabbed != null)
                {
                    blockGrabbed.DrawLine(e, graphics);
                    foreach (Block b in listOfBlock)
                        b.Draw(graphics);
                }
            }
        }
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxMarked.Enabled = false;
            if (e.Button == MouseButtons.Left)
            {
                foreach (Block block in listOfBlock)
                {
                    if (block.CheckIfNodeGrabbed(e.X, e.Y))
                    {
                        blockGrabbed = block;
                        break;
                    }
                }
                if (blockChosen == "operation")
                {
                    OperationBlock operationBlock = new OperationBlock(e.X, e.Y, language);
                    listOfBlock.Add(operationBlock);
                    operationBlock.Draw(graphics);
                }
                else if (blockChosen == "decision")
                {
                    DecisionBlock decisionBlock = new DecisionBlock(e.X, e.Y, language);
                    listOfBlock.Add(decisionBlock);
                    decisionBlock.Draw(graphics);
                }
                else if (blockChosen == "start")
                {
                    StartBlock startBlock = new StartBlock(e.X, e.Y);
                    if (!ifStartBlockExists)
                    {
                        listOfBlock.Add(startBlock);
                        startBlock.Draw(graphics);
                        ifStartBlockExists = true;
                    }
                }
                else if (blockChosen == "stop")
                {
                    StopBlock stopBlock = new StopBlock(e.X, e.Y);
                    listOfBlock.Add(stopBlock);
                    stopBlock.Draw(graphics);
                }
                else if (blockChosen == "remove")
                {
                    List<Block> listOfBlockToModify = new List<Block>();
                    foreach (Block block in listOfBlock)
                        listOfBlockToModify.Add(block);
                    foreach (Block block in listOfBlock)
                    {
                        if (block.CheckIfClicked(e.X, e.Y))
                        {
                            if (block is StartBlock s)
                                ifStartBlockExists = false;
                            listOfBlockToModify.Remove(block);
                        }
                    }
                    listOfBlock = listOfBlockToModify;
                    pictureBox.Refresh();
                    foreach (Block block in listOfBlock)
                    {
                        block.Draw(graphics);
                    }
                    textBoxMarked.Text = "";
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Block block = null;
                textBoxMarked.Text = "";
                foreach (Block b in listOfBlock)
                {
                    if (b.CheckIfClicked(e.X, e.Y))
                        block = b;
                    else
                        b.ChangeStateToDisActive(graphics);
                }
                if (block != null)
                    block.ChangeStateToActive(graphics, textBoxMarked);
                else
                    prev_x = prev_y = -1;
            }
        }
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && blockChosen == "bind")
            {
                if (blockGrabbed != null)
                {
                    foreach (Block block in listOfBlock)
                    {
                        if (block == blockGrabbed)
                            continue;
                        if (block.CheckIfNodeEnterAchieved(e.X, e.Y))
                        {
                            block.nodeEnter.IfExists = false;
                            block.Draw(graphics);
                            blockGrabbed.IfNodeGrabbed = true;
                            blockGrabbed.DrawLineAchieved(e, graphics);
                            blockGrabbed.IfLineActive = false;
                            blockGrabbed.IfLineLeftActive = false;
                            blockGrabbed.IfLineRightActive = false;
                        }
                        else
                        {
                            blockGrabbed.IfLineActive = false;
                            blockGrabbed.IfLineLeftActive = false;
                            blockGrabbed.IfLineRightActive = false;
                            blockGrabbed.DrawLine(e, graphics);
                        }
                    }
                }
            }
        }
        private void textBoxMarked_TextChanged(object sender, EventArgs e)
        {
            foreach (Block block in listOfBlock)
                if (block.IfActive)
                    block.ChangeText(textBoxMarked.Text, graphics);
        }

        private void buttonScheme1_Click(object sender, EventArgs e)
        {
            form2 = new Form2();
            form2.Location = new Point(Location.X + Width / 2 - 125, Location.Y + Height / 2 - 75);
            form2.ShowIcon = false;
            if (form2.ShowDialog() != DialogResult.OK)
            {
                if (!form2.IfClosedWithXButton)
                {
                    pictureBox.Width = form2.GetNewWidth;
                    pictureBox.Height = form2.GetNewHeight;
                    pictureBox.Refresh();
                    listOfBlock.Clear();
                    showScrollers();
                }
            }
            textBoxMarked.Text = "";
        }
        private void buttonScheme2_Click(object sender, EventArgs e)
        {
            saveText.Append(pictureBox.Width);
            saveText.Append(" ");
            saveText.Append(pictureBox.Height);
            saveText.Append("\n");
            foreach (Block block in listOfBlock)
            {
                saveText.Append(block.ID);
                saveText.Append(" ");
                saveText.Append(block.Text);
                saveText.Append(" ");
                saveText.Append(block.X);
                saveText.Append(" ");
                saveText.Append(block.Y);
                saveText.Append(" ");
                saveText.Append(block.IfActive);
                saveText.Append("\n");
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "diag";
            saveFileDialog.ShowDialog();
            string filePath = saveFileDialog.FileName;
            File.WriteAllText(filePath, saveText.ToString());
        }
        private void buttonScheme3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.DefaultExt = "diag";
            openFileDialog.Filter = ".diag files|*.diag";
            openFileDialog.ShowDialog();
            string fileName = openFileDialog.FileName;
            if(fileName == null || fileName == "")
            {
                if (language == "Polish")
                    MessageBox.Show("Nie wybrano pliku.");
                if (language == "English")
                    MessageBox.Show("File not chosen.");
                return;
            }
            fileContent = File.ReadAllText(fileName);

            if (fileContent != null)
            {
                int i = 0, id = 0, x = 0, y = 0, width, height;
                string text = "";
                bool ifActive = false;
                StringBuilder str = new StringBuilder();
                while (fileContent[i] != ' ')
                    str.Append(fileContent[i++]);
                width = int.Parse(str.ToString());
                str.Clear();
                i++;
                while (fileContent[i] != '\n')
                    str.Append(fileContent[i++]);
                height = int.Parse(str.ToString());
                str.Clear();
                pictureBox.Size = new Size(width, height);
                pictureBox.Refresh();
                i++;
                try
                {
                    while (i < fileContent.Length)
                    {
                        while (fileContent[i] != ' ')
                            str.Append(fileContent[i++]);
                        id = int.Parse(str.ToString());
                        str.Clear();
                        while (!char.IsDigit(fileContent[i]))
                            str.Append(fileContent[i++]);
                        text = str.ToString();
                        str.Clear();
                        while (fileContent[i] != ' ')
                            str.Append(fileContent[i++]);
                        x = int.Parse(str.ToString());
                        str.Clear();
                        i++;
                        while (fileContent[i] != ' ')
                            str.Append(fileContent[i++]);
                        y = int.Parse(str.ToString());
                        str.Clear();
                        i++;
                        while (fileContent[i] != '\n')
                            str.Append(fileContent[i++]);
                        if (str.ToString() == "True")
                            ifActive = true;
                        if (str.ToString() == "False")
                            ifActive = false;
                        str.Clear();
                        i++;
                        switch (id)
                        {
                            case 1:
                                {
                                    OperationBlock block = new OperationBlock(x, y, language);
                                    block.Text = text;
                                    block.IfActive = ifActive;
                                    listOfBlock.Add(block);
                                    block.Draw(graphics);
                                    break;
                                }
                            case 2:
                                {
                                    DecisionBlock block = new DecisionBlock(x, y, language);
                                    block.Text = text;
                                    block.IfActive = ifActive;
                                    listOfBlock.Add(block);
                                    block.Draw(graphics);
                                    break;
                                }
                            case 3:
                                {
                                    StartBlock block = new StartBlock(x, y);
                                    block.Text = text;
                                    block.IfActive = ifActive;
                                    listOfBlock.Add(block);
                                    block.Draw(graphics);
                                    break;
                                }
                            case 4:
                                {
                                    StopBlock block = new StopBlock(x, y);
                                    block.Text = text;
                                    block.IfActive = ifActive;
                                    listOfBlock.Add(block);
                                    block.Draw(graphics);
                                    break;
                                }
                        }
                    }
                }catch
                {
                    if (language == "Polish")
                        MessageBox.Show("Nieprawidłowa zawartość pliku.");
                    if (language == "English")
                        MessageBox.Show("Wrong file content.");
                }
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            showScrollers();
        }
        public abstract class Node
        {
            protected int x;
            protected int y;
            bool ifExists;
            protected Rectangle circleSize;
            protected SolidBrush brushWhite;
            public int X
            {
                get
                {
                    return x;
                }
                set
                {
                    x = value;
                }
            }
            public int Y
            {
                get
                {
                    return y;
                }
                set
                {
                    y = value;
                }
            }
            public bool IfExists
            {
                get
                {
                    return ifExists;
                }
                set
                {
                    ifExists = value;
                }
            }
            public Node(int x, int y)
            {
                this.x = x;
                this.y = y;
                ifExists = true;
                circleSize = new Rectangle(0, 0, 8, 8);
                brushWhite = new SolidBrush(Color.White);
            }
            public virtual void Draw(int x, int y, Graphics g)
            {
                circleSize.X = x;
                circleSize.Y = y;
                if (!IfExists)
                {
                    circleSize.Width = 20;
                    circleSize.Height = 20;
                    g.FillEllipse(brushWhite, circleSize);
                    circleSize.Width = 8;
                    circleSize.Height = 8;
                }
            }
        }
        public class NodeEnter : Node
        {

            Pen penBlack;
            public NodeEnter(int x, int y) : base(x, y)
            {
                penBlack = new Pen(Color.Black);
            }
            public override void Draw(int x, int y, Graphics g)
            {
                base.Draw(x, y, g);
                if (IfExists)
                {
                    g.FillEllipse(brushWhite, circleSize);
                    g.DrawEllipse(penBlack, circleSize);
                }
            }
        }
        public class NodeLeave : Node
        {
            SolidBrush brushBlack;
            public NodeLeave(int x, int y) : base(x, y)
            {
                brushBlack = new SolidBrush(Color.Black);
            }
            public override void Draw(int x, int y, Graphics g)
            {
                base.Draw(x, y, g);
                if (IfExists)
                    g.FillEllipse(brushBlack, circleSize);
            }
        }
        public class Line
        {
            int x1, y1;
            int x2, y2;
            int prev_x1, prev_y1;
            int prev_x2, prev_y2;
            public int xAchieved, yAchieved;
            Pen blackPen;
            SolidBrush brushBlack;
            Pen whitePen;
            SolidBrush brushWhite;
            public Line(int x1, int y1, int x2, int y2)
            {
                this.x1 = x1;
                this.x2 = x2;
                this.y1 = y1;
                this.y2 = y2;
                blackPen = new Pen(Color.Black);
                brushBlack = new SolidBrush(Color.Black);
                whitePen = new Pen(Color.White);
                brushWhite = new SolidBrush(Color.White);
                prev_x1 = prev_x2 = prev_y1 = prev_y2 = 0;
            }
            public void ChangePosition(int x1, int y1, int x2, int y2)
            {
                this.x1 = x1;
                this.x2 = x2;
                this.y1 = y1;
                this.y2 = y2;
            }
            public void Draw(Graphics g)
            {
                if (prev_x1 != 0)
                {
                    Blank(g);
                }
                g.DrawLine(blackPen, x1, y1, x2, y2);
                prev_x1 = x1;
                prev_x2 = x2;
                prev_y1 = y1;
                prev_y2 = y2;
            }
            public void Blank(Graphics g)
            {
                g.DrawLine(whitePen, prev_x1, prev_y1, prev_x2, prev_y2);
            }
            public void DrawArrow(Graphics g)
            {
                Point p1, p2, p3;
                Point[] points = new Point[3];
                double alpha, l = 20, beta = 15 * Math.PI / 180, gamma = -Math.PI / 2;
                alpha = -Math.Atan((double)(x2 - x1) / (double)(y2 - y1));
                p1 = new Point(x2, y2);
                p2 = new Point(x2 + (int)(l * Math.Cos(gamma + alpha - beta)), y2 + (int)(l * Math.Sin(gamma + alpha - beta)));
                p3 = new Point(x2 + (int)(l * Math.Cos(gamma + alpha + beta)), y2 + (int)(l * Math.Sin(gamma + alpha + beta)));
                points[0] = p1;
                points[1] = p2;
                points[2] = p3;
                g.FillPolygon(brushBlack, points);
            }
        }
        abstract class Block
        {
            protected int x;
            protected int y;
            protected int id;
            protected SolidBrush brushWhite;
            protected SolidBrush brushBlack;
            protected Pen penBlack;
            protected Pen penWhite;
            protected string text;
            protected Font font;
            protected float[] dashTableFull = { 10 };
            protected bool ifActive = false;
            protected TextFormatFlags flags;
            protected float[] dashTable = { 4, 4, 4, 4 };
            public NodeEnter nodeEnter;
            public NodeLeave nodeLeave;
            public NodeLeave nodeLeaveLeft;
            public NodeLeave nodeLeaveRight;
            public Line line;
            public Line lineLeft;
            public Line lineRight;
            protected bool ifLineAchieved = false;
            protected bool ifLineLeftActive = false;
            protected bool ifLineRightActive = false;
            protected bool ifNodeLeftGrabbed = false;
            protected bool ifNodeRightGrabbed = false;
            protected bool ifNodeGrabbed = false;
            protected bool ifLineActive = false;
            public bool IfActive
            {
                get
                {
                    return ifActive;
                }
                set
                {
                    ifActive = value;
                }
            }
            public string Text
            {
                get
                {
                    return text;
                }
                set
                {
                    text = value;
                }
            }
            public int X
            {
                get
                {
                    return x;
                }
                set
                {
                    x = value;
                }
            }
            public int Y
            {
                get
                {
                    return y;
                }
                set
                {
                    y = value;
                }
            }
            public int ID
            {
                get
                {
                    return id;
                }
                set
                {
                    id = value;
                }
            }
            public bool IfNodeLeftGrabbed
            {
                get
                {
                    return ifNodeLeftGrabbed;
                }
                set
                {
                    ifNodeLeftGrabbed = value;
                }
            }
            public bool IfNodeRightGrabbed
            {
                get
                {
                    return ifNodeRightGrabbed;
                }
                set
                {
                    ifNodeRightGrabbed = value;
                }
            }
            public bool IfNodeGrabbed
            {
                get
                {
                    return ifNodeGrabbed;
                }
                set
                {
                    ifNodeGrabbed = value;
                }
            }
            public bool IfLineActive
            {
                get
                {
                    return ifLineActive;
                }
                set
                {
                    ifLineActive = value;
                }
            }
            public bool IfLineLeftActive
            {
                get
                {
                    return ifLineLeftActive;
                }
                set
                {
                    ifLineLeftActive = value;
                }
            }
            public bool IfLineRightActive
            {
                get
                {
                    return ifLineRightActive;
                }
                set
                {
                    ifLineRightActive = value;
                }
            }
            public Block(int x, int y)
            {
                this.x = x;
                this.y = y;
                brushWhite = new SolidBrush(Color.White);
                brushBlack = new SolidBrush(Color.Black);
                font = new Font("Times New Roman", 9.0f);
                penBlack = new Pen(Color.Black, 2);
                penWhite = new Pen(Color.White, 3);
                flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl;
            }
            public virtual void Draw(Graphics g)
            {

            }
            public virtual void ChangeStateToActive(Graphics g, TextBox textBox)
            {

            }
            public void ChangeStateToDisActive(Graphics g)
            {
                ifActive = false;
                Draw(g);
            }
            public virtual void ChangeText(string text, Graphics g)
            {

            }
            public void MoveAndDraw(int xDiff, int yDiff, Graphics g)
            {
                if (ifActive)
                {
                    x += xDiff;
                    y += yDiff;
                }
                Draw(g);
            }
            public virtual void DrawLine(MouseEventArgs e, Graphics g)
            {
            }
            public virtual void DrawLineAchieved(MouseEventArgs e, Graphics g)
            {
            }
            public abstract bool CheckIfNodeGrabbed(int x, int y);
            public virtual bool CheckIfNodeEnterAchieved(int x, int y)
            {
                return false;
            }
            public virtual bool CheckIfClicked(int x, int y)
            {
                return true;
            }
        }
        class OperationBlock : Block
        {
            int Width = 100;
            int Height = 50;
            public OperationBlock(int x, int y, string language) : base(x, y)
            {
                if (language == "Polish")
                    text = "Blok Operacyjny";
                if (language == "English")
                    text = "Operation Block";
                id = 1;
                nodeEnter = new NodeEnter(x, y - Height / 2);
                nodeLeave = new NodeLeave(x, y + Height / 2);
                nodeLeaveLeft = nodeLeaveRight = null;
                lineLeft = lineRight = null;
            }
            public override void Draw(Graphics g)
            {
                Point point1 = new Point(x + Width / 2, y + Height / 2);
                Point point2 = new Point(x + Width / 2, y - Height / 2);
                Point point3 = new Point(x - Width / 2, y - Height / 2);
                Point point4 = new Point(x - Width / 2, y + Height / 2);
                Rectangle rect = new Rectangle(x - Width / 2, y - Height / 2, Width, Height);

                Point[] points = { point1, point2, point3, point4 };
                nodeEnter.X = x;
                nodeEnter.Y = Y - Height / 2;
                nodeLeave.X = x;
                nodeLeave.Y = Y + Height / 2;

                if (!nodeEnter.IfExists)
                    nodeEnter.Draw(nodeEnter.X - 4, nodeEnter.Y - 4, g);
                if (!nodeLeave.IfExists)
                    nodeLeave.Draw(nodeLeave.X - 4, nodeLeave.Y - 4, g);

                g.FillPolygon(brushWhite, points);
                g.DrawPolygon(penWhite, points);
                if (ifActive)
                {
                    penBlack.DashPattern = dashTable;
                    g.DrawPolygon(penBlack, points);
                    penBlack.DashPattern = dashTableFull;
                }
                else
                    g.DrawPolygon(penBlack, points);
                TextRenderer.DrawText(g, text, font, rect, Color.Black, flags);

                if(nodeEnter.IfExists)
                    nodeEnter.Draw(nodeEnter.X - 4, nodeEnter.Y - 4, g);
                if (nodeLeave.IfExists)
                    nodeLeave.Draw(nodeLeave.X - 4, nodeLeave.Y - 4, g);
                if (ifLineAchieved)
                {
                    line.ChangePosition(nodeLeave.X, nodeLeave.Y, line.xAchieved, line.yAchieved);
                    line.Draw(g);
                    line.DrawArrow(g);
                }
            }
            public override void ChangeStateToActive(Graphics g, TextBox textBox)
            {
                textBox.Enabled = true;
                textBox.Text = text;
                ifActive = true;
                Draw(g);
            }
            public override void ChangeText(string text, Graphics g)
            {
                if (text != "")
                    Text = text;
                Draw(g);
            }
            public override void DrawLine(MouseEventArgs e, Graphics g)
            {
                line.ChangePosition(nodeLeave.X, nodeLeave.Y, e.X, e.Y);
                if (ifLineActive)
                    line.Draw(g);
                else
                    line.Blank(g);
            }
            public override void DrawLineAchieved(MouseEventArgs e, Graphics g)
            {
                nodeLeave.IfExists = false;
                Draw(g);
                line.ChangePosition(nodeLeave.X, nodeLeave.Y, e.X, e.Y);
                line.Draw(g);
                line.DrawArrow(g);
                ifLineAchieved = true;
                line.xAchieved = e.X;
                line.yAchieved = e.Y;
            }
            public override bool CheckIfNodeEnterAchieved(int x, int y)
            {
                if (Math.Abs(x - nodeEnter.X) <= 10 && Math.Abs(y - nodeEnter.Y) <= 10)
                    return true;
                return false;
            }
            public override bool CheckIfNodeGrabbed(int x, int y)
            {
                if (Math.Abs(x - nodeLeave.X) <= 10 && Math.Abs(y - nodeLeave.Y) <= 10)
                {
                    line = new Line(0, 0, 0, 0);
                    ifLineActive = true;
                    return true;
                }
                ifLineActive = false;
                return false;
            }
            public override bool CheckIfClicked(int x, int y)
            {
                if (this.x - Width / 2 <= x && x <= this.x + Width / 2 && this.y - Height / 2 <= y && y <= this.y + Height / 2)
                    return true;
                return false;
            }
        }
        class DecisionBlock : Block
        {
            int Width = 125;
            int Height = 75;
            public DecisionBlock(int x, int y, string language) : base(x, y)
            {
                id = 2;
                if (language == "Polish")
                    text = "Blok Decyzyjny";
                if (language == "English")
                    text = "Decision Block";
                nodeEnter = new NodeEnter(x, y - Height / 2);
                nodeLeave = null;
                nodeLeaveLeft = new NodeLeave(x - Width / 2, y);
                nodeLeaveRight = new NodeLeave(x + Width / 2, y);
                line = null;
                lineLeft = null;
                lineRight = null;
            }
            public override void Draw(Graphics g)
            {
                Point point1 = new Point(x, y + Height / 2);
                Point point2 = new Point(x + Width / 2, y);
                Point point3 = new Point(x, y - Height / 2);
                Point point4 = new Point(x - Width / 2, y);

                Point[] points = { point1, point2, point3, point4 };
                Rectangle rect = new Rectangle(x - Width / 2, y - Height / 2, Width, Height);

                nodeEnter.X = x;
                nodeEnter.Y = y - Height / 2;
                nodeLeaveLeft.X = x - Width / 2;
                nodeLeaveLeft.Y = y;
                nodeLeaveRight.X = x + Width / 2;
                nodeLeaveRight.Y = y;

                if (!nodeEnter.IfExists)
                    nodeEnter.Draw(nodeEnter.X - 4, nodeEnter.Y - 4, g);
                if (!nodeLeaveLeft.IfExists)
                    nodeLeaveLeft.Draw(nodeLeaveLeft.X - 4, nodeLeaveLeft.Y - 4, g);
                if (!nodeLeaveRight.IfExists)
                    nodeLeaveRight.Draw(nodeLeaveRight.X - 4, nodeLeaveRight.Y - 4, g);
                
                g.FillPolygon(brushWhite, points);
                g.DrawPolygon(penWhite, points);
                if (ifActive)
                {
                    penBlack.DashPattern = dashTable;
                    g.DrawPolygon(penBlack, points);
                    penBlack.DashPattern = dashTableFull;
                }
                else
                    g.DrawPolygon(penBlack, points);

                TextRenderer.DrawText(g, text, font, rect, Color.Black, flags);
                g.DrawString("T", font, brushBlack, x - Width / 2 - 5, y - 20);
                g.DrawString("N", font, brushBlack, x + Width / 2 - 5, y - 20);

                if (nodeEnter.IfExists)
                    nodeEnter.Draw(nodeEnter.X - 4, nodeEnter.Y - 4, g);
                if (nodeLeaveLeft.IfExists)
                    nodeLeaveLeft.Draw(nodeLeaveLeft.X - 4, nodeLeaveLeft.Y - 4, g);
                if (nodeLeaveRight.IfExists)
                    nodeLeaveRight.Draw(nodeLeaveRight.X - 4, nodeLeaveRight.Y - 4, g);
            }
            public override void ChangeStateToActive(Graphics g, TextBox textBox)
            {
                textBox.Enabled = true;
                textBox.Text = text;
                ifActive = true;
                Draw(g);
            }
            public override void ChangeText(string text, Graphics g)
            {
                if (text != "")
                    Text = text;
                Draw(g);
            }
            public override void DrawLine(MouseEventArgs e, Graphics g)
            {
                nodeLeaveLeft.IfExists = false;
                nodeLeaveRight.IfExists = false;
                if (lineLeft != null)
                {
                    lineLeft.ChangePosition(nodeLeaveLeft.X, nodeLeaveLeft.Y, e.X, e.Y);
                    if (ifLineLeftActive)
                        lineLeft.Draw(g);
                    else
                        lineLeft.Blank(g);
                }
                if (lineRight != null)
                {
                    lineRight.ChangePosition(nodeLeaveRight.X, nodeLeaveRight.Y, e.X, e.Y);
                    if (ifLineRightActive)
                        lineRight.Draw(g);
                    else
                        lineRight.Blank(g);
                }
            }
            public override void DrawLineAchieved(MouseEventArgs e, Graphics g)
            {
                if (lineLeft != null)
                {
                    lineLeft.ChangePosition(nodeLeaveLeft.X, nodeLeaveLeft.Y, e.X, e.Y);
                    lineLeft.Draw(g);
                    lineLeft.DrawArrow(g);
                }
                if (lineRight != null)
                {
                    lineRight.ChangePosition(nodeLeaveRight.X, nodeLeaveRight.Y, e.X, e.Y);
                    lineRight.Draw(g);
                    lineRight.DrawArrow(g);
                }
            }
            public override bool CheckIfNodeEnterAchieved(int x, int y)
            {
                if (Math.Abs(x - nodeEnter.X) <= 10 && Math.Abs(y - nodeEnter.Y) <= 10)
                    return true;
                return false;
            }
            public override bool CheckIfNodeGrabbed(int x, int y)
            {
                if (Math.Abs(x - nodeLeaveLeft.X) <= 10 && Math.Abs(y - nodeLeaveLeft.Y) <= 10)
                {
                    ifLineLeftActive = true;
                    lineLeft = new Line(0, 0, 0, 0);
                    return true;
                }
                if (Math.Abs(x - nodeLeaveRight.X) <= 10 && Math.Abs(y - nodeLeaveRight.Y) <= 10)
                {
                    ifLineRightActive = true;
                    lineRight = new Line(0, 0, 0, 0);
                    return true;
                }
                return false;
            }
            public override bool CheckIfClicked(int x, int y)
            {
                if (x >= this.x && y >= this.y && y - this.y <= -0.6 * (x - this.x) + 75 / 2)
                    return true;
                else if (x >= this.x && y < this.y && y - this.y >= 0.6 * (x - this.x) - 75 / 2)
                    return true;
                else if (x < this.x && y >= this.y && y - this.y <= 0.6 * (x - this.x) + 75 / 2)
                    return true;
                else if (x < this.x && y < this.y && y - this.x >= -0.6 * (x - this.y) - 75 / 2)
                    return true;
                return false;
            }
        }
        class EllipseBlock : Block
        {
            protected int Width = 100;
            protected int Height = 50;
            protected Rectangle rect;
            public EllipseBlock(int x, int y) : base(x, y)
            {
                rect = new Rectangle(x - Width / 2, y - Height / 2, Width, Height);
            }
            public override void Draw(Graphics g)
            {
                rect.Location = new Point(x - Width / 2, y - Height / 2);
                g.FillEllipse(brushWhite, rect);
            }
            public override void ChangeStateToActive(Graphics g, TextBox textBox)
            {

            }
            public override bool CheckIfClicked(int x, int y)
            {
                if ((x - this.x) * (x - this.x) / (50 * 50) + (y - this.y) * (y - this.y) / (25 * 25) <= 1)
                    return true;
                return false;
            }
            public override void DrawLine(MouseEventArgs e, Graphics g)
            {

            }
            public override void DrawLineAchieved(MouseEventArgs e, Graphics g)
            {

            }
            public override bool CheckIfNodeGrabbed(int x, int y)
            {
                return false;
            }
        }
        class StartBlock : EllipseBlock
        {
            Pen greenPen;
            public StartBlock(int x, int y) : base(x, y)
            {
                id = 3;
                text = "START";
                greenPen = new Pen(Color.Green);
                nodeLeave = new NodeLeave(x, y + Height / 2);
                nodeEnter = null;
                nodeLeaveLeft = nodeLeaveRight = null;
                lineLeft = lineRight = null;
                line = new Line(0, 0, 0, 0);
            }
            public override void Draw(Graphics g)
            {
                base.Draw(g);
                nodeLeave.X = x;
                nodeLeave.Y = y + Height/2;
                if (!nodeLeave.IfExists)
                    nodeLeave.Draw(nodeLeave.X - 4, nodeLeave.Y - 4, g);

                if (ifActive)
                {
                    greenPen.DashPattern = dashTable;
                    g.DrawEllipse(greenPen, rect);
                }
                else
                {
                    greenPen.DashPattern = dashTableFull;
                    g.DrawEllipse(greenPen, rect);
                }

                TextRenderer.DrawText(g, text, font, rect, Color.Black, flags);

                if (nodeLeave.IfExists)
                    nodeLeave.Draw(nodeLeave.X - 4, nodeLeave.Y - 4, g);
                if (ifLineAchieved)
                {
                    line.ChangePosition(nodeLeave.X, nodeLeave.Y, line.xAchieved, line.yAchieved);
                    line.Draw(g);
                    line.DrawArrow(g);
                }
            }
            public override void ChangeStateToActive(Graphics g, TextBox textBox)
            {
                textBox.Enabled = false;
                textBox.Text = text;
                ifActive = true;
                Draw(g);
            }
            public override void DrawLine(MouseEventArgs e, Graphics g)
            {
                line.ChangePosition(nodeLeave.X, nodeLeave.Y, e.X, e.Y);
                if (ifLineActive)
                    line.Draw(g);
                else
                    line.Blank(g);
            }
            public override void DrawLineAchieved(MouseEventArgs e, Graphics g)
            {
                nodeLeave.IfExists = false;
                ifLineActive = true;
                Draw(g);
                line.ChangePosition(nodeLeave.X, nodeLeave.Y, e.X, e.Y);
                line.Draw(g);
                line.DrawArrow(g);
                ifLineAchieved = true;
                line.xAchieved = e.X;
                line.yAchieved = e.Y;
            }
            public override bool CheckIfNodeGrabbed(int x, int y)
            {
                if (Math.Abs(x - nodeLeave.X) <= 10 && Math.Abs(y - nodeLeave.Y) <= 10)
                {
                    ifLineActive = true;
                    return true;
                }
                return false;
            }
        }
        class StopBlock : EllipseBlock
        {
            Pen redPen;
            public StopBlock(int x, int y) : base(x, y)
            {
                id = 4;
                text = "STOP";
                redPen = new Pen(Color.Red);
                nodeEnter = new NodeEnter(x, y - Height / 2);
                nodeLeave = nodeLeaveLeft = nodeLeaveRight = null;
            }
            public override void Draw(Graphics g)
            {
                base.Draw(g);
                if (ifActive)
                {
                    redPen.DashPattern = dashTable;
                    g.DrawEllipse(redPen, rect);
                    redPen.DashPattern = dashTableFull;
                }
                else
                {
                    g.DrawEllipse(redPen, rect);
                }
                TextRenderer.DrawText(g, text, font, rect, Color.Black, flags);
                nodeEnter.X = x;
                nodeEnter.Y = y - Height / 2;
                if (nodeEnter.IfExists)
                    nodeEnter.Draw(nodeEnter.X - 4, nodeEnter.Y - 4, g);
            }
            public override void ChangeStateToActive(Graphics g, TextBox textBox)
            {
                textBox.Enabled = false;
                textBox.Text = text;
                ifActive = true;
                Draw(g);
            }
            public override void DrawLine(MouseEventArgs e, Graphics g)
            {

            }
            public override void DrawLineAchieved(MouseEventArgs e, Graphics g)
            {

            }
            public override bool CheckIfNodeEnterAchieved(int x, int y)
            {
                if (Math.Abs(x - nodeEnter.X) <= 10 && Math.Abs(y - nodeEnter.Y) <= 10)
                    return true;
                return false;
            }
            public override bool CheckIfNodeGrabbed(int x, int y)
            {
                return false;
            }
        }
        class ChooseButton : Button
        {
            Color colorOfNonClickedButton = System.Drawing.SystemColors.ButtonFace;
            Color colorOfClickedButton = Color.LightSteelBlue;
            public Color colorOfNonClickedWithMouseOn = Color.LightGray;
            public Color colorOfClickedWithMouseOn = Color.LightGray;
            public ChooseButton() : base()
            {
                FlatAppearance.BorderSize = 1;
                FlatStyle = FlatStyle.Flat;
            }
            public void ChangeColorToNonClicked()
            {
                BackColor = colorOfNonClickedButton;
                FlatAppearance.BorderColor = Color.Black;
                FlatAppearance.BorderSize = 1;
            }
            public void ChangeColorToClicked()
            {
                BackColor = colorOfClickedButton;
                FlatAppearance.BorderSize = 0;
            }
            public void ChangeColorWhenClickedAndMouseOn()
            {
                FlatAppearance.BorderColor = colorOfClickedWithMouseOn;
                FlatAppearance.BorderSize = 0;
            }
            public void ChangeColorWhenNonClickedAndMouseOn()
            {

                FlatAppearance.BorderColor = Color.Black;
                FlatAppearance.BorderSize = 1;
                BackColor = colorOfNonClickedButton;
            }
            public void ChangeColorWhenClickedAndMouseOff()
            {
                BackColor = colorOfClickedButton;
                FlatAppearance.BorderColor = Color.Black;
                FlatAppearance.BorderSize = 1;
            }
        }
        private void buttonBlockStart_Click(object sender, EventArgs e)
        {
            buttonBlockStart.ChangeColorToClicked();
            buttonBlockOperation.ChangeColorToNonClicked();
            buttonBlockBind.ChangeColorToNonClicked();
            buttonBlockStop.ChangeColorToNonClicked();
            buttonBlockDecision.ChangeColorToNonClicked();
            buttonBlockRemove.ChangeColorToNonClicked();
            blockChosen = "start";
        }

        private void buttonBlockOperation_Click(object sender, EventArgs e)
        {
            buttonBlockStart.ChangeColorToNonClicked();
            buttonBlockOperation.ChangeColorToClicked();
            buttonBlockBind.ChangeColorToNonClicked();
            buttonBlockStop.ChangeColorToNonClicked();
            buttonBlockDecision.ChangeColorToNonClicked();
            buttonBlockRemove.ChangeColorToNonClicked();
            blockChosen = "operation";
        }
        private void buttonBlockBind_Click(object sender, EventArgs e)
        {
            buttonBlockStart.ChangeColorToNonClicked();
            buttonBlockOperation.ChangeColorToNonClicked();
            buttonBlockBind.ChangeColorToClicked();
            buttonBlockStop.ChangeColorToNonClicked();
            buttonBlockDecision.ChangeColorToNonClicked();
            buttonBlockRemove.ChangeColorToNonClicked();
            blockChosen = "bind";
        }
        private void buttonBlockStop_Click(object sender, EventArgs e)
        {
            buttonBlockStart.ChangeColorToNonClicked();
            buttonBlockOperation.ChangeColorToNonClicked();
            buttonBlockBind.ChangeColorToNonClicked();
            buttonBlockStop.ChangeColorToClicked();
            buttonBlockDecision.ChangeColorToNonClicked();
            buttonBlockRemove.ChangeColorToNonClicked();
            blockChosen = "stop";
        }

        private void buttonBlockDecision_Click(object sender, EventArgs e)
        {
            buttonBlockStart.ChangeColorToNonClicked();
            buttonBlockOperation.ChangeColorToNonClicked();
            buttonBlockBind.ChangeColorToNonClicked();
            buttonBlockStop.ChangeColorToNonClicked();
            buttonBlockDecision.ChangeColorToClicked();
            buttonBlockRemove.ChangeColorToNonClicked();
            blockChosen = "decision";
        }

        private void buttonBlockRemove_Click(object sender, EventArgs e)
        {
            buttonBlockStart.ChangeColorToNonClicked();
            buttonBlockOperation.ChangeColorToNonClicked();
            buttonBlockBind.ChangeColorToNonClicked();
            buttonBlockStop.ChangeColorToNonClicked();
            buttonBlockDecision.ChangeColorToNonClicked();
            buttonBlockRemove.ChangeColorToClicked();
            blockChosen = "remove";
        }

        private void buttonBlockStart_MouseEnter(object sender, EventArgs e)
        {
            if (blockChosen == "start")
                buttonBlockStart.ChangeColorWhenClickedAndMouseOn();
            else
                buttonBlockStart.ChangeColorWhenNonClickedAndMouseOn();
        }

        private void buttonBlockStart_MouseLeave(object sender, EventArgs e)
        {
            if (blockChosen == "start")
                buttonBlockStart.ChangeColorWhenClickedAndMouseOff();
            else
                buttonBlockStart.ChangeColorToNonClicked();
        }

        private void buttonBlockOperation_MouseEnter(object sender, EventArgs e)
        {
            if (blockChosen == "operation")
                buttonBlockOperation.ChangeColorWhenClickedAndMouseOn();
            else
                buttonBlockOperation.ChangeColorWhenNonClickedAndMouseOn();
        }

        private void buttonBlockOperation_MouseLeave(object sender, EventArgs e)
        {
            if (blockChosen == "operation")
                buttonBlockOperation.ChangeColorWhenClickedAndMouseOff();
            else
                buttonBlockOperation.ChangeColorToNonClicked();
        }

        private void buttonBlockBind_MouseEnter(object sender, EventArgs e)
        {
            if (blockChosen == "bind")
                buttonBlockBind.ChangeColorWhenClickedAndMouseOn();
            else
                buttonBlockBind.ChangeColorWhenNonClickedAndMouseOn();
        }

        private void buttonBlockBind_MouseLeave(object sender, EventArgs e)
        {
            if (blockChosen == "bind")
                buttonBlockBind.ChangeColorWhenClickedAndMouseOff();
            else
                buttonBlockBind.ChangeColorToNonClicked();
        }

        private void buttonBlockStop_MouseEnter(object sender, EventArgs e)
        {
            if (blockChosen == "stop")
                buttonBlockStop.ChangeColorWhenClickedAndMouseOn();
            else
                buttonBlockStop.ChangeColorWhenNonClickedAndMouseOn();
        }

        private void buttonBlockStop_MouseLeave(object sender, EventArgs e)
        {
            if (blockChosen == "stop")
                buttonBlockStop.ChangeColorWhenClickedAndMouseOff();
            else
                buttonBlockStop.ChangeColorToNonClicked();
        }

        private void buttonBlockDecision_MouseEnter(object sender, EventArgs e)
        {
            if (blockChosen == "decision")
                buttonBlockDecision.ChangeColorWhenClickedAndMouseOn();
            else
                buttonBlockDecision.ChangeColorWhenNonClickedAndMouseOn();
        }

        private void buttonBlockDecision_MouseLeave(object sender, EventArgs e)
        {
            if (blockChosen == "decision")
                buttonBlockDecision.ChangeColorWhenClickedAndMouseOff();
            else
                buttonBlockDecision.ChangeColorToNonClicked();
        }

        private void buttonBlockRemove_MouseEnter(object sender, EventArgs e)
        {
            if (blockChosen == "remove")
                buttonBlockRemove.ChangeColorWhenClickedAndMouseOn();
            else
                buttonBlockRemove.ChangeColorWhenNonClickedAndMouseOn();
        }
        private void buttonBlockRemove_MouseLeave(object sender, EventArgs e)
        {
            if (blockChosen == "remove")
                buttonBlockRemove.ChangeColorWhenClickedAndMouseOff();
            else
                buttonBlockRemove.ChangeColorToNonClicked();
        }
        private void ChangeLanguage(string language)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            foreach (Control c in splitContainer.Panel2.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c, c.Name, new CultureInfo(language));
            }
            foreach (Control c in groupBoxFile.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c, c.Name, new CultureInfo(language));
            }
            foreach (Control c in groupBoxLanguage.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c, c.Name, new CultureInfo(language));
            }

            foreach (Control c in form2.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form2));
                resources.ApplyResources(c, c.Name, new CultureInfo(language));
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            pictureBox.Refresh();
            foreach (Block block in listOfBlock)
                block.Draw(graphics);
            pictureBox.Update();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            foreach (Block block in listOfBlock)
                block.Draw(graphics);
            pictureBox.Invalidate();
        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_ValueChanged_1(object sender, EventArgs e)
        {
            foreach (Block block in listOfBlock)
                block.Draw(graphics);
            pictureBox.Invalidate();
        }

        void buttonLanguage1_Click(object sender, EventArgs e)
        {
            ChangeLanguage("pl");
            language = "Polish";
        }
        void buttonLanguage2_Click(object sender, EventArgs e)
        {
            ChangeLanguage("en");
            language = "English";
        }
    }
}

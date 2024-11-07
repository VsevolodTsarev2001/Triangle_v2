using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tringle
{
    public partial class Tringle_Vorm2 : Form
    {
        private double sideA;   // Сторона A
        private double height;  // Высота
        private Panel drawingPanel;  // Панель для рисования треугольника

        public Tringle_Vorm2()
        {
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Minu kolmnurg";
            this.Size = new Size(500, 400);
            this.BackColor = Color.LightGreen;

            Label labelA = new Label
            {
                Text = "Külg A:",
                Location = new Point(10, 20),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Red
            };
            Controls.Add(labelA);

            TextBox textBoxA = new TextBox
            {
                Name = "textBoxA",
                Location = new Point(150, 20),
                Size = new Size(100, 30)
            };
            Controls.Add(textBoxA);

            Label labelHeight = new Label
            {
                Text = "Kõrgus:",
                Location = new Point(10, 60),
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Red
            };
            Controls.Add(labelHeight);

            TextBox textBoxHeight = new TextBox
            {
                Name = "textBoxHeight",
                Location = new Point(150, 60),
                Size = new Size(100, 30)
            };
            Controls.Add(textBoxHeight);

            Button calculateButton = new Button
            {
                Text = "Arvuta",
                Location = new Point(150, 100),
                Size = new Size(100, 40),
                BackColor = Color.LightYellow,
                Font = new Font("Arial", 12, FontStyle.Regular)
            };
            calculateButton.Click += CalculateButton_Click;
            Controls.Add(calculateButton);

            // Панель для рисования треугольника
            drawingPanel = new Panel
            {
                Size = new Size(300, 300),
                Location = new Point(200, 150),
                BackColor = Color.White
            };
            Controls.Add(drawingPanel);
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(Controls["textBoxA"].Text, out sideA) &&
                double.TryParse(Controls["textBoxHeight"].Text, out height))
            {
                // Перерисовываем панель для отображения треугольника
                this.Invalidate();
                drawingPanel.Invalidate();
            }
            else
            {
                MessageBox.Show("Palun sisestage kehtivad väärtused.");
            }
        }

        // Метод рисования треугольника
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        // Рисуем треугольник в пределах панели
        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            // Если данные корректны (сторона и высота)
            if (sideA > 0 && height > 0)
            {
                // Вычисление координат треугольника
                int panelWidth = drawingPanel.ClientSize.Width;
                int panelHeight = drawingPanel.ClientSize.Height;

                // Масштабирование для оптимального размещения треугольника в панели
                double scale = Math.Min(panelWidth / sideA, panelHeight / height) * 0.8;

                // Центрирование треугольника на панели
                int centerX = panelWidth / 2;
                int centerY = panelHeight / 2;

                // Точки треугольника
                Point[] points = new Point[3];
                points[0] = new Point(centerX, centerY - (int)(height * scale)); // Верхняя точка
                points[1] = new Point(centerX - (int)(sideA * scale / 2), centerY + (int)(height * scale / 2)); // Левая нижняя точка
                points[2] = new Point(centerX + (int)(sideA * scale / 2), centerY + (int)(height * scale / 2)); // Правая нижняя точка

                // Рисуем треугольник
                using (Pen pen = new Pen(Color.Blue, 3))
                {
                    e.Graphics.DrawPolygon(pen, points);
                }
            }
        }
    }
}

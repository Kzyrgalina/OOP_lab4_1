
namespace OOP_lab4_1
{
    public partial class Form1 : Form
    {

        private List<CCircle> container = new List<CCircle>();
        private int ctrl = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)//описание события Paint
        {
            foreach (CCircle circle in container)
            {
                circle.drawCircles(e.Graphics);//Рисует все круги из списка
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    //label1.Text = "Delete";
                    for (int i = 0; i < container.Count(); i++)
                    {
                        if (container[i].getColor() == "Red")//проверка выделения объектов
                        {
                            container.RemoveAt(i);//удаление выделенных объектов
                            i--;
                        }
                    }
                    Refresh();
                    this.KeyPreview = true;
                    break;
                case Keys.ControlKey:
                    checkBox1.Checked = !checkBox1.Checked;
                    switch (ctrl)//в зависимости от состояния флага
                    {
                        case 0:
                            ctrl++;
                            foreach (CCircle Circle1 in container)
                            {
                                Circle1.setCtrl(true);//если нажат Control устанавливает определенное значение
                            }
                            break;
                        case 1:
                            ctrl = 0;
                            foreach (CCircle Circle1 in container)
                            {
                                Circle1.setCtrl(false);//если отжат Control устанавливает определенное значение
                            }
                            break;
                    }
                    break;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (ctrl == 0)//не нажат ctrl
            {
                foreach (CCircle Circle1 in container)
                {
                    Circle1.setColor("Black");//снимает выделение со всех объектов
                }
                CCircle Circle = new CCircle(e.X, e.Y, 30);//создает новый объект с выделением
                container.Add(Circle);
            }
            if (ctrl == 1)//нажат ctrl
            {
                foreach (CCircle Circle1 in container)
                {
                    if (Circle1.checkCircles(e) == true && checkBox2.Checked == true)//проверка на массовое выделение
                    {
                        break;
                    }
                }
                Refresh();
            }
            Refresh();
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }


    public class CCircle
    {
        private int x, y, radius;
        private string color = "Red";
        private bool ctrl = false;

        public CCircle(int x, int y, int radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
        public void drawCircles(Graphics Canvas)//метод отрисовки круга
        {
            if (color == "Red")
            {
                Canvas.DrawEllipse(new Pen(Color.Red), x - radius, y - radius, radius * 2, radius * 2);
            }
            else
            {
                Canvas.DrawEllipse(new Pen(Color.Black), x - radius, y - radius, radius * 2, radius * 2);
            }
        }
        public bool checkCircles(MouseEventArgs e)//проверка на попадание курсора мыши во внутрь круга
        {
            if (ctrl)
            {
                if (Math.Pow(e.X - x, 2) + Math.Pow(e.Y - y, 2) <= Math.Pow(radius, 2) && color != "Red")
                {
                    color = "Red";
                    return true;
                }
            }
            return false;
        }

        public void setColor(string color)//сеттер цвета круга
        {
            this.color = color;
        }
        public void setCtrl(bool a)//сеттер флага выделения
        {
            ctrl = a;
        }
        public string getColor()//геттер цвета круга
        {
            return color;
        }

    }
}
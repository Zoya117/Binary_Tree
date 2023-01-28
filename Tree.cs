using System.Reflection.Emit;
using  System;
using System.Data;
using System.Windows.Forms;
using System.Linq;


namespace homework
{
    class Tree
    {
        private string value;
        private int count;
        private Tree left;
        private Tree right;
        //private Tree parent;
        public DataTable dt;
        
        /*public void init_dt()
        {
            dt = new DataTable("Books");
            DataColumn[] cols =
            {
                new DataColumn("Number UDK",typeof(Int32)),
                new DataColumn("Autor's Name",typeof(String)),
                new DataColumn("Name",typeof(String)), 
                new DataColumn("Year of publishing",typeof(string)),
                new DataColumn("Amount",typeof(Int32)),
            };
            dt.Columns.AddRange(cols);
            
            dt.Columns["Number UDK"].Unique = true;
            dt.Columns["Number UDK"].AutoIncrement = true;
            
            Object[][] rows = 
            {
                new Object[]{1, "Пушкин А.С", "Капитанская дочка", "1836 год", 1},
                new Object[]{2, "Достоевский Ф.М", "Преступление и наказание", "1866 год", 1},
                new Object[]{3, "Гоголь Н.В", "Мёртвые души", "1842 год", 1},
                new Object[]{4, "Шекспир У.", "Гамлет", "1603 год", 1},
                new Object[]{5, "Чехов А.П", "Хамелеон", "1884 год", 1},
                new Object[]{6, "Булгаков М.А", "Мастер и маргарита", "1967 год", 1},
                new Object[]{17, "Достоевский Ф.М", "Бесы", "1872 год", 1},
            };

            foreach (var s in rows) 
            {
                dt.Rows.Add(s);
            }

            dt = dt.Select()
                .OrderBy(row => row.Field<string>("Name"))
                .CopyToDataTable();        
        }
        
        static void BTreefromDatatable(Tree tree, DataRow[] list, int size)
        {
            if (size > 0) {
                int left, right, middle = size / 2;
                tree = new BinaryTree<string>(list[middle]);
                left = middle;
                right = size - middle - 1;
                BTreefromDatatable(&((*node)->left), list, left);
                BTreefromDatatable(&((*node)->right), list[middle + 1], right);
            }
        }*/

        /*public Tree tree_of_name(TreeView tv, DataTable dt)
        {
            
            tv.Nodes.Clear();
            int index = dt.Rows.Count / 2;

            var root = tv.Nodes.Add(dt.Select()
                .Where(row => row[index]));
        }*/
        
        
        // вставка
        public void Add(string value)
        {
            if (this.value == null)
                this.value = value;
            else
            {
                if (this.value.CompareTo(value) == 1)
                {
                    if (left == null)
                        this.left = new Tree();
                    left.Add(value);
                }
                else if (this.value.CompareTo(value) == -1)
                {
                    if (right == null)
                        this.right = new Tree();
                    right.Add(value);
                }
                else
                    throw new Exception("Узел уже существует");
            }

            this.count = Recount(this);
        }

        // поиск
        public Tree Search(string value)
        {
            if (this.value == value)
                return this;
            else if (this.value.CompareTo(value) == 1)
            {
                if (left != null)
                    return this.left.Search(value);
                else
                    throw new Exception("Искомого узла в дереве нет");
            }
            else
            {
                if (right != null)
                    return this.right.Search(value);
                else
                    throw new Exception("Искомого узла в дереве нет");
            }
        }

        // отображение в строку
        public string Display(Tree t)
        {
            string result = "";
            if (t.left != null)
                result += Display(t.left);

            result += t.value + " ";

            if (t.right != null)
                result += Display(t.right);

            return result;
        }

        //удаление
        public void Remove(string value)
        {
            Tree t = Search(value);
            string[] tmp1 = Display(t).TrimEnd().Split(';');
            string[] tmp2 = new string[tmp1.Length - 1];

            int i = 0;
            foreach (string s in tmp1)
            {
                if (s != value)
                    tmp2[i++] = s;
            }

            t.Clear();
            foreach (string s in tmp2)
                t.Add(s);

            this.count = Recount(this);
            
        }

        // подсчет
        private int Recount(Tree t)
        {
            int count = 0;

            if (t.left != null)
                count += Recount(t.left);

            count++;

            if (t.right != null)
                count += Recount(t.right);

            return count;
        }

        // очистка
        public void Clear()
        {
            this.value = null;
            this.left = null;
            this.right = null;
        }

        // проверка пустоты
        public bool IsEmpty()
        {
            if (this.value == null)
                return true;
            else
                return false;
        }
    }

}
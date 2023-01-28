
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace homework
{
   
    public class BinaryTree<T> where T : IComparable<T>
    {
        //private List<T> nodes = new List<T>();
        public BinaryTree<T> parent, left, right;
        private T val;
        private DataTable dt;
        public BinaryTree<string> btree;

        public BinaryTree(T val, BinaryTree<T> parent)
        {
          this.val = val;
          this.parent = parent;
        }
        
        public void init_dt()
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
        

        public void BTreeFromDatatable()
        {
            var tmp = dt.AsEnumerable()
                .Select(row => row.Field<string>("Name"))
                .OrderBy(s => s).ToArray();

            btree = BTreefromSortDatatable_Load(tmp, 0, tmp.Length);
        }
        private static BinaryTree<string> BTreefromSortDatatable_Load(string[] tree, int pos, int size)
        {
            if (size <= 0)
            {
                return null;
            }
            else
            {
                int numb_left, numb_right, middle = size / 2;
                var tmp_node = new BinaryTree<string>(tree[middle + pos], null);
                //tree = new BinaryTree<string>(rows.[middle], null);
                numb_left = middle;
                numb_right = size - middle - 1;
                
                tmp_node.left = BTreefromSortDatatable_Load(tree, pos, numb_left);
                tmp_node.right = BTreefromSortDatatable_Load(tree, pos + middle + 1, numb_right);

                return tmp_node;
            }
        }
        
        public void add(T val)
        {
              if(val.CompareTo(this.val) < 0)
              {
                  if(this.left == null)
                  {
                      this.left = new BinaryTree<T>(val, this);
                  }
                  else if(this.left != null)
                      this.left.add(val);
              }
              else
              {
                  if(this.right == null)
                  {
                      this.right = new BinaryTree<T>(val, this);
                  }
                  else if(this.right != null)
                      this.right.add(val);
              }
        }
        
        private BinaryTree<T> _search(BinaryTree<T> tree, T val)
        {
              if(tree == null) return null;
              switch (val.CompareTo(tree.val))
              {
                  case 1: 
                      return _search(tree.right, val);
                  case -1: 
                      return _search(tree.left, val);
                  case 0: 
                      return tree;
                  default: 
                      return null;
              }
          }
        
        public BinaryTree<T> search(T val)
        {
              return _search(this, val);
        }
        
        public bool remove(T val)
        {
              //Проверяем, существует ли данный узел
              BinaryTree<T> tree = search(val);
              if(tree == null)
              {
                  //Если узла не существует, вернем false
                  return false;
              }
              BinaryTree<T> curTree;
      
              //Если удаляем корень
              if(tree == this)
              {
                  if(tree.right != null) 
                  {
                      curTree = tree.right;
                  }
                  else curTree = tree.left;
      
                  while (curTree.left != null) 
                  {
                      curTree = curTree.left;
                  }
                  T temp = curTree.val;
                  this.remove(temp);
                  tree.val = temp;
      
                  return true;
              }
      
              //Удаление листьев
              if(tree.left == null && tree.right == null && tree.parent != null)
              {
                  if(tree == tree.parent.left)
                      tree.parent.left = null;
                  else 
                  {
                      tree.parent.right = null;
                  }
                  return true;
              }
      
              //Удаление узла, имеющего левое поддерево, но не имеющее правого поддерева
              if(tree.left != null && tree.right == null){
                  //Меняем родителя
                  tree.left.parent = tree.parent;
                  if(tree == tree.parent.left)
                  {
                      tree.parent.left = tree.left;
                  }
                  else if(tree == tree.parent.right)
                  {
                      tree.parent.right = tree.left;
                  }
                  return true;
              }
      
              //Удаление узла, имеющего правое поддерево, но не имеющее левого поддерева
              if(tree.left == null && tree.right != null){
                  //Меняем родителя
                  tree.right.parent = tree.parent;
                  if(tree == tree.parent.left)
                  {
                      tree.parent.left = tree.right;
                  }
                  else if(tree == tree.parent.right)
                  {
                      tree.parent.right = tree.right;
                  }
                  return true;
              }
      
              //Удаляем узел, имеющий поддеревья с обеих сторон
              if(tree.right != null && tree.left != null) 
              {
                  curTree = tree.right;
      
                  while (curTree.left != null) 
                  {
                      curTree = curTree.left;
                  }
      
                  //Если самый левый элемент является первым потомком
                  if(curTree.parent == tree) 
                  {
                      curTree.left = tree.left;
                      tree.left.parent = curTree;
                      curTree.parent = tree.parent;
                      if (tree == tree.parent.left) 
                      {
                          tree.parent.left = curTree;
                      } 
                      else if (tree == tree.parent.right) 
                      {
                          tree.parent.right = curTree;
                      }
                      return true;
                  }
                  //Если самый левый элемент НЕ является первым потомком
                  else {
                      if (curTree.right != null) 
                      {
                          curTree.right.parent = curTree.parent;
                      }
                      curTree.parent.left = curTree.right;
                      curTree.right = tree.right;
                      curTree.left = tree.left;
                      tree.left.parent = curTree;
                      tree.right.parent = curTree;
                      curTree.parent = tree.parent;
                      if (tree == tree.parent.left) 
                      {
                          tree.parent.left = curTree;
                      } else if (tree == tree.parent.right) 
                      {
                          tree.parent.right = curTree;
                      }
      
                      return true;
                  }
              }
              return false;
          }
        
          
          public override string ToString()
          {
            return val.ToString();
          }
    }
}
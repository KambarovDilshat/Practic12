
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practic12

{
    public interface iPropertychanged
    {
        event PropertyeventHandler Propertychanged;
    }

    public delegate void PropertyeventHandler(object sender, PropertyeventArgs e);

    public class PropertyeventArgs : EventArgs
    {
        public string PropertyName { get; private set; }

        public PropertyeventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    public class MyClass : iPropertychanged
    {
        private int myProperty;
        public event PropertyeventHandler Propertychanged;

        public int MyProperty
        {
            get { return myProperty; }
            set
            {
                if (myProperty != value)
                {
                    myProperty = value;
                    OnPropertyChanged(nameof(MyProperty));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            Propertychanged?.Invoke(this, new PropertyeventArgs(propertyName));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass myObj = new MyClass();
            myObj.Propertychanged += MyObj_Propertychanged;

            myObj.MyProperty = 10; 
            myObj.MyProperty = 20; 
        }

        private static void MyObj_Propertychanged(object sender, PropertyeventArgs e)
        {
            Console.WriteLine($"Property changed: {e.PropertyName}");
        }
    }
}
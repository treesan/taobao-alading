using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Alading.GetData
{
    public partial class Test : DevExpress.XtraEditors.XtraForm
    {
        public delegate void NumberReachedEventHandler(object sender, NumberReachedEventArgs e);


        public Test()
        {
            InitializeComponent();

        }

        private void Test_Load(object sender, EventArgs e)
        {
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Counter counter = new Counter();
            counter.NumberReached += new NumberReachedEventHandler(counter_NumberReached);
            counter.CountTo(100, 50);
        }

        void counter_NumberReached(object sender, Test.NumberReachedEventArgs e)
        {
            MessageBox.Show("Reached: " + e.ReachedNumber.ToString());
        }


        /// <summary> 

        /// Summary description for Counter. 

        /// </summary> 

        public class Counter
        {
            public event NumberReachedEventHandler NumberReached;

            public Counter()
            {

                // 

                // TODO: Add constructor logic here 

                // 

            }

            public void CountTo(int countTo, int reachableNum)
            {
                if (countTo < reachableNum)
                    throw new ArgumentException("reachableNum should be less than countTo");

                for (int ctr = 0; ctr <= countTo; ctr++)
                {
                    if (ctr == reachableNum)
                    {
                        NumberReachedEventArgs e = new NumberReachedEventArgs(reachableNum);
                        OnNumberReached(e);
                        return;//don't count any more 
                    }
                }
            }
            protected virtual void OnNumberReached(NumberReachedEventArgs e)
            {
                if (NumberReached != null)
                {
                    NumberReached(this, e);//Raise the event 
                }
            }
        }


        public class NumberReachedEventArgs : EventArgs
        {
            private int _reached;
            public NumberReachedEventArgs(int num)
            {
                this._reached = num;
            }
            public int ReachedNumber
            {
                get
                {
                    return _reached;
                }
            }

        }

        /*在Counter中，如果到达指定的时间点，就触发一次事件，有以下几个方面需要注意： 

          l        通过调用NumberReached（它是NumberReachedEventHandler委托的实例）来完成一次触发事件。 

          NumberReached(this, e);  通过这种方式，可以调用所有的注册函数。 

          l        通过 NumberReachedEventArgs e = new NumberReachedEventArgs(reachableNum); 为所有的注册函数提供事件数据。 

          l        看了上面的代码，你可能要问了：为什么我们直接用 OnNumberReached(NumberReachedEventArgs e)方法来调用NumberReached（this，e），而不用下面的代码呢？ 

              if(ctr == reachableNum) 

          { 

              NumberReachedEventArgs e = new NumberReachedEventArgs(reachableNum); 

              //OnNumberReached(e); 

              if(NumberReached != null) 

              { 

                  NumberReached(this, e);//Raise the event 

              } 

              return;//don't count any more 

          } 

          这个问题问得很好，那就让我们再看一下OnNumberReached 签名： 

          protected virtual void OnNumberReached(NumberReachedEventArgs e) 

          ①你也明白 关键字protected限定了 只有从该类继承的类才能调用该类中的所有方法。 
          ②关键字 virtual 表明了 在继承类中可以重写该方法。 

          这两点非常有用，假设你在写一个从Counter继承而来的类，通过重写OnNumberReached 方法，你可以在事件触发之前，进行一次其他的工作。 

          

          protected override void OnNumberReached(NumberReachedEventArgs e) 

          { 

              //Do additional work 

              base.OnNumberReached(e); 

          } 

          注意：如果你没有调用base.OnNumberReached(e), 那么从不会触发这个事件！在你继承该类而想剔出它的一些其他事件时，使用该方式是非常有用的。 

          l        还要注意到：委托 NumberReachedEventHandler 是在类定义的外部，命名空间内定义的，对所有类来说是可见的。 
         
  */


    }
}
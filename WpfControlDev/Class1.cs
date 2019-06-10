using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication4
{
    using MtTool.Model;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    namespace MtTool.Model
    {
        /// <summary>
        /// 数据接口
        /// </summary>
        public interface IDataHandle
        {
            /// <summary>
            /// 数据对象比较接口
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            bool Predicate(object obj);
            /// <summary>
            /// 数据对象更新接口
            /// </summary>
            /// <param name="obj"></param>
            void Update(object obj);
        }

        /// <summary>
        /// 数据集接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface IData<T> where T : IDataHandle
        {
            ObservableCollection<T> GetAll();
            ObservableCollection<T> GetByUser(string user);
            void AddByUser(string user, T t);
            void DelByUser(string user, T t);
        }

        /// <summary>
        /// 数据集接口实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class DataMgr<T> : IData<T> where T : IDataHandle
        {
            private ConcurrentDictionary<string, ObservableCollection<T>> AllataDict;
            private ObservableCollection<T> AllDatas;

            public DataMgr()
            {
                AllataDict = new ConcurrentDictionary<string, ObservableCollection<T>>();
                AllDatas = new ObservableCollection<T>();
            }

            public void AddByUser(string user, T t)
            {
                if (!AllataDict.ContainsKey(user))
                {
                    AllataDict[user] = new ObservableCollection<T>();
                }
                AllataDict[user].Add(t);
                AllDatas.Add(t);
            }
            public void DelByUser(string user, T t)
            {
                if (!AllataDict.ContainsKey(user))
                    return;
                var fd = AllDatas.FirstOrDefault((TSrc) =>
                {
                    return TSrc.Predicate(t);
                });
                if (fd != null)
                {
                    AllDatas.Remove(fd);
                    AllataDict[user].Remove(fd);
                }
            }
            public void UpdateData(T t)
            {
                var fd = AllDatas.FirstOrDefault((TSrc) =>
                {
                    return TSrc.Predicate(t);
                });
                if (fd != null)
                {
                    fd.Update(t);
                }
            }
            public T FindData(T t)
            {
                return AllDatas.FirstOrDefault((TSrc) =>
                {
                    return TSrc.Predicate(t);
                });
            }

            public ObservableCollection<T> GetAll()
            {
                ObservableCollection<T> all = new ObservableCollection<T>();
                foreach (var item in AllataDict.Values)
                {
                    foreach (var it in item)
                    {
                        all.Add(it);
                    }
                }
                return all;
            }

            public ObservableCollection<T> GetByUser(string user)
            {
                if (AllataDict.ContainsKey(user))
                    return AllataDict[user];
                return new ObservableCollection<T>();
            }
        }

        /// <summary>
        /// 数据实现1
        /// </summary>
        public class Trade : IDataHandle
        {
            public int Amount;
            public double Price;
            public string Code;

            public bool Predicate(object obj)
            {
                return ((Trade)obj).Code == this.Code;
            }

            public void Update(object obj)
            {
                Trade td = obj as Trade;
                this.Price = td.Price;
                this.Amount = td.Amount;
            }
        }

        /// <summary>
        /// 公共数据接口如ICoreDataService
        /// 接口够不够用，要做个统计分析，不能随意增减
        /// </summary>
        public interface IDataCoreMgr
        {
            void AddByUser<T>(string user, T t) where T : IDataHandle;
            void DelByUser<T>(string user, T t) where T : IDataHandle;
            ObservableCollection<T> GetAll<T>() where T : IDataHandle;
            ObservableCollection<T> GetByUser<T>(string user) where T : IDataHandle;
            T FindData<T>(T t) where T : IDataHandle;
            void UpdateData<T>(T t) where T : IDataHandle;
        }

        /// <summary>
        /// 数据集综合管理接口 如CoreDataService
        /// </summary>
        public class DataCoreMgr : IDataCoreMgr
        {
            private ConcurrentDictionary<Type, DataMgr<IDataHandle>> dataAll;
            public DataCoreMgr()
            {
                dataAll = new ConcurrentDictionary<Type, DataMgr<IDataHandle>>();
            }

            public void AddByUser<T>(string user, T t)
                where T : IDataHandle
            {
                Type tp = typeof(T);
                if (!dataAll.ContainsKey(tp))
                {
                    dataAll[tp] = new DataMgr<IDataHandle>();
                }
                dataAll[tp].AddByUser(user, t);
            }
            public void DelByUser<T>(string user, T t)
                where T : IDataHandle
            {
                Type tp = typeof(T);
                if (dataAll.ContainsKey(tp))
                {
                    dataAll[tp].DelByUser(user, t);
                }
            }
            public ObservableCollection<T> GetAll<T>()
                where T : IDataHandle
            {
                Type tp = typeof(T);
                if (dataAll.ContainsKey(tp))
                {
                    return (dataAll[tp] as DataMgr<T>).GetAll();
                }
                return new ObservableCollection<T>();
            }

            public ObservableCollection<T> GetByUser<T>(string user)
                where T : IDataHandle
            {
                Type tp = typeof(T);
                if (dataAll.ContainsKey(tp))
                {
                    return (dataAll[tp] as DataMgr<T>).GetByUser(user);
                }
                return new ObservableCollection<T>();
            }

            public T FindData<T>(T t) where T : IDataHandle
            {
                Type tp = typeof(T);
                if (dataAll.ContainsKey(tp))
                {
                    return (T)dataAll[tp].FindData(t);
                }
                return default(T);
            }
            public void UpdateData<T>(T t) where T : IDataHandle
            {
                Type tp = typeof(T);
                if (dataAll.ContainsKey(tp))
                {
                    dataAll[tp].UpdateData(t);
                }
            }

            #region 模块内部调用接口
            //如     public void SetFutuOrderQry(IUserMgrBase user, UserBase info, RspFutuQryOrder rsp, bool isend)
            //都该为 internal void SetFutuOrderQry(IUserMgrBase user, UserBase info, RspFutuQryOrder rsp, bool isend)
            #endregion
        }
        /// <summary>
        /// 数据效率测试
        /// </summary>
        public class TestDataMgr
        {
            DataCoreMgr dcm = new DataCoreMgr();
            public void AddData()
            {
                for (int i = 0; i < 100000; i++)
                {
                    Trade td = new Trade() { Code = "code" + i, Amount = i, Price = 1.1 + i };
                    dcm.AddByUser("zxh", td);
                }
            }
            public void Update()
            {
                Trade[] trades = { new Trade() { Code = "code99", Amount = 999, Price = 12999.9 }, new Trade() { Code = "code89999", Amount = 999, Price = 12999.9 }, new Trade() { Code = "code48888", Amount = 999, Price = 12999.9 } };
                foreach (var td in trades)
                {
                    var fd = dcm.FindData(td);
                    if (fd != null)
                        Console.WriteLine("Before Update  Code:{0},Amount:{1},Price:{2}", fd.Code, fd.Amount, fd.Price);
                    dcm.UpdateData(td);
                    //fd = dcm.FindData(td);
                    //if (fd != null)
                    //    Console.WriteLine("After  Update  Code:{0},Amount:{1},Price:{2}", fd.Code, fd.Amount, fd.Price);
                }
            }
        }

        #region 观察者模式

        /// <summary>
        /// 委托充当订阅者接口类
        /// </summary>
        /// <param name="sender"></param>
        public delegate void NotifyEventHandler(object sender);

        /// <summary>
        /// 订阅博客基类
        /// </summary>
        public class Blog
        {
            public NotifyEventHandler NotifyEvent;
            /// <summary>
            /// 博主名
            /// </summary>
            public string BlogName { get; set; }

            /// <summary>
            /// 博客标题
            /// </summary>
            public string BlogTitle { get; set; }

            /// <summary>
            /// 博客信息
            /// </summary>
            public string BlogInfo { get; set; }

            /// <summary>
            /// 博客构造函数
            /// </summary>
            /// <param name="blogTitle">博客标题</param>
            /// <param name="blogInfo">博客信息</param>
            public Blog(string name, string blogTitle, string blogInfo)
            {
                this.BlogName = name;
                this.BlogTitle = blogTitle;
                this.BlogInfo = blogInfo;
            }

            /// <summary>
            /// 绑定订阅事件
            /// </summary>
            /// <param name="ob">订阅者</param>
            public void AddObserver(NotifyEventHandler observer)
            {
                NotifyEvent += observer;
            }

            /// <summary>
            /// 取消绑定订阅
            /// </summary>
            /// <param name="observer"></param>
            public void RemoteObserver(NotifyEventHandler observer)
            {
                NotifyEvent -= observer;
            }

            /// <summary>
            /// 发布博客通知
            /// </summary>
            public void PublishBlog()
            {
                if (NotifyEvent != null)
                {
                    NotifyEvent(this);
                }
            }
        }

        /// <summary>
        /// 具体的订阅博客类
        /// </summary>
        public class JiYFBlog : Blog
        {
            public JiYFBlog(string name, string blogTitile, string blogInfo)
                : base(name, blogTitile, blogInfo)
            {

            }
        }

        /// <summary>
        /// 具体的订阅者类
        /// </summary>
        public class Observer
        {
            /// <summary>
            /// 订阅者名字
            /// </summary>
            private string m_Name;
            public string Name
            {
                get { return m_Name; }
                set { m_Name = value; }
            }

            /// <summary>
            /// 订阅者构造函数
            /// </summary>
            /// <param name="name">订阅者名字</param>
            public Observer(string name)
            {
                this.m_Name = name;
            }

            /// <summary>
            /// 订阅者接受函数
            /// </summary>
            /// <param name="blog"></param>
            public void Receive(object obj)
            {
                Blog blog = obj as Blog;
                if (blog != null)
                {
                    Console.WriteLine("订阅者:\"{0}\"观察到了:{1}发布的一篇博客,标题为:{2},内容为:{3}", Name, blog.BlogName, blog.BlogTitle, blog.BlogInfo);
                }
            }
        }

        class Client
        {
            static void Memg()
            {
                Console.WriteLine("--全部订阅者--");
                //创建一个JiYF的博客
                Blog jiyfBlog = new Blog("JiYF笨小孩", "丑小鸭", "丑小鸭的故事");

                //创建订阅者
                Observer obsZhangsan = new Observer("张三");
                Observer obsLiSi = new Observer("李四");
                Observer obsWangwu = new Observer("王五");

                //添加对JiYF博客的订阅者
                jiyfBlog.AddObserver(new NotifyEventHandler(obsZhangsan.Receive));
                jiyfBlog.AddObserver(new NotifyEventHandler(obsLiSi.Receive));
                jiyfBlog.AddObserver(new NotifyEventHandler(obsWangwu.Receive));

                ////通知订阅者
                jiyfBlog.PublishBlog();

                Console.WriteLine("---移除订阅者张三--");
                jiyfBlog.RemoteObserver(new NotifyEventHandler(obsZhangsan.Receive));
                jiyfBlog.PublishBlog();
                Console.ReadLine();
            }
        }


        public class Subject
        {
            private NotifyEventHandler NotifyEvent;
            public void AddObserver(NotifyEventHandler observer)
            {
                NotifyEvent += observer;
            }

            /// <summary>
            /// 取消绑定订阅
            /// </summary>
            /// <param name="observer"></param>
            public void RemoteObserver(NotifyEventHandler observer)
            {
                NotifyEvent -= observer;
            }

            /// <summary>
            /// 发布博客通知
            /// </summary>
            public void Publish(object obj)
            {
                NotifyEvent?.Invoke(obj);
            }
        }

        public interface IHandle
        {
            void MsgHandle(object t);
        }

        public interface IHandle<T>
        {
            void MsgHandle(T t);
        }
        public class A1
        {
            public string Name = "A1";
            public string Sex = "f";
        }
        public class A2
        {
            public string Name = "A2";
            public int Age = 22;
        }
        public class A3
        {
            public string Name = "A3";
            public double Money = 121.2;
        }

        public class AA : IHandle<A1>, IHandle<A2>, IHandle<A3>, IHandle, IDataHandle
        {
            public string Flag = "AA";
            public AA(string flag)
            {
                Flag = flag;
            }
            public void MsgHandle(A1 t)
            {
                //Console.WriteLine("I'm " + Flag + " Recv: " + t.Name + ":" + t.Sex);
                Thread.Sleep(5);
            }

            public void MsgHandle(A2 t)
            {
                //Console.WriteLine("I'm " + Flag + " Recv: " + t.Name);
                Thread.Sleep(4);
            }
            public void MsgHandle(A3 t)
            {
                //Console.WriteLine("I'm " + Flag + " Recv: " + t.Name);
                Thread.Sleep(2);
            }

            public void MsgHandle(object t)
            {
                //Console.WriteLine("I'm " + Flag + " Recv: " + t.GetType());
            }

            public bool Predicate(object obj)
            {
                Console.WriteLine("Predicate");
                return true;
            }

            public void Update(object obj)
            {
                Console.WriteLine("Update");
            }
        }

        public class TestInterface
        {
            public void Test()
            {
                SubjectMgr sm = new SubjectMgr();
                A1 a1 = new A1();
                A2 a2 = new A2();
                A3 a3 = new A3();
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 10000; i++)
                {
                    AA aa = new AA("AA" + i);
                    sm.AddObserver(aa);
                }
                sw.Stop();
                TimeSpan ts2 = sw.Elapsed;
                Console.WriteLine("Stopwatch1总共花费{0}ms.", ts2.TotalMilliseconds);

                sw = new Stopwatch();
                sw.Start();
                sm.Publish(a1);
                sw.Stop();
                ts2 = sw.Elapsed;
                Console.WriteLine("Stopwatch2总共花费{0}ms.", ts2.TotalMilliseconds);

                sw = new Stopwatch();
                sw.Start();
                sm.Publish(a2);
                sw.Stop();
                ts2 = sw.Elapsed;
                Console.WriteLine("Stopwatch3总共花费{0}ms.", ts2.TotalMilliseconds);

                sw = new Stopwatch();
                sw.Start();
                sm.Publish(a3);
                sw.Stop();
                ts2 = sw.Elapsed;
                Console.WriteLine("Stopwatch4总共花费{0}ms.", ts2.TotalMilliseconds);
            }
        }

        public class SubjectMgr
        {
            private ConcurrentDictionary<Type, List<object>> SubDict;
            public SubjectMgr()
            {
                SubDict = new ConcurrentDictionary<Type, List<object>>();
            }

            public void AddObserver(object observer)
            {
                var tps = observer.GetType().GetInterfaces();
                foreach (var tp in tps)
                {
                    if (!SubDict.ContainsKey(tp))
                    {
                        SubDict.TryAdd(tp, new List<object>());
                    }
                    SubDict[tp].Add(observer);
                }
            }

            /// <summary>
            /// 取消绑定订阅
            /// </summary>
            /// <param name="observer"></param>
            public void RemoteObserver(object observer)
            {
                var tps = observer.GetType().GetInterfaces();
                foreach (var tp in tps)
                {
                    if (SubDict.ContainsKey(tp))
                    {
                        SubDict[tp].Remove(observer);
                    }
                }
            }

            /// <summary>
            /// 发布博客通知
            /// </summary>
            public void Publish<T>(T obj)
            {
                Type tp = typeof(IHandle<T>);
                if (SubDict.ContainsKey(tp))
                {
                    var hds = SubDict[tp];
                    foreach (var hd in hds)
                    {
                        var handle = hd as IHandle<T>;
                        handle?.MsgHandle(obj);
                    }
                }
            }
        }

        #endregion

    }

}

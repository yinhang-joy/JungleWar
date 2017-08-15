using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Reflection;
using GameServer.Servers;
namespace GameServer.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> ControllerDic = new Dictionary<RequestCode, BaseController>();
        private Server server;
        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }
        private void InitController()
        {
            DefaultController defaultController = new DefaultController();
            ControllerDic.Add(defaultController.RequestCode, defaultController);
            ControllerDic.Add(RequestCode.User, new UserController());
            ControllerDic.Add(RequestCode.Room, new RoomController());
            ControllerDic.Add(RequestCode.Game, new GameController());
        }
        public void HandleRequest(RequestCode requestCode,ActionCode actionCode,string data,Client client)
        {
            BaseController controller;
            bool isGet = ControllerDic.TryGetValue(requestCode,out controller); //通过请求获取Controller
            if (isGet==false)
            {
                Console.WriteLine("无法得到【"+requestCode+"】所对应的Controller，无法处理请求");
                return;
            }
            string methodName = Enum.GetName(typeof(ActionCode),actionCode);//得到请求Controller中的方法名
            MethodInfo method= controller.GetType().GetMethod(methodName);//得到请求的方法的信息
            if (method ==null)
            {
                Console.WriteLine("在Controller【"+controller.GetType()+"】中没有对应的处理方法【"+ methodName+"】");
            }
            Console.WriteLine(controller.GetType().ToString()+methodName);
            object[] parameters = new object[] { data, client ,server};
            object o= method.Invoke(controller, parameters);//执行请求的方法
            if (o==null)
            {
                return;
            }
            server.SendResponse(client, actionCode, o as string);//给客户端发送响应
        }
    }
}

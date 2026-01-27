using System.Diagnostics;
using AllcandoJM.KohaFramework.ApiClientBiblios;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using AllcandoJM.KohaFramework.ApiCore;
using System.Runtime.CompilerServices;

namespace AllcandoJM.KohaFramework.ApiClientCLI
{
    internal class Program
    {
        static List<cmdarg> AvaiableCmdAargs;
        static List<cmdarg> ActivatedCmdArgs = new List<cmdarg>();

        
        static AuthMethod Authmethod;
        static string url;
        static string user;
        static string secret;
        static string method;
        static string query;
        static bool fromConfig = false;
        static bool saveToFile = false;
        static string saveToFilePath;

        static MethodInfo[] BuildFunctionList(Type type)
        {
            
            var methods = type.GetMethods(BindingFlags.Public| BindingFlags.Instance| BindingFlags.Static|BindingFlags.DeclaredOnly)
                 .Where(m => typeof(Task).IsAssignableFrom(m.ReturnType)).ToArray();

            
            return methods;
        }

        static void SetAuth(string auth)
        {
            if(auth == "HTTP_BASIC")
            {
                Authmethod = AuthMethod.Basic;
            }
            else
            {
                Authmethod = AuthMethod.ApiKey;
            }
        }

        static async Task Main(string[] args)
        {

            List<MethodInfo> methods = new List<MethodInfo>();
            methods.AddRange(BuildFunctionList(typeof(ApiClientBiblios.ApiClientBiblios)));
            methods.AddRange(BuildFunctionList(typeof(ApiClientHolds.ApiClientHolds)));
            methods.AddRange(BuildFunctionList(typeof(ApiClientItems.ApiClientItems)));
            methods.AddRange(BuildFunctionList(typeof(ApiClientPatrons.ApiClientPatrons)));

            StringBuilder methodstring = new StringBuilder();
            methods.ForEach(methods =>
            {
                methodstring.Append(methods.Name + ",");
            });

            AvaiableCmdAargs = new List<cmdarg>()
            {
                new cmdarg("-h", "-h - displays help message", (string x) => DisplayHelpMessage()),
                new cmdarg("-t", $"-t - method to use - {methodstring.ToString()}", (string x) => SetMethod(x)),
                new cmdarg("-u", "-u -url to koha instance",(string x)=> url=x),
                new cmdarg("-l","-l - user login or user id",(string x)=> user=x),
                new cmdarg("-p","-p - user password or user secret",(string x)=>secret=x),
                new cmdarg("-a","-a - authmethod HTTP_BASIC/oauth",(string x)=> SetAuth(x)),
                new cmdarg("-q","-q - input for method",(string x) => query = x),
                new cmdarg("-c","-c - load connection data from conf file",(string x)=>SetFromConfig()),
                new cmdarg("-o","-o - output file path - if not supplied output will be in console",(string x) =>SetToFile(x))
            };

            ParseCommandLineArgs2(args);
            CheckForRequiredArgs();
            InvokeParamSetters();


            MethodInfo m = methods.Where(x => x.Name == method && x.GetParameters().Length == 1).FirstOrDefault();
            //    public ApiClient(AuthMethod authMethod,string url,string user,string secret)

            if(m is null)
            {
                Environment.Exit(0);
            }
            else
            {
                object instance= null;
                if (fromConfig)
                {
                    
                   instance =  Activator.CreateInstance(m.DeclaringType);
                }
                else
                {
                    instance = Activator.CreateInstance(m.DeclaringType,
                    new object[] { Authmethod, url, user, secret });
                }

                
           

            var task = (Task)m.Invoke(instance, new[] { query});
            await task;

            string result = (string)task
    .GetType()
    .GetProperty("Result")!
    .GetValue(task);

                if (saveToFile)
                {
                    File.WriteAllText(saveToFilePath, result);
                }
                Console.WriteLine(result);
            }
        }

        static void SetMethod(string m)
        {
            method = m;
        }

        static void SetToFile(string fname)
        {
            saveToFile = true;
            saveToFilePath = fname;
        }

        static void SetFromConfig()
        {
            fromConfig = true;
        }

        static void ParseCommandLineArgs2(string[] args)
        {
            cmdarg ToActivate = null;
            bool AlreadyAssignedValue = false;

            foreach (string arg in args)
            {
                try
                {
                    cmdarg PickedArg = AvaiableCmdAargs.Where(x => x.ArgName == arg).Single();
                    ToActivate = new cmdarg(PickedArg.ArgName, PickedArg.ArgHelpMessage, null, PickedArg.ArgAction);
                    ActivatedCmdArgs.Add(ToActivate);
                    AlreadyAssignedValue = false;
                }
                catch (InvalidOperationException)
                {
                    if (AlreadyAssignedValue)
                    {
                        throw;// TODO: present error to user
                    }
                    else
                    {
                        ToActivate.ArgValue = arg;
                        AlreadyAssignedValue = true;
                    }
                }

            }
        }

        static void InvokeParamSetters()
        {
            foreach (cmdarg p in ActivatedCmdArgs)
            {
                p.ArgAction.Invoke(p.ArgValue);
            }
        }


        static void CheckForRequiredArgs()
        {
            List<cmdarg> RequiredArgs = AvaiableCmdAargs.Where(x => x.IsRequired == true).ToList();
            List<cmdarg> Activated = ActivatedCmdArgs;
            Activated.ForEach(active =>
            {
                IEnumerable<cmdarg> marked = RequiredArgs.Where(x => x.ArgName == active.ArgName);
                if (marked.Count() == 1)
                {
                    RequiredArgs.Remove(marked.Single());
                }
            });

            if (RequiredArgs.Count != 0)
            {
                RequiredArgs.ForEach(r => WriteErrorMessage($"[ERROR] Missing required argument - {r.ArgHelpMessage}"));
                Crash("[CRASH] Mail couln't be sent. Missing required arguments above");
            }
        }


        
        static void DisplayHelpMessage()
        {
            foreach (cmdarg c in AvaiableCmdAargs)
            {
                Console.WriteLine($"{c.ArgName}, {c.ArgHelpMessage}");
            }
        }

        static void WriteErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

  

        static void Crash(string CrashMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(CrashMessage);
            Console.ResetColor();
            Environment.Exit(0);
        }
    }
}
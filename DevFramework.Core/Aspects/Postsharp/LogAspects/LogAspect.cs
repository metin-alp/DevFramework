using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.CrossCuttingConcerns.Logging;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using PostSharp.Aspects.Configuration;
using PostSharp.Aspects.Serialization;
using PostSharp.Extensibility;
using PostSharp.Serialization;
using log4net;
using PostSharp.Aspects.Advices;
using PostSharp.Reflection;

namespace DevFramework.Core.Aspects.Postsharp.LogAspects
{
    //[PSerializableAttribute]
    [Serializable]
    //[OnMethodBoundaryAspectConfiguration(SerializerType = typeof(MsilAspectSerializer))]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
    public class LogAspect : OnMethodBoundaryAspect
    {

        private Type _loggerType;
        private LoggerService _loggerService;

        public LogAspect(LoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        public LogAspect(int AttributePriority, MulticastAttributes AttributeTargetMemberAttributes)
        {
            this.AttributePriority = AttributePriority;
            this.AttributeTargetMemberAttributes = AttributeTargetMemberAttributes;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType != typeof(LoggerService))
            {
                throw new Exception("Wrong logger type");
            }
            _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);
            base.RuntimeInitialize(method);
        }
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!_loggerService.IsInfoEnabled)
            {
                return;
            }
            try
            {
                var logParameters = args.Method.GetParameters().Select((t, i) => new LogParamater
                {
                    Name = t.Name,
                    Type = t.ParameterType.Name,
                    Value = args.Arguments.GetArgument(i)
                }).ToList();

                var logDetail = new LogDetail
                {
                    FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
                    MethodName = args.Method.Name,
                    Parameters = logParameters
                };
                _loggerService.Info(logDetail);
            }
            catch (Exception)
            {

            }

            base.OnEntry(args);
        }

        public object CreateInstance(AdviceArgs adviceArgs)
        {
            throw new NotImplementedException();
        }

        public void RuntimeInitializeInstance()
        {
            throw new NotImplementedException();
        }
    }
}


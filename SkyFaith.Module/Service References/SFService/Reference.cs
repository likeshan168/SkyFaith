﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkyFaith.Module.SFService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://service.expressservice.integration.sf.com/", ConfigurationName="SFService.IExpressService")]
    public interface IExpressService {
        
        // CODEGEN: 命名空间  的元素名称 arg0 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        SkyFaith.Module.SFService.sfexpressServiceResponse sfexpressService(SkyFaith.Module.SFService.sfexpressService request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<SkyFaith.Module.SFService.sfexpressServiceResponse> sfexpressServiceAsync(SkyFaith.Module.SFService.sfexpressService request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class sfexpressService {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="sfexpressService", Namespace="http://service.expressservice.integration.sf.com/", Order=0)]
        public SkyFaith.Module.SFService.sfexpressServiceBody Body;
        
        public sfexpressService() {
        }
        
        public sfexpressService(SkyFaith.Module.SFService.sfexpressServiceBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class sfexpressServiceBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string arg0;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string arg1;
        
        public sfexpressServiceBody() {
        }
        
        public sfexpressServiceBody(string arg0, string arg1) {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class sfexpressServiceResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="sfexpressServiceResponse", Namespace="http://service.expressservice.integration.sf.com/", Order=0)]
        public SkyFaith.Module.SFService.sfexpressServiceResponseBody Body;
        
        public sfexpressServiceResponse() {
        }
        
        public sfexpressServiceResponse(SkyFaith.Module.SFService.sfexpressServiceResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class sfexpressServiceResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public sfexpressServiceResponseBody() {
        }
        
        public sfexpressServiceResponseBody(string @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IExpressServiceChannel : SkyFaith.Module.SFService.IExpressService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ExpressServiceClient : System.ServiceModel.ClientBase<SkyFaith.Module.SFService.IExpressService>, SkyFaith.Module.SFService.IExpressService {
        
        public ExpressServiceClient() {
        }
        
        public ExpressServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ExpressServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExpressServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExpressServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SkyFaith.Module.SFService.sfexpressServiceResponse SkyFaith.Module.SFService.IExpressService.sfexpressService(SkyFaith.Module.SFService.sfexpressService request) {
            return base.Channel.sfexpressService(request);
        }
        
        public string sfexpressService(string arg0, string arg1) {
            SkyFaith.Module.SFService.sfexpressService inValue = new SkyFaith.Module.SFService.sfexpressService();
            inValue.Body = new SkyFaith.Module.SFService.sfexpressServiceBody();
            inValue.Body.arg0 = arg0;
            inValue.Body.arg1 = arg1;
            SkyFaith.Module.SFService.sfexpressServiceResponse retVal = ((SkyFaith.Module.SFService.IExpressService)(this)).sfexpressService(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SkyFaith.Module.SFService.sfexpressServiceResponse> SkyFaith.Module.SFService.IExpressService.sfexpressServiceAsync(SkyFaith.Module.SFService.sfexpressService request) {
            return base.Channel.sfexpressServiceAsync(request);
        }
        
        public System.Threading.Tasks.Task<SkyFaith.Module.SFService.sfexpressServiceResponse> sfexpressServiceAsync(string arg0, string arg1) {
            SkyFaith.Module.SFService.sfexpressService inValue = new SkyFaith.Module.SFService.sfexpressService();
            inValue.Body = new SkyFaith.Module.SFService.sfexpressServiceBody();
            inValue.Body.arg0 = arg0;
            inValue.Body.arg1 = arg1;
            return ((SkyFaith.Module.SFService.IExpressService)(this)).sfexpressServiceAsync(inValue);
        }
    }
}

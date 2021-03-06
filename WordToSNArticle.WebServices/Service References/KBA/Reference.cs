﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WordToSNArticle.WebServices.KBA {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.service-now.com/InsertKBA", ConfigurationName="KBA.ServiceNowSoap")]
    public interface ServiceNowSoap {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.service-now.com/InsertKBA/execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        WordToSNArticle.WebServices.KBA.executeResponse1 execute(WordToSNArticle.WebServices.KBA.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.service-now.com/InsertKBA/execute", ReplyAction="*")]
        System.Threading.Tasks.Task<WordToSNArticle.WebServices.KBA.executeResponse1> executeAsync(WordToSNArticle.WebServices.KBA.executeRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.service-now.com/InsertKBA")]
    public partial class execute : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string categoryField;
        
        private string topicField;
        
        private string htmlField;
        
        private string short_descriptionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string category {
            get {
                return this.categoryField;
            }
            set {
                this.categoryField = value;
                this.RaisePropertyChanged("category");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string topic {
            get {
                return this.topicField;
            }
            set {
                this.topicField = value;
                this.RaisePropertyChanged("topic");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string html {
            get {
                return this.htmlField;
            }
            set {
                this.htmlField = value;
                this.RaisePropertyChanged("html");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string short_description {
            get {
                return this.short_descriptionField;
            }
            set {
                this.short_descriptionField = value;
                this.RaisePropertyChanged("short_description");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.service-now.com/InsertKBA")]
    public partial class executeResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string messageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("message");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.service-now.com/InsertKBA", Order=0)]
        public WordToSNArticle.WebServices.KBA.execute execute;
        
        public executeRequest() {
        }
        
        public executeRequest(WordToSNArticle.WebServices.KBA.execute execute) {
            this.execute = execute;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.service-now.com/InsertKBA", Order=0)]
        public WordToSNArticle.WebServices.KBA.executeResponse executeResponse;
        
        public executeResponse1() {
        }
        
        public executeResponse1(WordToSNArticle.WebServices.KBA.executeResponse executeResponse) {
            this.executeResponse = executeResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceNowSoapChannel : WordToSNArticle.WebServices.KBA.ServiceNowSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceNowSoapClient : System.ServiceModel.ClientBase<WordToSNArticle.WebServices.KBA.ServiceNowSoap>, WordToSNArticle.WebServices.KBA.ServiceNowSoap {
        
        public ServiceNowSoapClient() {
        }
        
        public ServiceNowSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceNowSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceNowSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceNowSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WordToSNArticle.WebServices.KBA.executeResponse1 WordToSNArticle.WebServices.KBA.ServiceNowSoap.execute(WordToSNArticle.WebServices.KBA.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public WordToSNArticle.WebServices.KBA.executeResponse execute(WordToSNArticle.WebServices.KBA.execute execute1) {
            WordToSNArticle.WebServices.KBA.executeRequest inValue = new WordToSNArticle.WebServices.KBA.executeRequest();
            inValue.execute = execute1;
            WordToSNArticle.WebServices.KBA.executeResponse1 retVal = ((WordToSNArticle.WebServices.KBA.ServiceNowSoap)(this)).execute(inValue);
            return retVal.executeResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WordToSNArticle.WebServices.KBA.executeResponse1> WordToSNArticle.WebServices.KBA.ServiceNowSoap.executeAsync(WordToSNArticle.WebServices.KBA.executeRequest request) {
            return base.Channel.executeAsync(request);
        }
        
        public System.Threading.Tasks.Task<WordToSNArticle.WebServices.KBA.executeResponse1> executeAsync(WordToSNArticle.WebServices.KBA.execute execute) {
            WordToSNArticle.WebServices.KBA.executeRequest inValue = new WordToSNArticle.WebServices.KBA.executeRequest();
            inValue.execute = execute;
            return ((WordToSNArticle.WebServices.KBA.ServiceNowSoap)(this)).executeAsync(inValue);
        }
    }
}

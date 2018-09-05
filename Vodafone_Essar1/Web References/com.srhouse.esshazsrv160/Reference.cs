﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Vodafone_Essar1.com.srhouse.esshazsrv160 {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BasicHttpBinding_IMail", Namespace="http://tempuri.org/")]
    public partial class Mail : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SendEmailWithAttachmentsOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendEmailWithOutAttachmentsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDateOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Mail() {
            this.Url = global::Vodafone_Essar1.Properties.Settings.Default.Vodafone_Essar1_com_srhouse_esshazsrv160_Mail;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SendEmailWithAttachmentsCompletedEventHandler SendEmailWithAttachmentsCompleted;
        
        /// <remarks/>
        public event SendEmailWithOutAttachmentsCompletedEventHandler SendEmailWithOutAttachmentsCompleted;
        
        /// <remarks/>
        public event GetDateCompletedEventHandler GetDateCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IMail/SendEmailWithAttachments", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string SendEmailWithAttachments([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sEmailFrom, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sUserName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sPassword, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sTO, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sCC, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sBCC, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sSubject, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sMailBody, bool bIsBodyHtml, [System.Xml.Serialization.XmlIgnoreAttribute()] bool bIsBodyHtmlSpecified, MailPriority priority, [System.Xml.Serialization.XmlIgnoreAttribute()] bool prioritySpecified, [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)] [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.datacontract.org/2004/07/EmailService")] FileAttachment[] attachments) {
            object[] results = this.Invoke("SendEmailWithAttachments", new object[] {
                        sEmailFrom,
                        sUserName,
                        sPassword,
                        sTO,
                        sCC,
                        sBCC,
                        sSubject,
                        sMailBody,
                        bIsBodyHtml,
                        bIsBodyHtmlSpecified,
                        priority,
                        prioritySpecified,
                        attachments});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SendEmailWithAttachmentsAsync(string sEmailFrom, string sUserName, string sPassword, string sTO, string sCC, string sBCC, string sSubject, string sMailBody, bool bIsBodyHtml, bool bIsBodyHtmlSpecified, MailPriority priority, bool prioritySpecified, FileAttachment[] attachments) {
            this.SendEmailWithAttachmentsAsync(sEmailFrom, sUserName, sPassword, sTO, sCC, sBCC, sSubject, sMailBody, bIsBodyHtml, bIsBodyHtmlSpecified, priority, prioritySpecified, attachments, null);
        }
        
        /// <remarks/>
        public void SendEmailWithAttachmentsAsync(string sEmailFrom, string sUserName, string sPassword, string sTO, string sCC, string sBCC, string sSubject, string sMailBody, bool bIsBodyHtml, bool bIsBodyHtmlSpecified, MailPriority priority, bool prioritySpecified, FileAttachment[] attachments, object userState) {
            if ((this.SendEmailWithAttachmentsOperationCompleted == null)) {
                this.SendEmailWithAttachmentsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendEmailWithAttachmentsOperationCompleted);
            }
            this.InvokeAsync("SendEmailWithAttachments", new object[] {
                        sEmailFrom,
                        sUserName,
                        sPassword,
                        sTO,
                        sCC,
                        sBCC,
                        sSubject,
                        sMailBody,
                        bIsBodyHtml,
                        bIsBodyHtmlSpecified,
                        priority,
                        prioritySpecified,
                        attachments}, this.SendEmailWithAttachmentsOperationCompleted, userState);
        }
        
        private void OnSendEmailWithAttachmentsOperationCompleted(object arg) {
            if ((this.SendEmailWithAttachmentsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendEmailWithAttachmentsCompleted(this, new SendEmailWithAttachmentsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IMail/SendEmailWithOutAttachments", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string SendEmailWithOutAttachments([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sEmailFrom, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sUserName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sPassword, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sTO, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sCC, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sBCC, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sSubject, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string sMailBody) {
            object[] results = this.Invoke("SendEmailWithOutAttachments", new object[] {
                        sEmailFrom,
                        sUserName,
                        sPassword,
                        sTO,
                        sCC,
                        sBCC,
                        sSubject,
                        sMailBody});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SendEmailWithOutAttachmentsAsync(string sEmailFrom, string sUserName, string sPassword, string sTO, string sCC, string sBCC, string sSubject, string sMailBody) {
            this.SendEmailWithOutAttachmentsAsync(sEmailFrom, sUserName, sPassword, sTO, sCC, sBCC, sSubject, sMailBody, null);
        }
        
        /// <remarks/>
        public void SendEmailWithOutAttachmentsAsync(string sEmailFrom, string sUserName, string sPassword, string sTO, string sCC, string sBCC, string sSubject, string sMailBody, object userState) {
            if ((this.SendEmailWithOutAttachmentsOperationCompleted == null)) {
                this.SendEmailWithOutAttachmentsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendEmailWithOutAttachmentsOperationCompleted);
            }
            this.InvokeAsync("SendEmailWithOutAttachments", new object[] {
                        sEmailFrom,
                        sUserName,
                        sPassword,
                        sTO,
                        sCC,
                        sBCC,
                        sSubject,
                        sMailBody}, this.SendEmailWithOutAttachmentsOperationCompleted, userState);
        }
        
        private void OnSendEmailWithOutAttachmentsOperationCompleted(object arg) {
            if ((this.SendEmailWithOutAttachmentsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendEmailWithOutAttachmentsCompleted(this, new SendEmailWithOutAttachmentsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IMail/GetDate", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string GetDate() {
            object[] results = this.Invoke("GetDate", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetDateAsync() {
            this.GetDateAsync(null);
        }
        
        /// <remarks/>
        public void GetDateAsync(object userState) {
            if ((this.GetDateOperationCompleted == null)) {
                this.GetDateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDateOperationCompleted);
            }
            this.InvokeAsync("GetDate", new object[0], this.GetDateOperationCompleted, userState);
        }
        
        private void OnGetDateOperationCompleted(object arg) {
            if ((this.GetDateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDateCompleted(this, new GetDateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/System.Net.Mail")]
    public enum MailPriority {
        
        /// <remarks/>
        Normal,
        
        /// <remarks/>
        Low,
        
        /// <remarks/>
        High,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1038.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/EmailService")]
    public partial class FileAttachment {
        
        private string fileContentBase64Field;
        
        private string fileNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FileContentBase64 {
            get {
                return this.fileContentBase64Field;
            }
            set {
                this.fileContentBase64Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void SendEmailWithAttachmentsCompletedEventHandler(object sender, SendEmailWithAttachmentsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendEmailWithAttachmentsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendEmailWithAttachmentsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void SendEmailWithOutAttachmentsCompletedEventHandler(object sender, SendEmailWithOutAttachmentsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendEmailWithOutAttachmentsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendEmailWithOutAttachmentsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void GetDateCompletedEventHandler(object sender, GetDateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591
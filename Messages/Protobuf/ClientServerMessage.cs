// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ClientServerMessage.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ProtobufMessages {

  /// <summary>Holder for reflection information generated from ClientServerMessage.proto</summary>
  public static partial class ClientServerMessageReflection {

    #region Descriptor
    /// <summary>File descriptor for ClientServerMessage.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ClientServerMessageReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChlDbGllbnRTZXJ2ZXJNZXNzYWdlLnByb3RvGh9nb29nbGUvcHJvdG9idWYv",
            "dGltZXN0YW1wLnByb3RvInUKC1NlcnZpY2VJbmZvEi0KCW9wZXJhdGlvbhgB",
            "IAEoDjIaLlNlcnZpY2VJbmZvLk9wZXJhdGlvblR5cGUiNwoNT3BlcmF0aW9u",
            "VHlwZRIHCgNORVcQABINCglSRUNPTk5FQ1QQARIOCgpESVNDT05ORUNUEAIi",
            "fgoERGF0YRIKCgJpZBgBIAEoBRIjCgtzZXJ2aWNlSW5mbxgCIAEoCzIMLlNl",
            "cnZpY2VJbmZvSAASEAoGc3ltYm9sGAMgASgJSAASKgoEdGltZRgEIAEoCzIa",
            "Lmdvb2dsZS5wcm90b2J1Zi5UaW1lc3RhbXBIAEIHCgV2YWx1ZUITqgIQUHJv",
            "dG9idWZNZXNzYWdlc2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ProtobufMessages.ServiceInfo), global::ProtobufMessages.ServiceInfo.Parser, new[]{ "Operation" }, null, new[]{ typeof(global::ProtobufMessages.ServiceInfo.Types.OperationType) }, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ProtobufMessages.Data), global::ProtobufMessages.Data.Parser, new[]{ "Id", "ServiceInfo", "Symbol", "Time" }, new[]{ "Value" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ServiceInfo : pb::IMessage<ServiceInfo>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ServiceInfo> _parser = new pb::MessageParser<ServiceInfo>(() => new ServiceInfo());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ServiceInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ProtobufMessages.ClientServerMessageReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceInfo(ServiceInfo other) : this() {
      operation_ = other.operation_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceInfo Clone() {
      return new ServiceInfo(this);
    }

    /// <summary>Field number for the "operation" field.</summary>
    public const int OperationFieldNumber = 1;
    private global::ProtobufMessages.ServiceInfo.Types.OperationType operation_ = global::ProtobufMessages.ServiceInfo.Types.OperationType.New;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ProtobufMessages.ServiceInfo.Types.OperationType Operation {
      get { return operation_; }
      set {
        operation_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ServiceInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ServiceInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Operation != other.Operation) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Operation != global::ProtobufMessages.ServiceInfo.Types.OperationType.New) hash ^= Operation.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Operation != global::ProtobufMessages.ServiceInfo.Types.OperationType.New) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Operation);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Operation != global::ProtobufMessages.ServiceInfo.Types.OperationType.New) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Operation);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Operation != global::ProtobufMessages.ServiceInfo.Types.OperationType.New) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Operation);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ServiceInfo other) {
      if (other == null) {
        return;
      }
      if (other.Operation != global::ProtobufMessages.ServiceInfo.Types.OperationType.New) {
        Operation = other.Operation;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Operation = (global::ProtobufMessages.ServiceInfo.Types.OperationType) input.ReadEnum();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Operation = (global::ProtobufMessages.ServiceInfo.Types.OperationType) input.ReadEnum();
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the ServiceInfo message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      public enum OperationType {
        [pbr::OriginalName("NEW")] New = 0,
        [pbr::OriginalName("RECONNECT")] Reconnect = 1,
        [pbr::OriginalName("DISCONNECT")] Disconnect = 2,
      }

    }
    #endregion

  }

  public sealed partial class Data : pb::IMessage<Data>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Data> _parser = new pb::MessageParser<Data>(() => new Data());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Data> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ProtobufMessages.ClientServerMessageReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Data() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Data(Data other) : this() {
      id_ = other.id_;
      switch (other.ValueCase) {
        case ValueOneofCase.ServiceInfo:
          ServiceInfo = other.ServiceInfo.Clone();
          break;
        case ValueOneofCase.Symbol:
          Symbol = other.Symbol;
          break;
        case ValueOneofCase.Time:
          Time = other.Time.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Data Clone() {
      return new Data(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "serviceInfo" field.</summary>
    public const int ServiceInfoFieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ProtobufMessages.ServiceInfo ServiceInfo {
      get { return valueCase_ == ValueOneofCase.ServiceInfo ? (global::ProtobufMessages.ServiceInfo) value_ : null; }
      set {
        value_ = value;
        valueCase_ = value == null ? ValueOneofCase.None : ValueOneofCase.ServiceInfo;
      }
    }

    /// <summary>Field number for the "symbol" field.</summary>
    public const int SymbolFieldNumber = 3;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Symbol {
      get { return HasSymbol ? (string) value_ : ""; }
      set {
        value_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
        valueCase_ = ValueOneofCase.Symbol;
      }
    }
    /// <summary>Gets whether the "symbol" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasSymbol {
      get { return valueCase_ == ValueOneofCase.Symbol; }
    }
    /// <summary> Clears the value of the oneof if it's currently set to "symbol" </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearSymbol() {
      if (HasSymbol) {
        ClearValue();
      }
    }

    /// <summary>Field number for the "time" field.</summary>
    public const int TimeFieldNumber = 4;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Google.Protobuf.WellKnownTypes.Timestamp Time {
      get { return valueCase_ == ValueOneofCase.Time ? (global::Google.Protobuf.WellKnownTypes.Timestamp) value_ : null; }
      set {
        value_ = value;
        valueCase_ = value == null ? ValueOneofCase.None : ValueOneofCase.Time;
      }
    }

    private object value_;
    /// <summary>Enum of possible cases for the "value" oneof.</summary>
    public enum ValueOneofCase {
      None = 0,
      ServiceInfo = 2,
      Symbol = 3,
      Time = 4,
    }
    private ValueOneofCase valueCase_ = ValueOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ValueOneofCase ValueCase {
      get { return valueCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearValue() {
      valueCase_ = ValueOneofCase.None;
      value_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Data);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Data other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (!object.Equals(ServiceInfo, other.ServiceInfo)) return false;
      if (Symbol != other.Symbol) return false;
      if (!object.Equals(Time, other.Time)) return false;
      if (ValueCase != other.ValueCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (valueCase_ == ValueOneofCase.ServiceInfo) hash ^= ServiceInfo.GetHashCode();
      if (HasSymbol) hash ^= Symbol.GetHashCode();
      if (valueCase_ == ValueOneofCase.Time) hash ^= Time.GetHashCode();
      hash ^= (int) valueCase_;
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (valueCase_ == ValueOneofCase.ServiceInfo) {
        output.WriteRawTag(18);
        output.WriteMessage(ServiceInfo);
      }
      if (HasSymbol) {
        output.WriteRawTag(26);
        output.WriteString(Symbol);
      }
      if (valueCase_ == ValueOneofCase.Time) {
        output.WriteRawTag(34);
        output.WriteMessage(Time);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (valueCase_ == ValueOneofCase.ServiceInfo) {
        output.WriteRawTag(18);
        output.WriteMessage(ServiceInfo);
      }
      if (HasSymbol) {
        output.WriteRawTag(26);
        output.WriteString(Symbol);
      }
      if (valueCase_ == ValueOneofCase.Time) {
        output.WriteRawTag(34);
        output.WriteMessage(Time);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (valueCase_ == ValueOneofCase.ServiceInfo) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ServiceInfo);
      }
      if (HasSymbol) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Symbol);
      }
      if (valueCase_ == ValueOneofCase.Time) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Time);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Data other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      switch (other.ValueCase) {
        case ValueOneofCase.ServiceInfo:
          if (ServiceInfo == null) {
            ServiceInfo = new global::ProtobufMessages.ServiceInfo();
          }
          ServiceInfo.MergeFrom(other.ServiceInfo);
          break;
        case ValueOneofCase.Symbol:
          Symbol = other.Symbol;
          break;
        case ValueOneofCase.Time:
          if (Time == null) {
            Time = new global::Google.Protobuf.WellKnownTypes.Timestamp();
          }
          Time.MergeFrom(other.Time);
          break;
      }

      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            global::ProtobufMessages.ServiceInfo subBuilder = new global::ProtobufMessages.ServiceInfo();
            if (valueCase_ == ValueOneofCase.ServiceInfo) {
              subBuilder.MergeFrom(ServiceInfo);
            }
            input.ReadMessage(subBuilder);
            ServiceInfo = subBuilder;
            break;
          }
          case 26: {
            Symbol = input.ReadString();
            break;
          }
          case 34: {
            global::Google.Protobuf.WellKnownTypes.Timestamp subBuilder = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            if (valueCase_ == ValueOneofCase.Time) {
              subBuilder.MergeFrom(Time);
            }
            input.ReadMessage(subBuilder);
            Time = subBuilder;
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            global::ProtobufMessages.ServiceInfo subBuilder = new global::ProtobufMessages.ServiceInfo();
            if (valueCase_ == ValueOneofCase.ServiceInfo) {
              subBuilder.MergeFrom(ServiceInfo);
            }
            input.ReadMessage(subBuilder);
            ServiceInfo = subBuilder;
            break;
          }
          case 26: {
            Symbol = input.ReadString();
            break;
          }
          case 34: {
            global::Google.Protobuf.WellKnownTypes.Timestamp subBuilder = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            if (valueCase_ == ValueOneofCase.Time) {
              subBuilder.MergeFrom(Time);
            }
            input.ReadMessage(subBuilder);
            Time = subBuilder;
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code

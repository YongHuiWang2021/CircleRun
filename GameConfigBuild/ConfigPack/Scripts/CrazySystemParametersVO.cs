using System.Collections.Generic;
using _Game._Shared.Scripts.GamePlay.GameDataLocal.DataType;
using GamePlay.GameDataLocal.DataType;
using ProtoBuf;

namespace DataType
{
    [ProtoContract]
    public class CrazySystemParametersVO :CrazyProtobufDataBase
    {
     
        [ProtoMember(2)] public string Value1;
        [ProtoMember(3)] public string Value2;


        public override void CopyData(List<string> data)
        {
            if (data != null)
            {   
                this. Key = (data.Count > 0) ? data[0] : "ERROE CrazySystemParametersVO Key";
                this.  Value1 = (data.Count > 1) ? data[1] : "ERROE CrazySystemParametersVO Value1";
                this.  Value2 = (data.Count > 2) ? data[2] : "ERROE CrazySystemParametersVO Value2";
            }
         
        }
    }
    [ProtoContract]
    public class CrazySystemParametersPack 
    {
        [ProtoMember(1,Name = "DataType.CrazySystemParametersVO1")] 
        public List<CrazyProtobufDataBase> DataList;
        public  void CopyDataList(List<CrazyProtobufDataBase> data)
        {
            DataList = new List<CrazyProtobufDataBase>();
            if (data != null)
            {
                DataList.AddRange(data);
            }
        }
    }
}
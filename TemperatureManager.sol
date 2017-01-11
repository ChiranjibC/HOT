pragma solidity ^0.4.0;
import "BatchManager.sol";

contract TemperatureManager {
    
    //Private variables
    
    //Contract address
    address private _addrTemperatureManagerPublisher;
    address private _addrBatchManager;
    
    //Collection<BatchId,Temperature>
    mapping (bytes32 => Temperature) private _temperatures;
    //Collection<BatchId,RangeWiseTempCount>
    mapping (bytes32 => RangeWiseTempCount) private _rangeWiseTempCounts;
    
    //Contract references
    BatchManager _batchManager;
    
    
    //Business models
    struct Temperature
    {
        //Collection<index,TemperatureInfo>
        mapping(uint=>TemperatureInfo) temperatureInfos;
        uint lastIndex;
    }
    struct TemperatureInfo
    {
        uint time;
        int16 temperature;
    }
    struct RangeWiseTempCount
    {
        //Collection<RangeId,RangeInfo>
        mapping (uint8 => RangeInfo) rangeInfos;
        uint8[] rangeIds;
    }
    struct RangeInfo
    {
        //Collection<OwnerId,Count>
        mapping (address => int16) ownerWiseCounts;
        int16 totalCount;
    }
    
    
    //Modifiers
    
    //Check for only owner
    modifier onlyOwner {
        if (msg.sender != _addrTemperatureManagerPublisher)
            throw;
        _;
    }
    
    
    //Events
    event Error(bytes32 _batchId,bytes32 _action,address _addrSender,uint _time,bytes _info);
    event TempReceived(bytes32 _batchId,int16 _temp);

    
    //Constructor
    function TemperatureManager() {
        _addrTemperatureManagerPublisher=msg.sender;
    }
    
    
    //Owner functions
    
    function setAddrBatchManager(address _addrBatchManagerCtr) onlyOwner
    {
        _addrBatchManager=_addrBatchManagerCtr;
    }
    
    
    //Restricted users only
    
    //Only associated device can call
    function inputTemparatureTelemetry(bytes32 _batchId,int16 _temp) returns(bool)
    {
         _batchManager=BatchManager(_addrBatchManager);
        if(!_batchManager.isValidBatch(_batchId))
        {
            Error(_batchId,"inputTemparatureTelemetry",msg.sender,now,"Invalid Batch Id!");
            return;
        }
        
        var (associatedDeviceId, currentOwner,,) = _batchManager.getBatchInfo(_batchId);
        
        //Check if the DeviceId is associated with the Batch
        if(associatedDeviceId!=msg.sender)
        {
            Error(_batchId,"inputTemparatureTelemetry",msg.sender,now,"Access denied - Only associated device can send temperature!");
            return;
        }
        
        //Get TempRangeId by Temp
        uint8 tempRangeId=_batchManager.getTempRangeIdByTemp(_batchId,_temp);
        
        //Get BatchId wise RangeWiseTempCount - Which has list of RangeId wise TickCount
        RangeWiseTempCount storage rangeWiseTempCount=_rangeWiseTempCounts[_batchId];
        
        //Get RangeId wise RangeInfo from RangeWiseTempCount
        RangeInfo storage rangeInfo=rangeWiseTempCount.rangeInfos[tempRangeId];
        
        //Check if RangeId already exists in RangeInfo Id list
        bool isIdExists=false;
        if(rangeWiseTempCount.rangeInfos[tempRangeId].totalCount>0)
        {
            isIdExists=true;
        }
        
        //Get OwnerId wise count
        int16 count=rangeInfo.ownerWiseCounts[currentOwner];
        
        //Increase count by 1
        rangeInfo.ownerWiseCounts[currentOwner]=count+1;
        rangeInfo.totalCount=rangeInfo.totalCount+1;
        
        //Update values
        rangeWiseTempCount.rangeInfos[tempRangeId]=rangeInfo;
        if(!isIdExists)
        {
            rangeWiseTempCount.rangeIds.push(tempRangeId);
        }
        
        _rangeWiseTempCounts[_batchId]=rangeWiseTempCount;
        
        //Check Batch expiry
        _batchManager.updateBatchExpiryStatus(_batchId,tempRangeId,rangeInfo.totalCount);
        
        //Update Temperature history
        uint lastIndex=_temperatures[_batchId].lastIndex;
        _temperatures[_batchId].temperatureInfos[lastIndex+1]=TemperatureInfo(now,_temp);
        _temperatures[_batchId].lastIndex=lastIndex+1;
        
        TempReceived(_batchId,_temp);
        
        return true;
    }
    
    
    //Public functions
    
    function getTemperaturHistory(bytes32 _batchId,uint _startIndex) constant returns(uint[] _lstTime,int16[] _lstTemperature)
    {
        _batchManager=BatchManager(_addrBatchManager);
        if(!_batchManager.isValidBatch(_batchId))
        {
            Error(_batchId,"GetTemperaturHistory",msg.sender,now,"Invalid Batch Id!");
            return;
        }
        if(_startIndex<=0)
        {
            Error(_batchId,"GetTemperaturHistory",msg.sender,now,"StartIndex should be greater than zero!");
            return;
        }
        if(_startIndex>_temperatures[_batchId].lastIndex)
        {
            Error(_batchId,"GetTemperaturHistory",msg.sender,now,"StartIndex exceeded existing records count!");
            return;
        }
        
        //Max return array length could be 100
        uint possibleArrayLength=(_temperatures[_batchId].lastIndex-_startIndex+1)>=100?100:(_temperatures[_batchId].lastIndex-_startIndex+1);
        uint[] memory lstTime=new uint[](possibleArrayLength);
        int16[] memory lstTemperature=new int16[](possibleArrayLength);
        
        for(uint index=0; index<possibleArrayLength;index++)
        {
            lstTime[index]=_temperatures[_batchId].temperatureInfos[_startIndex].time;
            lstTemperature[index]=_temperatures[_batchId].temperatureInfos[_startIndex].temperature;
            _startIndex=_startIndex+1;
        }
        
        return(lstTime,lstTemperature);
    }
    
    function getRangeWiseTickCounts(bytes32 _batchId) returns(int16[] _lstMinTemp,int16[] _lstMaxTemp,int16[] _tickCount)
    {
        _batchManager=BatchManager(_addrBatchManager);
        if(!_batchManager.isValidBatch(_batchId))
        {
            Error(_batchId,"GetRangeWiseTickCounts",msg.sender,now,"Invalid Batch Id!");
            return;
        }
        
        //Get BatchId wise RangeWiseTempCount
        RangeWiseTempCount rangeWiseTempCount=_rangeWiseTempCounts[_batchId];
        
        int16[] memory lstMinTemp=new int16[](rangeWiseTempCount.rangeIds.length);
        int16[] memory lstMaxTemp=new int16[](rangeWiseTempCount.rangeIds.length);
        int16[] memory lstTickCount=new int16[](rangeWiseTempCount.rangeIds.length);
        
        
        for(uint index=0;index<rangeWiseTempCount.rangeIds.length;index++)
        {
            uint8 rangeId=rangeWiseTempCount.rangeIds[index];
            
            int16 min;
            int16 max;
            (min,max)=_batchManager.getMinMaxTempByRangeId(_batchId,rangeId);
            
                lstMinTemp[index]=min;
                lstMaxTemp[index]=max;
                lstTickCount[index]= rangeWiseTempCount.rangeInfos[rangeId].totalCount;
            }
        
        return(lstMinTemp,lstMaxTemp,lstTickCount);
    }
    
    function getOwnerWiseTickCounts(bytes32 _batchId,address _ownerId) returns(int16[] _lstMinTemp,int16[] _lstMaxTemp,int16[] _tickCount)
    {
        _batchManager=BatchManager(_addrBatchManager);
        if(!_batchManager.isValidBatch(_batchId))
        {
            Error(_batchId,"GetOwnerWiseTickCounts",msg.sender,now,"Invalid Batch Id!");
            return;
        }
        
        //Get BatchId wise RangeWiseTempCount
        RangeWiseTempCount storage rangeWiseTempCount=_rangeWiseTempCounts[_batchId];
        
        int16[] memory lstMinTemp=new int16[](rangeWiseTempCount.rangeIds.length);
        int16[] memory lstMaxTemp=new int16[](rangeWiseTempCount.rangeIds.length);
        int16[] memory lstTickCount=new int16[](rangeWiseTempCount.rangeIds.length);
        
        
        for(uint index=0;index<rangeWiseTempCount.rangeIds.length;index++)
        {
            uint8 rangeId=rangeWiseTempCount.rangeIds[index];
            
            int16 min;
            int16 max;
            (min,max)=_batchManager.getMinMaxTempByRangeId(_batchId,rangeId);
            
                lstMinTemp[index]=min;
                lstMaxTemp[index]=max;
                lstTickCount[index]= rangeWiseTempCount.rangeInfos[rangeId].ownerWiseCounts[_ownerId];
            }
        
        return(lstMinTemp,lstMaxTemp,lstTickCount);
    }
}
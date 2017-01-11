pragma solidity ^0.4.0;
import "OwnerManager.sol";


contract BatchManager{
    
    //Private variables
    
    //Contract addresses
    address private _addrBatchManagerPublisher;
    address private _addrOwnerManager;
    address private _addrTempManager;
    
    //Settings for non-applicable Batch Expiry 
    int16 private _infiniteMinTemp=-9999;
    int16 private _infiniteMaxTemp=9999;
    int16 private _infiniteExpireTickCount=9999;
    
    //Collection<BatchId,Batch>()
    mapping (bytes32 => Batch) private _batches;
   
    //Contract references
    OwnerManager private _ownerManager;
    
    //Events
    event Error(bytes32 _batchId,bytes32 _action,address _addrSender,uint _time,bytes _info);
    event BatchTemperatureTrackingInitiated(bytes32 _batchId);
    event AssociateDeviceIdModified(bytes32 _batchId,address _deviceId);
    event OwnershipAcknowledged(bytes32 _batchId,address _ownerId);
    event BatchExpired(bytes32 _batchId);
    
    //Business models
    struct Batch
    { 
      Owner[] owners;
      address producer;
      address currentOwner;
      TempRange[] tempRanges;
      address associatedDeviceId;
      bool isExpired;
    }
    struct Owner
    {    
      address ownerId;
      uint startTime;
      uint endTime;
    }
    struct TempRange
    {
        uint8 id;
        int16 minTemp;
        int16 maxTemp;
        int16 expireTickCount;
    }
    
    
    //Modifiers
    
    //Check for only owner
    modifier onlyOwner {
        if (msg.sender != _addrBatchManagerPublisher)
            throw;
        _;
    }
    
    
    //Constructor
    function BatchManager() {
        _addrBatchManagerPublisher = msg.sender;
    }
    
    
    
    //Owner functions
    
    function setAddrOwnerManager (address _addrOwnerManagerCtr) onlyOwner
    {
        _addrOwnerManager=_addrOwnerManagerCtr;
    }
    
     function setAddrTempManager (address _addrTempManagerCtr) onlyOwner
    {
        _addrTempManager=_addrTempManagerCtr;
    }
    
    function associateDevice(bytes32 _batchId,address _deviceId) returns(bool)
    {
        if(!isValidBatch(_batchId))
        {
            Error(_batchId,"AssociateDevice",msg.sender,now,"Invalid Batch Id!");
            return;
        }
        
        if(_batches[_batchId].producer != msg.sender){
            Error(_batchId,"AssociateDevice",msg.sender,now,"Only producer can change device association!");
            return; 
        }
        
        _batches[_batchId].associatedDeviceId=_deviceId;
        
        AssociateDeviceIdModified(_batchId,_deviceId);
        
        return true;
    }
    
    
    //Internal functions
    
    //Will be clled from TemperatureManager contract only
    function getTempRangeIdByTemp(bytes32 _batchId, int16 _temp) returns(uint8 _tempRangeId)
    {
        var tempRanges=_batches[_batchId].tempRanges;
        
        for(uint8 count=0;count<tempRanges.length;count++)
        {
            if((_temp>=tempRanges[count].minTemp || tempRanges[count].minTemp==_infiniteMinTemp) && (tempRanges[count].maxTemp==_infiniteMaxTemp || _temp<tempRanges[count].maxTemp))
            {
                return tempRanges[count].id;
            }
        }
        
        Error(_batchId,"GetTempRangeIdByTemp",msg.sender,now,"TempRange not found for given temperature!");
    }
    function updateBatchExpiryStatus(bytes32 _batchId,uint8 _tempRangeId,int16 _tickCount)
    {
        if(msg.sender!=_addrTempManager)
        {
             Error(_batchId,"updateBatchExpiryStatus",msg.sender,now,"Access denied!");
        }
        
        if(!_batches[_batchId].isExpired)
        {
            int16 expireTickCount=_batches[_batchId].tempRanges[_tempRangeId-1].expireTickCount;
            
            if(_tickCount>expireTickCount && expireTickCount!=_infiniteExpireTickCount)
            {
                _batches[_batchId].isExpired=true;
                BatchExpired(_batchId);
            }
        }
        
        
    }
    function getMinMaxTempByRangeId(bytes32 _batchId,uint8 _rangeId) returns(int16 _minTemp,int16 _maxTemp )
    {
        if(!isValidBatch(_batchId))
        {
            Error(_batchId,"getMinMaxTempByRangeId",msg.sender,now,"Invalid Batch Id!");
            return;
        }
        
        return(_batches[_batchId].tempRanges[_rangeId-1].minTemp,_batches[_batchId].tempRanges[_rangeId-1].maxTemp);
    }
    function getAssociatedDeviceId(bytes32 _batchId) returns(address _deviceId)
    {
        return _batches[_batchId].associatedDeviceId;
    }
    
    
    //Restricted users only
    
    function initiateBatchTracking(bytes32 _batchId,address _deviceId,int16[] _lstMinTemp,int16[] _lstMaxTemp,int16[] _lstExpireTickCount) returns(bool)
    {
        if(isValidBatch(_batchId))
        {
            Error(_batchId,"InitiateBatchTracking",msg.sender,now,"Duplicate BatchId not allowed!");
            return;
        }
        
        //Check length of three arrays
        //  should be equal and greater than zero
        if(_lstMinTemp.length==0 || (_lstMinTemp.length!=_lstMaxTemp.length || _lstMinTemp.length!=_lstExpireTickCount.length)){
            Error(_batchId,"InitiateBatchTracking",msg.sender,now,"Invalid input parameters!");
            return;
        }
        
        //Check if owner belongs to valid owners group
        _ownerManager=OwnerManager(_addrOwnerManager);
        if(!_ownerManager.isValidOwner(msg.sender)){
            Error(_batchId,"InitiateBatchTracking",msg.sender,now,"Access denied - owner does not belongs to valid owners group!");
            return;
        }
        
        
        //Create Batch
        
        _batches[_batchId].owners.push(Owner(msg.sender,now,/*End-Time*/0));
        //Set Caller as Producer & CurrentOwner
        _batches[_batchId].currentOwner=msg.sender;
        _batches[_batchId].producer=msg.sender;
        
        for(uint8 index=0;index<_lstMinTemp.length;index++)
        {
            _batches[_batchId].tempRanges.push(TempRange(index+1,_lstMinTemp[index],_lstMaxTemp[index],_lstExpireTickCount[index]));
        }
        
        _batches[_batchId].associatedDeviceId=_deviceId;
        _batches[_batchId].isExpired=false;
        
       //Success
       BatchTemperatureTrackingInitiated(_batchId);
       
       return true;
    }
    
    function acknowledgeReceive(bytes32 _batchId) returns(bool)
    {
        if(!isValidBatch(_batchId))
        {
            Error(_batchId,"AcknowledgeReceive",msg.sender,now,"Invalid Batch Id!");
            return;
        }
        
        //Check if owner belongs to valid owners group
        _ownerManager=OwnerManager(_addrOwnerManager);
        if(!_ownerManager.isValidOwner(msg.sender)){
            Error(_batchId,"AcknowledgeReceive",msg.sender,now,"Access denied - owner does not belongs to valid owners group!");
            return;
        }
        
        
        Batch storage batch=_batches[_batchId];
        batch.owners[batch.owners.length-1].endTime=now;
        batch.owners.push(Owner(msg.sender,now,/*End-Time*/0));
        batch.currentOwner=msg.sender;
        
        OwnershipAcknowledged(_batchId,msg.sender);
        
        return true;
    }
    
    
    //Public functions
    
    function isValidBatch(bytes32 _batchId) returns(bool _isValidBatch)
    {
        return _batches[_batchId].producer>0;
    }
    
    function getBatchInfo(bytes32 _batchId) returns(address _deviceId,address _currentOwner, address _producer,bool _expiryStatus)
    {
        if(!isValidBatch(_batchId))
        {
            Error(_batchId,"GetBatchInfo",msg.sender,now,"Invalid Batch Id!");
            return;
        }
        
        Batch batch=_batches[_batchId];
        return(batch.associatedDeviceId,batch.currentOwner,batch.producer,batch.isExpired);
    }
    
    function getAllowedBatchTempRanges(bytes32 _batchId) returns(uint8[] _lstRangeId,int16[] _lstMinTemp, int16[] _lstMaxTemp, int16[] _lstExpireTickCount)
    {
        if(!isValidBatch(_batchId))
        {
            Error(_batchId,"GetAllowedBatchTempRanges",msg.sender,now,"Invalid Batch Id!");
            return;
        }
        
        Batch batch=_batches[_batchId];
        
        uint8[] memory lstId=new uint8[](batch.tempRanges.length);
        int16[] memory lstMinTemp=new int16[](batch.tempRanges.length);
        int16[] memory lstMaxTemp=new int16[](batch.tempRanges.length);
        int16[] memory lstExpireTickCount=new int16[](batch.tempRanges.length);
        
        for(uint8 index=0;index<batch.tempRanges.length;index++)
        {
            lstId[index]=batch.tempRanges[index].id;
            lstMinTemp[index]=batch.tempRanges[index].minTemp;
            lstMaxTemp[index]=batch.tempRanges[index].maxTemp;
            lstExpireTickCount[index]=batch.tempRanges[index].expireTickCount;
        }
        
        return(lstId,lstMinTemp,lstMaxTemp,lstExpireTickCount);
    }
    
    function getBatchOwnershipHistory(bytes32 _batchId) returns(address[] _lstOwner, uint[] _lstStartTime, uint[] _lstEndTime )
    {
        if(!isValidBatch(_batchId))
        {
            Error(_batchId,"GetBatchOwnershipHistory",msg.sender,now,"Invalid Batch Id!");
            return;
        }
        
        Batch batch=_batches[_batchId];
        
        address[] memory owners=new address[](batch.owners.length);
        uint[] memory startTimes=new uint[](batch.owners.length);
        uint[] memory endTimes=new uint[](batch.owners.length);
        
        for(uint8 index=0;index<batch.owners.length;index++)
        {
            owners[index]=batch.owners[index].ownerId;
            startTimes[index]=batch.owners[index].startTime;
            endTimes[index]=batch.owners[index].endTime;
        }
        
        return(owners,startTimes,endTimes);
    }
}



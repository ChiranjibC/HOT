pragma solidity ^0.4.0;

contract OwnerManager
{
    //Private variables
    
    //Contract address
    address private _admin;
    
    mapping(address=>address) private _validOwners;
    
    
    //Events
    event Error(address _ownerId,bytes32 _action,address _addrSender,uint _time,bytes _info);
    
    
    
    //Modifiers
    
    //Check for only owner
    modifier onlyAdmin {
        if (msg.sender != _admin)
            throw;
        _;
    }
    
    

    //Constructor
    
    function OwnerManager()
    {
        _admin=msg.sender;
    }
    
    
    
    //Restricted users only
    
    function addValidOwners(address _ownerId) onlyAdmin returns(bool) 
    {
        //If OwnerId already exists throw error
        if(isValidOwner(_ownerId))
        {
            Error(_ownerId,"AddValidOwners",msg.sender,now,"Owner already added in valid owners group!");
        }
        
        //Add owner into valid owners group
        _validOwners[_ownerId]=_ownerId;
        
        return true;
    }
    
    function changeAdmin(address _newAdmin) onlyAdmin returns(bool) 
    {
        
       _admin=_newAdmin;
        
        return true;
    }
    
    
    
    //Public functions
    
    function isValidOwner(address _ownerId) returns(bool _isValidOwner)
    {
        return _validOwners[_ownerId]>0;
    }
}
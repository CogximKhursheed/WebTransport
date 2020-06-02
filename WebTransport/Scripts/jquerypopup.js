 /**********************************/
 function suppliercontact(supid,divname)
 {
   $.ajax(
     {
          type: "POST",  
		  url: "general.aspx",  
		  data: { supid: supid, action: "supplierdetail"},  
          success: function(response) 
              {
                   var msg=response.split("#_#");
                   $('#'+divname).empty().append(msg[0]); 
   			  },
          failure: function(msg)
              {
                   //$('#'+divname).empty().append("The action could not be completed. Please try again later.");
           	  }
      });
  }
  /**********************************/
  
  
   /**********************************/
 function agentcontact(agentid,divname)
 {
   $.ajax(
     {
          type: "POST",  
		  url: "general.aspx",  
		  data: { agentid: agentid, action: "agentdetail"},  
          success: function(response) 
              {
                   var msg=response.split("#_#");
                   $('#'+divname).empty().append(msg[0]); 
   			  },
          failure: function(msg)
              {
                   //$('#'+divname).empty().append("The action could not be completed. Please try again later.");
           	  }
      });
  }
  /**********************************/
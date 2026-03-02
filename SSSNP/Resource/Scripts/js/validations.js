// Validations JScript File
  function NumberhyphenOnly()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127) || (AsciiValue==45) || (AsciiValue==47))
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                alert("Enter NumericValues, '/', '-' Only");
               }
        }
        
        
        function Dates()
        {
        var AsciiValue=event.keyCode
            if((AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127) || (AsciiValue==45) ||(AsciiValue==46) || (AsciiValue==47))
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                alert("Enter Date Format(DD-MM-yyyy). Use Only Numbers, '.', '/', '-' Only");
               }
        }
        
        function NumberCharandHypehn()
        {
         var AsciiValue=event.keyCode
            if((AsciiValue>=48 && AsciiValue<=57) || (AsciiValue>=65 && AsciiValue<=90) || (AsciiValue>=97 && AsciiValue<=122 ) || (AsciiValue==8 || AsciiValue==127) || (AsciiValue==45) || (AsciiValue==47) || (AsciiValue==46) || (AsciiValue==32))
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                alert("Enter NumericValues, '/', '-' Only");
               }
        
        }
 
 function NumberOnly()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127))
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                alert("Enter NumericValues Only");
               }
        }
        function ContactNoOnly()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127) || AsciiValue==43 || AsciiValue==45 )
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                alert("Enter Numerics and + and - Only");
               }
        }
        function PhoneNumberOnly()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127) || (AsciiValue==45))
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                alert("Enter NumericValues Only");
               }
        }
        
function DecimalOnly()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=48 && AsciiValue<=57 ) || (AsciiValue==8 || AsciiValue==127) || AsciiValue==46)
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                alert("Enter DecimalValues Only");
               }
        }
        
function CharOnly()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=65 && AsciiValue<=90) || (AsciiValue>=97 && AsciiValue<=122 ))
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                alert("Enter Charcters Only");
               }
        }
        
        function CharbarOnly()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=65 && AsciiValue<=90) || (AsciiValue==97 || AsciiValue==47 || AsciiValue==45) || (AsciiValue>=97 && AsciiValue<=122) || (AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127) || (AsciiValue==32) || (AsciiValue==44) || (AsciiValue==46) )
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                 alert("Enter Alphanumaric,- and / Only");
               }
        }
        
        function CharnumOnly()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=65 && AsciiValue<=90) || (AsciiValue>=97 && AsciiValue<=122) || (AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127))
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                 alert("Enter Alphanumeric Only");
               }
           }

           function CharnumOnlyAll() {
               var AsciiValue = event.keyCode
               if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || (AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue == 97 || AsciiValue == 47 || AsciiValue == 45) || (AsciiValue >= 97 && AsciiValue <= 122) || (AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue == 32) || (AsciiValue == 44) || (AsciiValue == 46) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || (AsciiValue == 46) || (AsciiValue == 32))
                   event.returnValue = true;
               else {
                   event.returnValue = false;

                   alert("Enter Alphanumeric,.,-,/ and Space Only");
               }
           }
        
         function Names()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=65 && AsciiValue<=90) || (AsciiValue>=97 && AsciiValue<=122) || (AsciiValue==46) || (AsciiValue==32))
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                 alert("Enter Alphabets, '.' and Space Only");
               }
        }
        
         function Nameandbar()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=65 && AsciiValue<=90) || (AsciiValue>=97 && AsciiValue<=122) || (AsciiValue==46) || (AsciiValue==47) || (AsciiValue==32) || (AsciiValue==47)  || (AsciiValue>=48 && AsciiValue<=57) || (AsciiValue==8 || AsciiValue==127) || AsciiValue==43 || AsciiValue==45)
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                 alert("Enter Alphabets, '.', '/' and Space Only");
               }
        }

         function Sonof()
        {
           var AsciiValue=event.keyCode
            if((AsciiValue>=65 && AsciiValue<=90) || (AsciiValue>=97 && AsciiValue<=122) || (AsciiValue==46) || (AsciiValue==47) || (AsciiValue==32))
                event.returnValue=true;
            else
            {
                event.returnValue=false;
                
                 alert("Enter Alphabets, '.', '/' and Space Only");
               }
        }

//email validation
function echeck(email) 
{
 var str=document.getElementById(email).value;
 if(str=='')
 {
 return true
 }
 else
 {
		var at="@"
		var dot="."
		var lat=str.indexOf(at)
		var lstr=str.length
		var ldot=str.indexOf(dot)
		if (str.indexOf(at)==-1){
		   alert("Invalid E-mail ID.Enter like example@xxx.aa")
		   return false
		}

		if (str.indexOf(at)==-1 || str.indexOf(at)==0 || str.indexOf(at)==lstr){
		   alert("Invalid E-mail ID.Enter like example@xxx.aa")
		   return false
		}

		if (str.indexOf(dot)==-1 || str.indexOf(dot)==0 || str.indexOf(dot)==lstr){
		    alert("Invalid E-mail ID.Enter like example@xxx.aa")
		    return false
		}

		 if (str.indexOf(at,(lat+1))!=-1){
		    alert("Invalid E-mail ID.Enter like example@xxx.aa")
		    return false
		 }

		 if (str.substring(lat-1,lat)==dot || str.substring(lat+1,lat+2)==dot){
		    alert("Invalid E-mail ID.Enter like example@xxx.aa")
		    return false
		 }

		 if (str.indexOf(dot,(lat+2))==-1){
		    alert("Invalid E-mail ID.Enter like example@xxx.aa")
		    return false
		 }
		
		 if (str.indexOf(" ")!=-1){
		    alert("Invalid E-mail ID.Enter like example@xxx.aa")
		    return false
		 }

 		 return true
 		 }
 		 
 		 				
	}

function ValidateForm()
{
	var emailID=document.frmClientSetup.aspx.txtEmail
	
	if ((emailID.value==null)||(emailID.value=="")){
		alert("Please Enter your Email ID")
		emailID.focus()
		return false
	}
	if (echeck(emailID.value)==false){
		emailID.value=""
		emailID.focus()
		return false
	}
	return true
	
 }
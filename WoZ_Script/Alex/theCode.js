var globalStringTrack = '';
var holdString = '';

//For the item names, load in an array 4-5 items. For each item found, increment a global counter.

var OneTimeSwitch = 0;

var globalButtonMap = 
	{
	'beginYes': ['Participant has Reached Aisle','Menu/AI Name'],
	'beginNo': ["Menu/AI Name", "Let's Start!"],
	//this is for an initial setup only.
	'Menu/AI Name': ['List All Options','Command: Remove Item','Other'],
	'Participant has Reached Aisle': ["Finger is Raised","Finger is not Raised", "Menu/AI Name"],
	"Let's Start!" : ['Participant has Reached Aisle','Menu/AI Name'],
	"Begin Walking Search" : ['Participant has Reached Aisle','Menu/AI Name'],
	"Finger is Raised" : ["Item Has Been Located", "Item Has Not Been Located", "Menu/AI Name"],
	"Finger is not Raised" : ["Finger is Raised", "Menu/AI Name"],
	"Item Has Been Located" : ["Yes to Verification", "No to Verification", "Menu/AI Name"],
	"Item Has Not Been Located" : ["Yes to Rescan", "No to Rescan", "Menu/AI Name"],
	"Yes to Verification" : ["Correct Item Verification", "Incorrect Item Verification", "Menu/AI Name"],
	"No to Verification" : ["Move on to COOKIES", "Move on to PUMPKIN PIE MIX"],
	"Correct Item Verification" : ["Move on to COOKIES", "Move on to PUMPKIN PIE MIX"],
	"Incorrect Item Verification" : ["Yes to Verification", "Yes to Rescan", "Move on to COOKIES", "Move on to PUMPKIN PIE MIX"],
	"Yes to Rescan" : ["Item Has Been Located", "Item Has Not Been Located", "Menu/AI Name"],
	"No to Rescan" : ["Move on to COOKIES", "Move on to PUMPKIN PIE MIX"],
	"Move on to COOKIES" : ['Begin Walking Search', 'Menu/AI Name'],
	"Move on to PUMPKIN PIE MIX" : ["Participant has Reached Aisle", "Menu/AI Name"]

	};

var globalPrintTextMap = 
	{
	"beginYes" : "<b>Speak: \"Ok, great. Let's get started. If at any time you want to ask a question, just say MENU or ALEX. <br>The first item on your list is NUTELLA. It can be found in Aisle 1.\"</b> <br><br>At this time, the researchers give directions to the participant to navigate to the aisle. <br>Wait for participant to reach the aisle.<br><br> <b>Device Prompt: \"You have reached Aisle 1. The NUTELLA will be located on your right in a few steps. Continue walking while looking at the aisle until I beep.</b><br>",
	"beginNo" : "<b>Speak: Ok I will wait. If you want to ask a question, just say 'Menu' or ALEX. When you are ready to begin, say, 'Ok, ALEX, Let's start!'</b><br>Now wait for the participant to speak.<br>"	,
	"Menu/AI Name" : "<b>Speak: Main Menu. To hear all command options, say 'List my options', or just tell me what you want to do.</b><br>",
	"Let's Start!" : "<b>Speak: \"Ok, great. Let's get started. If at any time you want to ask a question, just say MENU or MY NAME. <br>The first item on your list is ITEM NAME HERE. It can be found in Aisle NUMBER HERE.\"</b> <br><br>At this time, the researchers give directions to the participant to navigate to the aisle. <br>Wait for participant to reach the aisle.<br><br> <b>Device Prompt: \"You have reached Aisle 1. The ITEM NAME HERE will be located on your right in approximately NUMOFSTEPS number of steps. Continue walking until I beep.</b><br>",
	"Participant has Reached Aisle" : "<b>Speak: You have reached the approximate area of the ITEM NAME HERE. When ready, please begin to point at the shelf to begin scanning for your item.</b><br><br>Wait for the participant to extend their finger to activate the scan.<br><br>",
	"Finger is Raised" : "Follow the participants finger until the item is located.<br>",
	"Finger is not Raised" : "Wait for a short period of time only. <br><b>Speak: Please raise your finger to begin scanning the shelf.<b><br>",
	"Item Has Been Located" : "<b>Speak: The item has been located. You may now grab the (ITEM NAME).</b><br>Wait for the participant to grab the item.<br><b>Speak: Would you like to attempt to verify that you have found the correct item?</b><br> Wait for yes/no prompt.<br>",
	"Item Has Not Been Located" : "<b>Speak: The item was not found. Would you like to try scanning again?</b><br>",
	"Yes to Verification" : "<b>Speak: OK. Begin to slowly rotate the item while I attempt to locate the barcode.</b><br>Observe the object until a barcode is within view of the item.<br>",
	"No to Verification" : "<b>Speak: OK, we will just move on to the next item on your list then.</b><br><br>Move on to the second/third/fourth item using the buttons below<br>",
	"Correct Item Verification" : "<b>Speak: The (ITEM NAME) was successfully verified!</b><br>",
	"Incorrect Item Verification" : "<b>Speak: I was unable to verify that item. Would you like to try to verify the item again? Would you like to rescan the shelf? Or would you just like to move on to the next item?</b><br>",
	"Yes to Rescan" : "<b>Speak: When ready, please begin to point at the shelf to begin scanning for your item.</b><br><br>Wait for the participant to extend their finger to activate the scan.</b><br><br>",
	"No to Rescan" : "<b>Speak: OK, we will just skip that item then and move on to the next one.</b><br>",
	"Move on to COOKIES" : "<b>Speak: The HOLIDAY COOKIES on your list is also in this aisle. It will be on your left in approximately 3 steps. Proceed there now.</b><br>",
	"Move on to PUMPKIN PIE MIX" : "<b>Speak: Let's move on to the next item. Your PUMPKIN PIE MIX is located in Aisle 2.</b><br>At this stage, direct the participant to Aisle 2. <br><b>Speak: The item will be on your right in just a few steps. Continue walking until I beep.</b><br>",
	"Begin Walking Search" : "At this time, the researchers give directions to the participant to navigate to the aisle. <br>Wait for participant to reach the aisle.<br>",
	
	};


function printText(inputString)
{
	if (OneTimeSwitch == 0)
		document.getElementById("origText").remove();
	OneTimeSwitch = 1;
	createText(globalPrintTextMap[inputString]);
	createButton(globalButtonMap[inputString]);
}

function createText(val)
{
	var currentString = document.getElementById("textOutput").innerHTML;
	holdString = currentString;
	globalStringTrack += currentString + "<hr>";
	document.getElementById("textOutput").innerHTML = val;
}

function createButton(ButtonVal)
{
	for(var i=0; i<ButtonVal.length; i++)
	{
	var textHold = ButtonVal[i];
	var btn = document.createElement("button");
	var t = document.createTextNode(textHold);
	btn.appendChild(t);
	btn.onclick = function(){printText(this.innerHTML)};
	//btn.setAttribute("value",val);
	document.getElementById("textOutput").appendChild(btn);
	delete window.btn;
	}
}
function printAtEnd(){
alert("the final text all was: " + globalStringTrack);
}
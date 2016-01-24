var globalStringTrack = '';
var holdString = '';

//For the item names, load in an array 4-5 items. For each item found, increment a global counter.

var OneTimeSwitch = 0;

var globalButtonMap = 
	{
	'beginYes': ['Participant has Made Request for apple juice','Participant has not Made Request','Menu/SAM'],
	'beginNo': ["Menu/SAM", "Let's Start!"],
	//this is for an initial setup only.
	'Menu/AI Name': ['List All Options','Command: Remove Item','Command: Add Item','Other'],
	'Participant has Made Request for apple juice': ['Participant has Reached Aisle','Participant has not Reached Aisle','Menu/SAM'],
	'Participant has Reached Aisle': ["Finger is Raised","Finger is not Raised", "Menu/SAM"],
	'Participant has not Reached Aisle': [],
	"Finger is Raised" : ["Item Has Been Located", "Item Has Not Been Located", "Item Has Been Located-Give Alternative Suggestion(1)", "Item Has Been Located-Give Alternative Suggestion(2)", "Menu/SAM"],
	"Finger is not Raised" : ["Prompt to Point", "Menu/SAM"],
	"Item Has Been Located" : ["Yes to Verification", "No to Verification", "Menu/SAM"],
	"Item Has Not Been Located" : ["Yes to Rescan", "No to Rescan", "Menu/SAM"],
	"Item Has Been Located-Give Alternative Suggestion(1)" : ["User Response: Yes", "User Response: No", "Menu/SAM"],
	"Item Has Been Located-Give Alternative Suggestion(2)" : ["User Response: Yes, Double It", "User Response: No", "Menu/SAM"],
	"User Response: Yes" : ["Finger is Raised", "Finger is not Raised", "Menu/SAM"],
	"User Response: No" : ["Finger Already Over Item", "Finger is Raised", "Finger is not Raised", "Menu/SAM"],
	"Finger Already Over Item" : ["Yes to Verification", "No to Verification", "Menu/SAM"],
	"Yes to Verification" : ["Correct Item Verification", "Incorrect Item Verification", "Menu/SAM"],
	"No to Verification" : ["Move on to Second Item", "Move on to Third Item", "Move on to Third Item, Second Selection", "Finish"],	
	"Correct Item Verification" : ["Move on to Second Item", "Move on to Third Item", "Move on to Third Item, Second Selection", "Finish"],
	"Incorrect Item Verification" : ["Yes to Verification", "Move on to Second Item", "Move on to Third Item", "Move on to Third Item, Second Selection", "Finish"],
	"Yes to Rescan" : ["Item Has Been Located", "Item Has Not Been Located", "Menu/SAM"],
	"No to Rescan" : ["Move on to Second Item", "Move on to Third Item", "Move on to Third Item, Second Selection", "Finish"],
	"Prompt to Point" : ["Finger is Raised", "Finger is not Raised"],	
	"Move on to Second Item" : ['Yes, find item', 'Menu/SAM'],
	"Yes, find item" : ["Participant has Reached Aisle, 2nd item", "Menu/SAM"],	
	"Participant has Reached Aisle, 2nd item" : ["Finger is Raised", "Finger is not Raised", "Menu/SAM"],
	"Move on to Third Item" : ["Participant has Reached Aisle, 3rd item", "Menu/SAM"],
	"Participant has Reached Aisle, 3rd item" : ["Finger is Raised", "Finger is not Raised", "Menu/SAM"],
	"User Response: Yes, Double It" : ["Grab First Item", "Menu/SAM"],
	"Grab First Item" : ["Item Has Been Located", "Item Has Not Been Located", "Cancel Second Item", "Menu/SAM"],
	"Move on to Third Item, Second Selection" : ["Finger is Raised", "Finger is Not Raised", "Cancel Second Item", "Menu/SAM"],
	"Finish" : ["DONE!!!!"]

	};

var globalPrintTextMap = 
	{
	"beginYes" : "<b>Speak: \"Ok, great. Let's get started. If at any time you want to ask a question, just say MENU or SAM. <br>What can I help you find today? When you are ready, please say 'SAM', followed by whatever you need.\"</b> <br><br>At this time, the researchers give directions to the participant on items they might be looking for. <br>Wait for participant to address SAM.<br><br>The participant will ask SAM for ITEM NAME HERE.",
	"beginNo" : "<b>Speak: Ok I will wait. If you want to ask a question, just say 'Menu' or my name. When you are ready to begin, say, 'Ok, APP NAME, Let's start!'</b><br>Now wait for the participant to speak.<br>"	,
	"Menu/AI Name" : "<b>Speak: Main Menu. To hear all command options, say 'List my options', or just tell me what you want to do.</b><br>",
	"Let's Start!" : "<b>Speak: \"Ok, great. Let's get started. If at any time you want to ask a question, just say MENU or SAM. <br>What can I help you find today? When you are ready, please say 'SAM', followed by whatever you need.\"</b> <br><br>At this time, the researchers give directions to the participant on items they might be looking for. <br>Wait for participant to address SAM.<br><br>The participant will ask SAM for ITEM NAME HERE.",
	"Participant has Made Request for apple juice" : "<b>Speak: \"Ok, let's find some apple juice. It is located in aisle 2. Please go there now.</b>\"<br><br>Wait for the participant to reach aisle 2.<br><br>",
	"Participant has Reached Aisle" : "<b>Speak: You have reached the approximate area of the APPLE JUICE. When ready, please begin to point at the shelf to begin scanning for your item.</b><br><br>Wait for the participant to extend their finger to activate the scan.<br><br>",
	"Participant has not Reached Aisle" : "EMPTY SPACE",
	"Finger is Raised" : "Follow the participants finger until the item is located.<br>",
	"Finger is not Raised" : "Wait for a short period of time only. <br><b>Speak: Please raise your finger to begin scanning the shelf.<b><br>",
	"Item Has Been Located-Give Alternative Suggestion(1)" : "Wait until the participant finds one of the apple juices and make a ding sound when it has been located.<br><b>Speak: \"The apple juice you wanted has been located. However, I notice that there is a cheaper apple juice available than the one you have selected. Would you like to try that one instead?\"</b><br><br>",
	"Item Has Not Been Located" : "<b>Speak: The item was not found. Would you like to try scanning again?</b><br>",
	"Yes to Verification" : "<b>Speak: OK. Begin to slowly rotate the item while I attempt to locate the barcode.</b><br>Observe the object until a barcode is within view of the item.<br>",
	"No to Verification" : "Move on to the second/third/fourth item using the buttons below<br>",
	"User Response: Yes" : "<b>Speak: OK, great we'll get the other item. When ready, please raise your finger to begin scanning the shelf.</b>\"<br><br>Wait for the participant to raise their finger.<br><br>",
	"User Response: No" : "<b>Speak: OK, no problem we'll just get the first one.</b><br>-At this point, if the participant has moved their finger, <b>Speak: I'm sorry, but I lost track of that item. Please raise your finger to scan for it again.</b><br>-But, if their finger is still pointing at the item, <b>Speak: It looks like you are still pointing at the first apple juice. When ready, reach forward and grab it.</b><br><br>",
	"Finger Already Over Item" : "Wait for the participant to grab the item.<br><b>Speak: Would you like to attempt to verify that you have found the correct item?</b><br> Wait for yes/no prompt.<br>",
	"Correct Item Verification" : "<b>Speak: The ITEM NAME was successfully verified!</b><br>",
	"Incorrect Item Verification" : "<b>Speak: I was unable to verify that item. Would you like to scan again?</b><br>",
	"Yes to Rescan" : "<b>Speak: When ready, please begin to point at the shelf to begin scanning for your item.</b><br><br>Wait for the participant to extend their finger to activate the scan.</b><br><br>",
	"No to Rescan" : "<b>Speak: OK, we will just skip that item then and move on to the next one.</b><br>",
	"Prompt to Point" : "<b>Speak: \"Please raise your finger to continue.\"</b><br><br>",
	"Item Has Been Located" : "<b>Speak: The item has been located. You may now grab the ITEM NAME.</b><br>Wait for the participant to grab the item.<br><b>Speak: Would you like to attempt to verify that you have found the correct item?</b><br> Wait for yes/no prompt.<br>",
	"Move on to Second Item" : "<b>Speak: Since you just got apple juice, would you like to get any GRAHAM CRACKERS? They are also located in this aisle.</b><br>",
	"Yes, find item" : "<b>Speak: OK, great. The GRAHAM CRACKERS are located to your left (or other direction if they have turned) in approximately one step. Proceed when ready</b><br><br>",
	"Participant has Reached Aisle, 2nd item" : "<b>Speak: You have reached the approximate area of the GRAHAM CRACKERS. When ready, please begin to point at the shelf to begin scanning for your item.</b><br><br>Wait for the participant to extend their finger to activate the scan.<br><br>",
	"Move on to Third Item" : "<b>Speak: OK, can I help you find anything else today?</b><br>Wait for participant to request to find peanut butter.<br><br>After they have requested peanut butter, <b>Speak: OK, the peanut butter is located in the next aisle. Please proceed there now.</b><br><br>",
	"Item Has Been Located-Give Alternative Suggestion(2)" : "<b>Speak: It looks like this peanut butter is currently on sale! For today only, it is 2 for the price of one. Do you want to get two jars of peanut butter or just one?</b><br><br>",
	"User Response: Yes, Double It" : "<b>Speak: OK, we'll grab two.</b><br>-At this point, if the participant has moved their finger, <b>Speak: I'm sorry, but I lost track of that item. Please raise your finger to scan for it again.</b><br>-But, if their finger is still pointing at the item, <b>Speak: It looks like you are still pointing at the first peanut butter. When ready, reach forward and grab it.</b><br><br>",
	"Grab First Item" : "Wait for the participant to grab the item.<br><b>Speak: Would you like to attempt to verify that you have found the correct item?</b><br> Wait for yes/no prompt.<br>",
	"Move on to Third Item, Second Selection" : "<b>Speak: OK, now let's get the second jar of peanut butter. When ready, please extend your finger to scan the shelf.</b><br>",
	"Finish" : "<b>Speak: Thank you! That concludes this experiment. ",
	"Participant has Reached Aisle, 3rd item" : "<b>Speak: OK, we have reached the area of the PEANUT BUTTER. Please raise your finger to scan the shelf for the item.</b><br>Wait for participant to raise their finger.<br>"
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